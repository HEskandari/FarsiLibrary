namespace FarsiLibrary.Utils.Formatter.TimeUnits
{
    public class Year : AbstractTimeUnit
    {
        public Year()
        {
            MillisPerUnit = 1000L * 60L * 60L * 24L * 30L * 12L;
        }

        protected override string GetResourcePrefix()
        {
            return "Year";
        }
    }
}