using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Split_Trays.Classes
{
    class SQLClass
    {
        private SqlConnection conn;
        private readonly string connectionString = "";

        public SQLClass(string ServerName, string DataBaseName, string UserName, string Secret)
        {
            connectionString =
           "Data Source=" + ServerName + ";" +
           "Initial Catalog=" + DataBaseName + ";" +
           "User id=" + UserName + ";" +
           "Password=" + Secret + ";";
        }

        private bool OpenConnection()
        {
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 53:
                        MessageBox.Show("Cannot connect to server. check your internet connection");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public int InsertScalar(string query)
        {
            int n = -1;
            if (this.OpenConnection() == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    n = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
            return n;
        }

        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
        }

        public void InsertNonQuery(string query)
        {
            if (this.OpenConnection() == true)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
        }

        internal DataTable SelectDB(string sql)
        {
            DataTable CmdResult = new DataTable();

            System.Data.Common.DbProviderFactory factory = System.Data.Common.DbProviderFactories.GetFactory("System.Data.SqlClient");
            System.Data.Common.DbConnection con = factory.CreateConnection();
            con.ConnectionString = connectionString;
            System.Data.Common.DbCommand cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            System.Data.Common.DbDataReader reader;

            try
            {
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                CmdResult.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return CmdResult;
        }

        public int SelectDemension(string query)
        {
            int Row = 0;
            int Col = 0;
            if (this.OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Col = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            Row = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        }
                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
            return Col * Row;
        }

        internal string SelectString(string v)
        {
            string Tray = "";
            if (this.OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(v, conn);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Tray = reader.IsDBNull(1) ? "" : reader.GetString(1).Trim();
                        }
                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
            return Tray;
        }

        internal string SelectLocation(string v)
        {
            string Loc = "";
            if (this.OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(v, conn);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Loc = reader.IsDBNull(0) ? "" : reader.GetString(0).Trim();
                            Loc += reader.IsDBNull(1) ? "" : reader.GetString(1).Trim().Length == 0 ? "" : ", " + reader.GetString(1).Trim();
                            Loc += reader.IsDBNull(2) ? "" : reader.GetString(2).Trim().Length == 0 ? "" : ", " + reader.GetString(2).Trim();
                        }
                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
            return Loc;
        }

        internal List<Row> SelectBaanData(string v)
        {
            List<Row> list = new List<Row>();
            try
            {
                OdbcConnection DbConnection = new OdbcConnection("DSN=Baan");
                DbConnection.Open();
                OdbcCommand DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandTimeout = 180;
                DbCommand.CommandText = v;
                OdbcDataReader DbReader = DbCommand.ExecuteReader();

                while (DbReader.Read() && DbReader.GetValue(0) != DBNull.Value)
                {
                    Row r = new Row();
                    r.Lot = DbReader.IsDBNull(0) ? "" : DbReader.GetString(0).Trim();
                    r.Item = DbReader.IsDBNull(1) ? "" : DbReader.GetString(1).Trim();
                    r.MPN = DbReader.IsDBNull(4) ? "" : DbReader.GetValue(4).ToString().Trim();
                    r.DateCode = DbReader.IsDBNull(5) ? "" : DbReader.GetValue(5).ToString().Trim();
                    r.MLot = DbReader.IsDBNull(6) ? "" : DbReader.GetValue(6).ToString().Trim();
                    r.Date = DbReader.GetDate(7);
                    r.Size = DbReader.IsDBNull(8) ? "" : DbReader.GetValue(8).ToString().Trim();
                    r.Qty = DbReader.IsDBNull(9) ? 0 : Convert.ToInt32(DbReader.GetValue(9));
                    r.KitDemand = DbReader.IsDBNull(10) ? "" : DbReader.GetValue(10).ToString().Trim();
                    r.Customer = DbReader.IsDBNull(11) ? "" : DbReader.GetValue(11).ToString().Trim();
                    r.Description = DbReader.IsDBNull(12) ? "" : DbReader.GetValue(12).ToString().Trim();
                    r.Month = r.DateCode != "" ? Convert.ToInt32(r.DateCode.Substring(0, 2)) : 0;
                    r.Year = r.DateCode != "" ? Convert.ToInt32(r.DateCode.Substring(2, 2)) : 0;
                    r.LineDemand = r.KitDemand;
                    r.LineDemand_Poly = Math.Round(Convert.ToDouble(r.KitDemand) * 1.1).ToString();

                    list.Add(r);
                }
                DbReader.Close();
                DbCommand.Dispose();
                DbConnection.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return list.OrderBy(x => x.Item).ThenBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => Convert.ToInt32(x.Qty)).ToList();
        }

        public int SelectInt(string query)
        {
            int n = 0;
            if (this.OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            n = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    this.CloseConnection();
                    MessageBox.Show(ex.Message);
                }
                this.CloseConnection();
            }
            return n;
        }

    }
}
