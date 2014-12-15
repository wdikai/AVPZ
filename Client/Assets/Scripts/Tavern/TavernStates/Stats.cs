//using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tavern.TavernStates
{
    class Stats:MonoBehaviour
    {
        public GUISkin Skin;
//        public User usr = new User();
        void OnGUI()
        {
            GUI.skin = Skin;
        GUI.Label(new Rect(0,0,1200,70),"");

        }
    }
}
