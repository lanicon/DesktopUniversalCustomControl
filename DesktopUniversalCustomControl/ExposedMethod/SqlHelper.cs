using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUniversalCustomControl.ExposedMethod
{
    class SqlHelper
    {
        private static string connectStr = "connectString";

        /// <summary>
        /// 返回行数（Update、Insert、Delete）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using(MySqlConnection conn = new MySqlConnection(connectStr))
            {
                conn.Open();
                using(MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 返回单一值(获取操作的数据)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using(MySqlConnection conn = new MySqlConnection(connectStr))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 返回DataTable(Select) <返回结果比较少的>
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            using(MySqlConnection conn = new MySqlConnection(connectStr))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        /// <summary>
        /// 基于序号的查询(Select)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<string> ExecuteDataReader(string sql, string columnName, params MySqlParameter[] parameters)
        {
            using(MySqlConnection conn = new MySqlConnection(connectStr))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    List<string> list = new List<string>(); 
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(columnName));
                        }
                        return list;
                    }
                }
            }
        }
    }
}
