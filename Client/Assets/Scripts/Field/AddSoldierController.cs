using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Field
{
    class AddSoldierController : MonoBehaviour
    {
        public GameObject Warior;
        public GameObject Troll;
        public GameObject IceGolem;
        private GameObject currentCharter;
        public bool IsChecked;
        public int CharterIndex
        {
            private set;
            public get;
        }
        public GameObject GetCharter()
        {
            return currentCharter;
        }

        public void SetCharter(int index)
        {

            switch (index)
            {
                case 0:
                    currentCharter = Warior;
                    CharterIndex = 0;
                    break;
                case 1:
                    currentCharter = Troll;
                    CharterIndex = 1;
                    break;
                case 2:
                    currentCharter = IceGolem;
                    CharterIndex = 2;
                    break;
            }
            IsChecked = true;
        }
    }
}
