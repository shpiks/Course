using Course.Context;
using Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
    class VictimViewModel
    {
        ApplicationContext db;

        Victim victim;
        Material material;

        public VictimViewModel(Material material, ApplicationContext db)
        {
            this.db = db;

            if(material.Victims == null)
            {

            }
            else
            {

            }

        }

    }
}
