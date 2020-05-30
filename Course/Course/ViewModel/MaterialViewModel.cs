using Course.Commands;
using Course.Context;
using Course.Model;
using Course.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class MaterialViewModel : INotifyPropertyChanged
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        ApplicationContext db;
        private Victim selectedVictim;
        private Employee Employee;

        public ObservableCollection<Victim> victimsList { get; private set; }
        public List<string> DecisionList { get; private set; }
        public List<string> PerspectiveList { get; private set; }

        public Material Material { get; private set; }

        //RelayCommand addVictimCommand;
        RelayCommand editVictimCommand;
        RelayCommand changeTermCommand;
        RelayCommand changeExtensionCommand;

        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AcceptCommand { get; private set; }


        public Victim SelectedVictim
        {
            get { return selectedVictim; }
            set
            {
                selectedVictim = value;
                OnPropertyChanged("SelectedVictim");
            }
        }


        public MaterialViewModel(Material material, Employee employee, ApplicationContext db)
        {
            this.db = db;
            victimsList = new ObservableCollection<Victim>();
            DecisionList = new List<string> { "Отказано в ВУД", "ВУД(факт)", "ВУД(лицо)", "Передано по территориальности",
                "Передано в др. службу", "Списано в дело" };
            PerspectiveList = new List<string> { "Отказной", "Факт", "Раскрытие" };
            Employee = employee;
            
            if (material == null)
            {
                
                    Material = new Material()
                    {
                        DateOfRegistration = DateTime.Today,

                        DateOfTerm = DateTime.Today.AddDays(10)

                    };
                    AcceptCommand = new RelayCommand(AddCommand);

            }
            else
            {
                //Material = material;

                Material = new Material()
                {
                    MaterialId = material.MaterialId,
                    NumberEK = material.NumberEK,
                    Story = material.Story,
                    DateOfRegistration = material.DateOfRegistration,
                    DateOfTerm = material.DateOfTerm,
                    Extension = material.Extension,
                    Decision = material.Decision,
                    ExecutedOrNotExecuted = material.ExecutedOrNotExecuted,
                    Perspective = material.Perspective
                };
                try
                {
                    db.Materials.Where(x => x.MaterialId == Material.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    logger.Error(exc, "Ошибка с загрузкой данных из БД в конструкторе класса");
                }
                AcceptCommand = new RelayCommand(EditCommand);
            }
        }

        private void AddCommand(object obj)
        {
            Material material = new Material()
            {
                NumberEK = Material.NumberEK,
                Story = Material.Story,
                DateOfRegistration =  Material.DateOfRegistration,
                DateOfTerm = Material.DateOfTerm,
                Extension = Material.Extension,
                Decision = Material.Decision,
                ExecutedOrNotExecuted = Material.ExecutedOrNotExecuted,
                Perspective = Material.Perspective

                //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
                //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            };

            try
            {
                db.Materials.Add(material);
                db.Employees.SingleOrDefault(x => x.EmployeeId == Employee.EmployeeId).Materials.Add(material);
                db.SaveChanges();
                logger.Info("Материал ЕК№" + material.NumberEK + " добавлен в БД");
                MessageBox.Show("Материал добавлен");
                ExitCommand.Execute();

        }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка c добавлением материала в БД ");
            }
}

        private void EditCommand(object obj)
        {
            //Material material = new Material()
            //{
            //    MaterialId = Material.MaterialId,
            //    NumberEK = Material.NumberEK,
            //    Story = Material.Story,
            //    DateOfRegistration = Material.DateOfRegistration,
            //    DateOfTerm = Material.DateOfTerm,
            //    Extension = Material.Extension,
            //    Decision = Material.Decision,
            //    ExecutedOrNotExecuted = Material.ExecutedOrNotExecuted,
            //    Perspective = Material.Perspective

            //    //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
            //    //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            //};

            try
            {

                var oldMaterial = db.Materials.Where(x => x.MaterialId == Material.MaterialId).SingleOrDefault();
                if (oldMaterial != null)
                {
                    //MaterialId = Material.MaterialId,
                    oldMaterial.NumberEK = Material.NumberEK;
                    oldMaterial.Story = Material.Story;
                    oldMaterial.DateOfRegistration = Material.DateOfRegistration;
                    oldMaterial.DateOfTerm = Material.DateOfTerm;
                    oldMaterial.Extension = Material.Extension;
                    oldMaterial.Decision = Material.Decision;
                    oldMaterial.ExecutedOrNotExecuted = Material.ExecutedOrNotExecuted;
                    oldMaterial.Perspective = Material.Perspective;
                }

                //oldMaterial = material;

                db.SaveChanges();
                logger.Info("Материал ЕК№" + oldMaterial.NumberEK + " изменен");
                MessageBox.Show("Материал изменен");
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка c изменением данных материала в БД ");
            }
        }



        public RelayCommand EditVictimCommand
        {
            get
            {
                return editVictimCommand ??
                  (editVictimCommand = new RelayCommand((o) =>
                  {
                      var result = MessageBox.Show("Изменить информацию о потерпевшем?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                      if (result == MessageBoxResult.Yes)
                      {
                          var material = o as Material;
                          VictimWindow victimWindow = new VictimWindow(Material, db, selectedVictim);
                          victimWindow.ShowDialog();

                          MessageBox.Show("Данные потерпевшего изменены");

                      }
                  }, (o => SelectedVictim != null)
                  ));
            }
        }

        public RelayCommand ChangeTermCommand
        {
            get
            {
                return changeTermCommand ??
                  (changeTermCommand = new RelayCommand((o) =>
                  {
                      if (o != null)
                      {
                          DateTime date = (DateTime)o;
                          Material.DateOfTerm = date.AddDays(10);
                      }
                  }
                  ));
            }
        }


        public RelayCommand ChangeExtensionCommand
        {
            get
            {
                return changeExtensionCommand ??
                  (changeExtensionCommand = new RelayCommand((o) =>
                  {
                      if ((bool)o == true)
                      {
                          MessageBox.Show("Материал продлен, не забудьте изменить срок");
                      }
                  }
                  ));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

