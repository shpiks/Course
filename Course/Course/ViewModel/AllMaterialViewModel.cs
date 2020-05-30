using Course.Commands;
using Course.Context;
using Course.Model;
using Course.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    class AllMaterialViewModel
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        RelayCommand filterMaterialsCommand;
        RelayCommand showMaterialCommand;
        RelayCommand showVictimCommand;
        RelayCommand deleteMaterialCommand;


        ApplicationContext db;
        private Employee selectedEmployee = new Employee() { LastName = " " };
        private string selectedDecision;
        private Material selectedMaterial;
        private Victim selectedVictim;


        private ObservableCollection<Material> materials;
        private ObservableCollection<Employee> employees;
        public List<string> DecisionList { get; set; }

        public DateTime StartData { get; set; }
        public DateTime FinishData { get; set; }

        public string SelectedDecision
        {
            get
            {
                if (selectedDecision == " ")
                    return null;
                else
                    return selectedDecision;
            }

            set
            {
                selectedDecision = value;
                OnPropertyChanged("SelectedDecision");
            }
        }

        public ObservableCollection<Material> Materials
        {
            get { return materials; }

            set
            {
                materials = value;
                OnPropertyChanged("Materials");
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                
            }
        }

        public Employee SelectedEmployee
        {
            //get { return selectedEmployee; }

            get
            {
                if (selectedEmployee.LastName == " ")
                    return null;
                else
                    return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                
                
            }
        }

        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;

            }
        }

        public Victim SelectedVictim
        {
            get { return selectedVictim; }
            set
            {
                selectedVictim = value;
                OnPropertyChanged("SelectedVictim");

            }
        }

        public AllMaterialViewModel(ApplicationContext db)
        {
            this.db = db;
            Materials = new ObservableCollection<Material>();
            Employees = new ObservableCollection<Employee>();
            try
            {
                db.Materials.ToList().Where(x => x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));

                Employees.Add(new Employee() { LastName = " " });
                db.Employees.ToList().ForEach(x => Employees.Add(x));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка загрузки БД в конструкторе ");
            }

            DecisionList = new List<string> {" ", "Отказано в ВУД", "ВУД(факт)", "ВУД(лицо)", "Передано по территориальности",
                "Передано в др. службу", "Списано в дело" };


            StartData = DateTime.Today;
            FinishData = DateTime.Today;
        }

        public RelayCommand FilterMaterialsCommand
        {
            get
            {
                return filterMaterialsCommand ??
                  (filterMaterialsCommand = new RelayCommand((o) =>
                  {

                      Materials.Clear();
                      try
                      {
                          if (SelectedEmployee != null && SelectedDecision != null)
                          {
                              //Materials.Where(x => x.Employees.FirstOrDefault().EmployeeId == SelectedEmployee.EmployeeId);
                              db.Materials.Where(x => x.DateOfRegistration >= StartData
                                && x.DateOfRegistration <= FinishData
                                && x.Employees.FirstOrDefault().EmployeeId == SelectedEmployee.EmployeeId
                                && x.Decision == SelectedDecision)
                                .ToList().ForEach(x => Materials.Add(x));
                          }
                          else if (SelectedEmployee == null && SelectedDecision != null)
                          {
                              db.Materials.Where(x => x.DateOfRegistration >= StartData
                               && x.DateOfRegistration <= FinishData
                               && x.Decision == SelectedDecision)
                               .ToList().ForEach(x => Materials.Add(x));
                          }
                          else if (SelectedEmployee != null && SelectedDecision == null)
                          {
                              db.Materials.Where(x => x.DateOfRegistration >= StartData
                                && x.DateOfRegistration <= FinishData
                                && x.Employees.FirstOrDefault().EmployeeId == SelectedEmployee.EmployeeId)
                                .ToList().ForEach(x => Materials.Add(x));
                          }
                          else
                          {
                              db.Materials.Where(x => x.DateOfRegistration >= StartData
                                && x.DateOfRegistration <= FinishData).ToList().ForEach(x => Materials.Add(x));
                          }
                      }
                      catch (Exception ex)
                      {
                          logger.Error(ex, "Ошибка при фильрации данных из БД");
                      }

                  }
                  ));
            }
        }

        public RelayCommand ShowMaterialCommand
        {
            get
            {
                return showMaterialCommand ??
                  (showMaterialCommand = new RelayCommand((o) =>
                  {
                      LookMaterialWindow lookMaterialWindow = new LookMaterialWindow();
                      lookMaterialWindow.DataContext = SelectedMaterial;
                      lookMaterialWindow.ShowDialog();
                  }, (o => SelectedMaterial != null)
                  ));
            }
        }

        public RelayCommand ShowVictimCommand
        {
            get
            {
                return showVictimCommand ??
                  (showVictimCommand = new RelayCommand((o) =>
                  {
                      try
                      {
                          SelectedVictim = db.Victims.FirstOrDefault(x => x.Materials.FirstOrDefault().MaterialId == SelectedMaterial.MaterialId);
                      }
                      catch (Exception ex)
                      {
                          logger.Error(ex, "Ошибка c БД в ShowVictimCommand");
                      }

                      LookVictimWindow lookVictimWindow = new LookVictimWindow(null, db, SelectedVictim);
                      lookVictimWindow.ShowDialog();
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
                          try
                          {
                              db.Materials.Remove(db.Materials.Where(x => x.MaterialId == SelectedMaterial.MaterialId).First());
                              db.SaveChanges();
                              logger.Info("Материал ЕК№" + SelectedMaterial.NumberEK + " удален из БД");
                          }
                          catch (Exception ex)
                          {
                              logger.Error(ex, "Ошибка c удалением материала в БД ");
                          }
                          materials.Remove(SelectedMaterial);
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
