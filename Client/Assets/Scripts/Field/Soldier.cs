using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Field
{
    class Soldier : MonoBehaviour
    {
        private GameObject model;
        public Model.SoliderData Data
        {
            public get;
            private set;
        }

        public int PlayerID;
        public Soldier(GameObject _model, Model.SoliderData _data,int _playerId)
        {
            model = _model;
            PlayerID = _playerId;
        }
        public void InstantinateSoldier(Vector3 _position)
        {
            Instantiate(model, _position, Quaternion.identity);
        }
    }
}
