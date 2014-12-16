using System;
using Model;
using LitJson;

namespace Connection
{
    class BuyTalantCommand: ICommand
    {

        private int _userId;
        private int _unitId;
        private UpgradeType _type;
        public User UData;
        public String Error { get; private set; }

        public BuyTalantCommand(int userId, int unitId, UpgradeType type)
        {
            _userId = userId;
            _unitId = unitId;
            _type = type;
            Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "buytalant");
            
            JsonData data = new JsonData();
            data["userid"] = _userId;
            data["unitid"] = _unitId;
            data["upgradetype"] = (int)_type;
            r.AddParametr("data", data.ToJson());

            try
            {
                String response = HTTPConnection.Execute(r);

                JsonData outer = JsonMapper.ToObject(response);
				String inner = outer["response"].ToString();
				try
				{
					UData = JsonMapper.ToObject<User>(inner);
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
