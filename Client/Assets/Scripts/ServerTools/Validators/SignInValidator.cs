using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestClient.Validators;

namespace Validators
{
    class SignInValidator: IValidator
    {
        private string login;
        private string password1;

        public SignInValidator(string _login, string _password1)
        {
            this.login = _login;
            this.password1 = _password1;
        }

        public bool Check()
        {
            string pattern = "\\w";

            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(login) || login.Length > 30 || login.Length < 3)
                return false;
            if (!regex.IsMatch(password1) || password1.Length > 30 || password1.Length < 3)
                return false;

            return true;
        }
    }
}
