using System;
using System.ComponentModel;
using DevExpress.Data.Mask;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Win.DevExpress
{
    public class PersianDateTimeMaskManager : MaskManagerPlainText
    {
    }

    public class XtraFADateEdit : DateEdit
    {
        public const string EditorName = "XtraFADateEdit";

        static XtraFADateEdit()
        {
            Register();
        }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(XtraFADateEdit), typeof(RepositoryItemXtraFADateEdit), typeof(DateEditViewInfo), new ButtonEditPainter(), true, EditImageIndexes.DateEdit));
        }

        public override string EditorTypeName
        {
            get { return EditorName; }
        }

        protected override MaskManager CreateMaskManager(MaskProperties mask)
        {
            return new PersianDateTimeMaskManager();
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            if (!CultureHelper.IsFarsiCulture) return base.CreatePopupForm();

            if (Properties.CalendarView == CalendarView.TouchUI) return new FATouchPopupDateEditForm(this);

            return new VistaPopupPersianDateEditForm(this);
        }
    }

    public class PersianDateEditFormatInfo : FormatInfo
    {
        private const string format = "yyyy/MM/dd";

        public PersianDateEditFormatInfo(IComponentLoading componentLoading) : base(componentLoading)
        {
            FormatType = FormatType.DateTime;
            FormatString = format;
        }
        public PersianDateEditFormatInfo()
        {
            FormatType = FormatType.DateTime;
            FormatString = format;
        }
        protected override void ResetFormatType()
        {
            FormatType = FormatType.DateTime;
        }
        public override bool ShouldSerialize()
        {
            return FormatType != FormatType.DateTime;
        }
        protected override bool ShouldSerializeFormatString()
        {
            return FormatString != format;
        }
        protected override void ResetFormatString()
        {
            FormatString = format;
        }
    }


    [UserRepositoryItem("Register")]
    public class RepositoryItemXtraFADateEdit : RepositoryItemDateEdit
    {
        [Browsable(false)]
        public override string EditorTypeName
        {
            get { return XtraFADateEdit.EditorName; }
        }

        public RepositoryItemXtraFADateEdit()
        {
            NullDateCalendarValue = PersianDate.MinValue;
        }

        static RepositoryItemXtraFADateEdit()
        {
            Register();
        }

        protected override FormatInfo CreateDisplayFormat()
        {
            return new PersianDateEditFormatInfo();
        }
        protected override FormatInfo CreateEditFormat()
        {
            return new PersianDateEditFormatInfo();
        }

        protected override DateEditValueConverter CreateConverter()
        {
            return new PersianDateEditValueConverter(this);
        }

        protected override DateTime ConvertToDateTime(object val)
        {
            return ((PersianDateEditValueConverter)Converter).ConvertToDateTime(val);
        }

        protected override bool IsNullValue(object editValue)
        {
            if (base.IsNullValue(editValue))
                return true;

            var dt = ConvertToDateTime(editValue);
            if (dt.Equals(NullDate) || dt.Equals(PersianDate.MinValue))
                return true;

            if (editValue != null)
                return editValue.Equals(NullDate);

            return false;
        }

        public static void Register()
        {
            XtraFADateEdit.Register();
        }

        protected new XtraFADateEdit OwnerEdit
        {
            get { return base.OwnerEdit as XtraFADateEdit; }
        }

        public override string GetDisplayText(object editValue)
        {
            if (editValue is DateTime)
            {
                DateTime dt = (DateTime)editValue;

                if (PersianCalendar.IsWithInSupportedRange(dt))
                {
                    PersianDate pd = new PersianDate(dt);
                    return FormatDisplayText(pd);
                }
            }

            return string.Empty;
        }

        protected virtual string FormatDisplayText(object value)
        {
            if (value is PersianDate)
            {
                PersianDate pd = (PersianDate)value;
                return pd.ToString("d");
            }

            if (value is DateTime)
            {
                DateTime dt = (DateTime)value;
                return dt.Date.ToShortDateString();
            }

            return NullText;
        }
    }

    public class PersianDateEditValueConverter : DateEditValueConverter
    {
        private readonly IDateTimeOwner owner;

        public PersianDateEditValueConverter(IDateTimeOwner owner) : base(owner)
        {
            this.owner = owner;
        }

        public new DateTime ConvertToDateTime(object val)
        {
            var converted = ConvertToObject(owner.DoParseEditValue(val));
            if (converted is DateTime)
                return (DateTime)converted;

            var editValueEventArgs = owner.DoFormatEditValue(converted);
            if (editValueEventArgs.Value is DateTime)
                return (DateTime)editValueEventArgs.Value;

            if (owner.NullDate is DateTime)
                return (DateTime)owner.NullDate;

            return PersianDate.MinValue;
        }
        
        protected override object ConvertToObject(ConvertEditValueEventArgs args)
        {
            var obj = args.Value;
            if (args.Handled)
                return obj;

            if (obj == null || obj == DBNull.Value)
                return null;

            if (obj.Equals(owner.NullDate))
                return null;

            if (obj is string && ((string)obj).Length == 0)
                return null;

            if (obj is DateTime)
            {
                var dt = (DateTime)obj;
                if (!PersianCalendar.IsWithInSupportedRange(dt))
                    return null;

                return dt;
            }

            try
            {
                DateTime result;
                if (DateTime.TryParse(obj.ToString(), out result) &&
                    PersianCalendar.IsWithInSupportedRange(result))
                {
                    return result;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}