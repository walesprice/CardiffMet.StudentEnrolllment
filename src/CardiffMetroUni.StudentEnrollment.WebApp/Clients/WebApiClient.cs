using System.Text.Json;
using CardiffMetroUni.StudentEnrollment.WebApp.Pages.Partials.Model;
using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Model;
using Microsoft.Extensions.Options;
using CSharpFunctionalExtensions;

namespace CardiffMetroUni.StudentEnrollment.WebApp.Clients
{

    public class ResponseData
    {
        public string Message { get; set; }
        public long StudentId { get; set; }
    }

    



    public class WebApiClient
    {

        public enum POSTORGET
        {
            POST, GET
        }

        private async Task<string> ContactApi<T>(POSTORGET postorget, string requestUrl)
        {
            var response = (postorget == POSTORGET.POST) ? await _httpClient.PostAsync(requestUrl, null) : await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else return "";
        }


        private async Task<Result<string>> PostAndReturnStudentId(string requestUrl)
        {

            var json = await ContactApi<ResponseData>(POSTORGET.POST, requestUrl);

            var badRequest = json.Contains("Student already exists", StringComparison.OrdinalIgnoreCase);
            if (badRequest) { 
                return Result.Failure<string>("The Stuuduent already exits"); ;
            }
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
            var responseData = JsonSerializer.Deserialize<ResponseData>(json, options);
            return (responseData != null) ? Result.Success<string> (responseData.StudentId.ToString()) : Result.Failure<string>("There has been an error");

        }
        private async Task<StudentModel> GetStudentModel(string requestUrl)
        {


            var json = await ContactApi<StudentCoreModel>(POSTORGET.GET, requestUrl);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };

            var student = JsonSerializer.Deserialize<StudentCoreModel>(json, options);
            var studentModel = StudentModel.Create(StudentModel.FormState.EDIT, student);

            return studentModel;

        }


        private async Task<List<StudentModel>> GetStudentModels(string requestUrl)
        {
            var json = await ContactApi<StudentCoreModel>(POSTORGET.GET, requestUrl);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };

            var students = JsonSerializer.Deserialize<List<StudentCoreModel>>(json, options);
            if (students.Count > 0)
            {
                return students.Select(s => StudentModel.Create(StudentModel.FormState.VIEW, s)).ToList();
            }
            else
            {
                return new List<StudentModel>();
            }
        }

        public WebApiClient(string webApiBaseUrl)
        {
            _webApiBaseUrl = webApiBaseUrl;
            _httpClient = new HttpClient();
        }

        public async Task<List<StudentModel>> GetAllStudents()
        {
            List<StudentModel> studentModels = new List<StudentModel>();
            try
            {
                var requestUrl = $"{_webApiBaseUrl}/api/GetStudents";

                return await GetStudentModels(requestUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return studentModels;
        }
        public async Task<List<StudentModel>> SearchStudents(string searchText)
        {
            List<StudentModel> studentModels = new List<StudentModel>();
            try
            {
                var requestUrl = $"{_webApiBaseUrl}/api/SearchStudents/{searchText}";

                return await GetStudentModels(requestUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return studentModels;
        }


        public async Task<StudentModel> GetStudentByStudentId(long studentId)
        {
            StudentModel studentModel = StudentModel.EmptyStudent();
            try
            {
                var requestUrl = $"{_webApiBaseUrl}/api/GetStudentByStudentId/{studentId}";
                return await GetStudentModel(requestUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return studentModel;
        }

        public async Task<Result<string>> CreateNewStudent(string firstName, string middleName, string surname, string universityCourse, string welshLanguageProficiency, DateTime dateOfBirth, DateTime startDate, DateTime endDate)
        {
            if (middleName == null)
            {
                middleName = "NULL";
            }
            string requestUrl = $"{_webApiBaseUrl}/api/create/{firstName}/{middleName}/{surname}/{universityCourse}/{welshLanguageProficiency}/{dateOfBirth.ToString("yyyy-MM-dd")}/{startDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}";


            var tryCreateNewStudent = await PostAndReturnStudentId(requestUrl);

            if (tryCreateNewStudent.IsSuccess)
            {
                return Result.Success<string>(tryCreateNewStudent.Value);

            } else
            {
                return Result.Failure<string>(tryCreateNewStudent.Error);
            }

        }


        public async Task<Result<string>>  UpdateStudent(long studentId, string firstName, string middleName, string surname, string welshLanguageProficiency)
        {
            if (middleName == null)
            {
                middleName = "NULL";
            }

            string requestUrl = $"{_webApiBaseUrl}/api/update/{studentId}/{firstName}/{middleName}/{surname}/{welshLanguageProficiency}";

            var tryUpdateStudent = await PostAndReturnStudentId(requestUrl);
            if (tryUpdateStudent.IsSuccess)
            {
                return Result.Success<string>(tryUpdateStudent.Value);

            }
            else
            {
                return Result.Failure<string>(tryUpdateStudent.Error);
            }
        }

        public async Task<Result<string>> DeleteStudent(long studentId)
        {
            string requestUrl = $"{_webApiBaseUrl}/api/delete/{studentId}/";
            var tryDeleteStudent = await PostAndReturnStudentId(requestUrl);
            if (tryDeleteStudent.IsSuccess)
            {
                return Result.Success<string>(tryDeleteStudent.Value);

            }
            else
            {
                return Result.Failure<string>(tryDeleteStudent.Error);
            }

        }

        //
        // Private helper classes





        //
        //  Fields

        private readonly HttpClient _httpClient;
        private readonly string _webApiBaseUrl;

    }
}