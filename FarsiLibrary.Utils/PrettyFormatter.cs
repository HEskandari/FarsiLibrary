using System;
using System.Collections.Generic;
using System.Linq;
using FarsiLibrary.Resources;

namespace FarsiLibrary.Utils
{
    public class PrettyFormatter : IPrettyFormatter
    {
        private FALocalizeManager localizerManager;
        private List<ConditionalFormatter> PastSecondFormatters;
        private List<ConditionalFormatter> FutureSecondFormatters;
        private List<ConditionalFormatter> PastDayFormatters;
        private List<ConditionalFormatter> FutureDayFormatters;

        public PrettyFormatter(FALocalizeManager localizerManager)
        {
            this.localizerManager = localizerManager;

            CreatePastSecondFormatters();
            CreateFutureSecondFormatters();
            CreatePastDayFormatters();
        }

        public PrettyFormatter()
            : this(FALocalizeManager.Instance)
        {
        }

        private void CreateFutureSecondFormatters()
        {
            
        }

        private void CreateFutureDaysFormatters()
        {

        }

        private void CreatePastSecondFormatters()
        {
            PastSecondFormatters = new List<ConditionalFormatter>
            {
                new ConditionalFormatter(x => x < 60, x => Localizer.GetLocalizedString(StringID.PrettyFormatter_JustNow)),
                new ConditionalFormatter(x => x < 120, x => Localizer.GetLocalizedString(StringID.PrettyFormatter_OneMinuteAgo)),
                new ConditionalFormatter(x => x < 3600, x => string.Format(Localizer.GetLocalizedString(StringID.PrettyFormatter_MinutesAgo), Math.Floor((double) x / 60))),
                new ConditionalFormatter(x => x < 7200, x => Localizer.GetLocalizedString(StringID.PrettyFormatter_OneHourAgo)),
                new ConditionalFormatter(x => x < 86400, x => string.Format(Localizer.GetLocalizedString(StringID.PrettyFormatter_HoursAgo), Math.Floor((double) x / 3600)))
            };
        }

        private void CreatePastDayFormatters()
        {
            PastDayFormatters = new List<ConditionalFormatter>
            {
                new ConditionalFormatter(x => x == 1, x => Localizer.GetLocalizedString(StringID.PrettyFormatter_Yesterday)),
                new ConditionalFormatter(x => x < 7, x => string.Format(Localizer.GetLocalizedString(StringID.PrettyFormatter_DaysAgo), x)),
                new ConditionalFormatter(x => x == 7, x => Localizer.GetLocalizedString(StringID.PrettyFormatter_OneWeekAgo)),
                new ConditionalFormatter(x => x < 31, x => string.Format(Localizer.GetLocalizedString(StringID.PrettyFormatter_WeeksAgo), Math.Ceiling((double) x / 7))),
            };
        }

        public string Format(PersianDate date)
        {
            var dt = date.ToDateTime();
            return Format(dt);
        }

        public string Format(DateTime date)
        {
            var diff = DateTime.Now.Subtract(date);
            var dayDiff = (int)diff.TotalDays;
            var secDiff = (int)diff.TotalSeconds;

            if(dayDiff == 0) //Same day
            {
                return FormatForSameDay(secDiff);
            }
            
            return FormatForOtherDays(dayDiff);
        }

        private string FormatForSameDay(int secondDiff)
        {
            var query = from formatter in PastSecondFormatters 
                        where formatter.Condition(secondDiff) 
                        select formatter.Message(secondDiff);

            return query.FirstOrDefault();
        }

        private string FormatForOtherDays(int dayDiff)
        {
            var query = from formatter in PastDayFormatters
                        where formatter.Condition(dayDiff)
                        select formatter.Message(dayDiff);

            return query.FirstOrDefault();
        }

        private BaseLocalizer Localizer
        {
            get { return localizerManager.GetLocalizer(); }
        }

        private class ConditionalFormatter
        {
            public ConditionalFormatter(Func<int, bool> func, Func<int, string> message)
            {
                Condition = func;
                Message = message;
            }

            public Func<int, bool> Condition { get; private set; }
            public Func<int, string> Message { get; private set; }
        }
    }
}