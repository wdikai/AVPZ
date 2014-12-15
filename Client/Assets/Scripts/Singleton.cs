using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Assets.Scripts
{
    class Singleton
    {
        static Singleton instance;
        public User UserData
        {
            get;
            private set;
        }
        static public Singleton GetInstance()
        {
            return instance;
        }
        static public void CreateInstance(User user)
        {
            instance = new Singleton(user);
        }
        private Singleton(User user)
        {
            UserData = user;
        }
    }
}
