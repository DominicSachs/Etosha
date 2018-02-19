using System;

namespace Etosha.Server.Common.Validation
{
    public static class Require
    {
        public static void ThatNotNull(object item, string parameterName)
        {
            if (item == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
