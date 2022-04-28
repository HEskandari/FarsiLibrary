using FarsiLibrary.Win.Controls;
using System;

namespace FarsiLibrary.Win.Helpers
{
    class EnglishCalendarScroller : ICalendarScroller
    {
        FADatePicker picker;

        private readonly int _yearIndex;
        private readonly int _monthIndex;
        private readonly int _dayIndex;
        private readonly int _hourIndex;
        private readonly int _minuteIndex;

        public EnglishCalendarScroller(FADatePicker picker)
        {
            this.picker = picker;

            _yearIndex = picker.mv.MonthViewControl.DefaultCulture.DateTimeFormat.ShortDatePattern.IndexOf("yyyy");
            _monthIndex = picker.mv.MonthViewControl.DefaultCulture.DateTimeFormat.ShortDatePattern.IndexOf("MM");
            _dayIndex = picker.mv.MonthViewControl.DefaultCulture.DateTimeFormat.ShortDatePattern.IndexOf("dd");
            _hourIndex = 11;
            _minuteIndex = 14;
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

            if (selectionStart >= _dayIndex && selectionStart <= _dayIndex + 3)
            {
                picker.SelectionStart = _dayIndex;
                picker.SelectionLength = 2;
            }
            else if (selectionStart >= _monthIndex && selectionStart <= _monthIndex + 3)
            {
                picker.SelectionStart = _monthIndex;
                picker.SelectionLength = 2;
            }
            else if (selectionStart >= _yearIndex && selectionStart <= _yearIndex + 5)
            {
                picker.SelectionStart = _yearIndex;
                picker.SelectionLength = 4;
            }
            else if (selectionStart >= _hourIndex && selectionStart <= _hourIndex + 3)
            {
                picker.SelectionStart = _hourIndex;
                picker.SelectionLength = 2;
            }
            else if (selectionStart >= _minuteIndex && selectionStart >= _minuteIndex + 3)
            {
                picker.SelectionStart = _minuteIndex;
                picker.SelectionLength = 2;
            }
            
        }

        public void SetDate(int delta)
        {
            delta = delta / 120;
            var newDate = new DateTime();
            int selectionStart = picker.SelectionStart;
            try
            {
                newDate = new DateTime(Convert.ToInt32(picker.Text.Substring(_yearIndex, 4)),
                    Convert.ToInt32(picker.Text.Substring(_monthIndex, 2)),
                    Convert.ToInt32(picker.Text.Substring(_dayIndex, 2)));
            }
            catch
            {
                //can't convert text to date parts
                return;
            }

           
            if (selectionStart >= _dayIndex && selectionStart <= _dayIndex + 3)
            {
                // Day
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddDays(delta).ToString("d"));
            }
            else if (selectionStart >= _monthIndex && selectionStart <= _monthIndex + 3)
            {
                // Month
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddMonths(delta).ToString("d"));
            }
            else if (selectionStart >= _yearIndex && selectionStart <= _yearIndex + 5)
            {
                // Year
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.AddYears(delta).ToString("d"));
            }
            else if (selectionStart >= _hourIndex && selectionStart <= _hourIndex + 3)
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
            else if (selectionStart >= _minuteIndex && selectionStart >= _minuteIndex + 3)
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
