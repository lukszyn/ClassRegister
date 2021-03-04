using System;
using System.Globalization;

namespace ClassRegister.BusinessLayer.Services
{
    public interface IValidationService
    {
        bool CheckFormatOfEnteredEmail(string email);
        bool CheckFormatOfEnteredPassword(string password);
    }

    public class ValidationService : IValidationService
    {
        public bool CheckFormatOfEnteredEmail(string email)
        {
            if (!email.Contains("@"))
            {
                return false;
            }

            return true;
        }

        public bool CheckFormatOfEnteredPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}
