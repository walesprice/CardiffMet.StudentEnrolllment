using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities;
using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;

namespace CardiffMetroUni.StudentEnrollment.WebApp.Pages.Partials.Model
{
    public class StudentModel
    {

        public enum FormState
        {
            CREATE,
            EDIT,
            VIEW
        }


        public FormState StudentModelState { get; }

        public string IsReadOnly(string field)
        {

            string readonlyfields = "UniversityCourse,DateOfBirth,StartDate,EndDate";
            if (StudentModelState.Equals(FormState.VIEW))
            {
                return "readonly";
            }
            else if (StudentModelState.Equals(FormState.EDIT) && field != "" && readonlyfields.Contains(field))
            {
                return "readonly";
            }

            return "";

        }

        public StudentModel()
        {

            StudentId = -1;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            Surname = string.Empty;
            UniversityCourse = string.Empty;
            WelshLanguageProficiency = string.Empty;
            DateOfBirth = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            StudentModelState = FormState.CREATE;
        }


        public StudentModel(FormState studentModelState, StudentCoreModel student)
        {
            if (student != null)
            {
                StudentModelState = studentModelState;
                StudentId = student.StudentId;
                FirstName = student.FirstName;
                MiddleName = student.MiddleName;
                Surname = student.Surname;
                UniversityCourse = student.UniversityCourse;
                WelshLanguageProficiency = student.WelshLanguageProficiency;
                DateOfBirth = student.DateOfBirth;
                StartDate = student.StartDate;
                EndDate = student.EndDate;
            }
        }
        public static StudentModel Create(FormState studentModelState, StudentCoreModel student)
        {
            return (student != null) ?
                new StudentModel(studentModelState, student) :
                EmptyStudent();
        }

        public static StudentModel EmptyStudent()
        {
            return new StudentModel();
        }

        public Result IsStartDateEndDatesValid()
        {
            int result = DateTime.Compare(StartDate, EndDate);
            return (result >= 0) ? Result.Failure("The Start Date is the same or after the End Date.") : Result.Success();
        }

        public Result IsDateOfBirthValid()
        {
            int minAge = 15;
            var earliestDateOfBirth = DateTime.Now.Date.AddYears(-minAge);

            if (DateOfBirth > earliestDateOfBirth)
            {
                return Result.Failure($"The Date of Birth must be at least {minAge} years in the past.");
            }
            return Result.Success();
        }

        //
        //  Behaviour
        public string GetFullName() => $"{this.FirstName} {this.MiddleName} {this.Surname}";



        public IReadOnlyList<SelectListItem> WelshLanguageProficiencyOptions()
        {
            List<SelectListItem> allWelshOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "None", Text = "None" },
                    new SelectListItem { Value = "basic", Text = "use basic expressions" },
                    new SelectListItem { Value = "Interact", Text = "Interact with other welsh speakers" },
                    new SelectListItem { Value = "Fluent", Text = "Fluent" }
                };

            if (!String.IsNullOrWhiteSpace(WelshLanguageProficiency))
            {
                for (int i = 0; i < allWelshOptions.Count(); i++)
                {
                    var selectListItem = allWelshOptions[i];
                    if (selectListItem != null)
                    {
                        if (selectListItem.Value == WelshLanguageProficiency)
                        {
                            allWelshOptions[i].Selected = true;
                            break;
                        }
                    }
                }
            }
            return allWelshOptions;


        }
        // 
        // parameters;

        [BindProperty]
        [DisplayName("StudentId")]
        public long StudentId { get; set; }


        [BindProperty, Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [BindProperty]
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }

        [BindProperty, Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }

        [BindProperty, Required]
        [DisplayName("University Course")]
        public string UniversityCourse { get; set; }

        [BindProperty, Required]
        [DisplayName("Welsh Language Proficiency")]
        public string WelshLanguageProficiency { get; set; }

        [BindProperty, Required]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [BindProperty, Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}