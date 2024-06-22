using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleStore.DataAccess.Exceptions
{
    public class RelapseException : Exception
    {
        public RelapseException(string message) : base(message)
        {
        }
    }
}
