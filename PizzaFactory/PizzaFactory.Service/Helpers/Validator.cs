using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Service.Helpers
{
    public class Validator : IValidator
    {
        public int ValidatePage(int page)
        {
            if (page < 1)
            {
                return 1;
            }
            else
            {
                return page;
            }
        }

        public int ValidatePageSize(int size, int maxSize = 10)
        {
            if (size < 1)
            {
                return 1;
            }

            if (size > maxSize)
            {
               return maxSize;
            }

            return size;
        }
    }
}
