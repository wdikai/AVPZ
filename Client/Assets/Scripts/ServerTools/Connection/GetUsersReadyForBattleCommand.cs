using System;
using System.Collections.Generic;
using LitJson;
using Model;

namespace Connection
{
    class  GetUsersReadyForBattleCommand:ICommand
    {

        public class UserList
        {
            public List<User> response;
        }

        public String Error { get; private set; }
        public UserList Data { get; private set; }

        public  GetUsersReadyForBattleCommand()
        {
            Data = new UserList();
            Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "getusersreadyforbattle");
            try
            {
                Console.WriteLine(r);
                string data = HTTPConnection.Execute(r);
                Console.WriteLine(data);

                try
                {
                    JsonData outer = JsonMapper.ToObject(data); 
                    String inner = outer["response"].ToString();
                    Data.response = JsonMapper.ToObject<List<User>>(inner);
                }
                catch (Exception)
                {
                    Error = "Данные повреждены!";
                }
            }
            catch (Exception)
            {
                Error = "Ошибка соединения!";
            }
        }
    }
}
