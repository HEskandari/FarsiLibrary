using System;

namespace FarsiLibrary.Utils.Formatter.TimeUnits
{
    public class Month : AbstractTimeUnit
    {
        public Month()
        {
            MillisPerUnit = 2629743830L;
        }

        protected override string GetResourcePrefix()
        {
            return "Month";
        }
    }
}