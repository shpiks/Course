using Course.Commands;
using Course.Context;
using Course.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    class NotificationViewModel
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        ApplicationContext db;

        public ObservableCollection<Material> Materials { get; set; }

        public NotificationViewModel(ApplicationContext db, bool b)
        {
            this.db = db;
            Materials = new ObservableCollection<Material>();
            try
            {
                if (b == true)
                {
                    db.Materials.ToList().Where(x => x.DateOfTerm == DateTime.Today.AddDays(1) && x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));
                    logger.Info("Вызов уведомления таймером");
                }
                else
                    db.Materials.ToList().Where(x => x.DateOfTerm == DateTime.Today && x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка с загрузкой данных в уведомлениях");
            }

        }


    }
}
