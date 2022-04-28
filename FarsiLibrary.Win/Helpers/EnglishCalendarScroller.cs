using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Formatter.TimeUnits;
using FarsiLibrary.Win.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarsiLibrary.Win.Helpers
{
    class EnglishCalendarScroller : ICalendarScroller
    {
        FADatePicker picker;

        const int _yearIndex = 6;
        const int _monthIndex = 0;
        const int _dayIndex = 3;

        public EnglishCalendarScroller(FADatePicker picker)
        {
            this.picker = picker;

        }

        bool ICalendarScroller.CanScroll
        {
            get
            {
                if (picker.mv.Visible)
                    return false;

                if (picker.SelectedDateTime == null)
                    return false;

                return (picker.FormatInfo == Enums.FormatInfoTypes.DateShortTime
                    || picker.FormatInfo == Enums.FormatInfoTypes.ShortDate);
            }
        }

        public void SetSelection(int selectionStart)
        {
            // Select part of date based on the mouse position
            switch (selectionStart)
            {
                case < 3: //month selected
                    picker.SelectionStart = 0;
                    picker.SelectionLength = 2;
                    break;
                case < 5: //day selected
                    picker.SelectionStart = 3;
                    picker.SelectionLength = 2;
                    break;
                case < 11: //year selected
                    picker.SelectionStart = 6;
                    picker.SelectionLength = 4;
                    break;
                case < 14: //hour selected
                    picker.SelectionStart = 11;
                    picker.SelectionLength = 2;
                    break;
                case < 17: //min selected
                    picker.SelectionStart = 14;
                    picker.SelectionLength = 2;
                    break;
                case < 20: //second selected
                    picker.SelectionStart = 17;
                    picker.SelectionLength = 3;
                    break;
            }
        }

        public void SetDate(int delta)
        {
            delta = delta / 120;
            var newDate = new DateTime();
            int selectionStart = picker.SelectionStart;
            try
            {
                newDate = new DateTime(Convert.ToInt32(picker.Text.Substring(6, 4)),
                    Convert.ToInt32(picker.Text.Substring(0, 2)),
                    Convert.ToInt32(picker.Text.Substring(3, 2)));
            }
            catch
            {
                //can't convert text to date parts
                return;
            }

            if (picker.SelectionStart < 3)
            {
                // Month
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddMonths(delta).ToString("d"));
            }
            else if (picker.SelectionStart < 5)
            {
                // Day
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddDays(delta).ToString("d"));
            }
            else if (picker.SelectionStart < 11)
            {
                // Year
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddYears(delta).ToString("d"));
            }
            else if (picker.SelectionStart < 14)
            {
                // Hour
                var newHour = Convert.ToInt32(picker.Text.Substring(11, 2));
                newHour += delta;
                if (picker.Text.Length > 16 ) // 12 Hour Oclock
                    newHour = newHour > 12 ? 1 : newHour < 1 ? 12 : newHour;
                else
                    newHour = newHour > 23 ? 0 : newHour < 0 ? 23 : newHour;
              

                picker.Text = picker.Text.Remove(11, 2).Insert(11, string.Format("{0:00}", newHour));

            }
            else if (picker.SelectionStart < 17)
            {
                var newMinute = Convert.ToInt32(picker.Text.Substring(14, 2));
                newMinute -= newMinute % 5;
                newMinute += delta * 5;
                newMinute = newMinute >= 60 ? 0 : newMinute < 0 ? 55 : newMinute;

                picker.Text = picker.Text.Remove(14, 2).Insert(14, string.Format("{0:00}", newMinute));

            }
           
            SetSelection(selectionStart);
        }
    }
}
