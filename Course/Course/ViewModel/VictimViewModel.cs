using Course.Commands;
using Course.Context;
using Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.ViewModel
{
    class VictimViewModel
    {
        ApplicationContext db;

        public Victim Victim { get; private set; }
        Material Material;

        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand ExitCommand { get; set; }

        public VictimViewModel(Material material, ApplicationContext db, Victim victim)
        {
            this.db = db;
            Material = material;

            if (victim == null)
            {
                Victim = new Victim();
                AcceptCommand = new RelayCommand(AddCommand);
            }
            else
            {
                Victim = victim;
                AcceptCommand = new RelayCommand(EditCommand);
            }
        }


        private void AddCommand(object obj)
        {
            Victim victim = new Victim()
            {
                FirstName = Victim.FirstName,
                LastName = Victim.LastName,
                Patronymic = Victim.Patronymic,
                PhoneNumber = Victim.PhoneNumber,
                City = Victim.City,
                Street = Victim.Street,
                Home = Victim.Home,
                Flat = Victim.Flat,
                DateOfBirth = Victim.DateOfBirth
            };

            //try
            //{
                db.Victims.Add(victim);
                db.Materials.SingleOrDefault(x => x.MaterialId == Material.MaterialId).Victims.Add(victim);
                db.SaveChanges();
                ExitCommand.Execute();
            //}

            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //}

        }

        private void EditCommand(object obj)
        {
            Victim victim = new Victim()
            {
                FirstName = Victim.FirstName,
                LastName = Victim.LastName,
                Patronymic = Victim.Patronymic,
                PhoneNumber = Victim.PhoneNumber,
                City = Victim.City,
                Street = Victim.Street,
                Home = Victim.Home,
                Flat = Victim.Flat,
                DateOfBirth = Victim.DateOfBirth

                //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
                //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            };

            try
            {

                var oldVictim = db.Victims.Where(x => x.VictimId == Victim.VictimId).SingleOrDefault();
                if (oldVictim != null)
                    oldVictim = victim;

                db.SaveChanges();
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}
