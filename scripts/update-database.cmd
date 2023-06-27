pushd ..\src\CardiffMetroUni.StudentEnrollment.Infrastructure
dotnet ef database update  --context StudentDbContext  --startup-project ..\CardiffMetroUni.StudentEnrollment.WebApi
popd