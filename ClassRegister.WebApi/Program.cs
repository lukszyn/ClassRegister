using System;
using Unity;

namespace ClassRegister.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DIContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
        }

        private void Run()
        {
            throw new NotImplementedException();
        }
    }
}