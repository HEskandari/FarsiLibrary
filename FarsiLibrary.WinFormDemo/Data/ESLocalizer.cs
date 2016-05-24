using FarsiLibrary.Localization;

namespace FarsiLibrary.WinFormDemo.Data
{
    /// <summary>
    /// Implements a spanish localizer.
    /// </summary>
    public class ESLocalizer : ENLocalizer
    {
        public override string GetLocalizedString(StringID id)
        {
            switch(id)
            {
                case StringID.Validation_NullText: return "<Ningun fecha esta seleccionada>";
                case StringID.FAMonthView_None : return "Nada";
                case StringID.FAMonthView_Today: return "¡Hoy!";
            }

            return base.GetLocalizedString(id);
        }
    }
}