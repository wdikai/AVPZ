using System;
using Model;
using LitJson;


namespace Connection
{
    class SignInCommand: ICommand
    {
        private string _login;
        private string _password;
        public string Error { get; private set; }
        public User Data { get; private set; }

        public SignInCommand(string login, string password)
        {
            _login = login;
            _password = password;
            Error = "";
            Data = null;
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "signin");

            JsonData data = new JsonData();
            data["login"] = _login;
            data["password"] = _password;
            Console.WriteLine(data.ToJson());
            r.AddParametr("data", data.ToJson());

            try
            {
                string response = HTTPConnection.Execute(r);
                try
                {
                    JsonData outer = JsonMapper.ToObject(response);
                    string inner = outer["response"].ToString();
                    Console.WriteLine("SignIn\n" + inner);
                    Data = JsonMapper.ToObject<User>(inner);
                }
                catch (Exception e) 
                {
                    Error = "Не верный пользователь или пароль!";
                    Console.WriteLine();
                    Console.WriteLine(e);
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                Error = "Ошибка соединения!";
            }
        }
    }
}
