using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FarsiLibrary.Web
{
    #region DateChangedEventArgs

    /// <summary>
    /// Used in firing Day/Month/Year change events.
    /// </summary>
    public class DateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        public DateChangedEventArgs(DateTime? newValue, DateTime? oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        /// <summary>
        /// OldValue
        /// </summary>
        public DateTime? OldValue
        {
            get;
            private set;
        }

        /// <summary>
        /// NewValue
        /// </summary>
        public DateTime? NewValue
        {
            get;
            private set;
        }
    }

    #endregion

    #region RenderCalendarCellArgs

    public class RenderCalendarCellArgs : HandledEventArgs
    {
        public RenderCalendarCellArgs(HtmlTextWriter writer, Style cellStyle, CalendarDay dayInfo, string eventArgument, string text, string title) : base(false)
        {
            Writer = writer;
            CellStyle = cellStyle;
            Text = text;
            Title = title;
            DayInfo = dayInfo;
            EventArgument = eventArgument;
        }

        public HtmlTextWriter Writer
        { 
            get; 
            private set;
        }

        public Style CellStyle
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            set;
        }

        public string EventArgument
        { 
            get; 
            set;
        }

        public string Title
        {
            get; 
            set;
        }

        public CalendarDay DayInfo
        { 
            get; 
            private set;
        }
    }

    #endregion

}
