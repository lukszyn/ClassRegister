using ClassRegister.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            bool exit = false; 

            do
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("Press 1 to Add a coach");
                Console.WriteLine("Press 2 to Add a student");
                Console.WriteLine("Press 3 to Add a course");
                Console.WriteLine("Press 4 to Exit");


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
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }

            } while (!exit);
        }

        private void AddStudent()
        {
            var student = ProvideStudent();

            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(@"http://localhost:10500/api/students", content).Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Success. Response content: {responseText}");
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private Student ProvideStudent()
        {
            return new Student()
            {
                Name = _ioHelper.GetStringFromUser("Enter student name:"),
                Surname = _ioHelper.GetStringFromUser("Enter student surname:"),
                Email = _ioHelper.GetEmailFromUser("Enter student email:"),
                DateOfBirth = _ioHelper.GetDateTimeFromUser("Enter student's birthday:"),
                Password = _ioHelper.GetPasswordFromUser("Enter password:")
            };
        }

        private void AddCoach()
        {
            var coach = ProvideCoach();

            var content = new StringContent(JsonConvert.SerializeObject(coach), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(@"http://localhost:10500/api/coaches", content).Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Success. Response content: {responseText}");
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private Coach ProvideCoach()
        {
            var newCoach = new Coach()
            {
                Name = _ioHelper.GetStringFromUser("Enter coach name:"),
                Surname = _ioHelper.GetStringFromUser("Enter coach surname:"),
                Email = _ioHelper.GetEmailFromUser("Enter coach email:"),
                BirthDate = _ioHelper.GetDateTimeFromUser("Enter coach's bday date:"),
                Password = _ioHelper.GetPasswordFromUser("Enter password:")
            };

            return newCoach;
        }

        private void AddCourse()
        {
            var course = GetCourse();

            var content = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(@"http://localhost:10500/api/courses", content).Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Success. Response content: {responseText}");
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private Course GetCourse()
        {
            return new Course()
            {
                Name = _ioHelper.GetStringFromUser("Enter the name of the course: "),
                StartDate = _ioHelper.GetDateTimeFromUser("Enter the starting date: "),
                AttendanceThreshold = _ioHelper.GetPercentsFromUser("Enter the required attendance threshold (0-100%): "),
                HomeworkThreshold = _ioHelper.GetPercentsFromUser("Enter the required homework threshold (0-100%): "),
                TestThreshold = _ioHelper.GetPercentsFromUser("Enter the required tests threshold (0-100%): "),
                Coach = GetCoach(_ioHelper.GetStringFromUser("Enter the coach email: ")),
                Students = GetStudents()
            };
        }

        private ICollection<Student> GetStudents()
        {
            var students = new List<Student>();

            while (students.Count < 5)
            {
                var studentEmail = _ioHelper.GetEmailFromUser("Enter student\'s email: ");
                var student = GetStudent(studentEmail);

                if (student != null)
                {
                    students.Add(student);
                }

                if (students.Count >= 20)
                {
                    break;
                }
            }

            return students;
        }

        private Student GetStudent(string email)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/students/{email}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<Student>(responseText);
                    return responseObject;
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }

        private Coach GetCoach(string email)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/coaches/{email}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<Coach>(responseText);
                    return responseObject;
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
