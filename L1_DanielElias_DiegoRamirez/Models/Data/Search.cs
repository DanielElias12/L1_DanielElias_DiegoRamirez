using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L1_DanielElias_DiegoRamirez.Models.Data
{
    public class Search
    {

        void search(List<Player> list, int count, string data)
        {
            List<Player> resultList = null;

            for (int i = 0; i < count; i++)
            {
                if(list.ElementAt(i).Name == data)
                {
                   resultList.Add(list.ElementAt(i));
                }
            }

            
        }
    }
}
