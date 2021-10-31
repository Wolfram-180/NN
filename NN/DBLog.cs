using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient; 

namespace MnFrm
{
    public class DBLog
    {
        string ConnectString = "";

        public DBLog()
        {
            ConnectString = GetConnectionString();
        }

        public void Log(string net, string symb, string mess)
        {
            if (mess.Length > 80)
            {
                mess = string.Format("{0}{1}{2}", mess.Substring(0, 14), "...", mess.Substring(mess.Length - 65));
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectString))
                {
                    MySqlCommand cmd;

                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO nlc_log (net, symbol, mess) VALUES (@net, @symbol, @mess)";
                    cmd.Parameters.AddWithValue("@net", net);
                    cmd.Parameters.AddWithValue("@symbol", symb);
                    cmd.Parameters.AddWithValue("@mess", mess);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private string GetConnectionString()
        {
            return "SERVER=localhost;DATABASE=nalice;UID=root;PASSWORD=cfitymrf;";
        }


    }
}
