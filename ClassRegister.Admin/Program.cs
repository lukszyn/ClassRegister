using ClassRegister.Admin.Models;
using System;
using Unity;

namespace ClassRegister.Admin
{
    class Program
    {
        private readonly IIoHelper _ioHelper;

        static void Main(string[] args)
        {
            var container = new DIContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
        }

        public Program(IIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        private void Run()
        {
            do
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("Press 1 to Add a coach");
                Console.WriteLine("Press 2 to Add a student");
                Console.WriteLine("Press 3 to Add a course");

                int adminChoice = _ioHelper.GetIntFromUser("Select option:");

                switch (adminChoice)
                {
                    case 1:
                        AddCoach();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        AddCourse();
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }

            } while (true);
        }

        private void AddCourse()
        {
            var course = new Course();

            course.Name = _ioHelper.GetStringFromUser("Enter the name of the course: ");
            course.StartDate = _ioHelper.GetDateFromUser("Enter the starting date: ");
            course.AttendanceThreshold = _ioHelper.GetPercentsFromUser("Enter the required attendance threshold (0-100%): ");
            course.HomeworkThreshold = _ioHelper.GetPercentsFromUser("Enter the required homework threshold (0-100%): ");
            course.TestThreshold = _ioHelper.GetPercentsFromUser("Enter the required tests threshold (0-100%): ");
            course.Coach = _ioHelper.GetStringFromUser("Enter the coach email: ");

        }

    }

        //public string Name { get; set; }
        //public DateTime StartDate { get; set; }
        //public Coach Coach { get; set; }
        //public double AttendanceThreshold { get; set; } = 0.7;
        //public double HomeworkThreshold { get; set; } = 0.7;
        //public double TestThreshold { get; set; } = 0.7;
        //public ICollection<Student> Students { get; set; }

        private void AddStudent()
        {
            throw new NotImplementedException();
        }

        private void AddCoach()
        {
            throw new NotImplementedException();
        }
    }
}
