using System;

namespace DoAnFramework
{
    public class DerivedImplementationNotFoundException : Exception
    {
        private const string ExceptionMessage = "The method should be implemented in the derived class";

        public DerivedImplementationNotFoundException()
            : base(ExceptionMessage)
        {
        }
    }
}
