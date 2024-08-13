using System;

namespace BanVeXemPhimApi.Common
{
    public class ValidateError : Exception
    {
        public ValidateError(string message) : base(message)
        {
        }
    }
}
