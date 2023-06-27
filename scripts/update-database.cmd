pushd ..\src\CardiffMetroUni.StudentEnrollment.Infrastructure
dotnet ef database update  --context StudentDbContext  --startup-project ..\CC.Boxing.MMS.WebApi
popd