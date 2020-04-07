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
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AcceptCommand { get; private set; }
        public Material Material { get; private set; }

        public MaterialViewModel(Material material)
        {
            db = new ApplicationContext();
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


            //db.Materials.Load();
            //this.Materials = db.Materials.Local.ToBindingList();
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
                db.SaveChanges();
                //_transferData.ID_Author = author.ID_Author;
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
                MaterialId =Material.MaterialId,
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
                db.Materials.Remove(db.Materials.Where(x => x.MaterialId == material.MaterialId).First());
                db.Materials.Add(material);
                db.SaveChanges();
                //_transferData.ID_Author = author.ID_Author;
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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

