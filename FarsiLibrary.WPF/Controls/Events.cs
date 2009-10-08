using System;
using System.Collections.Generic;
using System.Windows;

namespace FarsiLibrary.WPF.Controls
{
    /// <summary>
    /// The delegate type for handling a selection changed event
    /// </summary>
    public delegate void DateSelectionChangedEventHandler(object sender, DateSelectionChangedEventArgs e);

    /// <summary>
    /// The delegate type for handling the InvalidEntry event
    /// </summary>
    public delegate void InvalidEntryEventHandler(object sender, InvalidEntryEventArgs e);

    #region InvalidEntryEventArgs

    /// <summary>
    /// The InvalidEntry event args, occurs when the datepicker can't parse user input string correctly
    /// </summary>
    public class InvalidEntryEventArgs : RoutedEventArgs
    {
        private readonly string _entry;

        /// <summary>
        /// Ctor
        /// </summary>
        public InvalidEntryEventArgs(RoutedEvent id, string entry)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            RoutedEvent = id;
            _entry = entry;
        }

        /// <summary>
        /// The input string
        /// </summary>
        public string Entry
        {
            get { return _entry; }
        }

        /// <summary>
        /// This method is used to perform the proper type casting in order to
        /// call the type-safe InvalidEntryEventHandler delegate for the InvalidEntry event.
        /// </summary>
        /// <param name="genericHandler">The handler to invoke.</param>
        /// <param name="genericTarget">The current object along the event's route.</param>
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            InvalidEntryEventHandler handler = (InvalidEntryEventHandler)genericHandler;
            handler(genericTarget, this);
        }
    }

    #endregion

    #region DateSelectionChangedEventArgs

    /// <summary>
    /// The inputs to a selection changed event handler
    /// </summary>
    public class DateSelectionChangedEventArgs : RoutedEventArgs
    {
        #region Fields

        private readonly List<DateTime?> _addedDates;
        private readonly List<DateTime?> _removedDates;

        #endregion

        #region Ctor

        /// <summary>
        /// The constructor for date selection changed args
        /// </summary>
        /// <param name="id">The event ID for the event about to fire -- should probably be MonthCalendar.DateSelectionChangedEventID</param>
        /// <param name="removedDates">The dates that were unselected during this event</param>
        /// <param name="addedDates">The dates that were selected during this event</param>
        public DateSelectionChangedEventArgs(RoutedEvent id, IEnumerable<DateTime?> removedDates, IEnumerable<DateTime?> addedDates)
        {
            if (removedDates == null)
                throw new ArgumentNullException("removedDates");

            if (addedDates == null)
                throw new ArgumentNullException("addedDates");

            RoutedEvent = id;

            _removedDates = new List<DateTime?>(removedDates);
            _addedDates = new List<DateTime?>(addedDates);
        }

        internal DateSelectionChangedEventArgs(RoutedEvent id)
        {
            RoutedEvent = id;
            _removedDates = new List<DateTime?>(1);
            _addedDates = new List<DateTime?>(1);
        }

        internal DateSelectionChangedEventArgs(IEnumerable<DateTime?> removedDates, IEnumerable<DateTime?> addedDates) : this(FXMonthView.SelectedDateTimeChangedEvent, removedDates, addedDates)
        {
        }

        internal DateSelectionChangedEventArgs()
        {
            RoutedEvent = FXMonthView.SelectedDateTimeChangedEvent;
            _removedDates = new List<DateTime?>(1);
            _addedDates = new List<DateTime?>(1);
        }

        #endregion

        #region Props

        /// <summary>
        /// An IList containing the dates that were unselected during this event
        /// </summary>
        public IList<DateTime?> RemovedDates
        {
            get { return _removedDates; }
        }

        /// <summary>
        /// An IList containing the dates that were selected during this event
        /// </summary>
        public IList<DateTime?> AddedDates
        {
            get { return _addedDates; }
        }

        #endregion

        #region Methods

        ///// <summary>
        ///// This method is used to perform the proper type casting in order to
        ///// call the type-safe DateSelectionChangedEventHandler delegate for the DateSelectionChangedEvent event.
        ///// </summary>
        ///// <param name="genericHandler">The handler to invoke.</param>
        ///// <param name="genericTarget">The current object along the event's route.</param>
        //protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        //{
        //    DateSelectionChangedEventHandler handler = (DateSelectionChangedEventHandler)genericHandler;
        //    handler(genericTarget, this);
        //}

        #endregion
    }

    #endregion
}
