using LitJson;
using Message;

namespace TCP.Message
{
    class StepMessage
    {
        private JsonData _message = new JsonData();
        public JsonData CreateMessage(int curremt, int target)
        {
            _message["type"] = new JsonData(MessageType.Step);
            _message["currentCellIndex"] = curremt;
            _message["turgetCellIndex"] = target;
            return _message;
        }
    }
}
