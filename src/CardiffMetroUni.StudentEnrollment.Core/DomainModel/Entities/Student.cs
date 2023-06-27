using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities
{
    public partial class Student : Entity
    {

        public Student() {
        
        }

        public static Student Create(string firstName, string middleName, string surname, string universityCourse, string welshLanguageProficency, DateTime dateOfBirth, DateTime startDate, DateTime endDate)
        {
            return new Student(
                firstName: firstName,
                middleName: middleName,
                surname: surname,
                universityCourse: universityCourse,
                welshLanguageProficency: welshLanguageProficency,
                dateOfBirth: dateOfBirth,
                startDate: startDate,
                endDate: endDate);
        }


        public void Update(string firstName, string middleName, string surname,string welshLanguageProficency)
        {
            FirstName = firstName;
            MiddleName = middleName;
            Surname = surname;
            WelshLanguageProficency = welshLanguageProficency;
        }

        public Student(string firstName, string middleName, string surname, string universityCourse, string welshLanguageProficency, DateTime dateOfBirth, DateTime startDate, DateTime endDate)
        {
            FirstName = firstName;
            MiddleName = middleName;
            Surname = surname;
            UniversityCourse = universityCourse;
            WelshLanguageProficency = welshLanguageProficency;
            DateOfBirth = dateOfBirth;
            StartDate = startDate;
            EndDate = endDate;
        }


        // 
        // parameters;

        //[NotMapped]
        //public long StudentId { get; }
        
        public string FirstName { get; set; }

        
        public string? MiddleName { get; set; }
        public string Surname { get; set; }
        public string UniversityCourse { get; set; }
        public string WelshLanguageProficency { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}