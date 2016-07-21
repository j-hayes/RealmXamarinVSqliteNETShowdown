using System;
using System.IO;
using RealmSqliteNetShowdown;
using RealmSqliteNetShowdown.iOS;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteConnectionProviderIOS))]
namespace RealmSqliteNetShowdown.iOS

{
    public class SqliteConnectionProviderIOS : ISqliteConnectionProvider
    {
        public SqliteConnectionProviderIOS() { }

        public SQLiteConnection Connection
        {
            get
            {
                
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, "showdown.sqlite");
                var platform = new SQLitePlatformIOS();
                return new SQLiteConnection(platform,path );
            }
        }
    }

}

