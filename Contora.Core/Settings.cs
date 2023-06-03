namespace Contora.Core
{
    /// <summary>
    /// Класс содержащий необходимые настройки для работы с БД.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets or sets строку подключения к БД.
        /// </summary>
        public static string? ConnectionString
        {
            get => Data.Settings.ConnectionString;
            set => Data.Settings.ConnectionString = string.IsNullOrEmpty(value)
                ? throw new ArgumentException("Connection string to DB is null or empty.")
                : value;
        }
    }
}
