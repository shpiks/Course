using Course.Commands;
using Course.Context;
using Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ViewModel
{
    class AllMaterialViewModel
    {
        RelayCommand filterMaterialsCommand;
        ApplicationContext db;
        private Employee selectedEmployee;
        private string selectedDecision;



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
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                
            }
        }

        public AllMaterialViewModel(ApplicationContext db)
        {
            this.db = db;
            Materials = new ObservableCollection<Material>();
            Employees = new ObservableCollection<Employee>();
            db.Materials.ToList().Where(x => x.ExecutedOrNotExecuted != true).ToList().ForEach(x => Materials.Add(x));

            Employees.Add(null);
            db.Employees.ToList().ForEach(x => Employees.Add(x));

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
