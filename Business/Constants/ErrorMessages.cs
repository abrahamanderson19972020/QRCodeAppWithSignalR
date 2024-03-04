using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class ErrorMessages<T> where T : class
    {
        public static string NoItemFound = $"No {typeof(T).Name} is found";
    }
}
