$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

$requiredPaths = @(
    'docs\FRAMEWORK.md',
    'docs\manifest.yaml',
    'docs\requirements\index.yaml',
    'docs\requirements\business\goals.md',
    'docs\requirements\business\capabilities.md',
    'docs\requirements\constraints\performance.md',
    'docs\requirements\constraints\security.md',
    'docs\ai\context\containers.md',
    'docs\architecture\capacity.md',
    'docs\architecture\specs\assemblies.md',
    'openspec\specs\examples\spec.md',
    'openspec\specs\categories\spec.md',
    'openspec\specs\auth\spec.md',
    'docs\architecture\openapi\components\openapi.yaml'
)

$missing = @()
foreach ($rel in $requiredPaths) {
    $full = Join-Path $root $rel
    if (-not (Test-Path $full)) {
        $missing += $rel
    }
}

if ($missing.Count -gt 0) {
    Write-Host "Manifest check FAILED — missing paths:" -ForegroundColor Red
    $missing | ForEach-Object { Write-Host "  $_" }
    exit 1
}

# Capability keys in manifest must match openspec/specs folders
$manifestContent = Get-Content (Join-Path $root 'docs\manifest.yaml') -Raw
$specDirs = Get-ChildItem (Join-Path $root 'openspec\specs') -Directory | ForEach-Object { $_.Name }
foreach ($cap in $specDirs) {
    if ($manifestContent -notmatch "(?m)^\s+$cap\s*:") {
        Write-Host "Manifest check FAILED — capability '$cap' in openspec/specs but not in manifest.yaml" -ForegroundColor Red
        exit 1
    }
}

Write-Host "Manifest check OK ($($requiredPaths.Count) paths, $($specDirs.Count) capabilities)" -ForegroundColor Green
