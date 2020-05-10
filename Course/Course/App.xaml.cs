using Course.Context;
using Course.View;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Course
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        

        System.Threading.Mutex mutex;
        private void Application_Startup()
        {
            bool createdNew;
            string mutName = "Course";

            mutex = new System.Threading.Mutex(true, mutName, out createdNew);
            if (!createdNew)
            {
                this.Shutdown();
            }

        }



        [STAThread]
        static void Main()
        {
            App app = new App();
            app.Application_Startup();

            var splash = new SplashScreen("Resources/preview.jpeg");
            splash.Show(true);
            ApplicationContext db;
            db = new ApplicationContext();
            MainWindow window = new MainWindow(db);

            //TimerCallback tm = new TimerCallback(NotificationTimer.NotificationTimer.Notification);
            //Timer timer = new Timer(tm, db, 3000, 4000);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((sender, e) => dispatcherTimer_Tick(sender, e, db, dispatcherTimer));
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();

            app.Run(window);



        }

        private static void dispatcherTimer_Tick(object sender, EventArgs e, ApplicationContext db, DispatcherTimer dispatcherTimer)
        {
            ArrayList listMaterials = new ArrayList();
            

            db.Materials.ToList().Where(x => x.DateOfTerm == DateTime.Today.AddDays(1)).ToList().ForEach(x => listMaterials.Add(x));

            if (listMaterials.Count != 0)
            {
                NotificationWindow notificationWindow = new NotificationWindow(db);
                notificationWindow.ShowDialog();
            }
            dispatcherTimer.Interval = new TimeSpan(2, 0, 0);
        }
    }
}

