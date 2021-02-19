using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIbreriaRD;
namespace L1_DanielElias_DiegoRamirez.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public List<Player> PlayersList;
        public  Manual_List<Player> playeradder;
        private Singleton()
        {
            PlayersList = new List<Player>();
            playeradder = new Manual_List<Player>();
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
