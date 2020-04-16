using Course.Context;
using Course.Model;
using Course.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course.View
{
    /// <summary>
    /// Логика взаимодействия для VictimWindow.xaml
    /// </summary>
    public partial class VictimWindow : Window
    {
        public VictimWindow(Material material, ApplicationContext db)
        {
            InitializeComponent();
            VictimViewModel victimViewModel = new VictimViewModel(material, db);
            //materialViewModel.ExitCommand = new Commands.RelayCommand(x => this.Close());
            this.DataContext = victimViewModel;
        }
    }
}
