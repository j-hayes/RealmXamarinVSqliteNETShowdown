using System;
namespace RealmSqliteNetShowdown
{
    public interface ISqliteConnectionProvider
    {
        SQLite.Net.SQLiteConnection Connection { get; }
    }
}

