using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class MongoDbSettings
    {
        public static string ConnectionURI { get; set; } = null!;
        public static string DatabaseName { get; set; } = null!;
        public static string CollectionName { get; set; } = null!;
    }
}
