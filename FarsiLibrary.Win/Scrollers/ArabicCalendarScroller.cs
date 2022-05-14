using FarsiLibrary.Win.Controls;

namespace FarsiLibrary.Win.Scrollers
{
    class ArabicCalendarScroller : ICalendarScroller
    {
        FADatePicker picker;

        private readonly int yearIndex, monthIndex, dayIndex , hourIndex , minuteIndex;
        private readonly int yearLength, monthLength, dayLength, hourLength, minuteLength;
        private readonly string datePattern;

        public ArabicCalendarScroller(FADatePicker picker)
        {
            this.picker = picker;

            datePattern = picker.mv.MonthViewControl.DefaultCulture.DateTimeFormat.ShortDatePattern;
            
            yearIndex = datePattern.IndexOf("y") ;
            yearLength = datePattern.LastIndexOf("y") - datePattern.IndexOf("y") + 1;

            monthIndex = datePattern.IndexOf("M");
            monthLength = datePattern.LastIndexOf("M") - datePattern.IndexOf("M") + 1;


            dayIndex = datePattern.IndexOf("d");
            dayLength = datePattern.LastIndexOf("d") - datePattern.IndexOf("d") + 1;

            hourIndex = datePattern.Length + 1;
            hourLength = 2;

            minuteIndex = hourIndex + 3;
            minuteLength = 2;
        }

        public bool CanScroll
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

            if (selectionStart >= dayIndex && selectionStart <= dayIndex + dayLength)
            {
                picker.SelectionStart = dayIndex;
                picker.SelectionLength = dayLength;
            }
            else if (selectionStart >= monthIndex && selectionStart <= monthIndex + monthLength)
            {
                picker.SelectionStart = monthIndex;
                picker.SelectionLength = monthLength;
            }
            else if (selectionStart >= yearIndex && selectionStart <= yearIndex + yearLength)
            {
                picker.SelectionStart = yearIndex;
                picker.SelectionLength = yearLength;
            }
            else if (selectionStart >= hourIndex && selectionStart <= hourIndex + hourLength)
            {
                picker.SelectionStart = hourIndex;
                picker.SelectionLength = hourLength;
            }
            else if (selectionStart >= minuteIndex && selectionStart <= minuteIndex + minuteLength)
            {
                picker.SelectionStart = minuteIndex;
                picker.SelectionLength = minuteLength;
            }
        }

        public void SetDate(int delta)
        {
            delta = delta / 120;
            int selectionStart = picker.SelectionStart;

            var dateTime = picker.mv.MonthViewControl.SelectedDateTime.Value;

            if (selectionStart >= dayIndex && selectionStart <= dayIndex + dayLength)
            {
                // Day
                dateTime = dateTime.AddDays(delta);
            }
            else if (selectionStart >= monthIndex && selectionStart <= monthIndex + monthLength)
            {
                // Month
                dateTime = dateTime.AddMonths(delta);
            }
            else if (selectionStart >= yearIndex && selectionStart <= yearIndex + yearLength)
            {
                // Year
                dateTime = dateTime.AddYears(delta);
            }
            else if (selectionStart >= hourIndex && selectionStart <= hourIndex + hourLength)
            {
                // Hour
                dateTime = dateTime.AddHours(delta);
            }
            else if (selectionStart >= minuteIndex && selectionStart <= minuteIndex + minuteLength)
            {
                // Minutes
                dateTime = dateTime.AddMinutes(delta);
            }

            picker.mv.MonthViewControl.SelectedDateTime = dateTime;

            SetSelection(selectionStart);
        }

      
    }
}
