using System.Text.RegularExpressions;

namespace Backend.helpers
{
    public static class ValidationHelper
    {
        public static bool ValidUsername(string username) =>
            Regex.IsMatch(username, @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,20}$");

        public static bool ValidPassword(string password) =>
            Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[\W])\S{8,}$");

        public static bool ValidCedula(string id) =>
            Regex.IsMatch(id, @"^\d{10}$") &&
            !Regex.IsMatch(id, @"(\d)\1\1\1");
    }
}
