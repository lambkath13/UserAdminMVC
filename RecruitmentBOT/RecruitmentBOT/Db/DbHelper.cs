using Microsoft.Data.Sqlite;
using System.Data;

namespace RecruitmentBOT.Db
{
    public static class DbHelper
    {
        private static readonly string DbPath = Path.Combine(AppContext.BaseDirectory, "RecruitmentBot.db");
        private static readonly string ConnectionString = $"Data Source={DbPath}";

        public static void RegisterUser(long chatId)
        {
            Execute("INSERT OR IGNORE INTO Users (ChatId) VALUES ($id)", new() { ["$id"] = chatId });
        }

        public static void Execute(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);
            cmd.ExecuteNonQuery();
        }

        public static object? Scalar(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);
            return cmd.ExecuteScalar();
        }

        public static List<Dictionary<string, object>> Query(string sql, Dictionary<string, object>? parameters = null)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);

            var result = new List<Dictionary<string, object>>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.GetValue(i);
                result.Add(row);
            }
            return result;
        }

        private static void AddParameters(SqliteCommand cmd, Dictionary<string, object>? parameters)
        {
            if (parameters == null) return;
            foreach (var p in parameters)
                cmd.Parameters.AddWithValue(p.Key, p.Value);
        }
    }
    
}
