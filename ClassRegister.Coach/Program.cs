using System;
using Unity;

namespace ClassRegister.Coach
{
    class Program
    {
        private IIoHelper _ioHelper;

        public Program(IIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        static void Main(string[] args)
        {
            var container = new DIContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
        }



        private void Run()
        {
            bool exit = false;

            do
            {
                Console.WriteLine("Welcome to the trainer app!");
                Console.WriteLine("Choose option:");
                Console.WriteLine("Press 1 to Log In");
                Console.WriteLine("Press 2 to Exit");

                int adminChoice = _ioHelper.GetIntFromUser("Select option:");

                switch (adminChoice)
                {
                    case 1:
                        LogIn();
                        break;
                    case 2:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
            while (!exit);
        }

        private void LogIn()
        {
            //var coach = new Coach
        }
    }
}
