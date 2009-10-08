using System.Windows.Input;

namespace FarsiLibrary.WPF.Controls
{
    public static class FXMonthViewCommands
    {
        public static readonly RoutedCommand ChangeToNextMonth = new RoutedCommand("FXMonthViewCommands.ChangeToNextMonth", typeof(FXMonthViewCommands));
        public static readonly RoutedCommand ChangeToNextYear = new RoutedCommand("FXMonthViewCommands.ChangeToNextYear", typeof(FXMonthViewCommands));
        public static readonly RoutedCommand ChangeToPrevMonth = new RoutedCommand("FXMonthViewCommands.ChangeToPrevMonth", typeof(FXMonthViewCommands));
        public static readonly RoutedCommand ChangeToPrevYear = new RoutedCommand("FXMonthViewCommands.ChangeToPrevYear", typeof(FXMonthViewCommands));
        public static readonly RoutedCommand SelectTodayDate = new RoutedCommand("FXMonthViewCommands.SelectTodayDate", typeof(FXMonthViewCommands));
        public static readonly RoutedCommand SelectEmptyDate = new RoutedCommand("FXMonthViewCommands.SelectEmptyDate", typeof(FXMonthViewCommands));
    }
}