using Course.Commands;
using Course.Context;
using Course.Model;
using Course.View;
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
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        private Material selectedMaterial;
        private Employee selectedEmployee;

        RelayCommand addMaterialCommand;
        RelayCommand deleteMaterialCommand;
        RelayCommand editMaterialCommand;
        RelayCommand addEmployeeCommand;
        RelayCommand deleteEmployeeCommand;
        RelayCommand editEmployeeCommand;

        private ObservableCollection<Material> materials;
        private ObservableCollection<Employee> employees;


        public ObservableCollection<Material> Materials
        {
            get { return materials; }

            set
            {
                materials = value;
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;
                OnPropertyChanged("SelectedMaterial");
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
                materials.Clear();
                if (selectedEmployee != null)
                {
                    db.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId).Materials.ToList().ForEach(x => materials.Add(x));
                    //FilterBooks = "";
                }
            }

        }

        public MainWindowViewModel()
        {
            db = new ApplicationContext();
            //db.Materials.Load();
            this.Materials = new ObservableCollection<Material>();
            db.Employees.Load();
            this.Employees = new ObservableCollection<Employee>(db.Employees.Local.ToBindingList());
        }

        public RelayCommand AddMaterialCommand
        {
            get
            {
                return addMaterialCommand ??
                  (addMaterialCommand = new RelayCommand((o) =>
                  {
                      MaterialWindow materialWindow = new MaterialWindow(null, selectedEmployee, db);
                      materialWindow.ShowDialog();

                      materials.Clear();
                      db.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId).Materials.ToList().ForEach(x => materials.Add(x));

                      MessageBox.Show("Материал добавлен");

                  }, (o => SelectedEmployee != null)
                  ));
            }
        }

        public RelayCommand EditMaterialCommand
        {
            get
            {
                return editMaterialCommand ??
                  (editMaterialCommand = new RelayCommand((o) =>
                  {
                      var result = MessageBox.Show("Изменить информацию о материале?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                      if (result == MessageBoxResult.Yes)
                      {
                          var material = o as Material;
                          MaterialWindow materialWindow = new MaterialWindow(material, selectedEmployee, db);
                          materialWindow.ShowDialog();

                          MessageBox.Show("Материал изменен");

                      }
                  }, (o => SelectedMaterial != null)
                  ));
            }
        }

        public RelayCommand DeleteMaterialCommand
        {
            get
            {
                return deleteMaterialCommand ??
                  (deleteMaterialCommand = new RelayCommand((o) =>
                  {
                  var result = MessageBox.Show("Удалить материал?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                      if (result == MessageBoxResult.Yes)
                      {
                          var material = o as Material;
                          if (material != null)
                          {
                              db.Materials.Remove(db.Materials.Where(x => x.MaterialId == material.MaterialId).First());
                              db.SaveChanges();
                              materials.Remove(material);
                          }
                      }
                  }, (o => SelectedMaterial != null))
                  );
            }
        }

        public RelayCommand AddEmployeeCommand
        {
            get
            {
                return addEmployeeCommand ??
                  (addEmployeeCommand = new RelayCommand((o) =>
                  {
                      EmployeeWindow employeeWindow = new EmployeeWindow(null);
                      employeeWindow.ShowDialog();
                      var newEmployee = db.Employees.ToList().Except(employees.ToList()).FirstOrDefault();
                      if (newEmployee != null)
                          employees.Add(newEmployee);
                      MessageBox.Show("Сотрудник добавлен");
                  }));
            }
        }

        public RelayCommand EditEmployeeCommand
        {
            get
            {
                return editEmployeeCommand ??
                  (editEmployeeCommand = new RelayCommand((o) =>
                  {
                      var result = MessageBox.Show("Изменить информацию о сотруднике?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                      if (result == MessageBoxResult.Yes)
                      {
                          var employee = o as Employee;
                          EmployeeWindow employeeWindow = new EmployeeWindow(employee);
                          employeeWindow.ShowDialog();
                          employees.Remove(employee);
                          var newEmployee = db.Employees.ToList().Except(employees.ToList()).FirstOrDefault();
                          if (newEmployee != null)
                              employees.Add(newEmployee);
                          MessageBox.Show("Данные сотрудника изменены");


                      }
                  }, (o => SelectedEmployee != null)
                  ));
            }
        }

        public RelayCommand DeleteEmployeeCommand
        {
            get
            {
                return deleteEmployeeCommand ??
                  (deleteEmployeeCommand = new RelayCommand((o) =>
                  {
                      var result = MessageBox.Show("Удалить сотрудника?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                      if (result == MessageBoxResult.Yes)
                      {
                          var material = o as Material;
                          if (material != null)
                          {
                              db.Materials.Remove(db.Materials.Where(x => x.MaterialId == material.MaterialId).First());
                              db.SaveChanges();
                              materials.Remove(material);
                          }
                      }
                  }, (o => SelectedEmployee != null))
                  );
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
