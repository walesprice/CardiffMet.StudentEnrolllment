using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities;
using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Model;
using CardiffMetroUni.StudentEnrollment.Infrastructure.Data;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CardiffMetroUni.StudentEnrollment.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        public StudentController(ILogger<StudentController> logger, StudentDbContext repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("/api/GetStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _repository.Students.ToListAsync();

            List<StudentCoreModel> studentsModel = students.Select(student => StudentCoreModel.Create(StudentCoreModel.FormState.VIEW,student)).ToList();

            return Ok(studentsModel);
        }
        [HttpGet("/api/SearchStudents/{searchText}")]
        public async Task<IActionResult> SearchStudents(string searchText)
        {

            IQueryable<Student> baseQuery = _repository.Students;

            if (int.TryParse(searchText, out int studentId))
            {
                baseQuery = baseQuery.Where(s => s.Id == studentId);
            } else {

                foreach (var str in searchText.Split(" "))
                {
                    baseQuery = baseQuery.Where(s => s.FirstName.Contains(str) ||
                                                        s.MiddleName.Contains(str) ||
                                                        s.Surname.Contains(str)
                    );
                }
            }
            var allRecords = await baseQuery.ToListAsync();

            List<StudentCoreModel> studentsModel = allRecords.Select(student => StudentCoreModel.Create(StudentCoreModel.FormState.VIEW, student)).ToList();


            return Ok(studentsModel);
        }

        [HttpPost("/api/create/{firstName}/{middleName}/{surname}/{universityCourse}/{welshLanguageProficiency}/{dateOfBirth}/{startDate}/{endDate}")]
        public async Task<IActionResult> CreateNewStudent(string firstName, string middleName, string surname, string universityCourse, string welshLanguageProficiency, DateTime dateOfBirth, DateTime startDate, DateTime endDate)
        {
            if (middleName == "NULL")
            {
                middleName = "";
            }

            IQueryable<Student> students = _repository.Students;

            var existingStudents = students.Where(s => s.DateOfBirth == dateOfBirth &&
                                                s.FirstName == firstName && s.MiddleName == middleName && s.Surname == surname);

            if (existingStudents.Any())
            {
                return BadRequest(new { Message = "Student already exists", StudentId = -1 });

            }



            var student = Student.Create(firstName, middleName, surname, universityCourse, welshLanguageProficiency, dateOfBirth, startDate, endDate);
            _repository.Students.Add(student);
            _repository.SaveChanges();
            return Ok(new { Message = "Student created successfully", StudentId = student.Id });
        }


        [HttpPost("/api/update/{studentId}/{firstName}/{middleName}/{surname}/{welshLanguageProficiency}")]
        public async Task<IActionResult> UpdateStudent(int studentId, string firstName, string middleName, string surname, string welshLanguageProficiency)
        {

            if (middleName == "NULL")
            {
                middleName = "";
            }

            IQueryable<Student> students = _repository.Students;

            var existingStudents = students.Where(s => s.Id != studentId &&
                                                s.FirstName == firstName && s.MiddleName == middleName && s.Surname == surname);

            if (existingStudents.Any())
            {
                return BadRequest(new { Message = "Student already exists", StudentId = -1 });

            }

            var student = _repository.Students.Where(s => s.Id == studentId).First();
            student.Update(firstName, middleName, surname, welshLanguageProficiency);
            _repository.SaveChanges();
            return Ok(new { Message = "Student updated successfully", StudentId = student.Id });
        }

        [HttpPost("/api/delete/{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = _repository.Students.Where(s => s.Id == studentId).First();
            _repository.Students.Remove(student);
            _repository.SaveChanges();
            return Ok(new { Message = "Student deleted successfully", StudentId = student.Id });
        }

        [HttpGet("/api/GetStudentByStudentId/{studentId}")]
        public async Task<IActionResult> GetStudentByStudentId(int studentId)
        {
            var student = _repository.Students.Where(s => s.Id == studentId).First();

            StudentCoreModel studentModel = StudentCoreModel.Create(StudentCoreModel.FormState.VIEW, student);

            return Ok(studentModel);
        }



        private readonly ILogger<StudentController> _logger;

        protected StudentDbContext _repository;

    }
}