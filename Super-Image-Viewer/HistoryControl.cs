using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Super_Image_Viewer.Models;
using System.Data;

namespace Super_Image_Viewer
{
    public class HistoryControl
    {
        SqlConnection connection = null;
        string connectionString = "";
        public HistoryControl()
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings css in settings)
                {
                    if (css.Name == "Default")
                    {
                        connectionString = css.ConnectionString;
                    }
                }
            }
            connection = new SqlConnection(connectionString);
        }
        public async Task AddHistory(string file_path)
        {
            string file_name = "";
            int splitted_length = file_path.Split('\\').Length;
            file_name = file_path.Split('\\')[splitted_length-1];
            await connection.OpenAsync();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO HISTORY([ImageDate],[ImageName],[ImagePath])values" + 
            @"(@p1, @p2, @p3)";
            command.Parameters.Add("@p1", System.Data.SqlDbType.DateTime);
            command.Parameters["@p1"].Value = DateTime.Now;

            command.Parameters.Add("@p2", System.Data.SqlDbType.NVarChar);
            command.Parameters["@p2"].Value = file_name;

            command.Parameters.Add("@p3", System.Data.SqlDbType.NVarChar);
            command.Parameters["@p3"].Value = file_path;

            await command.ExecuteNonQueryAsync();
            connection.Close();

        }
        public async Task Clean()
        {
            await connection.OpenAsync();
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"delete from History";
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch(Exception err)
            {
                connection.Close();
                throw new Exception($"Internal error\n {err.Message}");
            }
        }
        public async Task<List<FileHistoryModel>> GetFilesHistory()
        {
            await connection.OpenAsync();
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * from History";
                DataTable table = new DataTable();
                List<FileHistoryModel> fhm = new List<FileHistoryModel>();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    int line = 0;

                    do
                    {
                        while (await reader.ReadAsync())
                        {
                            if (line == 0)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                    table.Columns.Add(reader.GetName(i));
                            }

                            line++;
                            DataRow row = table.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = await reader.GetFieldValueAsync<object>(i);
                            }
                            FileHistoryModel fileHistoryModel = new FileHistoryModel() { Id = int.Parse(row[0].ToString()), FileName = row[1].ToString(), FilePath = row[2].ToString(), dateTime = DateTime.Parse(row[3].ToString()) };
                            fhm.Add(fileHistoryModel);
                        }
                    } while (reader.NextResult());
                }
                connection.Close();
                return fhm;
            }
            catch(Exception err)
            {
                connection.Close();
                throw new Exception($"Internal error\n {err.Message}");
            }
        }
    }
}