using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.BaseClasses
{
    /// <summary>
    /// TextEdit control which emulates a textbox control. 
    /// </summary>
    [ToolboxItem(false)]
    public class TextEditBase : BaseControl
    {
        #region Fields

        internal TextBox TextBox;
        internal RightToLeft rtl;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of TextEditBase class.
        /// </summary>
		public TextEditBase()
		{
			TextBox = new TextBox();
			Width = 120;
			Height = 20;
			
			rtl = RightToLeft.Yes;
			base.BackColor = SystemColors.Window;
            TextBox.RightToLeft = rtl;
			TextBox.BorderStyle = BorderStyle.None;
            TextBox.AutoSize = false;

            TextBox.MouseEnter += TextBox_MouseEnter;
			TextBox.MouseLeave += TextBox_MouseLeave;
			TextBox.GotFocus += TextBox_GotFocus;
			TextBox.LostFocus += TextBox_LostFocus;
			TextBox.SizeChanged += TextBox_SizeChanged;
			TextBox.TextChanged += TextBox_TextChanged;
			TextBox.MouseUp += InvokeMouseUp;
			TextBox.MouseDown += InvokeMouseDown;
			TextBox.MouseEnter += InvokeMouseEnter;
			TextBox.MouseHover += InvokeMouseHover;
			TextBox.MouseLeave += InvokeMouseLeave;
			TextBox.MouseMove += InvokeMouseMove;
			TextBox.KeyDown += InvokeKeyDown;
			TextBox.KeyPress += InvokeKeyPress;
			TextBox.KeyUp += InvokeKeyUp;
			TextBox.Click += InvokeClick;
			TextBox.DoubleClick += InvokeDoubleClick;

            ThemeChanged += OnThemeChanged;
            Controls.Add(TextBox);
		}

		#endregion

        #region Props

        /// <summary>
        /// Checks if the textbox control should be in multiline mode.
        /// </summary>
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.All)]
        public bool Multiline
        {
            get
            {
                return TextBox.Multiline;
            }
            set
            {
                if (TextBox.Multiline == value)
                    return;

                TextBox.Multiline = value;
                OnPropertyChanged("Multiline");

                if (value == false)
                {
                    Height = 20;
                    SetPosTextBox();
                }
            }
        }

        /// <summary>
        /// BackColor of the control.
        /// </summary>
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (base.BackColor == value)
                    return;

                base.BackColor = value;
                TextBox.BackColor = value;
                OnPropertyChanged("BackColor");
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                if (base.ForeColor == value)
                    return;

                base.ForeColor = value;
                TextBox.ForeColor = value;
                OnPropertyChanged("ForeColor");
            }
        }

        /// <summary>
        /// RightToLeft state of the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [DefaultValue(RightToLeft.Yes)]
        [RefreshProperties(RefreshProperties.All)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return rtl;
            }
            set
            {
                if (rtl == value)
                    return;

                rtl = value;
                TextBox.RightToLeft = value;

                OnPropertyChanged("RightToLeft");
                OnRightToLeftChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Selection Text of the textbox control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual string SelectionText
        {
            get
            {
                return TextBox.SelectedText;
            }
            set
            {
                if (TextBox.SelectedText == value)
                    return;

                TextBox.SelectedText = value;
                OnPropertyChanged("SelectionText");
            }
        }

        /// <summary>
        /// SelectionStart of the control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int SelectionStart
        {
            get
            {
                return TextBox.SelectionStart;
            }
            set
            {
                TextBox.SelectionStart = value;
            }
        }

        /// <summary>
        /// Text of the control.
        /// </summary>
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                if (Text == value)
                    return;

                base.Text = value;
                TextBox.Text = value;
            }
        }

        /// <summary>
        /// Font of the control.
        /// </summary>
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (base.Font == value)
                    return;

                base.Font = value;
                TextBox.Font = value;
                OnPropertyChanged("Font");
            }
        }

        /// <summary>
        /// Selection length of the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual int SelectionLength
        {
            get
            {
                return TextBox.SelectionLength;
            }
            set
            {
                TextBox.SelectionLength = value;
            }
        }

        /// <summary>
        /// Sets the control in disabled mode.
        /// </summary>
        [Category("Behavior"), DefaultValue(false)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool IsDisabled
        {
            get
            {
                return base.IsDisabled;
            }
            set
            {
                if (base.IsDisabled == value)
                    return;

                base.IsDisabled = value;

                DisableTextbox(value);
                Repaint();
            }
        }

        /// <summary>
        /// Sets or Gets the control in Readonly mode.
        /// </summary>
        [Category("Behavior"), DefaultValue(false)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool IsReadonly
        {
            get
            {
                return base.IsReadonly;
            }
            set
            {
                if (base.IsReadonly == value)
                    return;

                base.IsReadonly = value;

                ReadonlyTextbox(value);
                Repaint();
            }
        }

        /// <summary>
        /// MaxLength of the TextBox control.
        /// </summary>
        [Category("Behavior"), DefaultValue(32767)]
        public virtual int MaxLength
        {
            get
            {
                return TextBox.MaxLength;
            }
            set
            {
                TextBox.MaxLength = value;
            }
        }

        /// <summary>
        /// HideSelection of the TextBox control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool HideSelection
        {
            get
            {
                return TextBox.HideSelection;
            }
            set
            {
                TextBox.HideSelection = value;
            }
        }

        /// <summary>
        /// Background Image of the control.
        /// </summary>
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or Sets the control in Focused state.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override bool IsFocused
        {
            get
            {
                if (!Focused)
                {
                    return TextBox.Focused;
                }
                return true;
            }
            set
            {
                if (base.IsFocused == value)
                    return;

                base.IsFocused = value;

                if (value)
                {
                    TextBox.Focus();
                }
            }
        }

        #endregion

        #region Methods

        protected virtual Rectangle GetContentRect()
        {
            return new Rectangle(2, 2, Width - 2, Height - 4);
        }

        private void ReadonlyTextbox(bool isReadonly)
        {
            if (isReadonly)
            {
                TextBox.ReadOnly = true;

                //Note : Textboxes on Windows XP have white backgrounds
                if (Theme != ThemeTypes.WindowsXP)
                {
                    TextBox.BackColor = SystemColors.Control;
                }
                else
                {
                    TextBox.BackColor = BackColor;
                }
            }
            else
            {
                if (!IsDisabled)
                {
                    TextBox.BackColor = BackColor;
                }

                TextBox.ReadOnly = false;
            }
        }

        private void DisableTextbox(bool isDisabled)
        {
            if (isDisabled)
            {
                TextBox.Enabled = false;

                if (Theme != ThemeTypes.WindowsXP)
                {
                    TextBox.BackColor = SystemColors.Control;
                }
                else
                {
                    TextBox.BackColor = BackColor;
                }
            }
            else
            {
                if (!IsReadonly)
                {
                    TextBox.BackColor = BackColor;
                }

                TextBox.Enabled = true;
            }
        }

        #endregion

        #region Invoke Event Methods

        private void InvokeClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void InvokeDoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        private void InvokeKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void InvokeKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void InvokeKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void InvokeMouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void InvokeMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void InvokeMouseHover(object sender, EventArgs e)
        {
            IsHot = true;
            OnMouseHover(e);
        }

        private void InvokeMouseLeave(object sender, EventArgs e)
        {
            IsHot = false;
            OnMouseLeave(e);
        }

        private void InvokeMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void InvokeMouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle clipRect = new Rectangle(0, 0, Width, Height);
            PaintEventArgs args = new PaintEventArgs(e.Graphics, clipRect);

            if (IsDisabled || IsReadonly)
            {
                OnDrawDisabledBackground(args);
            }

            if (UseThemes && Theme == ThemeTypes.WindowsXP)
            {
                OnDrawXPTextBoxBorder(args);
            }
            else if (UseThemes && (Theme == ThemeTypes.Office2003 || Theme == ThemeTypes.Office2007))
            {
                OnDrawOffice2003Border(args);
            }
            else
            {
                OnDrawNormalTextBoxBorder(args);
            }

            OnDrawButtons(args);

            base.OnPaint(e);
        }

        protected virtual void OnDrawDisabledBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Control, e.ClipRectangle);
        }

        protected virtual void OnDrawButtons(PaintEventArgs e)
        {
        }

        private void OnDrawOffice2003Border(PaintEventArgs e)
        {
            if (IsDisabled || IsReadonly)
            {
                e.Graphics.FillRectangle(SystemBrushes.Control, e.ClipRectangle);
                e.Graphics.DrawRectangle(SystemPens.ControlDark, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1));
            }
            else if ((IsHot || IsFocused || Theme == ThemeTypes.Office2007) && !IsReadonly && !IsDisabled)
            {
                Color c = Office2003Colors.Default[Office2003Color.NavBarBackColor2];

                using (Pen p = new Pen(c))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1));
                }
            }
        }

        private void OnDrawXPTextBoxBorder(PaintEventArgs e)
        {
            Rectangle clipRect = e.ClipRectangle;
            VisualStyleRenderer renderer = null;

            if (IsDisabled || IsReadonly)
            {
                e.Graphics.FillRectangle(SystemBrushes.Control, clipRect);
            }

            if (IsDisabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.ReadOnly);
            }
            else if (IsReadonly)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.ReadOnly);
            }
            else if (IsHot && !IsReadonly && !IsDisabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot);
            }
            else if (IsFocused && !IsReadonly && !IsDisabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Focused);
            }

            if (renderer == null)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
            }

            renderer.DrawBackground(e.Graphics, clipRect);
        }

        private void OnDrawNormalTextBoxBorder(PaintEventArgs e)
        {
            Rectangle clipRect = e.ClipRectangle;
            Rectangle contentRect = GetContentRect();

            if (IsReadonly)
            {
                IsHot = false;
            }

            if (IsDisabled || IsReadonly)
            {
                e.Graphics.FillRectangle(SystemBrushes.Control, contentRect);
            }
            else
            {
                using (SolidBrush br = new SolidBrush(BackColor))
                {
                    e.Graphics.FillRectangle(br, contentRect);
                }
            }

            ControlPaint.DrawBorder3D(e.Graphics, clipRect, Border3DStyle.Sunken);
        }

        #endregion

        #region Overrides

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            TextBox.RightToLeft = rtl;
            SetPosTextBox();
            Repaint();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            //TextBox.BackColor = BackColor;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            TextBox.Focus();
            base.OnGotFocus(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetPosTextBox();
            base.OnSizeChanged(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            TextBox.Text = Text;
        }

        protected virtual void SetPosTextBox(Rectangle content)
        {
            try
            {
                if (UseThemes && (Theme == ThemeTypes.WindowsXP || Theme == ThemeTypes.Office2003))
                {
                    TextBox.Top = (Height - TextBox.Height + 2) / 2;
                    TextBox.Size = new Size(content.Width - 4, content.Height);
                    TextBox.Left = content.Left;
                }
                else
                {
                    TextBox.Top = (Height + 2 - TextBox.Height) / 2;
                    TextBox.Size = new Size(content.Width - 3, content.Height);
                    TextBox.Left = content.Left;
                }
            }
            catch { }

            Repaint();
        }

        protected virtual void SetPosTextBox()
        {
            SetPosTextBox(GetContentRect());
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            Repaint();
            InvokeGotFocus(this, e);
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            Repaint();
            InvokeLostFocus(this, e);
        }

        private void TextBox_MouseEnter(object sender, EventArgs e)
        {
            IsHot = true;
            Repaint();
        }

        private void TextBox_MouseLeave(object sender, EventArgs e)
        {
            IsHot = ClientRectangle.Contains(PointToClient(Cursor.Position));
            Repaint();
        }

        private void TextBox_SizeChanged(object sender, EventArgs e)
        {
            SetPosTextBox();
            Repaint();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Text = TextBox.Text;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            SetPosTextBox();
            ReadonlyTextbox(IsReadonly);
            DisableTextbox(IsDisabled);
        }

        #endregion
    }
}
