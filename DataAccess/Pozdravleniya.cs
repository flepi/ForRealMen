using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;


namespace DataAccess
{
    public class Pozdravleniya:Connection
    {
        //Добавление данных из sql
        MySqlDataReader leer;
        //Чтобы просчитать строки таблицы 
        DataTable table = new DataTable();
        //Добавляем sql-запрос для запуска
        MySqlCommand command = new MySqlCommand();


        //
        //Метод для отображения записи таблицы doctors 
        //
        public DataTable listPozdr()
        {
            command.Connection = ConnOpen();
            command.CommandText = "SELECT * FROM Congratulations";
            //Исправляем. чтобы можно использовать несколько строк
            command.CommandType = CommandType.Text;
            leer = command.ExecuteReader();
            table.Clear();
            //Таблица будет заполняться sql-запросом 
            table.Load(leer);
            ConnClose();
            return table;
        }

        //
        //Метод для добавление пользователя
        //

        public void AddPozdr(string fio, string position)
        {
            //MySqlCommand command = new MySqlCommand();
            command.Connection = ConnOpen();
            command.CommandText = "INSERT INTO `Congratulations` (`name` , `congratulations`) " +
            "VALUES(@fio, @position)";
            //Исправляем. чтобы можно использовать несколько строк
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@fio", fio);
            command.Parameters.AddWithValue("@position", position);

            command.ExecuteNonQuery();
            //Очищает параметры 
            command.Parameters.Clear();
        }

    }
}
