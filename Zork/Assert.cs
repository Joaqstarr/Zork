using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    public static class Assert
    {
        [Conditional("DEBUG")]
        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
            {
                throw new Exception("Assertion Failed. " + message);
            }
        }
    }
}
