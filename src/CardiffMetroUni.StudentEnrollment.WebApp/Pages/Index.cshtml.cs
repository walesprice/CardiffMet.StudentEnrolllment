using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities;
using CardiffMetroUni.StudentEnrollment.WebApp.Clients;
using CardiffMetroUni.StudentEnrollment.WebApp.Pages.Partials.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CardiffMetroUni.StudentEnrollment.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string _webApIBaseUrl { get; }


        [BindProperty, Required]
        [DisplayName("Search Text using Student Name or Student Id")]
        public string SearchText { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _webApIBaseUrl = configuration["WebAPI.BaseUrl"];
        }

        public async Task OnGetAsync()
        {
            var webApiClient = new WebApiClient(_webApIBaseUrl);
            try
            {
                Students = await webApiClient.GetAllStudents();
            } catch (Exception ex) {
                Students = new List<StudentModel>();
            }
        }


        //
        //  View Model Binding
        [BindProperty]
        public IReadOnlyList<StudentModel> Students { get; private set; }



        public async Task<IActionResult> OnPostDeleteStudentAsync(int studentId)
        {
            var webApiClient = new WebApiClient(_webApIBaseUrl);
            try
            {
                var result =  await webApiClient.DeleteStudent(studentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            return Redirect("~/");
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {

            var webApiClient = new WebApiClient(_webApIBaseUrl);
            try
            {
                Students = await webApiClient.SearchStudents(SearchText);
            }
            catch (Exception ex)
            {
                Students = new List<StudentModel>();
            }
            return Page();
        }

    }
}