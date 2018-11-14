using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace TextEditorBelLibrary
{
    public class TextEditorBelDataAccess
    {

        public static List<string> LoadFilesName()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var filesName = connection.Query<string>("SELECT Name FROM Files;");
                return filesName.ToList();
            }
        }

        public  static FileModel LoadFile(string name) 
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var file = connection.QueryFirst<FileModel>("SELECT Name, Data FROM Files WHERE Name=@Name LIMIT 1;", new { Name = name });               
                return file;
            }
        }

        public static void CreateNewFile(FileModel file)
        {

            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                 connection.Execute(@"INSERT INTO Files (Name, Data) VALUES (@Name, @Data)", file);
            }
        }

        public static void SaveExistFile(FileModel file)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute(@"UPDATE Files SET Data = @Data  WHERE Name = @Name", new { Name = file.Name, Data = file.Data });
            }
        }

        private static string LoadConnectionString(string nameConn = "Default")
        {
            return ConfigurationManager.ConnectionStrings[nameConn].ConnectionString;
        }
    }
}
