param(
    [ValidateSet('update', 'add', 'remove')]
    [string]$Action = 'update',

    [string]$MigrationName = 'NewMigration'
)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$dataProject = Join-Path $root 'src\webapi\cnt_sp_webapi\5 Infrastructure.Implementation\SP.WebApi.DataAccess.Postgres\SP.WebApi.DataAccess.Postgres.csproj'
$webProject = Join-Path $root 'src\webapi\cnt_sp_webapi\6 WebApp\SP.WebApi.WebApp.csproj'

switch ($Action) {
    'update' {
        dotnet ef database update --project $dataProject --startup-project $webProject
    }
    'add' {
        dotnet ef migrations add $MigrationName --project $dataProject --startup-project $webProject --output-dir Migrations
    }
    'remove' {
        dotnet ef migrations remove --project $dataProject --startup-project $webProject
    }
}
