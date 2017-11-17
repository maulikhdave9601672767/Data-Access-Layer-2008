using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooleanHelpers
{
    public static class BooleanMethods
    {
        public static Boolean If(this Boolean b, Boolean expression)
        {
            if (expression)
                return b;
            else
                return !b;
        }
        public static Boolean And(this Boolean b, Boolean expression)
        {
            return b && expression;
        }
        public static Boolean Or(this Boolean b, Boolean expression)
        {
            return b || expression;
        }



    }
}
