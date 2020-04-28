﻿using Course.Commands;
using Course.Context;
using Course.Model;
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
    class EmployeeViewModel 
    {
        ApplicationContext db;
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand AcceptCommand { get; private set; }
        public Employee Employee { get; private set; }

        public EmployeeViewModel(Employee employee, ApplicationContext db)
        {
            this.db = db;
            if (employee == null)
            {
                Employee = new Employee();
                AcceptCommand = new RelayCommand(AddCommand);
            }
            else
            {
                //Employee = employee;
                Employee = new Employee()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Patronymic = employee.Patronymic,
                    Rank = employee.Rank,
                    Position = employee.Position
                };

                AcceptCommand = new RelayCommand(EditCommand);
            }
        }

        private void AddCommand(object obj)
        {
            Employee employee = new Employee()
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Patronymic = Employee.Patronymic,
                Rank = Employee.Rank,
                Position = Employee.Position

                //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
                //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            };
            try
            {
                db.Employees.Add(employee);
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
            //Employee employee = new Employee()
            //{
            //    EmployeeId = Employee.EmployeeId,
            //    FirstName = Employee.FirstName,
            //    LastName = Employee.LastName,
            //    Patronymic = Employee.Patronymic,
            //    Rank = Employee.Rank,
            //    Position = Employee.Position

            //    //Patronymic = (Author.Patronymic is null) ? "" : Author.Patronymic,
            //    //Nickname = (Author.Nickname is null) ? "" : Author.Nickname
            //};

            try
            {
                var oldEmployee = db.Employees.Where(x => x.EmployeeId == Employee.EmployeeId).SingleOrDefault();
                if (oldEmployee != null)
                {
                    //oldEmployee.EmployeeId = Employee.EmployeeId;
                    oldEmployee.FirstName = Employee.FirstName;
                    oldEmployee.LastName = Employee.LastName;
                    oldEmployee.Patronymic = Employee.Patronymic;
                    oldEmployee.Rank = Employee.Rank;
                    oldEmployee.Position = Employee.Position;
                }
                //oldEmployee = Employee;


                //db.Employees.Remove(db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).First());
                //db.Employees.Add(employee);
                db.SaveChanges();
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
