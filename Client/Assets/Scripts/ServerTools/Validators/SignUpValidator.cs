using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestClient.Validators;

namespace Validators
{
    class SignUpValidator: IValidator
    {

        private string name;
        private string surname;
        private string login;
        private string password1;
        private string password2;
        private string nick;

        public SignUpValidator(string _name, string _surname, string _login, string _password1, string _password2, string _nick)
        {
            this.name = _name;
            this.surname = _surname;
            this.login = _login;
            this.password1 = _password1;
            this.password2 = _password2;
            this.nick = _nick;
        }

        public bool Check()
        {
            string pattern = "\\w";

            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(name) || name.Length > 30 || name.Length < 3)
                return false;
            if (!regex.IsMatch(surname) || surname.Length > 30 || surname.Length < 3)
                return false;
            if (!regex.IsMatch(login) || login.Length > 30 || login.Length < 3)
                return false;
            if (!regex.IsMatch(nick) || nick.Length > 30 || nick.Length < 3)
                return false;
            if (!regex.IsMatch(password1) || password1.Length > 30 || password1.Length < 3)
                return false;
            if (!regex.IsMatch(password2) || password2.Length > 30 || password2.Length < 3)
                return false;
            if (!password1.Equals(password2))
                return false;

            return true;
        }
    }
}
