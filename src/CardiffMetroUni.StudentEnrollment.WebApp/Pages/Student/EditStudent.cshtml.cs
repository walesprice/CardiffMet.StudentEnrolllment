using CardiffMetroUni.StudentEnrollment.WebApp.Clients;
using CardiffMetroUni.StudentEnrollment.WebApp.Pages.Partials.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CardiffMetroUni.StudentEnrollment.WebApp.Pages.Student
{
    public class EditStudentModel : PageModel
    {

        public EditStudentModel(IConfiguration configuration)
        {
            _webApiBaseUrl = configuration["WebAPI.BaseUrl"];
        }


        public async Task<IActionResult> OnGetAsync(long studentId)
        {

            var webApiClient = new WebApiClient(_webApiBaseUrl);

            var student = await webApiClient.GetStudentByStudentId(studentId);

            Student = (student == null) ? StudentModel.EmptyStudent() : student;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(long studentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var webApiClient = new WebApiClient(_webApiBaseUrl);

                var result = await webApiClient.UpdateStudent(studentId,
                    Student.FirstName,
                    Student.MiddleName,
                    Student.Surname,
                    Student.WelshLanguageProficiency
                );

                if (result.IsFailure)
                {
                    ModelState.AddModelError("UpdateStudent", result.Error);
                    return Page();
                }


                return Redirect("~/");
                
            }
        }

        [BindProperty]
        public StudentModel Student { get; set; }
        private string _webApiBaseUrl { get; }

    }
}
