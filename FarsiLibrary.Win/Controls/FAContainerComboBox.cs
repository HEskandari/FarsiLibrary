using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using FarsiLibrary.Win.FAPopup;
using FarsiLibrary.Win.Drawing;
using FarsiLibrary.Win.Events;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.BaseClasses;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// A combo box that can host other controls inside itself and shown them by pressing the dropdown button.
    /// This control already supports three different themes. If you want to use this control, you might want to
    /// look into FADatePicker control which is a sample control using this functionality.
    /// </summary>
    [ToolboxItem(false)]
    public class FAContainerComboBox : DateEditBase
    {
        #region Fields

        private IPopupControl bindedControl;
        private bool readOnly;
        private bool isButtonHot;
        private bool isButtonPressed;

        #endregion

        #region Events

        protected event EventHandler ButtonClick;
        protected event EventHandler PopupShowing;
        protected event EventHandler PopupClosing;
        protected event BindPopupControlEventHandler BindPopupControl;
        
        #endregion

        #region Props

        protected IPopupControl BindedControl
        {
            get { return bindedControl; }
        }

        /// <summary>
        /// Text of the control.
        /// </summary>
        [DefaultValue("")]
        [Browsable(false)]
        [Description("Text of the control.")]
        public new string Text
        {
            get { return TextBox.Text; }
            set
            {
                if (value == TextBox.Text)
                    return;

                TextBox.Text = value;
                OnValueChanged();

                Repaint();
            }
        }

        /// <summary>
        /// Readonly State of the control
        /// </summary>
        [DefaultValue(false)]
        [Description("Readonly State of the control")]
        [Obsolete("This property is obsolete. Use IsReadonly property instead.")]
        public bool Readonly
        {
            get { return readOnly; }
            set
            {
                if (readOnly == value)
                    return;

                readOnly = value;
                OnReadOnlyStateChanged();

                Repaint();
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of FAContainerComboBox class.
        /// </summary>
        public FAContainerComboBox()
        {
            SetStyle(ControlStyles.Selectable, false);
            LostFocus += OnInternalLostFocus;
            TextBox.RightToLeft = base.RightToLeft;
            TextBox.Font = base.Font;
            TextBox.MouseMove += OnDropDownMouseMove;
            TextBox.KeyDown += OnDropDownKeyDown;
        }

        /// <summary>
        /// Disposes the control.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (TextBox != null)
            {
                LostFocus -= OnInternalLostFocus;
                TextBox.MouseMove -= OnDropDownMouseMove;
                TextBox.KeyDown -= OnDropDownKeyDown;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetPosTextBox();
        }
        
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            TextBox.Font = Font;
        }

        protected virtual Rectangle GetButtonRect()
        {
            int buttonWidth = 18;
            Rectangle bounds = GetContentRect();

            if (!UseThemes || Theme == ThemeTypes.Office2000)
            {
                if (RightToLeft != RightToLeft.Yes)
                {
                    return new Rectangle(bounds.Width, bounds.Top, buttonWidth, bounds.Height + 2);
                }

                return new Rectangle(2, bounds.Top, buttonWidth, bounds.Height + 2);
            }
            else
            {
                if (RightToLeft != RightToLeft.Yes)
                {
                    return new Rectangle(bounds.Width + 1, bounds.Top - 1, buttonWidth, bounds.Height + 2);
                }

                return new Rectangle(1, bounds.Top - 1, buttonWidth, bounds.Height + 2);
            }
        }

        protected override Rectangle GetContentRect()
        {
            int buttonWidth = 18;

            if (!UseThemes || Theme == ThemeTypes.Office2000)
            {
                if (RightToLeft != RightToLeft.Yes)
                {
                    return new Rectangle(4, 2, Width - buttonWidth - 2, Height - 6);
                }

                return new Rectangle(buttonWidth + 5, 2, Width - buttonWidth - 7, Height - 6);
            }
            else
            {
                if (RightToLeft != RightToLeft.Yes)
                {
                    return new Rectangle(4, 2, Width - buttonWidth - 2, Height - 4);
                }

                return new Rectangle(buttonWidth + 5, 2, Width - buttonWidth - 7, Height - 4);
            }
        }

        private void OnDropDownMouseMove(object sender, MouseEventArgs e)
        {
            isButtonHot = false;
            Repaint();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (IsMouseOverButton())
            {
                isButtonPressed = true;
                isButtonHot = false;
            }
            else
            {
                isButtonPressed = false;
                isButtonHot = false;
            }

            Repaint();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (IsMouseOverButton() && isButtonPressed && !IsDisabled && !IsReadonly)
            {
                ShowDropDown();
                RaiseButtonClick();
            }

            isButtonPressed = false;
            isButtonHot = false;
            Repaint();
        }

        #endregion

        #region Binding

        protected virtual void OnBindingPopupControl(BindPopupControlEventArgs e)
        {
            if (BindPopupControl != null)
                BindPopupControl(this, e);

            bindedControl = e.BindedControl;
        }

        #endregion

        #region Show/Hide DropDown Window

        /// <summary>
        /// Fires the BindPopupControl and shows the container panel with the control specified in the <see cref="BindPopupControlEventArgs.BindedControl">BindPopupControlEventArgs.BindedControl</see>.
        /// Binded control should implement <see cref="IPopupControl">IPopupControl</see> interface.
        /// </summary>
        public void ShowDropDown()
        {
            if (BindedControl == null)
            {
                OnBindingPopupControl(new BindPopupControlEventArgs(this));
            }

            if (BindedControl != null)
            {
                OnPopupShowing(EventArgs.Empty);
                BindedControl.ShowPopup();
            }
        }

        /// <summary>
        /// Hides the Popup control.
        /// </summary>
        public void HideDropDown()
        {
            if (BindedControl == null)
                return;

            BindedControl.ClosePopup();
        }

        #endregion

        #region EventHandler Methods

        protected virtual void OnPopupClosing(EventArgs e)
        {
            if (PopupClosing != null)
                PopupClosing(this, EventArgs.Empty);
        }

        protected virtual void OnPopupShowing(EventArgs e)
        {
            if (PopupShowing != null)
                PopupShowing(this, EventArgs.Empty);
        }

        protected void RaiseButtonClick()
        {
            if (ButtonClick != null)
                ButtonClick(this, EventArgs.Empty);
        }

        #endregion

        #region DropDown Eventhandlers

        private void OnInternalLostFocus(object sender, EventArgs e)
        {
            HideDropDown();
        }

        private void OnDropDownKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Up))
            {
                HideDropDown();
            }
            if (e.Alt && (e.KeyCode == Keys.Down))
            {
                ShowDropDown();
            }
            else if ((e.Modifiers & (Keys.Shift | Keys.Alt | Keys.Control)) == 0)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    HideDropDown();
                }
                else if (e.KeyCode == Keys.F4)
                {
                    HideDropDown();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ShowDropDown();
                }
            }
        }

        private bool IsMouseOverButton()
        {
            Point point = PointToClient(MousePosition);
            if (GetButtonRect().Contains(point) && !GetContentRect().Contains(point))
                return true;

            return false;
        }

        #endregion

        #region Paint

        protected override void OnDrawButtons(PaintEventArgs e)
        {
            base.OnDrawButtons(e);

            Rectangle rect = GetButtonRect();
            
            if (UseThemes && Theme == ThemeTypes.WindowsXP)
            {
                VisualStyleRenderer renderer;

                if (IsDisabled || IsReadonly)
                {
                    renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Disabled);
                }
                else
                {
                    if (isButtonPressed)
                    {
                        renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Pressed);
                    }
                    else if (isButtonHot)
                    {
                        renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Hot);
                    }
                    else
                    {
                        renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Normal);
                    }
                }

                renderer.DrawBackground(e.Graphics, rect);
            }
            else if (UseThemes && (Theme == ThemeTypes.Office2003 || Theme == ThemeTypes.Office2007))
            {
                if (IsDisabled || IsReadonly)
                {
                    Painter.DrawVerticalArrow(e.Graphics, rect, IsRightToLeft, true, 3);
                }
                else
                {
                    if(isButtonPressed)
                    {
                        Painter.DrawButton(e.Graphics, rect, string.Empty, Font, null, ItemState.Pressed, false, true);
                        Painter.DrawVerticalArrow(e.Graphics, rect, IsRightToLeft, false, 3);

                        Color c = Office2003Colors.Default[Office2003Color.NavBarBackColor2];
                        using (Pen p = new Pen(c))
                        {
                            e.Graphics.DrawLine(p, new Point(IsRightToLeft ? rect.Right : rect.Left, rect.Top), new Point(IsRightToLeft ? rect.Right : rect.Left, rect.Bottom));
                        }
                    }
                    else if(IsHot || IsFocused)
                    {
                        Painter.DrawButton(e.Graphics, rect, string.Empty, Font, null, ItemState.HotTrack, false, true);
                        Painter.DrawVerticalArrow(e.Graphics, rect, IsRightToLeft, false, 3);

                        if (Theme != ThemeTypes.Office2007)
                        {
                            Color c = Office2003Colors.Default[Office2003Color.NavBarBackColor2];
                            using (Pen p = new Pen(c))
                            {
                                e.Graphics.DrawLine(p, new Point(IsRightToLeft ? rect.Right : rect.Left, rect.Top), new Point(IsRightToLeft ? rect.Right : rect.Left, rect.Bottom));
                            }
                        }
                    }
                    else
                    {
                        Painter.DrawButton(e.Graphics, rect, string.Empty, Font, null, ItemState.Normal, false, true);
                        Painter.DrawVerticalArrow(e.Graphics, rect, IsRightToLeft, false, 3);
                    }
                }
            }
            else if (!UseThemes || Theme == ThemeTypes.Office2000)
            {
                if (isButtonPressed)
                {
                    ControlPaint.DrawComboButton(e.Graphics, rect, ButtonState.Pushed);
                }
                else if (isButtonHot)
                {
                    ControlPaint.DrawComboButton(e.Graphics, rect, ButtonState.Normal);
                }
                else
                {
                    ControlPaint.DrawComboButton(e.Graphics, rect, ButtonState.Normal);
                }
            }
        }

        #endregion
    }
}
