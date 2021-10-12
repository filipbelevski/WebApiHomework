using System;
using System.Text.RegularExpressions;

namespace Shared.Validators
{
    public static class UserValidators
    {
        public static bool PasswordValidator(string password)
        {
            Regex regEx = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$");
            if (!regEx.IsMatch(password))
            {
                throw new Exception("Password must contain minimum of 6 characters, at least 1 uppercase letter, 1 lowercase letter and 1 number with no spaces");
            }
            return true;
        }
    }
}
