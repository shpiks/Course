using Course.View;
using System;
using System.Threading;
using System.Windows;

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
            MainWindow window = new MainWindow();

            TimerCallback tm = new TimerCallback(NotificationTimer.NotificationTimer.Notification);
            Timer timer = new Timer(tm, null, 0, 2000);

            app.Run(window);


        }



    }
}
