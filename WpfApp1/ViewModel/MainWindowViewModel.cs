using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Infrastructure;
using WpfApp1.Models.MpgSql;
using WpfApp1.Models.Sql;
using WpfApp1.Services;
using WpfApp1.ViewModel.Base;
using WpfApp1.Views.Main.Accessory;

namespace WpfApp1.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        IDataRepository<Person> _personRepostitory;
        IDataRepository<Products> _productRepostitory;

        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Домашняя работа 17";

        #endregion

        #region ListPerson : ObservableCollection<Person> - Список с клиентами

        /// <summary>Список с клиентами</summary>
        private ObservableCollection<Person> _listPerson;

        /// <summary>Список с клиентами</summary>
        public ObservableCollection<Person> ListPerson { get => _listPerson; set => Set(ref _listPerson, value); }

        #endregion

        #region ListPersonSelectedItem : Person - Выделенный клиент

        /// <summary>Выделенный клиент</summary>
        private Person _listPersonSelectedItem;

        /// <summary>Выделенный клиент</summary>
        public Person ListPersonSelectedItem { get => _listPersonSelectedItem; set => Set(ref _listPersonSelectedItem, value); }

        #endregion

        #region ListProductSelectedItem : Products - Выделенный продукт

        /// <summary>Выделенный продукт</summary>
        private Products _listProductSelectedItem;

        /// <summary>Выделенный продукт</summary>
        public Products ListProductSelectedItem { get => _listProductSelectedItem; set => Set(ref _listProductSelectedItem, value); }

        #endregion

        #endregion

        #region Команды

        #region Открыть окно для проверки соединения с базами данных.

        /// <summary>Открыть окно для проверки соеднинения с базой</summary>
        public ICommand OpenDBConnectionTestWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для проверки соеднинения с базой</summary>
        private bool CanOpenDBConnectionTestWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для проверки соеднинения с базой</summary>
        private void OnOpenDBConnectionTestWindowCommanExecuted(object p)
        {
            DBConnectionTest DBConTest = new DBConnectionTest();
            DBConTest.Show();
        }

        #endregion

        #region Открыть окно для добавляения или редактирования пользователя

        /// <summary>Открыть окно для добавляения или редактирования пользователя</summary>
        public ICommand OpenPersonWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для добавляения или редактирования пользователя</summary>
        private bool CanOpenPersonWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для добавляения или редактирования пользователя</summary>
        private void OnOpenPersonWindowCommanExecuted(object p)
        {
            PersonWindow personeWindow;
            Person person;

            if (Convert.ToInt32(p) != 0 && ListPersonSelectedItem == null) return;

            // Добавляем новую запись
            if (Convert.ToInt32(p) == 0)
            {
                personeWindow = new PersonWindow();
                if (personeWindow.ShowDialog() == true)
                {
                    person = new Person();
                    person.secondName = personeWindow.Surname;
                    person.firstName = personeWindow.Name;
                    person.lastName = personeWindow.Patronymic;
                    person.phoneNumber = personeWindow.Tel;
                    person.email = personeWindow.Email;

                    _personRepostitory.Add(person);
                    MessageBox.Show("Успешное добавление", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            // Обновляем запись
            else
            {
                personeWindow = new PersonWindow();
                personeWindow.Id = ListPersonSelectedItem.id;
                personeWindow.Surname = ListPersonSelectedItem.secondName;
                personeWindow.Name = ListPersonSelectedItem.firstName;
                personeWindow.Patronymic = ListPersonSelectedItem.lastName;
                personeWindow.Tel = ListPersonSelectedItem.phoneNumber;
                personeWindow.Email = ListPersonSelectedItem.email;

                if (personeWindow.ShowDialog() == true)
                {
                    person = new Person();
                    person.id = personeWindow.Id;
                    person.secondName = personeWindow.Surname;
                    person.firstName = personeWindow.Name;
                    person.lastName = personeWindow.Patronymic;
                    person.phoneNumber = personeWindow.Tel;
                    person.email = personeWindow.Email;

                    _personRepostitory.Update(person);
                    MessageBox.Show("Успешное изменение", "Изменения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            ListPerson.Clear();
            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);

        }

        #endregion

        #region Удаление пользователя

        /// <summary>Удаление пользователя</summary>
        public ICommand DeletePersonCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление пользователя</summary>
        private bool CanDeletePersonCommanExecute(object p) => ListPersonSelectedItem != null;

        /// <summary>Логика выполнения - Удаление пользователя</summary>
        private void OnDeletePersonCommanExecuted(object p)
        {
            _personRepostitory.Remove(ListPersonSelectedItem.id);
            ListPerson.Remove(ListPersonSelectedItem);
        }

        #endregion

        #region Открыть окно для добавляения или редактирования продукта

        /// <summary>Открыть окно для добавляения или редактирования продукта</summary>
        public ICommand OpenProductWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для добавляения или редактирования продукта</summary>
        private bool CanOpenProductWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для добавляения или редактирования продукта</summary>
        private void OnOpenProductWindowCommanExecuted(object p)
        {
            // Если не выделили клиента и нажали на кнопку добавить нового.
            if (ListPersonSelectedItem == null) return;

            // Если не выделили продукт и нажали на кнопку редактировать.
            if (Convert.ToInt32(p) != 0 && ListProductSelectedItem == null) return;

            ProductWindow productWindow;
            Products product;

            // Создаем новый продукт
            if (Convert.ToInt32(p) == 0)
            {
                productWindow = new ProductWindow();
                productWindow.Email = ListPersonSelectedItem.email;

                if (productWindow.ShowDialog() == true)
                {
                    product = new Products();
                    product.email = productWindow.Email;
                    product.productcode = productWindow.ProductCode;
                    product.productname = productWindow.ProductName;

                    _productRepostitory.Add(product);
                    MessageBox.Show("Успешное добавление", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            // Делаем изменения выделенного продукта
            else
            {
                productWindow = new ProductWindow();
                productWindow.ID = ListProductSelectedItem.id;
                productWindow.Email = ListProductSelectedItem.email;
                productWindow.ProductCode = ListProductSelectedItem.productcode;
                productWindow.ProductName = ListProductSelectedItem.productname;

                if (productWindow.ShowDialog() == true)
                {
                    product = new Products();
                    product.id = productWindow.ID;
                    product.email = productWindow.Email;
                    product.productcode = productWindow.ProductCode;
                    product.productname = productWindow.ProductName;

                    _productRepostitory.Update(product);
                    MessageBox.Show("Успешное изменение", "Изменения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            ListPerson.Clear();
            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);

        }

        #endregion

        #region Удаление продукта

        /// <summary>Удаление продукта</summary>
        public ICommand DeleteProductCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление продукта</summary>
        private bool CanDeleteProductCommanExecute(object p) => ListProductSelectedItem != null;

        /// <summary>Логика выполнения - Удаление продукта</summary>
        private void OnDeleteProductCommanExecuted(object p)
        {
            _productRepostitory.Remove(ListProductSelectedItem.id);
            ListPersonSelectedItem.Products = _productRepostitory.GetAllData.Where(x => x.email == ListPersonSelectedItem.email);

            // Рефрешим выделенную запись.
            var s = ListPersonSelectedItem;
            ListPersonSelectedItem = null;
            ListPersonSelectedItem = s;
        }

        #endregion

        #region Удаление всего

        /// <summary>Удаление пользователя</summary>
        public ICommand DeleteAllCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление пользователя</summary>
        private bool CanDeleteAllCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Удаление пользователя</summary>
        private void OnDeleteAllCommandExecuted(object p)
        {
            _productRepostitory.RemoveAll();
            _personRepostitory.RemoveAll();
            ListPerson.Clear();
        }

        #endregion

        #endregion

        public MainWindowViewModel(IDataRepository<Person> personRepostitory, IDataRepository<Products> productRepostitory)
        {
            _personRepostitory = personRepostitory;
            _productRepostitory = productRepostitory;

            #region Команды

            OpenDBConnectionTestWindowCommand = new LambdaCommand(OnOpenDBConnectionTestWindowCommanExecuted, CanOpenDBConnectionTestWindowCommanExecute);
            OpenPersonWindowCommand = new LambdaCommand(OnOpenPersonWindowCommanExecuted, CanOpenPersonWindowCommanExecute);
            DeletePersonCommand = new LambdaCommand(OnDeletePersonCommanExecuted, CanDeletePersonCommanExecute);
            DeleteAllCommand = new LambdaCommand(OnDeleteAllCommandExecuted, CanDeleteAllCommandExecute);
            OpenProductWindowCommand = new LambdaCommand(OnOpenProductWindowCommanExecuted, CanOpenProductWindowCommanExecute);
            DeleteProductCommand = new LambdaCommand(OnDeleteProductCommanExecuted, CanDeleteProductCommanExecute);

            #endregion

            // Заполняем список.
            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);
        }
    }
}
