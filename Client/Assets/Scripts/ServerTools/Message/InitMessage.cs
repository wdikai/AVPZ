using LitJson;
using Message;
using System.Collections.Generic;

namespace TCP.Message
{
    class InitMessage
    {
        private class Sold
        {
            public int Index;
            public int Id;
            public int UserID;

            public Sold(int _plId, int _index, int _troopId)
            {
                Index = _index;
                Id = _troopId;
                UserID = _plId;
            }
        }
        List<Sold> Troops = new List<Sold>();

        private JsonData _message = new JsonData();
        public JsonData CreateMessage(int curremt, int target)
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
