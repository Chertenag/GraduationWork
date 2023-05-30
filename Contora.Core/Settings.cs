using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
