Write-Host " =======================================================================================================" -ForegroundColor Green
Write-Host "|| TSI - ERP" -ForegroundColor Green
Write-Host "|| This tool will help generate the context class and the entities model" -ForegroundColor Green
Write-Host " =======================================================================================================" -ForegroundColor Green
Write-Host ""
# Building tables list
$FILE_CONTENT = Get-Content -Raw tables.json 
Write-Host $FILE_CONTENT
$TABLES = $FILE_CONTENT | ConvertFrom-Json 
Write-Host "Detected $($TABLES.Length) tables:"
Write-Host "Generating Context " -NoNewline; 
Write-Host "ApplicationContext" -ForegroundColor Red -NoNewline; 
Write-Host " for the following tables:"
Write-Host""

$TABLES_LIST = ""
For ($i=0; $i -lt $TABLES.Length; $i++) {
    Write-Host " - $($TABLES[$i])" -ForegroundColor Red
    $TABLES_LIST = "$($TABLES_LIST) -t $($TABLES[$i])"
} 

# Building command to run!
Write-Host ""
$command ="dotnet ef dbcontext scaffold Name=ConnectionStrings:default Microsoft.EntityFrameworkCore.SqlServer -o Entities -c ApplicationContext --context-dir Data --data-annotations $($TABLES_LIST) --no-onconfiguring --force"
Write-Host "Will scaffold Entities and Context"
Write-Host ""
Invoke-Expression -Command $command 
Write-Host ""
Write-Host " =======================================================================================================" -ForegroundColor Green
Write-Host "|| Ended database scaffolding successfully" -ForegroundColor Green
Write-Host " =======================================================================================================" -ForegroundColor Green