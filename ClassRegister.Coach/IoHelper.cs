﻿using ClassRegister.CoachApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ClassRegister.CoachApp
{
    public interface IIoHelper
    {
        int GetIntFromUser(string message);
        int GetPercentsFromUser(string message);
        string GetStringFromUser(string message);
        bool ValidatePercentage(int percentage);        
        public DateTime GetDateTimeFromUser(string message);
        string GetEmailFromUser(string message);
        string GetPasswordFromUser(string message);
        Status GetAttendanceStatus(string v);
        void PrintCourse(Course course);
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

        public DateTime GetDateTimeFromUser(string message)
        {
            string format = "dd/MM/yyyy";
            DateTime result;

            while(!DateTime.TryParseExact(
                GetStringFromUser($"{message} [{format}]"),
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result))
            {
                Console.WriteLine("Not an valid date. Try again...");
            }

            return result;
        }

        public string GetEmailFromUser(string message)
        {
            string email;
            bool validation;

            do
            {
                email = GetStringFromUser(message);
                validation = true;

                if (!email.Contains("@"))
                {
                    Console.WriteLine("Invalid email address, does not contain @. Try again...");
                    validation = false;
                }
            } 
            while (validation == false);

            return email;
        }

        public string GetPasswordFromUser(string message)
        {
            string password;
            bool validation;

            do
            {
                password = GetStringFromUser(message);
                validation = true;

                if (password.Length < 6)
                {
                    Console.WriteLine("Password is too short (min. 6 characters). Try again...");
                    validation = false;
                }

            }
            while (validation == false);

            return password;
        }

        public Status GetAttendanceStatus(string message)
        {
            var correctValues = "";

            foreach (var status in (Status[])Enum.GetValues(typeof(Status)))
            {
                correctValues += $"{status},";
            }

            object result;

            while (!Enum.TryParse(typeof(Status), GetStringFromUser($"{message} ({correctValues}):"), out result))
            {
                Console.WriteLine("Incorrect value, try again");
            }

            return (Status)result;
        }

        public void PrintCourse(Course course)
        {
            Console.WriteLine($"{course.Id}. {course.Name}");
        }
    }
}
