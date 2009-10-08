using System;
using System.ComponentModel;
using System.Windows;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    /// <summary>
    /// Designer control to visually set FXMonthView's
    /// properties on design-time.
    /// </summary>
    public partial class MonthViewDesignerUI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _ShowEmptyButton;
        private bool _ShowTodayButton;
        private bool _ShowWeekDayNames;
        private DateTime? _SelectedDateTime;
        private DateTime _MinDate;
        private DateTime _MaxDate;

        public MonthViewDesignerUI()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        public bool ShowEmptyButton
        {
            get { return _ShowEmptyButton; }
            set
            {
                _ShowEmptyButton = value;
                RaisePropertyChanged("ShowEmptyButton", value);
            }
        }

        public bool ShowTodayButton
        {
            get { return _ShowTodayButton; }
            set
            {
                _ShowTodayButton = value;
                RaisePropertyChanged("ShowTodayButton", value);
            }
        }

        public bool ShowWeekDayNames
        {
            get { return _ShowWeekDayNames; }
            set
            {
                _ShowWeekDayNames = value;
                RaisePropertyChanged("ShowWeekDayNames", value);
            }
        }

        public DateTime? SelectedDateTime
        {
            get { return _SelectedDateTime; }
            set
            {
                _SelectedDateTime = value;
                RaisePropertyChanged("SelectedDateTime", value);
            }
        }

        public DateTime MaxDate
        {
            get { return _MaxDate; }
            set
            {
                _MaxDate = value;
                RaisePropertyChanged("MaxDate", value);
            }
        }

        public DateTime MinDate
        {
            get { return _MinDate; }
            set
            {
                _MinDate = value;
                RaisePropertyChanged("MinDate", value);
            }
        }

        protected void RaisePropertyChanged(string propertyName, object value)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new DesignerPropertyChangedEventArgs(propertyName, value));
            }
        }
    }
}
