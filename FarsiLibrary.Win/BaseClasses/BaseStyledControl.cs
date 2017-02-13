using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Windows.Forms;
using FarsiLibrary.Win.Design;
using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.BaseClasses
{
    /// <summary>
    /// Base class for all controls, which provides painting functionality bases on selected theme.
    /// </summary>
    [ToolboxItem(false)]
    public class BaseStyledControl : Control
    {
        #region Fields

        private ThemeTypes theme;
        private int lockUpdate;
        private StringFormat format;
        public const int ControlWidth = 166;
        public const int ControlHeight = 166;

        #endregion

        #region Events

        /// <summary>
        /// Fired when current theme changes.
        /// </summary>
        public event EventHandler ThemeChanged;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of BaseStyledControl class.
        /// </summary>
        public BaseStyledControl()
        {
            // Set painting style for better performance.
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            format = new StringFormat();

            FAThemeManager.ManagerThemeChanged += OnInternalManagerThemeChanged;

            if (UseThemes)
            {
                //NOTE : Added due to backward compatibility.
                //NOTE : This calls the initialization of office colors
                var colors = Office2003Colors.Default;
            }

            if (!DesignMode && FAThemeManager.UseGlobalThemes)
                Theme = FAThemeManager.Theme;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Locks the control for update.
        /// </summary>
        public void BeginUpdate()
        {
            lockUpdate++;
        }

        /// <summary>
        /// Removes a update lock from control.
        /// </summary>
        public void EndUpdate()
        {
            lockUpdate--;
        }

        /// <summary>
        /// Cancels all previous locks on the control. Does NOT repaint the control.
        /// </summary>
        public void CancelUpdate()
        {
            lockUpdate = 0;
        }

        /// <summary>
        /// Decides if the user is updatable or in lock mode.
        /// </summary>
        [Browsable(false)]
        public bool CanUpdate
        {
            get { return lockUpdate == 0; }
        }

        /// <summary>
        /// Invalidate and repaints the control if it is not in lock mode.
        /// </summary>
        public void Repaint()
        {
            if (CanUpdate)
                Invalidate();
        }

        /// <summary>
        /// Painter object which helps control paint itself on the screen, based on the current selected theme.
        /// </summary>
        [Browsable(false)]
        public IFAPainter Painter
        {
            get { return FAPainterFactory.GetPainter(this); }
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            Invalidate();
        }

        private void OnInternalManagerThemeChanged(object sender, EventArgs e)
        {
            Theme = FAThemeManager.Theme;
        }

        #endregion

        #region Props

        /// <summary>
        /// Determines if the control is in enable / disabled state.
        /// </summary>
        [DefaultValue(true)]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                Refresh();
            }
        }

        /// <summary>
        /// Displays the about form of the control when in Design-Mode.
        /// </summary>
        [DesignOnly(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ParenthesizePropertyName(true)]
        [Editor(typeof(AboutDialogEditor), typeof(UITypeEditor))]
        public object About
        {
            get { return null; }
        }

        [Browsable(false)]
        internal bool IsRightToLeft
        {
            get
            {
                return RightToLeft == RightToLeft.Yes;
            }
        }

        /// <summary>
        /// Checks if the control can paint itself using styles. Styles are only available on WindowsXP or 
        /// greater, and should be enabled by the developer, using <see cref="Application.RenderWithVisualStyles">RenderWithVisualStyles</see> property of <see cref="Application">Application</see> class.
        /// </summary>
        [Browsable(false)]
        public virtual bool UseThemes
        {
            get { return FAThemeManager.UseThemes; }
        }

        /// <summary>
        /// Currently selected theme.
        /// </summary>
        [DefaultValue(typeof(ThemeTypes), "Office2000")]
        public ThemeTypes Theme
        {
            get 
            {
                if (UseThemes == false)
                    theme = ThemeTypes.Office2000;

                return theme; 
            }
            set
            {
                if (theme == value)
                    return;

                if (!UseThemes)
                    theme = ThemeTypes.Office2000;
                else
                    theme = value;

                OnThemeChanged(EventArgs.Empty);
            }
        }

        [Browsable(false)]
        internal StringFormat OneLineNoTrimming
        {
            get
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                format.Trimming = StringTrimming.None;
                format.FormatFlags = StringFormatFlags.LineLimit;
                format.HotkeyPrefix = HotkeyPrefix.Show;

                return format;
            }
        }

        #endregion

        #region Theme Selection

        protected bool IsVista 
        {
            get { return Environment.OSVersion.Version.Major == 6; }
        }

        #endregion

        #region Protected Methods

        protected virtual void OnThemeChanged(EventArgs e)
        {
            if (ThemeChanged != null)
                ThemeChanged(this, e);

            Repaint();
        }

        #endregion

        #region Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode && Theme != FAThemeManager.Theme && FAThemeManager.UseGlobalThemes)
                Theme = FAThemeManager.Theme;
        }
        
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                format?.Dispose();
                format = null;
            }

            base.Dispose(disposing);
        }
        
        #endregion
    }
}
