using System;
using System.ComponentModel;
using DevExpress.Data.Mask;
using DevExpress.Data.Mask.Internal;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Popups;
using FarsiLibrary.Localization;
using FarsiLibrary.Utils;

namespace FarsiLibrary.Win.DevExpress
{
    public class FAMinsItemsProvider : MinsItemsProvider
    {
        public FAMinsItemsProvider(int count) : base(count)
        {
        }

        protected override IItemPainter CreatePainter(int itemIndex)
        {
            return new FAMinsItemsPainter();
        }
    }

    public class FASecondsItemsProvider : SecondsItemsProvider
    {
        protected override IItemPainter CreatePainter(int itemIndex)
        {
            return new FASecondsItemsPainter();
        }

        public FASecondsItemsProvider(int count) : base(count)
        {
        }
    }

    public class FAHoursItemsProvider : HoursItemsProvider
    {
        public FAHoursItemsProvider(int count) : base(count)
        {
        }

        protected override IItemPainter CreatePainter(int itemIndex)
        {
            return new FAHoursItemsPainter(StartIndex);
        }
    }

    public class FAMeridiemItemsProvider : MeridiemItemsProvider, IItemsProvider
    {
        public FAMeridiemItemsProvider(int count) : base(count)
        {
        }

        IItemPainter IItemsProvider.GetItemPainter(int itemIndex)
        {
            return new FAMeridiemItemsPainter();
        }
    }

    public class FAYearItemsProvider : YearItemsProvider, IItemsProvider
    {
        public FAYearItemsProvider(int count) : base(count)
        {
        }

        IItemPainter IItemsProvider.GetItemPainter(int itemIndex)
        {
            return new FAYearItemsPainter();
        }
    }

    public class FADaysItemsProvider : DaysItemsProvider
    {
        public FADaysItemsProvider(int count) : base(count)
        {
        }

        protected override IItemPainter CreatePainter()
        {
            return new FADaysItemsPainter();
        }
    }

    public class FAMonthItemsProvider : MonthItemsProvider, IItemsProvider
    {
        public FAMonthItemsProvider(int count) : base(count)
        {
        }

        IItemPainter IItemsProvider.GetItemPainter(int itemIndex)
        {
            return new FAMonthItemsPainter();
        }
    }

    public class FAYearItemsPainter : YearItemsPainter
    {
        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            PickItemsPainter painter = new PickItemsPainter();
            string firstString = GetYear(info);
            painter.DrawItem(cache, drawInfo, info, firstString, string.Empty);
        }

