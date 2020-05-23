using Course.Commands;
using Course.Context;
using Course.Model;
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
            this.Employees = new ObservableCollection<Employee>(/*this.db.Employees.Local.ToBindingList()*/);
            db.Employees.ToList().ForEach(x => Employees.Add(x));
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
                          ExitCommand.Execute();
                      }
                      catch (Exception exc)
                      {
                          MessageBox.Show(exc.Message);
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
