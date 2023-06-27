using CardiffMetroUni.StudentEnrollment.WebApp.Clients;
using CardiffMetroUni.StudentEnrollment.WebApp.Pages.Partials.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace CardiffMetroUni.StudentEnrollment.WebApp.Pages.Student
{
    public class AddStudentModel : PageModel
    {

        public AddStudentModel(IConfiguration configuration)
        {
            _webApIBaseUrl = configuration["WebAPI.BaseUrl"];
        }



        [BindProperty]
        public StudentModel Student { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            Student = StudentModel.EmptyStudent();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var tryStartDateEndDateValid = Student.IsStartDateEndDatesValid();

            if (tryStartDateEndDateValid.IsFailure)
            {
                ModelState.AddModelError("StartDateEndDate", tryStartDateEndDateValid.Error);
            }

            var tryDateOfBirthValid = Student.IsDateOfBirthValid();

            if (tryDateOfBirthValid.IsFailure)
            {
                ModelState.AddModelError("StartDateEndDate", tryDateOfBirthValid.Error);
            }

            /*
            if (Student.WelshLanguageProficiency == "NULL")
            {
                ModelState.AddModelError("WelshLanguageProficiency", "The Welsh Language Proficiency option is required.");
            }
            */

            if (!ModelState.IsValid)
            {
                return Page();

            }
            else
            {

                var webApiClient = new WebApiClient(_webApIBaseUrl);

                var result = await webApiClient.CreateNewStudent(Student.FirstName,
                    Student.MiddleName,
                    Student.Surname,
                    Student.UniversityCourse,
                    Student.WelshLanguageProficiency,
                    Student.DateOfBirth,
                    Student.StartDate,
                    Student.EndDate
                );
                if (result.IsFailure)
                {
                    ModelState.AddModelError("UserAlreadyAdded", result.Error);
                    return Page();
                }

                return Redirect("~/");
            }
        }

        private string _webApIBaseUrl { get; }



    }
}
