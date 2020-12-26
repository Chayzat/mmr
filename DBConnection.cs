using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mmr
{
    class DBConnection
    {
        static string connectionString = @"Database = MurmanRyb; Data Source = localhost; UserID = root; Password = qwerty "; //строка подключения
        static MySqlConnection msConnect; //объект для установки соединения с БД
        static MySqlCommand msCommand; //объект для выполнения запросов
        static public MySqlDataAdapter msDataAdapter;
        //установка соединения с БД
        static public bool Connect()
        {
            try
            {
                //создание объекта соединения с заданной строкой подключения
                msConnect = new MySqlConnection(connectionString);
                msConnect.Open(); //открытие подключение
                                  //создание объекта-запрос
                msCommand = new MySqlCommand();
                msCommand.Connection = msConnect;
                msDataAdapter = new MySqlDataAdapter(msCommand);
                return true; //результат «истина»
            }
            catch (Exception ex) //при возникновении ошибки
            {
                //вывод сообщения
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Ошибка!");
                return false; //результат «ложь»
            }
        }
        //отключение соединения с БД
        static public void Close()
        {
            msConnect.Close();
        }
        static public string User; //логин авторизованного пользователя
        static public string Role; //роль авторизованного пользователя
                                   //авторизация пользователя, принимает параметры с формы авторизации
        static public DataTable dtUsers = new DataTable();
        //метод получения списка пользователей
        //selectedRole – значение роли для фильтрации
        //по умолчанию = null
        static public void GetUserList(string selectedRole = null)
        {
            //если роль не выбрана
            if (selectedRole == null)
            {
                //формируем запрос на выборку всех записей
                msCommand.CommandText = "SELECT * FROM Users";
            }
            else
            {
                //иначе, формируем запрос с фильтрацией
                msCommand.CommandText = "SELECT * FROM Users WHERE Users.role='" +
                selectedRole + "'";
            }
                dtUsers.Clear(); //очистка набора данных
                msDataAdapter.Fill(dtUsers); //заполнение набора данных
            }


            static public void Authorization(string login, string password)
        {
            try
            {
                //формируем запрос: выбрать поле из таблицы значения,
                //где логин и пароль равны введенным пользователем значениям
                string sql = "SELECT Role FROM Users WHERE Login = '" + login
                 + "' AND Password = '" + password + "' ;";
                //создаем объект-запрос
                msCommand = new MySqlCommand(sql, msConnect);
                //фиксируем результат запроса
                Object result = msCommand.ExecuteScalar();
                //если в результате выполнения запроса получено непустое значение
                if (result != null)
                {
                    //заполняем информацию об авторизованном пользователе
                    Role = result.ToString();
                    User = login;
                }
                else
                {
                    //иначе тип пользователя - неавторизованный
                    Role = null;
                }
            }
            catch (Exception ex) //при возникновении ошибки
            {
                Role = User = null; //обнуляем значения полей
                MessageBox.Show(ex.ToString(), "Ошибка!");
            }
        }
    }

}
