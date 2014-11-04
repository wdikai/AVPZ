
using Newtonsoft.Json.Linq;

namespace Server
{
  public class SignInManager:IServerListener
  {
    [ServerMethod]
    public JObject SignIn(JObject signInParams)
    {
      var login = signInParams["login"].ToString();
      var password = signInParams["password"].ToString();
      var response = new JObject(new JProperty("response", string.Format("User signed in as {0} with pass {1}", login, password)));
      return response;
    }
  }
}