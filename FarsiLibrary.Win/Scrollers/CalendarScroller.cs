using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarsiLibrary.Win.Scrollers
{
    interface ICalendarScroller
    {
        public bool CanScroll { get; }

        public void SetSelection(int selectionStart);

        public void SetDate(int delta);

    }
}
