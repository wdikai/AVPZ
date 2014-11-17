using Models;
using UnityEngine;
using System.Collections;

namespace Requests
{
	public class SignIn: Requests.IRequest
	{
		private string login, password;	
		private UserModel user;

		public SignIn (string login, string password, UserModel user)
		{
			this.login = login;
			this.password = password;
			this.user = user;
		}

		public void Execute ()
		{	
			string url = "http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=signin&uid=0&data={" +
				"login:" +login+
				",password:" + password+
				"}";
			var connect = new WWW (url);
			if (connect.isDone)
			{
				Debug.Log(connect.text);
				user.Name = connect.text;
			}
			else if (connect.error == null)
			{
				Debug.Log("Error");
			}

		}
	}
}

