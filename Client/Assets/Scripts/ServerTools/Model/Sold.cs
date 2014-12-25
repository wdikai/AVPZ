using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ServerTools.Model
{
    public class Sold
    {
        public int Index;
        public int Id;
        public int UserID;

        public Sold(int _plId, int _index, int _troopId)
        {
            Index = _index;
            Id = _troopId;
            UserID = _plId;
        }
    }
}
