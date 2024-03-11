using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.DTOs
{
    public class GeneralResponeDto<T> where T : class
    {
        public T Item { get; set; }
        public string Message { get; set; }
    }
}
