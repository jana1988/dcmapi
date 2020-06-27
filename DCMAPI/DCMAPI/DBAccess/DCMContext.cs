using DCMAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DCMAPI.DBAccess
{
    public class DCMContext : DbContext
    {
        public DCMContext() : base("DCMAuthDb2Entities") { }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public IDataReader SQLReader { get; set; }

        #region GetConnection
        /// <summary>
        /// This static method returns a SqlConnection
        /// </summary>
        /// <remarks>Static method having database connection string which needs to be modified according to envoronment</remarks>
        /// <returns>SqlConnection</returns>
        private static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (System.Data.DataException ex)
            {
                throw ex;
            }
            catch (System.Security.SecurityException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return con;
        }
        #endregion
        private void GetDataReader(string SQLStatement, SqlParameter[] SPList)
        {
            // Make sure Code First has built the model before we open the connection
            this.Database.Initialize(force: false);
            // Create a SQL command to execute the stored procedure
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = SQLStatement;
            cmd.CommandTimeout = 180;
            //add the sql parameters
            if (SPList != null)
            {
                foreach (var sp in SPList)
                    cmd.Parameters.Add(sp);
            }
            //if the connection is closed then open
            if (this.Database.Connection.State != ConnectionState.Open)
                this.Database.Connection.Open();
            //set the SQL reader
            this.SQLReader = cmd.ExecuteReader();
        }

        private void CloseConnection()
        {
            if (this.Database != null)
            {
                if (this.Database.Connection.State == ConnectionState.Open)
                    this.Database.Connection.Close();
            }
        }
        public SingleResultSet<T1> GetSingleResultSet<T1>(string SQL, SqlParameter[] SPList = null)
        {
            try
            {
                //declare result
                SingleResultSet<T1> result = new SingleResultSet<T1>();

                //Get the data reader
                GetDataReader(SQL, SPList);

                try
                {
                    result.ResultSet = SQLReader.AutoMap<T1>();
                }
                finally
                {
                    CloseConnection();
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        ~DCMContext()
        {
            CloseConnection();
        }
    }
}