namespace FarsiLibrary.Utils.Formatter.TimeUnits
{
    public class Year : AbstractTimeUnit
    {
        public Year()
        {
            MillisPerUnit = 2629743830L * 12L;
        }

        protected override string GetResourcePrefix()
        {
            return "Year";
        }
    }
}