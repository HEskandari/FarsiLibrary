namespace FarsiLibrary.Utils.Formatter.TimeUnits
{
    public class Millennium : AbstractTimeUnit
    {
        public Millennium()
        {
            MillisPerUnit = 31556926000000L;
        }

        protected override string GetResourcePrefix()
        {
            return "Millennium";
        }
    }
}