using System;
using Model;
using LitJson;

namespace Connection
{
    class BuyCommand: ICommand
    {

        private int _userId;
        private int _unitId;
        public String Error { get; private set; }

        public BuyCommand(int userId, int unitId)
        {
            _userId = userId;
            _unitId = unitId;
            Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "buyunit");
            
            JsonData data = new JsonData();
            data["userid"] = _userId;
            data["unitid"] = _unitId;
            r.AddParametr("data", data.ToJson());

            try
            {
                String response = HTTPConnection.Execute(r);

                JsonData outer = JsonMapper.ToObject(response);
				String inner = outer["response"].ToString();
				try
				{
					User Data = JsonMapper.ToObject<User>(inner);
				}
				catch (Exception)
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
