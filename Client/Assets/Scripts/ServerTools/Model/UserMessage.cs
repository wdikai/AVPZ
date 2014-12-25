using System;

namespace Model
{
    public class UserMessage
    {
        public int MessageId;
        public int UserId;
        public int TargetUserId;
        public UserMessageId MessageTypeId;
        public bool IsRead;

        public override string ToString()
        {
            return String.Format("mId:{0}\n" +
                                 "uId:{1}\n" +
                                 "tId:{2}\n" +
                                 "mTipe:{3}\n" +
                                 "isRead:{4}\n",
                                 MessageId, UserId, TargetUserId, MessageTypeId, IsRead);
        }
    }
}
