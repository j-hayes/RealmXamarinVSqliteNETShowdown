using System;
using System.IO;
using RealmSqliteNetShowdown;
using RealmSqliteNetShowdown.Droid;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteConnectionProviderDroid))]
namespace RealmSqliteNetShowdown.Droid
{ 

    public class SqliteConnectionProviderDroid : ISqliteConnectionProvider
    {

        public SqliteConnectionProviderDroid() { }


        public SQLiteConnection Connection
        {
            get
            {
                var platform = new SQLitePlatformAndroid();
      
              

                var sqliteFilename = "showdown.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
                // Create the connection
                var conn = new SQLiteConnection(platform, path);
                // Return the database connection
                return conn;
            }
        }
    }
}

