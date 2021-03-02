using System;
using Unity;

namespace ClassRegister.Admin
{
    class Program
    {
        private readonly IIoHelper _ioHelper;

        //TODO: add container
        //public Program(IIoHelper ioHelper)
        //{
        //    _ioHelper = ioHelper;
        //}

        static void Main(string[] args)
        {
<<<<<<< HEAD
            var container = new DIContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
=======
            new Program().Run();
>>>>>>> remotes/origin/feature/add_AdminApp
        }

        private void Run()
        {
<<<<<<< HEAD
=======
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
            throw new NotImplementedException();
        }

        private void AddStudent()
        {
            throw new NotImplementedException();
        }

        private void AddCoach()
        {
>>>>>>> remotes/origin/feature/add_AdminApp
            throw new NotImplementedException();
        }
    }
}
