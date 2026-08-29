using ReciclaMe.Domain;
using SQLite;

namespace ReciclaMe.Infrastructure;

public sealed class DatabaseService
{
    private static Lock _noLock = new();
    private static DatabaseService? _instance;
    internal SQLiteAsyncConnection Database;
    
    private DatabaseService()
    {
        
    }

    internal async Task InitAsync()
    {
        Database ??= new SQLiteAsyncConnection(ConstantsSqlite.DatabasePath, ConstantsSqlite.Flags);
        _ = await Database.CreateTableAsync<UserProfile>();
    }

    public static DatabaseService GetInstance()
    {
        lock (_noLock)
        {
            _instance ??= new DatabaseService();
            return _instance;
        }
    }
}