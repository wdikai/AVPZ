using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Concrete.Entities
{
  public class User
  {
    public int UserId;
    public string Login;
    public string Password;

    public UserGameData GameData;
  }
}