using System;
using System.Collections.Generic;
using Model;
using LitJson;

namespace Connection
{
    class GetStaticDataCommand:ICommand
    {
        public String Error { get; private set; }
        public StaticData Data { get; private set; }

        public GetStaticDataCommand()
        {
            Data = new StaticData();
            Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "getstaticdata");
            try
            {
                string data = HTTPConnection.Execute(r);

                Console.WriteLine(data);
                try
                {
					JsonData outer = JsonMapper.ToObject(data);
                    String inner = outer["response"].ToString();
                    Console.WriteLine("StaticData\n" + inner);
					Data.response = JsonMapper.ToObject<List<GameObject>>(inner);
                }
                catch (Exception e)
                {
                    Error = "Данные повреждены!";
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
