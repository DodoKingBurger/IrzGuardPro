using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.Utility
{
    /// <summary>
    /// База для хранения данных в файле.
    /// </summary>
    public static class Hash_table
    {
        /// <summary>
        /// Дирректория в которой расположен .exe.
        /// </summary>
        private static string mainDir = FileSystem.Current.AppDataDirectory;


        public static string GetString(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (!File.Exists(Path.Combine(mainDir, key)))
                    return "---";
                using var stream = File.OpenRead(Path.Combine(mainDir, key));
                using var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(contents))
                    return contents;
                else
                    throw new ArgumentException($"{contents} - string is null or empty.");
            }
            else
                throw new ArgumentException($"{key} - string is null or empty.");
        }

        public static async void SetString(string key, string value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (!string.IsNullOrEmpty(value)) 
                {
                    if (!File.Exists(Path.Combine(mainDir, key)))
                        File.Create(Path.Combine(mainDir, key));
                    using var stream = File.OpenRead(Path.Combine(mainDir, key));
                    using var writer = new StreamWriter(stream);
                    await writer.WriteAsync($"{value}");
                }
                else
                    throw new ArgumentException($"{value} - string is null or empty.");
            }
            else
                throw new ArgumentException($"{key} - string is null or empty.");

        }

        public static int GetInt(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (!File.Exists(Path.Combine(mainDir, key)))
                    return -1;

                using var stream = File.OpenRead(Path.Combine(mainDir, key));
                using var reader = new StreamReader(stream);
                if (int.TryParse(reader.ReadToEnd(), out var contents))
                    return contents;
                else
                    throw new ArgumentException($"{contents} - can't convert to number.");
            }
            else
                throw new ArgumentException($"{key} - string is null or empty.");
        }

        public static async void SetInt(string key, int value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (!File.Exists(Path.Combine(mainDir, key)))
                    File.Create(Path.Combine(mainDir, key));
                if (!value.Equals(null))
                {
                    using var stream = File.OpenWrite(Path.Combine(mainDir, key));
                    using var writer = new StreamWriter(stream);
                    await writer.WriteAsync($"{value}");
                }
                else
                    throw new ArgumentException($"{value} - int is null.");
            }
            else
                throw new ArgumentException($"{key} - string is null or empty.");
        }
    }
}
