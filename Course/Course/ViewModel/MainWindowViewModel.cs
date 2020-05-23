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
using System.Threading;
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
        RelayCommand lookVictimCommand;
        RelayCommand addVictimCommand;
        RelayCommand lookAllMaterialCommand;
        RelayCommand rewriteMaterialCommand;

        public RelayCommand ExitCommand { get; set; }



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
                    db.Materials.Where(x => x.DateOfTerm < DateTime.Today).ToList().ForEach(x => x.ExecutedOrNotExecuted = true);
                    db.SaveChanges();
                    db.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId).Materials.Where(x => x.ExecutedOrNotExecuted != true).ToList().ForEach(x => materials.Add(x));
                    //FilterBooks = "";
                }
            }

        }

        public MainWindowViewModel(ApplicationContext db)
        {
            //db = new ApplicationContext();
            this.db = db;

            this.Materials = new ObservableCollection<Material>();
            this.db.Employees.Load();
            this.Employees = new ObservableCollection<Employee>(this.db.Employees.Local.ToBindingList());


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
                      db.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId).Materials.Where(x => x.ExecutedOrNotExecuted != true).ToList().ForEach(x => materials.Add(x));

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
                      EmployeeWindow employeeWindow = new EmployeeWindow(null, db);
                      employeeWindow.ShowDialog();
                      var newEmployee = db.Employees.ToList().Except(employees.ToList()).FirstOrDefault();
                      if (newEmployee != null)
                          employees.Add(newEmployee);
                      //MessageBox.Show("Сотрудник добавлен");
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
                          EmployeeWindow employeeWindow = new EmployeeWindow(employee, db);
                          employeeWindow.ShowDialog();


                          //employees.Remove(employee);
                          //var newEmployee = db.Employees.ToList().Except(employees.ToList()).FirstOrDefault();
                          //if (newEmployee != null)
                          //    employees.Add(newEmployee);
                          //MessageBox.Show("Данные сотрудника изменены");


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

        public RelayCommand LookVictimCommand
        {
            get
            {
                return lookVictimCommand ??
                  (lookVictimCommand = new RelayCommand((o) =>
                  {
                      var victim = o as Victim;
                      LookVictimWindow victimWindow = new LookVictimWindow(null, db, victim);
                      victimWindow.ShowDialog();

                  }, (o => SelectedMaterial != null)
                  ));
            }
        }

        public RelayCommand AddVictimCommand 
        {
            get
            {
                return addVictimCommand ??
                  (addVictimCommand = new RelayCommand((o) =>
                  {
                      VictimWindow victimWindow = new VictimWindow(SelectedMaterial, db, null);
                      victimWindow.ShowDialog();

                      //victimsList.Clear();
                      //db.Materials.Where(x => x.MaterialId == SelectedMaterial.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));

                      //MessageBox.Show("Потерпевший добавлен");

                  }, (o => SelectedMaterial != null)
                  ));
            }
        }

        public RelayCommand LookAllMaterialCommand
        {
            get
            {
                return lookAllMaterialCommand ??
                  (lookAllMaterialCommand = new RelayCommand((o) =>
                  {
                      AllMaterialWindow allMaterialWindow = new AllMaterialWindow(db);
                      allMaterialWindow.ShowDialog();

                      //victimsList.Clear();
                      //db.Materials.Where(x => x.MaterialId == SelectedMaterial.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));

                      //MessageBox.Show("Потерпевший добавлен");

                  }
                  ));
            }
        }

        public RelayCommand RewriteMaterialCommand
        {
            get
            {
                return rewriteMaterialCommand ??
                  (rewriteMaterialCommand = new RelayCommand((o) =>
                  {
                      RewriteMaterialWindow rewriteMaterialWindow = new RewriteMaterialWindow(db, SelectedMaterial, SelectedEmployee);
                      
                      rewriteMaterialWindow.ShowDialog();

                      //victimsList.Clear();
                      //db.Materials.Where(x => x.MaterialId == SelectedMaterial.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));

                      //MessageBox.Show("Потерпевший добавлен");
                      Materials.Clear();
                      db.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId).Materials.Where(x => x.ExecutedOrNotExecuted != true).ToList().ForEach(x => materials.Add(x));

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
