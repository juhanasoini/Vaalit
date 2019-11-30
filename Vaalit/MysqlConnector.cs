using MySql.Data.MySqlClient;
using System;

namespace Vaalit
{
    class MysqlConn
    {
        public MySqlConnection conn;

        public MysqlConn()
        {
            conn = new MySqlConnection("server=jj820qt5lpu6krut.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;user=qg988zods0upey3t;password=qse6pw2oqoigf76m;database=jz2karrwun9i9hek");
        }

        public void Connect()
        {
            if (conn.State.ToString() == "Open")
                conn.Close();

            conn.Open();

        }
        public void Close()
        {
            conn.Close();
        }

        public void CreateTable()
        {
            try
            {
                string query = "CREATE TABLE IF NOT EXISTS `c_ehdokkaat` ("
                    + "`id` int(11) NOT NULL AUTO_INCREMENT,"
                    + "`etunimi` varchar(255) COLLATE utf8_unicode_ci NOT NULL,"
                    + "`sukunimi` varchar(255) COLLATE utf8_unicode_ci NOT NULL,"
                    + "`puolue` varchar(50) COLLATE utf8_unicode_ci NOT NULL,"
                    + "`aanimaara` int(11) DEFAULT NULL,"
                    + "`vertailuluku` double DEFAULT NULL,"
                    + "`slug` varchar(255) COLLATE utf8_unicode_ci NOT NULL,"
                    + "PRIMARY KEY(`id`),"
                    + "UNIQUE KEY `id` (`id`)"
                    + ") ENGINE = InnoDB DEFAULT CHARSET = utf8 COLLATE = utf8_unicode_ci;";

                MySqlCommand cmd = Query(query);
                var reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public MySqlCommand Query(string query)
        {
            try
            {
                Connect();
                MySqlCommand command = new MySqlCommand(query, conn);
                return command;

                Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
