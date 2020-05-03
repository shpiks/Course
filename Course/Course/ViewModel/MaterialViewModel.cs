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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class MaterialViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        private Victim selectedVictim;
        private Employee Employee;

        public ObservableCollection<Victim> victimsList { get; private set; }

        public Material Material { get; private set; }

        RelayCommand addVictimCommand;
        RelayCommand editVictimCommand;
        RelayCommand changeTermCommand;

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
            Employee = employee;
            
            if (material == null)
            {
                Material = new Material();
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

                db.Materials.Where(x => x.MaterialId == Material.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));
                AcceptCommand = new RelayCommand(EditCommand);
            }
        }

        private void AddCommand(object obj)
        {
            Material material = new Material()
            {
                NumberEK = Material.NumberEK,
                Story = Material.Story,
                DateOfRegistration = Material.DateOfRegistration,
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
                ExitCommand.Execute();

        }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public RelayCommand AddVictimCommand
        {
            get
            {
                return addVictimCommand ??
                  (addVictimCommand = new RelayCommand((o) =>
                  {
                      VictimWindow victimWindow = new VictimWindow(Material, db, null);
                      victimWindow.ShowDialog();

                      victimsList.Clear();
                      db.Materials.Where(x => x.MaterialId == Material.MaterialId).SingleOrDefault().Victims.ToList().ForEach(x => victimsList.Add(x));

                      MessageBox.Show("Потерпевший добавлен");
                  }
                  ));
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
                      DateTime date = (DateTime) o;
                      Material.DateOfTerm = date.AddDays(10);
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

