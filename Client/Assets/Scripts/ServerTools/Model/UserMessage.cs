using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestClient.Model
{
    public class UserMessage
    {
        public long MessageId;
        public long UserId;
        public long TargetUserId;
        public UserMessageId MessageTypeId;
        public bool IsRead;
    }
}
