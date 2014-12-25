using Assets.Scripts.ServerTools.Model;
using LitJson;
using Message;
using System.Collections.Generic;

namespace TCP.Message
{
    class InitMessage
    {
        
        List<Sold> Troops = new List<Sold>();

        private JsonData _message = new JsonData();
        public JsonData CreateMessage()
        {
            _message["type"] = new JsonData(MessageType.Init);
            _message["troopsPosition"] = JsonMapper.ToJson(Troops);
            return _message;
        }

        public void AddSoldier(int _plId, int _index, int _troopId)
        {
            Troops.Add(new Sold(_plId, _index, _troopId));
        }
    }
}
