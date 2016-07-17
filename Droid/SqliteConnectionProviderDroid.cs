using System;
using RealmSqliteNetShowdown;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Xamarin.Forms.Dependency(typeof(ISqliteConnectionProvider))]
namespace RealmSqliteNetShowdown.Droid
{
    public class SqliteConnectionProviderDroid : ISqliteConnectionProvider
    {
        public SQLiteConnection Connection
        {
            get
            {
                var platform = new SQLitePlatformAndroid();
                return new SQLiteConnection(platform, "showdownDroid.sqlite");
            }
        }
    }
}

