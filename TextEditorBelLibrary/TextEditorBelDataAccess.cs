using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace TextEditorBelLibrary
{
    public class TextEditorBelDataAccess
    {

        public static async Task<List<string>> LoadFilesNameAsync()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var filesName = await connection.QueryAsync<string>("SELECT Name FROM Files;");
                return filesName.ToList();
            }
        }

        public  static async Task<FileModel> LoadFileAsync(string name) 
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var file = await connection.QueryFirstAsync<FileModel>("SELECT Name, Data FROM Files WHERE Name=@Name LIMIT 1;", new { Name = name });               
                return file;
            }
        }

        public static async Task CreateNewFileAsync(FileModel file)
        {

            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                await connection.ExecuteAsync(@"INSERT INTO Files (Name, Data) VALUES (@Name, @Data)", file);
            }
        }

        public static async Task SaveExistFileAsync(FileModel file)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                await connection.ExecuteAsync(@"UPDATE Files SET Data = @Data  WHERE Name = @Name", new { Name = file.Name, Data = file.Data });
            }
        }

        private static string LoadConnectionString(string nameConn = "Default")
        {
            return ConfigurationManager.ConnectionStrings[nameConn].ConnectionString;
        }
    }
}
