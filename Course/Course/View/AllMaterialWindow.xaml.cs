using Course.Context;
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
    /// Логика взаимодействия для AllMaterialWindow.xaml
    /// </summary>
    public partial class AllMaterialWindow : Window
    {
        public AllMaterialWindow(ApplicationContext db)
        {
            InitializeComponent();
            AllMaterialViewModel allMaterialViewModel = new AllMaterialViewModel(db);
            this.DataContext = allMaterialViewModel;
        }
    }
}
