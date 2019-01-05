using System;

namespace Assets.Scripts.Infrastructure
{
    public static class ArgumentValidator
    {
        public static void ValidateArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentException("The argument " + argument + " cannot be null.");
            }
        }
    }
}
