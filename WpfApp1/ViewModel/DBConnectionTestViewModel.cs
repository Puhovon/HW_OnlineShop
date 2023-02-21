using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Infrastructure;
using WpfApp1.Models.MpgSql;
using WpfApp1.Models.Sql;
using WpfApp1.Services;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    internal class DBConnectionTestViewModel : ViewModelBase
    {
        IDataRepository<Person> _sqlRepository;
        IDataRepository<Products> _accessRepository;

        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Проверка соединения с базами данных";

        #endregion

        #region SQLDataBase : string - Строка подключения к БД SQL

        /// <summary>Строка подключения к БД SQL</summary>
        private string _sqlDataBase;

        /// <summary>Строка подключения к БД SQL</summary>
        public string SQLDataBase { get => _sqlDataBase; set => Set(ref _sqlDataBase, value); }

        #endregion

        #region SQLStatus : string - Статус подключения к базе данных SQL

        /// <summary>Статус подключения к базе данных SQL</summary>
        private string _sqlStatus;

        /// <summary>Статус подключения к базе данных SQL</summary>
        public string SQLStatus { get => _sqlStatus; set => Set(ref _sqlStatus, value); }

        #endregion

        #region AccessDataBase : string - Строка подключения к БД Access

        /// <summary>Строка подключения к БД Access</summary>
        private string _accessDataBase;

        /// <summary>Строка подключения к БД Access</summary>
        public string AccessDataBase { get => _accessDataBase; set => Set(ref _accessDataBase, value); }

        #endregion

        #region AccessStatus : string - Статус подключения к базе данных Access

        /// <summary>Статус подключения к базе данных Access</summary>
        private string _accessStatus;

        /// <summary>Статус подключения к базе данных Access</summary>
        public string AccessStatus { get => _accessStatus; set => Set(ref _accessStatus, value); }

        #endregion

        #endregion

        #region Команды

        #region Проверить соединение с базой данных

        /// <summary>Проверить соединение с базой данных</summary>
        public ICommand DBConnectionTestCommand { get; }

        /// <summary>Проверка возможности выполнения - Проверить соединение с базой данных</summary>
        private bool CanDBConnectionTestCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Проверить соединение с базой данных</summary>
        private void OnDBConnectionTestCommanExecuted(object p)
        {
            ThreadPool.QueueUserWorkItem(
            o =>
            {
                // Здесь что-то делаем
                switch (p)
                {
                    case "SQL":
                        SQLStatus = _sqlRepository.ConnectionsTest(SQLDataBase);
                        break;
                    case "Access":
                        AccessStatus = _accessRepository.ConnectionsTest(AccessDataBase);
                        break;
                }
            });
        }

        #endregion

        #endregion

        public DBConnectionTestViewModel(IDataRepository<Person> sqlRepository, IDataRepository<Products> accessRepository)
        {
            _sqlRepository = sqlRepository;
            _accessRepository = accessRepository;

            #region Команды

            DBConnectionTestCommand = new LambdaCommand(OnDBConnectionTestCommanExecuted, CanDBConnectionTestCommanExecute);

            #endregion

            // Выводим строки подключения во View
            _sqlDataBase = _sqlRepository.GetConnectionString();
            _accessDataBase = _accessRepository.GetConnectionString();

            _sqlStatus = "Статус проверки подключения к SQL Server DB";
            _accessStatus = "Статус проверки подключения к Access";
        }
    }
}
