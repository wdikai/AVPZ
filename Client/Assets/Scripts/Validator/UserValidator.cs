using System;


namespace Validaors
{
	public class UserValidator 
	{
		String name;
		String password1;
		String password2;
		
		public UserValidator(String name, String password1, String password2)
		{
			this.name = name;
			this.password1 = password1;
			this.password2 = password2;
		}

		public bool isValid()
		{
			if (name.Length < 6 || name.Length > 16)
				return false;
			if (!password1.Equals (password2))
				return false;
			return true;
		}
	}
}