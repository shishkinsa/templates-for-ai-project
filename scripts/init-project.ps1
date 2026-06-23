param(
    [Parameter(Mandatory = $true)]
    [string]$ProjectName,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[A-Z]{2,4}$')]
    [string]$ProjectPrefix,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[a-z0-9]+(-[a-z0-9]+)*$')]
    [string]$ProjectSlug,

    [switch]$WhatIf
)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

$newContainer = "cnt_$($ProjectPrefix.ToLower())"
$newPrefixLower = $ProjectPrefix.ToLower()

Write-Host "Initializing project: $ProjectName ($ProjectPrefix / $ProjectSlug)" -ForegroundColor Cyan
if ($WhatIf) {
    Write-Host "WhatIf mode — no files will be modified." -ForegroundColor Yellow
}

# Rename container directories
$renames = @(
    @{ Path = Join-Path $root "src\frontend\cnt_sp_web"; NewName = "${newContainer}_web" },
    @{ Path = Join-Path $root "src\webapi\cnt_sp_webapi"; NewName = "${newContainer}_webapi" },
    @{ Path = Join-Path $root "docs\architecture\diagram\containers\cnt_sp_web"; NewName = "${newContainer}_web" },
    @{ Path = Join-Path $root "docs\architecture\diagram\containers\cnt_sp_webapi"; NewName = "${newContainer}_webapi" },
    @{ Path = Join-Path $root "docs\architecture\diagram\containers\cnt_sp_db"; NewName = "${newContainer}_db" },
    @{ Path = Join-Path $root "docs\architecture\diagram\data\cnt_sp_db"; NewName = "${newContainer}_db" }
)

foreach ($item in $renames) {
    if (-not (Test-Path $item.Path)) { continue }
    $parent = Split-Path $item.Path -Parent
    $newPath = Join-Path $parent $item.NewName
    if ($WhatIf) {
        Write-Host "  [WhatIf] Rename: $($item.Path) -> $newPath"
    } else {
        Rename-Item $item.Path $item.NewName
    }
}

$replacements = @{
    'Sample Project'        = $ProjectName
    'sample-project'        = $ProjectSlug
    'cnt_sp_db'             = "${newContainer}_db"
    'cnt_sp_db_user'        = "${newContainer}_db_user"
    'cnt_sp_web'            = "${newContainer}_web"
    'cnt_sp_webapi'         = "${newContainer}_webapi"
    'CNT_SP_'               = "CNT_$ProjectPrefix`_"
    'SP.WebApi'             = "$ProjectPrefix.WebApi"
    'SP.Shared'             = "$ProjectPrefix.Shared"
    'SP.WebApi.WebApp'      = "$ProjectPrefix.WebApi.WebApp"
    'AddSpObservability'    = "Add${ProjectPrefix}Observability"
    'sp-web'                = "$newPrefixLower-web"
    'sp-backend'            = "$newPrefixLower-backend"
    'sp-frontend'           = "$newPrefixLower-frontend"
    'sp-postgres'           = "$newPrefixLower-postgres"
    'sp-otel-collector'     = "$newPrefixLower-otel-collector"
    'sp-prometheus'         = "$newPrefixLower-prometheus"
    'sp-loki'               = "$newPrefixLower-loki"
    'sp-tempo'              = "$newPrefixLower-tempo"
    'sp-grafana'            = "$newPrefixLower-grafana"
    'sp_app_network'        = "${newPrefixLower}_app_network"
    'sp_monitoring_network' = "${newPrefixLower}_monitoring_network"
    'sp_pg_data'            = "${newPrefixLower}_pg_data"
    'sample-project.slnx'   = "$ProjectSlug.slnx"
}

$excludeDirPatterns = @('\.git\', '\node_modules\', '\bin\', '\obj\', '\\_ref-gh\', '\\_ref-greenhouse\')
$textExtensions = @(
    '.md', '.cs', '.csproj', '.props', '.json', '.yaml', '.yml', '.c4',
    '.ts', '.tsx', '.js', '.css', '.html', '.sql', '.ps1', '.slnx', '.mdc',
    '.conf', '.example', '.gitignore', '.dockerignore', '.cursorrules', '.editorconfig'
)

Get-ChildItem -Path $root -Recurse -File | Where-Object {
    $path = $_.FullName
    $excluded = $false
    foreach ($pattern in $excludeDirPatterns) {
        if ($path -like "*$pattern*") { $excluded = $true; break }
    }
    if ($excluded) { return $false }
    return $textExtensions -contains $_.Extension.ToLower()
} | ForEach-Object {
    $content = Get-Content $_.FullName -Raw -Encoding UTF8
    if ($null -eq $content) { return }

    $updated = $content
    foreach ($key in $replacements.Keys) {
        $updated = $updated.Replace($key, $replacements[$key])
    }

    if ($updated -ne $content) {
        if ($WhatIf) {
            Write-Host "  [WhatIf] Update: $($_.FullName)"
        } else {
            Set-Content -Path $_.FullName -Value $updated -NoNewline -Encoding UTF8
        }
    }
}

$sln = Join-Path $root 'sample-project.slnx'
$newSln = Join-Path $root "$ProjectSlug.slnx"
if (Test-Path $sln) {
    if ($WhatIf) {
        Write-Host "  [WhatIf] Rename: $sln -> $newSln"
    } else {
        Rename-Item $sln $newSln
    }
}

Write-Host "Done. Next steps:" -ForegroundColor Green
Write-Host "  1. Update docs/process/context/containers.md and docs/requirements/business/goals.md"
Write-Host "  2. psql -U postgres -f scripts/create-cnt-db.sql"
Write-Host "  3. dotnet run --project `"src/webapi/$newContainer`_webapi/6 WebApp/$ProjectPrefix.WebApi.WebApp.csproj`""
Write-Host "  4. cd src/frontend/$newContainer`_web && npm install && npm run dev"
