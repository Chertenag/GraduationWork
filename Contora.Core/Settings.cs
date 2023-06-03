namespace Contora.Core
{
    public static class Settings
    {
        public static string ConnectionString
        {
            get => Data.Settings.ConnectionString;
            set => Data.Settings.ConnectionString = value;
        }
    }
}
