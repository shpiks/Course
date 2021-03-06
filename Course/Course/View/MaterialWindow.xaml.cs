﻿using Course.Context;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course.View
{
    /// <summary>
    /// Логика взаимодействия для MaterialWindow.xaml
    /// </summary>
    public partial class MaterialWindow : Window
    {
        
        public MaterialWindow(Material material, Employee employee, ApplicationContext db)
        {
            InitializeComponent();
            MaterialViewModel materialViewModel = new MaterialViewModel(material, employee, db);
            materialViewModel.ExitCommand = new Commands.RelayCommand(x => this.Close());
            this.DataContext = materialViewModel;
        }



    }
}
