using Course.Commands;
using Course.Context;
using Course.Model;
using NLog;
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
        Logger logger = LogManager.GetCurrentClassLogger();

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
                Victim = new Victim()
                {
                    DateOfBirth = new DateTime(1980, 01, 01),
                    FirstName ="Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович"
                };
                
                AcceptCommand = new RelayCommand(AddCommand);
            }
            else if (Material == null)
            {
                Victim = victim;
                AcceptCommand = new RelayCommand(LookCommand);
            }
            else
            {
                //Victim = victim;
                Victim = new Victim()
                {
                    VictimId = victim.VictimId,
                    FirstName = victim.FirstName,
                    LastName = victim.LastName,
                    Patronymic = victim.Patronymic,
                    PhoneNumber = victim.PhoneNumber,
                    City = victim.City,
                    Street = victim.Street,
                    Home = victim.Home,
                    Flat = victim.Flat,
                    DateOfBirth = victim.DateOfBirth,
                    Corps = victim.Corps
                };
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
                DateOfBirth = Victim.DateOfBirth,
                Corps = Victim.Corps
            };

            try
            {
            db.Victims.Add(victim);
            db.Materials.SingleOrDefault(x => x.MaterialId == Material.MaterialId).Victims.Add(victim);
            db.SaveChanges();
            MessageBox.Show("Потерпевший добавлен, после обновления выбора материала или сотрудника он будет отображаться в соответсвующем поле");
            logger.Info("Потерпевший " + Victim.FirstName + Victim.LastName + " добавлен в БД");
            ExitCommand.Execute();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка в комманде AddCommand");
            }

        }

        private void EditCommand(object obj)
        {
            try
            {

                var oldVictim = db.Victims.Where(x => x.VictimId == Victim.VictimId).SingleOrDefault();
                if (oldVictim != null)
                {
                    oldVictim.FirstName = Victim.FirstName;
                    oldVictim.LastName = Victim.LastName;
                    oldVictim.Patronymic = Victim.Patronymic;
                    oldVictim.PhoneNumber = Victim.PhoneNumber;
                    oldVictim.City = Victim.City;
                    oldVictim.Street = Victim.Street;
                    oldVictim.Home = Victim.Home;
                    oldVictim.Flat = Victim.Flat;
                    oldVictim.DateOfBirth = Victim.DateOfBirth;
                    oldVictim.Corps = Victim.Corps;
                }

                db.SaveChanges();
                logger.Info("Данные потерпевшего изменены на " + Victim.FirstName +" " + Victim.LastName);
                ExitCommand.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                logger.Error(exc, "Ошибка в комманде EditCommand");
            }
        }

        private void LookCommand(object obj)
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
                DateOfBirth = Victim.DateOfBirth,
                Corps = Victim.Corps

                //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
                //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            };

            ExitCommand.Execute();
        }

    }
 }
