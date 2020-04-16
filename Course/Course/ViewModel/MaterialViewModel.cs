using Course.Commands;
using Course.Context;
using Course.Model;
using Course.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    public class MaterialViewModel
    {
        ApplicationContext db;

        private Employee Employee;

        public Material Material { get; private set; }

        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AcceptCommand { get; private set; }

        RelayCommand addVictimCommand;


        public MaterialViewModel(Material material, Employee employee, ApplicationContext db)
        {
            this.db = db;
            Employee = employee;
            if (material == null)
            {
                Material = new Material();
                AcceptCommand = new RelayCommand(AddCommand); 
            }
            else
            {
                Material = material;
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
            Material material = new Material()
            {
                MaterialId = Material.MaterialId,
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

                var oldMaterial = db.Materials.Where(x => x.MaterialId == Material.MaterialId).SingleOrDefault();
                if (oldMaterial != null)
                    oldMaterial = material;

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
                      VictimWindow victimWindow = new VictimWindow(Material, db);
                      victimWindow.ShowDialog();

                      MessageBox.Show("Материал добавлен");

                  }
                  ));
            }
        }







        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged([CallerMemberName]string prop = "")
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //}
    }
}

