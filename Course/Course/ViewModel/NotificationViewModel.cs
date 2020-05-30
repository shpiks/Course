using Course.Commands;
using Course.Context;
using Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
    class NotificationViewModel
    {
        ApplicationContext db;

        RelayCommand NotificationTomorrowCommand;

        public ObservableCollection<Material> Materials { get; set; }

        public NotificationViewModel(ApplicationContext db, bool b)
        {
            this.db = db;
            Materials = new ObservableCollection<Material>();
            if (b == true)
            {
                db.Materials.ToList().Where(x => x.DateOfTerm == DateTime.Today.AddDays(1) && x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));
            }
            else
                db.Materials.ToList().Where(x => x.DateOfTerm == DateTime.Today && x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));


        }


    }
}
