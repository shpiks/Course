using Course.Commands;
using Course.Context;
using Course.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    class RewriteMaterialViewModelcs : INotifyPropertyChanged
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        ApplicationContext db;
        private Employee selectedEmployee;
        private Material material;
        private Employee oldEmployee;

        private ObservableCollection<Employee> employees;

        RelayCommand rewriteMaterialCommand;
        public RelayCommand ExitCommand { get; set; }



        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                //OnPropertyChanged("SelectedMaterial");
            }
        }

        public RewriteMaterialViewModelcs(ApplicationContext db, Material material, Employee employee)
        {
            this.db = db;
            this.material = material;
            oldEmployee = employee;
            this.Employees = new ObservableCollection<Employee>();
            try
            {
                db.Employees.ToList().ForEach(x => Employees.Add(x));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка с загрузкой данных из БД в конструкторе");
            }
            Employees.Remove(oldEmployee);
        }

        public RelayCommand RewriteMaterialCommand
        {
            get
            {
                return rewriteMaterialCommand ??
                  (rewriteMaterialCommand = new RelayCommand((o) =>
                  {
                      try
                      {
                          
                          db.Employees.FirstOrDefault(x => x.EmployeeId == SelectedEmployee.EmployeeId).Materials.Add(material);
                          db.Employees.FirstOrDefault(x => x.EmployeeId == oldEmployee.EmployeeId).Materials.Remove(material);
                          db.SaveChanges();
                          MessageBox.Show("Материал ЕК№" + material.NumberEK + " переписан на " + SelectedEmployee.LastName);
                          logger.Info("Материал ЕК№" + material.NumberEK + " переписан на " + SelectedEmployee.LastName);
                          ExitCommand.Execute();
                      }
                      catch (Exception exc)
                      {
                          MessageBox.Show(exc.Message);
                          logger.Error(exc, "Ошибка в комманде RewriteMaterialCommand");
                      }
                  }, (o => SelectedEmployee != null)
                  ));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
