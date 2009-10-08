using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.Enums;
using System.Drawing;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// FaMonthViewStrip is a wrapper for <see cref="FAMonthView"/> class, which
    /// makes it usable on <see cref="ToolStrip"/> Controls.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class FaMonthViewStrip : ToolStripControlHost
    {
        /// <summary>
        /// Creates a new instance of <see cref="FaMonthViewStrip"/>.
        /// </summary>
        public FaMonthViewStrip() : base(CreateControlInstance())
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
            FAMonthView mv = new FAMonthView(false);

            if (FAThemeManager.UseThemes)
            {
                mv.Theme = ThemeTypes.Office2003;
            }
            else
            {
                mv.Theme = ThemeTypes.Office2000;
            }

            return mv;
        }

        /// <summary>
        /// Returns the <see cref="FAMonthView"/> instance the control is hosting.
        /// </summary>
        [Description("Represents a FAMonthView control that will be displayed by this tool strip.")]
        public FAMonthView FAMonthView
        {
            get
            {
                return Control as FAMonthView;
            }
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

