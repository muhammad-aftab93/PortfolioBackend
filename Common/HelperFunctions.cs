using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class HelperFunctions
    {
        public static string EvaluateCollectionName<T>()
        {
            var collectionName = typeof(T).ToString().ToLower().Split('.');
            if (collectionName.Contains("user"))
                return "users";
            else if (collectionName.Contains("weather"))
                return "weatherforecasts";
            else
                return "unknown";
        }
    }
}
