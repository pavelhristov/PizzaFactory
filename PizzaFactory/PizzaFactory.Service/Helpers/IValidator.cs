using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Service.Helpers
{
    public interface IValidator
    {
        int ValidatePage(int page);

        int ValidatePageSize(int pageSize, int maxSize = 10);
    }
}
