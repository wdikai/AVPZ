using LitJson;
using Message;

namespace TCP.Message
{
    class DefeatMessage
    {
        private JsonData _message = new JsonData();
        public JsonData CreateMessage()
        {
            _message["type"] = new JsonData(MessageType.Defeat);
            return _message;
        }
    }
}
