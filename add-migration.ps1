$Name = Read-Host -Prompt 'Input migration name please : '
cd .\src\ACA.Infrastructure\
dotnet ef migrations add '$Name' -o Data\Migration
cd ../..