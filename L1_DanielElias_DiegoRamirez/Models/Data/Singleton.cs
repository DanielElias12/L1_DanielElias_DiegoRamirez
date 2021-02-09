using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L1_DanielElias_DiegoRamirez.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Player> PlayersList;

        private Singleton()
        {
            PlayersList = new List<Player>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
