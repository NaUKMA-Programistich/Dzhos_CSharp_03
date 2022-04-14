using Dzhos_CSharp_03.Models;
using Dzhos_CSharp_03.Utils;
using Dzhos_CSharp_03.Utils.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Dzhos_CSharp_03.ViewModels
{
    class BirthDayViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;

        private string _information = "";
        private bool _enableButton = true;

        private RelayCommand<object> _proceedCommand;
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }
        public string Information
        {
            get { return _information; }
            set
            {
                _information = value;
                OnPropertyChanged("Information");
            }
        }
        public bool ProceedEnabled
        {
            get { return _enableButton; }
            set
            {
                _enableButton = value;
                OnPropertyChanged("ProceedEnabled");
            }
        }
        public RelayCommand<object> ProceedCommand
        {
            get { return _proceedCommand ??= new RelayCommand<object>(InfomationProceedCommand, _ => CanExecute()); }
        }
        #endregion

        private async void InfomationProceedCommand(object obj)
        {
            Information = "";
            try
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(500);
                    Person = new Person(Name, Surname, Email, Date);
                    Information = Person.ToString();
                    Thread.Sleep(500);
                });
            }
            catch (MyException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Email)
                && Date != DateTime.MinValue;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
