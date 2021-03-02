using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRegister.Admin
{
    public interface IIoHelper
    {
        int GetIntFromUser(string message);
        string GetStringFromUser(string message);
    }

    public class IoHelper : IIoHelper
    {
        public int GetIntFromUser(string message)
        {
            int result;

            while (!int.TryParse(GetStringFromUser(message), out result))
            {
                Console.WriteLine("Not an integer - try again...");
            }

            return result;
        }

        public string GetStringFromUser(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
