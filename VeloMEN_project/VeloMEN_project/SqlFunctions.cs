using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMEN_project
{
    class SqlFunctions
    {
        public static SqlConnection connect = null;
        public static void openConnection(string connectionString)
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
        }

        public static void closeConnection()
        {
            connect.Close();
        }
        public static string get(string request)
        {
            string sqlRequest = request;
            string Answer = "";
            SqlCommand cmd1 = new SqlCommand(sqlRequest, SqlFunctions.connect);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            while (rd1.Read())
            {
                Answer = (rd1[0].ToString());
            }
            rd1.Close();
            return Answer;
        }
        public static DataTable GetAllInventoryAsDataTable(string request)
        {
            DataTable sqlAnswer = new DataTable();
            string sqlRequest = request;
            using (SqlCommand cmd = new SqlCommand(sqlRequest, SqlFunctions.connect))
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                sqlAnswer.Load(dataReader);
                dataReader.Close();
            }
            return sqlAnswer;
        }

        public static void DeliteVelo(string idvelo)
        {
            string sqlRequest = $"use [VeloMen] Delete From [dbo].[Велотранспорт] Where ID_Велотранспорта = {idvelo}";
            SqlCommand cmd = new SqlCommand(sqlRequest, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlRequest, connect);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

        }

        public static void AddVelo(string mark, string model, string color, string stat, string sz, string sa, string so, string foto, string code)
        {
            string sqlRequest = "use [VeloMen] insert into [dbo].[Велотранспорт]([Марка],[Модель],[Цвет],[Статус],[Стоимость_Залога],[Стоимость_аренды_в_час],[Оценочная_Стоимость],[Фото],[Код_в_велопрокате]) values('" + mark + "', '" + model + "', '" + color + "', '" + stat + "', '" + sz + "', '" + sa + "'," +
                "'" + so + "', '" + foto + "', '" + code + "')";
            SqlCommand cmd = new SqlCommand(sqlRequest, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlRequest, connect);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

        }
        public static void ApdeitVelo(string mark, string model, string color, string stat, string sz, string sa, string so, string foto, string idvelo)
        {
            string sqlRequest2 = "use [VeloMen] update [dbo].[Велотранспорт] set  Марка='" + mark + "',Модель='" + model + "',Цвет='" + color + "'," +
            "Статус='" + stat + "',Стоимость_Залога='" + sz + "',Стоимость_аренды_в_час='" + sa + "',Оценочная_Стоимость='" + so + "',Фото='" + foto + "'" +
            "where ID_Велотранспорта='" + idvelo + "'";


            SqlCommand cmd2 = new SqlCommand(sqlRequest2, connect);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sqlRequest2, connect);
            DataSet dataSet2 = new DataSet();

            adapter2.Fill(dataSet2);
        }
        public static void AddClient(string fam, string nam, string otc, string tel, string serp, string nomp, string adr)
        {
            string sqlRequest = "use [VeloMen] insert into [dbo].[Клиенты]([Фамилия],[Имя],[Отчество],[Номер_Телефона],[Серия_Паспорта],[Номер_Пасорта],[Адрес_Регистрации]) values('" + fam + "', '" + nam + "', '" + otc + "', '" + tel + "', '" + serp + "', '" + nomp + "'," +
                "'" + adr +  "')";
            SqlCommand cmd = new SqlCommand(sqlRequest, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlRequest, connect);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

        }
        public static void ApdeitClient(string idk, string fam, string nam, string otc, string tel, string serp, string nomp, string adr)
        {
            string sqlRequest2 = "use [VeloMen] update [dbo].[Клиенты] set  Фамилия='" + fam + "',Имя='" + nam + "',Отчество='" + otc + "'," +
            "Номер_Телефона='" + tel + "',Серия_Паспорта='" + serp + "',Номер_Пасорта='" + nomp + "',Адрес_Регистрации='" + adr + "'" +
            "where ID_Клиента ='" + idk + "'";


            SqlCommand cmd2 = new SqlCommand(sqlRequest2, connect);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sqlRequest2, connect);
            DataSet dataSet2 = new DataSet();

            adapter2.Fill(dataSet2);
        }

        public static void DeliteClient(string idk)
        {
            string sqlRequest = $"use [VeloMen] Delete From [dbo].[Клиенты] Where ID_Клиента = {idk}";
            SqlCommand cmd = new SqlCommand(sqlRequest, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlRequest, connect);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

        }





        public static List<string> getArray(string request)
        {
            //DataTable sqlAnswer = new DataTable();
            string sqlRequest = request;
            List<string> Answer = new List<string> { };
            SqlCommand cmd1 = new SqlCommand(sqlRequest, SqlFunctions.connect);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            while (rd1.Read())
            {

                Answer.Add(rd1[0].ToString());
            }
            rd1.Close();
            return Answer;
        }

    }
}
