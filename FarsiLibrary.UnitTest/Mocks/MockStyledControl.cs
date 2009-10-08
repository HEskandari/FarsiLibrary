using FarsiLibrary.Win.BaseClasses;

namespace FarsiLibrary.UnitTest.Mocks
{
    public class MockStyledControl : BaseStyledControl
    {
        private bool _themeAvailable;

        public override bool UseThemes
        {
            get { return _themeAvailable; }
        }

        public void SetThemeAvailability(bool value)
        {
            _themeAvailable = value;
        }
    }
}