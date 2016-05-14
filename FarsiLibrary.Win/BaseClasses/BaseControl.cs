using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using FarsiLibrary.Localization;
using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.BaseClasses
{
    /// <summary>
    /// Base Control for all UI controls which adds base functionality, events and properties to inheriting controls.
    /// </summary>
    [ToolboxItem(false)]
	public abstract class BaseControl : BaseStyledControl, INotifyPropertyChanged
	{
		#region Fields

		private bool isReadOnly;
        private bool isDisabled;
		private bool isHot;
		private bool isFocused;
        private bool isDefault;
        private bool isPressed;
        private bool allowWrap = true;
        
		private readonly ErrorProvider error;
		private Font font;
        private TextAlignment textHorizontalAlignment;
        private TextAlignment textVerticalAlignment;

		#endregion

		#region Ctor & Dispose


        /// <summary>
        /// Creates a new instance of BaseControl class.
        /// </summary>
        protected BaseControl()
        {
            error = new ErrorProvider();
            error.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
            error.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            error.SetIconPadding(this, 5);

            font = new Font("TAHOMA", 8, FontStyle.Regular);
            allowWrap = true;

            textHorizontalAlignment = TextAlignment.Default;
            textVerticalAlignment = TextAlignment.Default;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

		#endregion

    	#region Props
        
        /// <summary>
        /// Specifies to use WordWrapping displaying the Text of the control.
        /// </summary>
        [DefaultValue(true)]
        public bool WordWrap
        {
            get
            {
                return allowWrap;
            }
            set
            {
                if (allowWrap == value)
                    return;

                allowWrap = value;
                OnPropertyChanged("WordWrap");
                Repaint();
            }
        }

        /// <summary>
        /// Specifies Horizontal Alignment of the Text of the control.
        /// </summary>
        [DefaultValue(typeof(TextAlignment), "Default")]
        public TextAlignment TextHorizontalAlignment
        {
            get
            {
                return textHorizontalAlignment;
            }
            set
            {
                if (textHorizontalAlignment == value)
                    return;

                textHorizontalAlignment = value;
                OnPropertyChanged("TextHorizontalAlignment");
                Repaint();
            }
        }

        /// <summary>
        /// Specifies Vertical Alignment of the Text of the control.
        /// </summary>
        [DefaultValue(typeof(TextAlignment), "Default")]
        public TextAlignment TextVerticalAlignment
        {
            get
            {
                return textVerticalAlignment;
            }
            set
            {
                if (textVerticalAlignment == value)
                    return;

                textVerticalAlignment = value;
                OnPropertyChanged("TextVerticalAlignment");
                Repaint();
            }
        }

        /// <summary>
        /// Specifies if the contorl is the default control.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(false)]
        public virtual bool IsDefault
        {
            get
            {
                return isDefault;
            }
            set
            {
                if (IsDefault == value)
                    return;

                isDefault = value;
                OnPropertyChanged("IsDefault");
            }
        }

        /// <summary>
        /// Specifies if the control is in Pressed state.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(false)]
        public virtual bool IsPressed
        {
            get
            {
                return isPressed;
            }
            set
            {
                if (IsPressed == value)
                    return;

                isPressed = value;
                OnPropertyChanged("IsPressed");
            }
        }

        /// <summary>
        /// Specifies if the control is in Focused state.
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public virtual bool IsFocused
        {
            get
            {
                return isFocused;
            }
            set
            {
                if (isFocused == value)
                    return;

                isFocused = value;
                OnPropertyChanged("IsFocused");
            }
        }

        /// <summary>
        /// Specifies if the control is in Readonly state.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool IsReadonly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                if (isReadOnly == value)
                    return;

                isReadOnly = value;
                OnPropertyChanged("IsReadonly");
                Repaint();
            }
        }

        /// <summary>
        /// Specifies if the control is in Hot state and mouse is over the control.
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public virtual bool IsHot
        {
            get
            {
                return isHot;
            }
            set
            {
                if (isHot == value)
                    return;

                isHot = value;
                OnPropertyChanged("IsHot");
            }
        }

        /// <summary>
        /// Specifies if the control is disabled.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool IsDisabled
        {
            get
            {
                return isDisabled;
            }
            set
            {
                if (isDisabled == value)
                    return;

                isDisabled = value;
                OnPropertyChanged("IsDisabled");
                Repaint();
            }
        }

        /// <summary>
        /// Internal error provider of the control which displays the errors.
        /// </summary>
        [Browsable(false)]
		public ErrorProvider Error
		{
			get { return error; }
		}

        /// <summary>
        /// Checks if the control currently has any errors.
        /// </summary>
		[Browsable(false)]
        [DefaultValue(false)]
		public bool HasErrors
		{
			get { return !string.IsNullOrEmpty(Error.GetError(this)); }
            set
            {
                if(!value)
                {
                    ClearErrors();
                }
                else
                {
                    AddError(FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NotValid));
                }
            }
		}

        /// <summary>
        /// Font of the control.
        /// </summary>
		public new virtual Font Font
		{
			get { return font; }
			set
			{
				if(font == value)
					return;

				font = value;

                OnPropertyChanged("Font");

                Repaint();
			}
		}

        protected virtual StringFormat TextFormat
        {
            get
            {
                var fmt = (StringFormat)StringFormat.GenericDefault.Clone();

                switch (TextHorizontalAlignment)
                {
                    case TextAlignment.Default:
                    case TextAlignment.Center:
                        fmt.Alignment = StringAlignment.Center;
                        break;

                    case TextAlignment.Near:
                        fmt.Alignment = StringAlignment.Near;
                        break;

                    case TextAlignment.Far:
                        fmt.Alignment = StringAlignment.Far;
                        break;
                }

                switch (TextVerticalAlignment)
                {
                    case TextAlignment.Default:
                    case TextAlignment.Center:
                        fmt.LineAlignment = StringAlignment.Center;
                        break;

                    case TextAlignment.Near:
                        fmt.LineAlignment = StringAlignment.Near;
                        break;

                    case TextAlignment.Far:
                        fmt.LineAlignment = StringAlignment.Far;
                        break;
                }

                fmt.HotkeyPrefix = HotkeyPrefix.Show;
                fmt.Trimming = StringTrimming.Word;

                if (RightToLeft == RightToLeft.Yes)
                {
                    fmt.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }

                if (!WordWrap)
                {
                    fmt.FormatFlags |= StringFormatFlags.NoWrap;
                    fmt.Trimming = StringTrimming.EllipsisCharacter;
                }

                return fmt;
            }
        }

		#endregion

		#region Overrides

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            if (RightToLeft == RightToLeft.Yes)
                error.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
            else
                error.SetIconAlignment(this, ErrorIconAlignment.MiddleRight);

            Repaint();
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);

            Office2003Colors.Default.ReinitializeColors();
            Repaint();
        }

        /// <summary>
        /// Refreshes the control if it is not in locked mode. <seealso cref="BaseStyledControl.BeginUpdate" />, <seealso cref="BaseStyledControl.EndUpdate" /> and <seealso cref="BaseStyledControl.CancelUpdate"/> methods.
        /// </summary>
		public override void Refresh() 
		{
			OnLayoutChanged();
            Repaint();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
            Repaint();
		}

        protected override void OnMouseMove(MouseEventArgs e)
        {
            IsHot = true;

            Repaint();
            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            IsHot = true;

            Repaint();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            IsHot = false;

            Repaint();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            Focus();
            IsFocused = true;
            IsPressed = true;

            Repaint();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;

            IsPressed = false;

            Repaint();
        }

        protected override void OnEnter(EventArgs e)
        {
            IsFocused = true;
            IsDefault = true;

            Repaint();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            IsFocused = false;
            IsDefault = false;

            Repaint();
            base.OnLeave(e);
        }

		#endregion

		#region Virtual Methods

        /// <summary>
        /// Adds an error to the control.
        /// </summary>
        /// <param name="errorMsg">The error MSG.</param>
        public virtual void AddError(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
                throw new InvalidOperationException("error message can not be empty");

            Error.SetError(this, errorMsg);
        }

        /// <summary>
        /// Clears the errors.
        /// </summary>
        public virtual void ClearErrors()
        {
            Error.SetError(this, string.Empty);
        }

        /// <summary>
        /// Focuses the next control on the form.
        /// </summary>
        protected virtual void FocusNextControl()
        {
            var f = FindForm();
            if (f != null)
            {
                var ctrl = f.GetNextControl(this, true);
                if (ctrl != null) 
                    ctrl.Focus();
            }
        }

        protected virtual void OnValueValidating(ValueValidatingEventArgs e)
        {
            if (ValueValidating != null)
                ValueValidating(this, e);
        }

        protected virtual void OnReadOnlyStateChanged()
		{
			if(ReadOnlyChanged != null)
				ReadOnlyChanged(this, EventArgs.Empty);
		}

		protected override void OnGotFocus(EventArgs e)
		{
            IsFocused = true;

            Repaint();

			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e)
		{
            IsFocused = false;

            Repaint();

			base.OnLostFocus(e);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta < 0)
			{
				OnPreviousScrollItems(e);
			}
			else
			{
				OnNextScrollItems(e);
			}
		}

		protected virtual void OnNextScrollItems(object sender, KeyEventArgs e)
		{
			if(NextScrollItems != null)
				NextScrollItems(sender, e);
		}

		protected virtual void OnNextScrollItems(MouseEventArgs e)
		{
			if(NextScrollItems != null)
				NextScrollItems(this, e);
		}

		protected virtual void OnPreviousScrollItems(MouseEventArgs e)
		{
			if(PreviousScrollItems != null)
				PreviousScrollItems(this, e);
		}

		protected virtual void OnPreviousScrollItems(object sender, KeyEventArgs e)
		{
			if(PreviousScrollItems != null)
				PreviousScrollItems(sender, e);
		}

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

		#endregion

		#region EventHandler Methods

		protected virtual void OnLayoutChanged() 
		{
			if(!IsHandleCreated || IsDisposed) 
				return;

			if(LayoutChanging != null)
				LayoutChanging(this, EventArgs.Empty);

            Repaint();

			if(LayoutChanged != null)
				LayoutChanged(this, EventArgs.Empty);
		}

		protected virtual void OnValueChanged()
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
        }

		#endregion

		#region Events

        /// <summary>
        /// Fires when control is validating the entered value.
        /// </summary>
        public event ValueValidatingEventHandler ValueValidating;

        /// <summary>
        /// Fires when value of the control is changing.
        /// </summary>
		public event EventHandler ValueChanged;

        /// <summary>
        /// Fires when layout of the control is changed.
        /// </summary>
		public event EventHandler LayoutChanged;

        /// <summary>
        /// Fires when layout of the control is changing.
        /// </summary>
		public event EventHandler LayoutChanging;

        /// <summary>
        /// Fires when user scrolls to next item of the control using MouseWheel.
        /// </summary>
		public event EventHandler NextScrollItems;

        /// <summary>
        /// Fires when user scrolls to previous item of the control using MouseWheel.
        /// </summary>
		public event EventHandler PreviousScrollItems;

        /// <summary>
        /// Fires when readonly state of the control changes.
        /// </summary>
		public event EventHandler ReadOnlyChanged;

		#endregion

        #region Serialization

        /// <summary>
        /// Decides to serialize the Font property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeFont()
        {
            using (var fnt = new Font("Tahoma", 8, FontStyle.Regular))
            {
                return !Font.Equals(fnt);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Fires when every Property of the control changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
