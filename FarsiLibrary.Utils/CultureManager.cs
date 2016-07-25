using System.ComponentModel;
using System.Globalization;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Utils
{
    public class CultureManager
    {
        private CultureInfo controlsCulture;

        private CultureManager()
        {
            controlsCulture = CultureInfo.InvariantCulture;
            UseDefaultCulture = true;
        }

        static CultureManager()
        {
            Instance = new CultureManager();
        }

        public static CultureManager Instance { get; private set; }

        [DefaultValue(true)]
        public bool UseDefaultCulture { get; set; }

        public CultureInfo ControlsCulture
        {
            get
            {
                if(UseDefaultCulture)
                {
                    return CultureHelper.CurrentCulture;
                }

                return controlsCulture;
            }
            set
            {
                controlsCulture = value;
                UseDefaultCulture = false;
            }
        }
    }
}