using System.Collections.Generic;
using TestClient.Model;

namespace Model
{
    public class UserGameData
    {
        public string Name;
        public string SecondName;
        public string NickName;

        public Resources CurrentResources;
        public List<Troop> AllTroops;
        public List<UserMessage> PendingMessages;
    }
}
