using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class SqlReader : IDisposable
    {
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand = null;
        private Microsoft.Data.Sqlite.SqliteDataReader reader = null;
        private bool writeToConsoleOnError = true;
        private string sql = ""; // Only used for error messages
        public SqlReader(string sql, Microsoft.Data.Sqlite.SqliteConnection conn = null, bool writeToConsoleOnError = true)
        {
            sqliteCommand = new Microsoft.Data.Sqlite.SqliteCommand(sql, conn == null ? Form_Main.connection : conn);
            reader = sqliteCommand.ExecuteReader();
            this.sql = sql;
            this.writeToConsoleOnError = writeToConsoleOnError;
        }
        public void Dispose()
        {
            reader.Dispose();
            sqliteCommand.Dispose();
        }
        public bool Read() => reader.Read();
        public DataTable GetSchemaTable() => reader.GetSchemaTable();
        public string GetString(string fieldName, string defaultValue = "")
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                {
                    string value = reader.GetString(reader.GetOrdinal(fieldName));
                    if (value.Length > 0)
                        return value;
                }
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetString exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public float GetFloat(string fieldName, float defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetFloat(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetFloat exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public double GetDouble(string fieldName, double defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetDouble(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetDouble exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public bool GetBoolean(string fieldName, bool defaultValue = false)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetBoolean(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetBoolean exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public short GetInt16(string fieldName, short defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetInt16(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetInt16 exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public int GetInt32(string fieldName, int defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetInt32(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetInt32 exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public long GetInt64(string fieldName, long defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetInt64(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetInt64 exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public Guid GetGuid(string fieldName, Guid defaultValue = new Guid())
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetGuid(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetGuid exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public decimal GetDecimal(string fieldName, decimal defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetDecimal(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetDecimal exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public char GetChar(string fieldName, char defaultValue = ' ')
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetChar(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetChar exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public byte GetByte(string fieldName, byte defaultValue = 0)
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetByte(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetByte exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public TimeSpan GetTimeSpan(string fieldName, TimeSpan defaultValue = new TimeSpan())
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetTimeSpan(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetTimeSpan exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public DateTimeOffset GetDateTimeOffset(string fieldName, DateTimeOffset defaultValue = new DateTimeOffset())
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetDateTimeOffset(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetDateTimeOffset exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        public DateTime GetDateTime(string fieldName, DateTime defaultValue = new DateTime())
        {
            try
            {
                if (!reader.IsDBNull(reader.GetOrdinal(fieldName)))
                    return reader.GetDateTime(reader.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                if (writeToConsoleOnError)
                    Console.WriteLine($"GetDateTime exception thrown \"{ex.Message}\" for field {fieldName}; SQL = \"{sql}\"");
            }
            return defaultValue;
        }
        // Extra functions not in SqliteDataReader
        public string GetStr(string fieldName, string defaultValue = "") => GetString(fieldName, defaultValue);
        public int GetInt(string fieldName, int defaultValue = 0) => GetInt32(fieldName, defaultValue);
        public long GetLong(string fieldName, long defaultValue = 0) => GetInt64(fieldName, defaultValue);
        public short GetShort(string fieldName, short defaultValue = 0) => GetInt16(fieldName, defaultValue);
        public bool GetBool(string fieldName, bool defaultValue = false) => GetBoolean(fieldName, defaultValue);
        // Extra generic function
        public string Get(string fieldName, string defaultValue = "") => GetString(fieldName, defaultValue);
        public int Get(string fieldName, int defaultValue) => GetInt(fieldName, defaultValue);
        public float Get(string fieldName, float defaultValue) => GetFloat(fieldName, defaultValue);
        public double Get(string fieldName, double defaultValue) => GetDouble(fieldName, defaultValue);
        public long Get(string fieldName, long defaultValue) => GetLong(fieldName, defaultValue);
        public short Get(string fieldName, short defaultValue) => GetShort(fieldName, defaultValue);
        public bool Get(string fieldName, bool defaultValue) => GetBoolean(fieldName, defaultValue);
        public Guid Get(string fieldName, Guid defaultValue) => GetGuid(fieldName, defaultValue);
        public decimal Get(string fieldName, decimal defaultValue) => GetDecimal(fieldName, defaultValue);
        public char Get(string fieldName, char defaultValue) => GetChar(fieldName, defaultValue);
        public byte Get(string fieldName, byte defaultValue) => GetByte(fieldName, defaultValue);
        public TimeSpan Get(string fieldName, TimeSpan defaultValue) => GetTimeSpan(fieldName, defaultValue);
        public DateTimeOffset Get(string fieldName, DateTimeOffset defaultValue) => GetDateTimeOffset(fieldName, defaultValue);
        public DateTime Get(string fieldName, DateTime defaultValue) => GetDateTime(fieldName, defaultValue);
    }
}
