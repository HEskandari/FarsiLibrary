using System;
using System.Globalization;
using System.Threading;

namespace FarsiLibrary.UnitTest.Helpers
{
    public class CultureSwitchContext : IDisposable
    {
        private CultureInfo newCulture;
        private CultureInfo oldCulture;
        private CultureInfo oldUICulture;

        public CultureSwitchContext(CultureInfo newCulture)
        {
            this.newCulture = newCulture;
            this.SnapshotCulture();
        }

        private void SnapshotCulture()
        {
            oldUICulture = Thread.CurrentThread.CurrentUICulture;
            oldCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentUICulture = oldUICulture;
            Thread.CurrentThread.CurrentCulture = oldCulture;
        }
    }
}