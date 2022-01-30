using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPFPasswordGenerator.BaseModels;
namespace WPFPasswordGenerator.ViewModels
{
    class PasswordGenerator : INotifyPropertyChangedBase
    {
        public ObservableCollection<PasswordItem> Passwords { get; set; }
        public PasswordGenerator()
        {
            Password = "";
            PasswordLength = 0;
            Passwords = new ObservableCollection<PasswordItem>();
        }
        private int CurrentId { get; set; }
        private int _passwordLength;
        public int PasswordLength
        {
            get => _passwordLength;
            set
            {
                _passwordLength = value;
                NotifyPropertyChanged(nameof(PasswordLength));
            }
        }
        private string _password { get; set; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }
        private bool _englishSmallLetters;
        public bool EnglishSmallLetters
        {
            get => _englishSmallLetters;
            set
            {
                _englishSmallLetters = value;
                NotifyPropertyChanged(nameof(EnglishSmallLetters));
            }
        }
        private bool _englishBigLetters;
        public bool EnglishBigLetters
        {
            get => _englishBigLetters;
            set
            {
                _englishBigLetters = value;
                NotifyPropertyChanged(nameof(EnglishBigLetters));
            }
        }
        private bool _russianSmallLetters;
        public bool RussianSmallLetters
        {
            get => _russianSmallLetters;
            set
            {
                _russianSmallLetters = value;
                NotifyPropertyChanged(nameof(RussianSmallLetters));
            }
        }
        private bool _russianBigLetters;
        public bool RussianBigLetters
        {
            get => _russianBigLetters;
            set
            {
                _russianBigLetters = value;
                NotifyPropertyChanged(nameof(RussianBigLetters));
            }
        }
        private bool _specialSymbols;
        public bool SpecialSymbols
        {
            get => _specialSymbols;
            set
            {
                _specialSymbols = value;
                NotifyPropertyChanged(nameof(SpecialSymbols));
            }
        }
        private bool _numbers;
        public bool Numbers
        {
            get => _numbers;
            set
            {
                _numbers = value;
                NotifyPropertyChanged(nameof(Numbers));
            }
        }
        public bool AllOptions
        {
            get => EnglishSmallLetters && EnglishBigLetters && RussianSmallLetters && RussianBigLetters && Numbers && SpecialSymbols;
            set
            {
                if (EnglishSmallLetters && EnglishBigLetters && RussianSmallLetters && RussianBigLetters && Numbers && SpecialSymbols)
                {
                    EnglishSmallLetters = false;
                    EnglishBigLetters = false;
                    RussianSmallLetters = false;
                    RussianBigLetters = false;
                    Numbers = false;
                    SpecialSymbols = false;
                }
                else
                {
                    EnglishSmallLetters = true;
                    EnglishBigLetters = true;
                    RussianSmallLetters = true;
                    RussianBigLetters = true;
                    Numbers = true;
                    SpecialSymbols = true;
                }
                NotifyPropertyChanged(nameof(AllOptions));
            }
        }
        public void GeneratePassword()
        {
            var Password = new StringBuilder();
            var random = new Random();
            var RussianLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            var CapitalRussianLetters = RussianLetters.ToUpper();
            var SpecialSymbol = "!@#$%^&*()[]|";
            for (int i = 0; i < PasswordLength;)
            {
                switch (random.Next(0, 6))
                {
                    case 0:
                        if (EnglishSmallLetters)
                        {
                            Password.Append(Convert.ToChar(random.Next(97, 123)));
                            i++;
                        }
                        break;
                    case 1:
                        if (EnglishBigLetters)
                        {
                            Password.Append(Convert.ToChar(random.Next(65, 91)));
                            i++;
                        }
                        break;
                    case 2:
                        if (RussianSmallLetters)
                        {
                            Password.Append(RussianLetters[random.Next(RussianLetters.Length)]);
                            i++;
                        }
                        break;
                    case 3:
                        if (RussianBigLetters)
                        {
                            Password.Append(CapitalRussianLetters[random.Next(CapitalRussianLetters.Length)]);
                            i++;
                        }
                        break;
                    case 4:
                        if (Numbers)
                        {
                            Password.Append(random.Next(0, 10).ToString());
                            i++;
                        }
                        break;
                    case 5:
                        if (SpecialSymbols)
                        {
                            Password.Append(SpecialSymbol[random.Next(SpecialSymbol.Length)]);
                            i++;
                        }
                        break;
                }
            }
            this.Password = Password.ToString();
            Passwords.Add(new PasswordItem() { Id = ++CurrentId, PasswordItemValue = this.Password });
            NotifyPropertyChanged(nameof(this.Password));
            NotifyPropertyChanged(nameof(Passwords));
        }
        public void CopyPassword()
        {
            Clipboard.SetDataObject(Password);
        }
        public ICommand GeneratePasswordCommand => new RelayCommand(
        x =>
        {
            GeneratePassword();
        },
        x => (EnglishSmallLetters || EnglishBigLetters || RussianSmallLetters || RussianBigLetters || SpecialSymbols || Numbers) && PasswordLength > 0);
        public ICommand CopyPasswordCommand => new RelayCommand(
        x =>
        {
            CopyPassword();
        },
        x => Password.Length > 0);
        public ICommand DeletePasswordCommand => new RelayCommand(
        x =>
        {
            var item = x as PasswordItem;
            Passwords.Remove(item);
            NotifyPropertyChanged(nameof(Passwords));
        },
        x => true);
    }
}