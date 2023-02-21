using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Views.Main.Accessory
{
    /// <summary>
    /// Логика взаимодействия для PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        #region Поля

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
            nameof(Id),
            typeof(int),
            typeof(PersonWindow),
            new PropertyMetadata(default(int)));

        [Description("Номер")]
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }

        public static readonly DependencyProperty SurnameProperty = DependencyProperty.Register(
            nameof(Surname),
            typeof(string),
            typeof(PersonWindow),
            new PropertyMetadata(default(string)));

        [Description("Фамилия")]
        public string Surname { get => (string)GetValue(SurnameProperty); set => SetValue(SurnameProperty, value); }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            nameof(Name),
            typeof(string),
            typeof(PersonWindow),
            new PropertyMetadata(default(string)));

        [Description("Имя")]
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }

        public static readonly DependencyProperty PatronymicProperty = DependencyProperty.Register(
            nameof(Patronymic),
            typeof(string),
            typeof(PersonWindow),
            new PropertyMetadata(default(string)));

        [Description("Отчество")]
        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }

        public static readonly DependencyProperty TelProperty = DependencyProperty.Register(
            nameof(Tel),
            typeof(string),
            typeof(PersonWindow),
            new PropertyMetadata(default(string)));

        [Description("Телефон")]
        public string Tel { get => (string)GetValue(TelProperty); set => SetValue(TelProperty, value); }

        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register(
            nameof(Email),
            typeof(string),
            typeof(PersonWindow),
            new PropertyMetadata(default(string)));

        [Description("email")]
        public string Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }

        #endregion

        public PersonWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Name)
               || String.IsNullOrEmpty(Surname)
               || String.IsNullOrEmpty(Patronymic)
               || String.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Можно не заполнять только телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
