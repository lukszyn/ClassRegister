using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ClassRegister.Admin
{
    public interface IIoHelper
    {
        DateTime GetDateFromUser(string message);
        int GetIntFromUser(string message);
        int GetPercentsFromUser(string message);
        string GetStringFromUser(string message);
        bool ValidatePercentage(int percentage);
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

        public DateTime GetDateFromUser(string message)
        {
            string format = "dd/MM/yyyy";
            DateTime result;

            while (!DateTime.TryParseExact(
                GetStringFromUser($"{message} [{format}]"),
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result))
            {
                Console.WriteLine("Not an valid date, try again.");
            }

            return result;
        }

        public int GetPercentsFromUser(string message)
        {
            int percent;

            do
            {
                percent = GetIntFromUser(message);
            } while (!ValidatePercentage(percent));

            return percent;
        }

        public bool ValidatePercentage(int percentage)
        {
            return (percentage > 0 && percentage < 100) ? true : false;
        }
    }
}
