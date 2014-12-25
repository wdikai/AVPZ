using System;
using Model;
using LitJson;

namespace Connection
{
    class AcceptBattleCall: ICommand
    {

        private int _userId;
        private int _targetId;
        public String Error { get; private set; }

        public AcceptBattleCall(int userId, int targetId)
        {
            _userId = userId;
            _targetId = targetId;
            Error = "";
        }

        public void Execute()
        {
            var r = new Request("http://185.50.248.10:313/TestServer/MainHandler.ashx/");
            r.AddParametr("methodName", "acceptbattlecall");
            
            JsonData data = new JsonData();
            data["uid"] = _userId;
            data["target"] = _targetId;
            data["typeId"] = new JsonData {UserMessageId.BattleCall};
            r.AddParametr("data", data.ToJson());

            try
            {
                String response = HTTPConnection.Execute(r);

                JsonData outer = JsonMapper.ToObject(response);
				String inner = outer["response"].ToString();
                Error = inner;
            }
            catch (Exception)
            {
                Error = "Ошибка соединения!";
            }
        }

    }
}
