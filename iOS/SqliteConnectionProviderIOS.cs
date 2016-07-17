using System;
using RealmSqliteNetShowdown;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Xamarin.Forms.Dependency(typeof(ISqliteConnectionProvider))]
namespace RealmSqliteNetShowdown.iOS

{
    public class SqliteConnectionProviderIOS : ISqliteConnectionProvider
    {
        public SqliteConnectionProviderIOS() { }

        public SQLiteConnection Connection
        {
            get
            {
                var platform = new SQLitePlatformIOS();
                return new SQLiteConnection(platform, "showdown.sqlite");
            }
        }
    }

}

