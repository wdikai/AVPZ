using LitJson;
using Message;

namespace TCP.Message
{
    class AttackMessage
    {
        private JsonData _message = new JsonData();
        public JsonData CreateMessage(int target, int damege)
        {
            _message["type"] = new JsonData(MessageType.Step);
            _message["turgetAttackCellIndex"] = target;
            _message["Damege"] = damege;
            return _message;
        }
    }
}
