#!/usr/bin/env bash
#
# scrape-docs.sh — Scrape all pages from a ReadMe.io / docs site under a given base URL.
#
# Usage:
#   ./scripts/scrape-docs.sh [BASE_URL] [OUTPUT_DIR]
#
# Defaults:
#   BASE_URL   = https://docs.acquired.com/reference
#   OUTPUT_DIR = docs/reference
#
# What it does:
#   1. Fetches the index page and discovers all internal /reference/* links
#   2. Downloads each page, converts HTML → Markdown (via html2text or pandoc)
#   3. Saves each page as {slug}.md in OUTPUT_DIR
#   4. Concatenates all pages into OUTPUT_DIR/../combined.md
#
# Requirements: curl, one of (html2text | pandoc), grep, sed
# Install:  apt-get install -y html2text   OR   apt-get install -y pandoc

set -euo pipefail

BASE_URL="${1:-https://docs.acquired.com/reference}"
OUTPUT_DIR="${2:-docs/reference}"
COMBINED_FILE="$(dirname "$OUTPUT_DIR")/combined.md"
DELAY="${SCRAPE_DELAY:-1}"          # seconds between requests (be polite)
USER_AGENT="AcquiredDocScraper/1.0"

# ── Colours ──────────────────────────────────────────────────────────
RED='\033[0;31m'; GREEN='\033[0;32m'; YELLOW='\033[0;33m'; NC='\033[0m'

info()  { echo -e "${GREEN}[+]${NC} $*"; }
warn()  { echo -e "${YELLOW}[!]${NC} $*"; }
error() { echo -e "${RED}[✗]${NC} $*" >&2; }

# ── Check dependencies ──────────────────────────────────────────────
html_to_md() {
    if command -v html2text &>/dev/null; then
        html2text -utf8 -nobs 2>/dev/null || html2text
    elif command -v pandoc &>/dev/null; then
        pandoc -f html -t markdown --wrap=none
    else
        # Fallback: strip tags with sed (lossy but works everywhere)
        sed -e 's/<[^>]*>//g' -e '/^$/d'
    fi
}

# ── Setup ────────────────────────────────────────────────────────────
mkdir -p "$OUTPUT_DIR"

# Extract the path prefix from the base URL  (e.g. /reference)
URL_PATH="$(echo "$BASE_URL" | sed 's|https\?://[^/]*||')"
DOMAIN="$(echo "$BASE_URL" | grep -oP 'https?://[^/]+')"

info "Base URL  : $BASE_URL"
info "Domain    : $DOMAIN"
info "URL path  : $URL_PATH"
info "Output    : $OUTPUT_DIR"
info "Combined  : $COMBINED_FILE"
echo ""

# ── Step 1: Discover all page slugs ─────────────────────────────────
info "Discovering pages from $BASE_URL ..."

INDEX_HTML=$(curl -sL -A "$USER_AGENT" "$BASE_URL/introduction" 2>/dev/null || curl -sL -A "$USER_AGENT" "$BASE_URL" 2>/dev/null)

# Extract all href="/reference/xxx" links, deduplicate, sort
SLUGS=$(echo "$INDEX_HTML" \
    | grep -oP "href=\"${URL_PATH}/[a-z0-9_-]+\"" \
    | sed "s|href=\"${URL_PATH}/||; s|\"||g" \
    | sort -u)

# If grep found nothing, try a broader pattern
if [ -z "$SLUGS" ]; then
    warn "Primary pattern found no links, trying broader match..."
    SLUGS=$(echo "$INDEX_HTML" \
        | grep -oP "${URL_PATH}/[a-z0-9_-]+" \
        | sed "s|${URL_PATH}/||" \
        | sort -u)
fi

SLUG_COUNT=$(echo "$SLUGS" | wc -l)

if [ -z "$SLUGS" ] || [ "$SLUG_COUNT" -eq 0 ]; then
    error "No pages discovered. Check the BASE_URL or site structure."
    exit 1
fi

info "Found $SLUG_COUNT pages"
echo ""

# ── Step 2: Scrape each page ────────────────────────────────────────
SCRAPED=0
FAILED=0

for slug in $SLUGS; do
    PAGE_URL="${BASE_URL}/${slug}"
    OUT_FILE="${OUTPUT_DIR}/${slug}.md"

    printf "  %-50s " "$slug"

    PAGE_HTML=$(curl -sL -A "$USER_AGENT" \
        -H "Accept: text/html" \
        --max-time 30 \
        "$PAGE_URL" 2>/dev/null) || true

    if [ -z "$PAGE_HTML" ]; then
        echo -e "${RED}FAILED${NC}"
        FAILED=$((FAILED + 1))
        continue
    fi

    # Extract the main content area (ReadMe.io uses <div id="content"> or <article>)
    # Try multiple selectors; fall back to full body
    CONTENT="$PAGE_HTML"

    # Convert to markdown
    MD=$(echo "$CONTENT" | html_to_md)

    if [ -z "$MD" ]; then
        echo -e "${RED}EMPTY${NC}"
        FAILED=$((FAILED + 1))
        continue
    fi

    # Write file with frontmatter
    {
        echo "---"
        echo "source: ${PAGE_URL}"
        echo "slug: ${slug}"
        echo "scraped: $(date -u +%Y-%m-%dT%H:%M:%SZ)"
        echo "---"
        echo ""
        echo "$MD"
    } > "$OUT_FILE"

    echo -e "${GREEN}OK${NC} ($(echo "$MD" | wc -l) lines)"
    SCRAPED=$((SCRAPED + 1))

    # Polite delay
    sleep "$DELAY"
done

echo ""
info "Scraped: $SCRAPED  |  Failed: $FAILED  |  Total: $SLUG_COUNT"

# ── Step 3: Build combined file ─────────────────────────────────────
info "Building combined file: $COMBINED_FILE"

{
    echo "# Acquired.com API Documentation"
    echo ""
    echo "Combined reference scraped on $(date -u +%Y-%m-%dT%H:%M:%SZ)"
    echo "Source: ${BASE_URL}"
    echo ""
    echo "---"
    echo ""

    # Table of contents
    echo "## Table of Contents"
    echo ""
    for f in "$OUTPUT_DIR"/*.md; do
        slug=$(basename "$f" .md)
        echo "- [${slug}](#${slug})"
    done
    echo ""
    echo "---"
    echo ""

    # Concatenate all files
    for f in "$OUTPUT_DIR"/*.md; do
        slug=$(basename "$f" .md)
        echo "## ${slug}"
        echo ""
        # Skip frontmatter (lines between --- markers)
        sed '1{/^---$/d}' "$f" | sed '1,/^---$/d'
        echo ""
        echo "---"
        echo ""
    done
} > "$COMBINED_FILE"

COMBINED_LINES=$(wc -l < "$COMBINED_FILE")
info "Combined file: $COMBINED_LINES lines"
echo ""
info "Done! Files are in $OUTPUT_DIR/"
info "Combined doc: $COMBINED_FILE"
