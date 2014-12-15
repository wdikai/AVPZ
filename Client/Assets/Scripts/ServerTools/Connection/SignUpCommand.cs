using System;
using LitJson;

namespace Connection
{
    class SignUpCommand: ICommand
    {

        private string _login;
        private string _password;
        private string _nickName;
        private string _name;
        private string _surname;
        public string Error { get; private set; }

       public SignUpCommand(string name, string surname, string login, string password, string nick)
        {
           this._name = name;
           this._surname = surname;
           this._login = login;
           this._password = password;
           this._nickName = nick;
           this.Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "signin");

            JsonData data = new JsonData();
            data["login"] = _login;
            data["password"] = _password;
            data["nickname"] = _nickName;
            data["name"] = _name;
            data["secondname"] = _surname;
            Console.WriteLine(data.ToJson());
            r.AddParametr("data", data.ToJson());

            try
            {
                string response = HTTPConnection.Execute(r);
                JsonData outer = JsonMapper.ToObject(response);
                string inner = outer["response"].ToString();
                Console.WriteLine("SignUp\n"+inner);
            }
            catch (Exception)
            {
                Error = "Ошибка соединения!";
            }
        }
    }
}