        protected string GetYear(PickItemInfo info)
        {
            var container = GetCalendar(info);
            var date = container.GetDateFromIndex(info.Panel.ItemsProvider, info.ItemIndex);
            var year = info.ItemIndex + 1;
            var month = date.Month;
            var day = date.Day;
            PersianDate pd = new DateTime(year, month, day);

            return toFarsi.Convert(pd.Year.ToString());
        }
    }

    public class FAMonthItemsPainter : MonthItemsPainter
    {
        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            var painter = new PickItemsPainter();
            var pd = GetDate(info);
            var monthNo = toFarsi.Convert(pd.Month.ToString());
            var monthName = PersianDateTimeFormatInfo.AbbreviatedMonthGenitiveNames[pd.Month - 1];

            var descriptionIsExist = GetCalendar(info).Handler.ShowTime();
            var firstString = GetCalendar(info).Handler.ShowTime() ? monthNo : monthName;
            var description = descriptionIsExist && painter.ShouldDrawDescription(info) ? monthName : string.Empty;
            painter.DrawItem(cache, drawInfo, info, firstString, description);
        }

        protected PersianDate GetDate(PickItemInfo info)
        {
            var container = GetCalendar(info);
            var date = container.GetDateFromIndex(info.Panel.ItemsProvider, info.ItemIndex);
            var year = date.Year;
            var month = info.ItemIndex + 1;
            var day = date.Day;
            date = new DateTime(year, month, day);

            return date;
        }
    }

    public class FADaysItemsPainter : DaysItemsPainter
    {
        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            var painter = new PickItemsPainter();
            var date = GetDate(info);
            var firstString = toFarsi.Convert(date.Day.ToString());
            var descriptionIsExist = GetCalendar(info).Handler.ShowTime();
            var description = descriptionIsExist && painter.ShouldDrawDescription(info) ? date.LocalizedWeekDayName : string.Empty;

            painter.DrawItem(cache, drawInfo, info, firstString, description);
        }

        protected PersianDate GetDate(PickItemInfo info)
        {
            var container = GetCalendar(info);
            var date = container.GetDateFromIndex(info.Panel.ItemsProvider, info.ItemIndex);
            var year = date.Year;
            var month = date.Month;
            var day = info.ItemIndex + 1;
            date = new DateTime(year, month, day);

            return date;
        }
    }

    public class FAMeridiemItemsPainter : MeridiemItemsPainter
    {
        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            var painter = new PickItemsPainter();
            var descriptionIsExist = GetCalendar(info).Handler.ShowTime();
            var firstString = info.ItemIndex == 0 ? PersianDateTimeFormatInfo.AMDesignator : PersianDateTimeFormatInfo.PMDesignator;

            painter.DrawItem(cache, drawInfo, info, firstString, string.Empty);
        }
    }

    public class FAHoursItemsPainter : HoursItemsPainter
    {
        private readonly int startIndex;
        private readonly BaseLocalizer localizer;

        public FAHoursItemsPainter(int startIndex) : base(startIndex)
        {
            this.startIndex = startIndex;
            this.localizer = Localization.FALocalizeManager.Instance.GetLocalizer();
        }

        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            var painter = new PickItemsPainter();
            var firstString = toFarsi.Convert(painter.ConvertIntToString(info.ItemIndex + startIndex, StringLength));
            var description = painter.ShouldDrawDescription(info) ? localizer.GetLocalizedString(StringID.Hour) : string.Empty;
            painter.DrawItem(cache, drawInfo, info, firstString, description);
        }
    }

    public class FASecondsItemsPainter : SecondsItemsPainter
    {
        private BaseLocalizer localizer;

        public FASecondsItemsPainter()
        {
            this.localizer = Localization.FALocalizeManager.Instance.GetLocalizer();
        }

        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            PickItemsPainter painter = new PickItemsPainter();
            int secondIncrement = 1;
            string firstString = toFarsi.Convert(painter.ConvertIntToString(info.ItemIndex * secondIncrement, StringLength));
            string description = painter.ShouldDrawDescription(info) ? (localizer.GetLocalizedString(StringID.Second)) : string.Empty;
            painter.DrawItem(cache, drawInfo, info, firstString, description);
        }

    }

    public class FAMinsItemsPainter : MinsItemsPainter
    {
        private readonly BaseLocalizer localizer;

        public FAMinsItemsPainter()
        {
            this.localizer = Localization.FALocalizeManager.Instance.GetLocalizer();
        }

        protected override void DrawCore(GraphicsCache cache, PickItemInfo info, IPickItemsContainerDrawInfo drawInfo)
        {
            PickItemsPainter painter = new PickItemsPainter();
            int minuteIncrement = 1;
            string firstString = toFarsi.Convert(painter.ConvertIntToString(info.ItemIndex * minuteIncrement, StringLength));
            string description = painter.ShouldDrawDescription(info) ? localizer.GetLocalizedString(StringID.Minute) : string.Empty;
            painter.DrawItem(cache, drawInfo, info, firstString, description);
        }
    }

    [ToolboxItem(false)]
    public class FADateEditTouchCalendar : DateEditTouchCalendar
    {
        public FADateEditTouchCalendar(FATouchPopupDateEditForm form) : base(form)
        {
        }

        protected override TouchCalendarHandler CreateHandler()
        {
            return new FADateEditTouchCalendarHandler(this);
        }
    }

    public class FADateEditTouchCalendarHandler : DateEditTouchCalendarHandler
    {
        int firstTimeProviderIndex = -1;

        public FADateEditTouchCalendarHandler(IDateTouchCalendarControl calendar) : base(calendar)
        {
        }

        public override void AddNewProvider(DateTimeMaskFormatElementEditable editableFormat)
        {
            if (!ShowTime() && IsTimeProvider(editableFormat))
                return;

            IItemsProvider provider = CreateNewFarsiProvider(editableFormat);
            if (provider != null)
            {
                if (ShouldInsertProvider(provider))
                    Providers.Insert(firstTimeProviderIndex, provider);
                else Providers.Add(provider);

                if (IsTimeProvider(editableFormat))
                {
                    if (firstTimeProviderIndex == -1) firstTimeProviderIndex = Providers.Count - 1;
                    IsTimeProviderAdded = true;
                }

                TotalProviders += 1;
            }
        }

        protected IItemsProvider CreateNewFarsiProvider(DateTimeMaskFormatElementEditable editableFormat)
        {
            if (editableFormat is DateTimeMaskFormatElement_h12)
            {
                FAHoursItemsProvider hoursItemsProvider = new FAHoursItemsProvider(12);
                hoursItemsProvider.StartIndex = 1;
                return hoursItemsProvider;
            }
            if (editableFormat is DateTimeMaskFormatElement_H24)
                return new FAHoursItemsProvider(24);
            if (editableFormat is DateTimeMaskFormatElement_d)
                return new FADaysItemsProvider(31);
            if (editableFormat is DateTimeMaskFormatElement_Min)
                return new FAMinsItemsProvider(60 / GetMinuteIncrement());
            if (editableFormat is DateTimeMaskFormatElement_Month)
                return new FAMonthItemsProvider(12);
            if (editableFormat is DateTimeMaskFormatElement_s)
                return new FASecondsItemsProvider(60 / GetSecondIncrement());
            if (editableFormat is DateTimeMaskFormatElement_Year)
                return new FAYearItemsProvider(9999);
            if (editableFormat is DateTimeMaskFormatElement_AmPm)
                return new FAMeridiemItemsProvider(2);
            return null;
        }
    }

    public class FATouchPopupDateEditForm : TouchPopupDateEditForm
    {
        public FATouchPopupDateEditForm(PopupBaseEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override void CreateTouchCalendar()
        {
            TouchCalendar = new FADateEditTouchCalendar(this);
        }
    }
}