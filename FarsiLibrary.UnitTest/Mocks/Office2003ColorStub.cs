using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.UnitTest.Mocks
{
    internal class Office2003ColorStub : Office2003Colors
    {
        public void InitColor(XPThemeType themeType)
        {
            var id = GetStyleId(themeType);
            base.InitOfficeColors(id);
        }

        private int GetStyleId(XPThemeType themeType)
        {
            return ((int)themeType) - 1;
        }

        public void InitStandard()
        {
            base.InitStandardColors();
        }
    }
}