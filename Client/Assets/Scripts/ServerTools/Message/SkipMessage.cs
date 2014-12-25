using LitJson;
using Message;

namespace TCP.Message
{
    class SkipMessage
    {
        private JsonData _message = new JsonData();
        public JsonData CreateMessage()
        {
            _message["type"] = new JsonData(MessageType.Skip);

            return _message;
        }
    }
}
