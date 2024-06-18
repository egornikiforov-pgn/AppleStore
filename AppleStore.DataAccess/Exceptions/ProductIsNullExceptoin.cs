using System;

namespace AppleStore.DataAccess.Exceptions
{
    public class ProductIsNullExceptoin : Exception
    {
        public ProductIsNullExceptoin(string message) : base(message)
        {
        }
    }
}
