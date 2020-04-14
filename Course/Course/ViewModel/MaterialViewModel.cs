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

        private Employee Employee;

        public Material Material { get; private set; }

        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AcceptCommand { get; private set; }
        

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

                //db.Materials.Remove(db.Materials.Where(x => x.MaterialId == Material.MaterialId).First());
                //db.Materials.Add(material);

                db.SaveChanges();
                //_transferData.ID_Author = author.ID_Author;
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            //var 

            //var oldBook = _dbContext.Books.Where(x => x.ID_Book == Book.ID_Book).SingleOrDefault();

            //if (oldBook != null)
            //{
            //    oldBook.Name = Book.Name;
            //    oldBook.ID_Company = (SelectedCompany is null) ? null : SelectedCompany.ID_Company;
            //    oldBook.Year = (Book.Year is null) ? null : Book.Year;
            //    oldBook.ISBN = Book.ISBN;
            //    oldBook.Description = Book.Description;
            //    oldBook.ID_Genre = (SelectedGenre is null) ? null : SelectedGenre.ID_Genre;
            //};

            //_dbContext.SaveChanges();

        }







            //public event PropertyChangedEventHandler PropertyChanged;

            //public void OnPropertyChanged([CallerMemberName]string prop = "")
            //{
            //    if (PropertyChanged != null)
            //        PropertyChanged(this, new PropertyChangedEventArgs(prop));
            //}
        }
}

