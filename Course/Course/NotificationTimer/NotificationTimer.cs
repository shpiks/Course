using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.NotificationTimer
{
    class NotificationTimer
    {
        static public void Notification(object odj)
        {
            //var listMaterials = db.Materials.ToList();
            //listMaterials.Where(x => x.DateOfTerm == DateTime.Today.AddDays(-1));
            MessageBox.Show("Приветики");
        }
    }
}
