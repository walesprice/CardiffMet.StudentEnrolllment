pushd ..\src\CardiffMetroUni.StudentEnrollment.Infrastructure
dotnet ef migrations add %1  --context StudentDbContext  --startup-project ..\CardiffMetroUni.StudentEnrollment.WebApi -o Data\EntityFramework\Migrations
dotnet ef database update    --context StudentDbContext  --startup-project ..\CardiffMetroUni.StudentEnrollment.WebApi
popd