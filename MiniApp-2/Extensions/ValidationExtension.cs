using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniApp_2.Extensions
{
    static class ValidationExtension
    {
        public static bool IsValidName(this string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3 && name.IndexOf(' ') == -1 && char.IsUpper(name[0]);
        }

        public static bool IsValidSurname(this string surname)
        {
            return !string.IsNullOrEmpty(surname) && surname.Length >= 3 && surname.IndexOf(' ') == -1 && char.IsUpper(surname[0]);
        }

        public static bool IsValidClassroomName(this string classroomName)
        {
            return !string.IsNullOrEmpty(classroomName) && Regex.IsMatch(classroomName, @"^[A-Z]{2}[0-9]{3}$");
        }
    }
}
