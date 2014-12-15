using System;
using System.Collections.Generic;
using Model;
using LitJson;

namespace Connection
{
    class RefreshCommand: ICommand
    {

        public String Error { get; private set; }
        public List<UserMessage> Messages{ get; private set; }
        
        private List<int> _messegeNumbers;
        private bool _readyDeleteMessege;
        private string _login;
        private string _password;

        public RefreshCommand(string login, string password)
        {
            _login = login;
            _password = password;
            _messegeNumbers = new List<int>();
            _readyDeleteMessege = false;
            Messages = new List<UserMessage>();
            Error = "";
        }

        public void AddMassegeNumber(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                _messegeNumbers.Add(numbers[i]);
            }
            _readyDeleteMessege = true;
        }


        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "refresh");
            
            JsonData data = new JsonData();
            data["login"] = _login;
            data["password"] = _password;
            if (_readyDeleteMessege)
            {
                data["messages"] = JsonMapper.ToJson(_messegeNumbers);
                _readyDeleteMessege = false;
            }
            else
            {
                data["messages"] = "[]";
            }
            r.AddParametr("data", data.ToJson());

            try
            {
                String response = HTTPConnection.Execute(r);

                JsonData outer = JsonMapper.ToObject(response);
                String inner = outer["response"].ToString();
				try
				{
                    Messages = JsonMapper.ToObject<List<UserMessage>>(inner);
				}
				catch (Exception e)
                {
					Error = inner;
				}
            }
            catch (Exception)
            {
                Error = "Ошибка соединения!";
            }
        }

    }
}
