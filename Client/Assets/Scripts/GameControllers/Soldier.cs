using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class Soldier
    {
        public GameObject model;
        public Model.SoliderData Data;
        public int PlayerID;
        void Init(GameObject _model,Model.SoliderData _data,int _playerId)
        {
            model = _model;
            PlayerID = _playerId;
            Data = _data;
        }
        public static Soldier Create(GameObject _model,Model.SoliderData _data,int _playerId)
        {
            Soldier tmp = new Soldier();
            tmp.Init(_model,_data,_playerId);
            return tmp;
        }
       
    }
}
