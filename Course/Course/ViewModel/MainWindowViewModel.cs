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
        private ObservableCollection<Material> materials { get; set; }
        private ObservableCollection<Employee> employees { get; set; }


        public ObservableCollection<Material> Materials
        {
            get { return materials; }
            set
            {
                materials = value;
                //OnPropertyChanged("Materials");
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                //OnPropertyChanged("Materials");
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
            }
        }

        public MainWindowViewModel()
        {
            db = new ApplicationContext();
            db.Materials.Load();
            this.Materials = new ObservableCollection<Material>(db.Materials.Local.ToBindingList());
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
                      MaterialWindow materialWindow = new MaterialWindow(null);
                      materialWindow.ShowDialog();
                      var newMaterial = db.Materials.ToList().Except(materials.ToList()).FirstOrDefault();
                      if (newMaterial != null)
                          materials.Add(newMaterial);
                      MessageBox.Show("Материал добавлен");
                      //_transferData.ID_Author = null;
                  }));
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
                          MaterialWindow materialWindow = new MaterialWindow(material);
                          //materialWindow.
                          materialWindow.ShowDialog();
                          materials.Remove(material);
                          var newMaterial = db.Materials.ToList().Except(materials.ToList()).FirstOrDefault();
                          if (newMaterial != null)
                              materials.Add(newMaterial);
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
                              //SelectedAuthor = SelectedAuthor;
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
                      //_transferData.ID_Author = null;
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
                          //materialWindow.
                          employeeWindow.ShowDialog();
                          employees.Remove(employee);
                          var newEmployee = db.Employees.ToList().Except(employees.ToList()).FirstOrDefault();
                          if (newEmployee != null)
                              employees.Add(newEmployee);
                          MessageBox.Show("Данные сотрудника изменены");


                      }
                  }, (o => SelectedMaterial != null)
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
