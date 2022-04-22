using FarsiLibrary.Utils;
using FarsiLibrary.Win.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarsiLibrary.Win.Helpers
{

    class PersianCalendarScroller : ICalendarScroller
    {
        private FADatePicker picker;

        public PersianCalendarScroller(FADatePicker picker)
        {
            this.picker = picker;
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
            switch (selectionStart)
            {
                case < 5: //year selected
                    picker.SelectionStart = 0;
                    picker.SelectionLength = 4;
                    break;
                case < 8: //month selected
                    picker.SelectionStart = 5;
                    picker.SelectionLength = 2;
                    break;
                case < 11: //day selected
                    picker.SelectionStart = 8;
                    picker.SelectionLength = 2;
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
            var newDate = new PersianDate();
            int selectionStart = picker.SelectionStart;
            try
            {
                newDate.Year = Convert.ToInt32(picker.Text.Substring(0, 4));
                newDate.Month = Convert.ToInt32(picker.Text.Substring(5, 2));
                newDate.Day = Convert.ToInt32(picker.Text.Substring(8, 2));
            }
            catch
            {
                //can't convert text to date parts
                return;
            }

            if (picker.SelectionStart < 5)
            {
                // Year
                if (newDate.Month == 11 && delta > 0 && newDate.Day > 29)
                {
                    newDate.Day = 29;
                    picker.Text = newDate.ToString("d");
                }

                var updatedYear = (newDate.Year + delta).ToString();
                picker.Text = picker.Text.Remove(0, 4).Insert(0, updatedYear.Substring(updatedYear.Length - 4));
               
            }
            else if (picker.SelectionStart < 8)
            {
                // Month
                var newMonth = newDate.Month + delta;
                Math.DivRem(newMonth, 12, out newMonth);
                if (newMonth == 0)
                {
                    newMonth = 12;
                }
                else if (newMonth < 0)
                {
                    newMonth = Math.Abs(newMonth);
                }

                if (newMonth == 12 && newDate.Day > 29)
                {
                    newDate.Day = 29;
                }
                else if (newMonth > 6 && newDate.Day == 31)
                {
                    newDate.Day = 30;
                }

                newDate.Month = newMonth;
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDate.ToString("d"));
               
            }
            else if (picker.SelectionStart < 11)
            {
                // Day
                var newDay = PersianDateConverter.ToPersianDate(newDate.ToDateTime().AddDays(delta));
                picker.Text = picker.Text.Remove(0, 10).Insert(0, newDay.ToString("d"));
               
            }
            else if (picker.SelectionStart < 14)
            {
                // Hour
                var newHour = Convert.ToInt32(picker.Text.Substring(11, 2));
                newHour += delta;
                newHour = newHour > 12 ? 1 : newHour < 1 ? 12 : newHour;

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
            else if (picker.SelectionStart <= 20)
            {
                picker.Text = picker.Text.Remove(17, 3).Insert(17, picker.Text.Contains(PersianDateTimeFormatInfo.AMDesignator) ? PersianDateTimeFormatInfo.PMDesignator : PersianDateTimeFormatInfo.AMDesignator);
               
            }
            SetSelection(selectionStart);
        }
       
    }
}
