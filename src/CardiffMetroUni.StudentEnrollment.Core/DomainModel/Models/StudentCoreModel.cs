using CardiffMetroUni.StudentEnrollment.Core.DomainModel.Entities;

namespace CardiffMetroUni.StudentEnrollment.Core.DomainModel.Model
{
    public class StudentCoreModel
    {

        public enum FormState
        {
            CREATE,
            EDIT,
            VIEW
        }


        public FormState StudentModelState { get; }


        public StudentCoreModel()
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


        public StudentCoreModel(FormState studentModelState, Student student)
        {
            if (student != null)
            {
                StudentId = student.Id;
                FirstName = student.FirstName;
                MiddleName = student.MiddleName;
                Surname = student.Surname;
                UniversityCourse = student.UniversityCourse;
                WelshLanguageProficiency = student.WelshLanguageProficency;
                DateOfBirth = student.DateOfBirth;
                StartDate = student.StartDate;
                EndDate = student.EndDate;
            }
        }
        public static StudentCoreModel Create(FormState studentModelState, Student student)
        {
            return (student != null) ?
                new StudentCoreModel(studentModelState, student) :
                EmptyStudent();
        }

        public static StudentCoreModel EmptyStudent()
        {
            return new StudentCoreModel();
        }

        // 
        // parameters;


        public long StudentId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string UniversityCourse { get; set; }

        public string WelshLanguageProficiency { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}