using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// FADatePickerStrip is a wrapper for <see cref="FADatePicker"/> class, which
    /// makes it usable on <see cref="ToolStrip"/> Controls.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class FADatePickerStrip : ToolStripControlHost
    {
        /// <summary>
        /// Creates a new instance of <see cref="FADatePickerStrip"/>.
        /// </summary>
        public FADatePickerStrip() : base(CreateControlInstance())
        {
        }

        /// <summary>
        /// Create the actual control, note this is static so it can be called from the
        /// constructor.
        /// 
        /// </summary>
        /// <returns></returns>
        private static Control CreateControlInstance()
        {
            FADatePicker dp = new FADatePicker();
            
            if(FAThemeManager.UseThemes)
            {
                dp.Theme = ThemeTypes.Office2003;
            }
            else
            {
                dp.Theme = ThemeTypes.Office2000;
            }
            
            return dp;
        }

        /// <summary>
        /// Represents the FADatePicker control that will be displayed by the tool strip.
        /// </summary>
        [Description("Represents the FADatePicker control that will be displayed by the tool strip.")]
        public FADatePicker FADatePicker
        {
            get { return Control as FADatePicker; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get
            {
                return string.Empty;
            }
            set
            {
                base.Text = string.Empty;
            }
        }

        /// <summary>
        /// Determines when to serialize Text value of the control.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeText()
        {
            return false;
        }
    }
}

