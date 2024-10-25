using Microsoft.EntityFrameworkCore;

namespace LearningCenter_Shayhilislamov.Classes.Common
{
    public class Config
    {
        public static string ConnectionConfig = "server=127.0.0.1;port=3306;uid=root;pwd=;database=LearningCenter;SslMode=None;";

        public static MySqlServerVersion Version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
