using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.Win32;

namespace FarsiLibrary.Win.Controls
{
	/// <summary>
	/// An advanced MessageBox that supports customizations like Font, Icon,
	/// Buttons and Saved Responses
	/// </summary>
	internal class FAMessageBoxForm : Form
	{
		#region Constants

		private const int DEF_LEFT_PADDING = 12;
		private const int DEF_RIGHT_PADDING = 12;
		private const int DEF_TOP_PADDING = 12;
		private const int DEF_BOTTOM_PADDING = 12;
		private const int DEF_BUTTON_LEFT_PADDING = 4;
		private const int DEF_BUTTON_RIGHT_PADDING = 4;
		private const int DEF_BUTTON_TOP_PADDING = 4;
		private const int DEF_BUTTON_BOTTOM_PADDING = 4;
		private const int DEF_MIN_BUTTON_HEIGHT = 23;
		private const int DEF_MIN_BUTTON_WIDTH = 74;
		private const int DEF_ITEM_PADDING = 10;
		private const int DEF_ICON_MESSAGE_PADDING = 15;
		private const int DEF_BUTTON_PADDING = 5;
		private const int DEF_CHECKBOX_WIDTH = 20;

		#endregion

		#region Fields

		private Label _lblMessage;
		private IContainer _components;
		private PictureBox _pbIcon;
		private CheckBox _chbSaveResponse;
		private List<FAMessageBoxButton> _buttons = new List<FAMessageBoxButton>();
		private Button _defaultButton = null;
        private ToolTip buttonToolTip;
        private FAMessageBoxButton _cancelButton = null;
        private MessageBoxIcon _standardIcon = MessageBoxIcon.None;
	    
        private int _maxWidth;
        private int _maxHeight;
		private int _maxLayoutWidth;

        private bool _allowSaveResponse;
        private bool _playSound = true;
		private bool _allowCancel = true;

		private string _result = null;
	    
		#endregion

		#region Props
	    
	    /// <summary>
        /// Message string of the <see cref="FAMessageBox"/> control.
	    /// </summary>
		public string Message
		{
			set { _lblMessage.Text = value; }
            get { return _lblMessage.Text; }
		}

	    /// <summary>
	    /// Caption of the <see cref="FAMessageBox"/> control.
	    /// </summary>
		public string Caption
		{
			set { Text = value; }
            get { return Text; }
		}

	    /// <summary>
	    /// Custom font for the <see cref="CustomFont"/> control.
	    /// </summary>
		public Font CustomFont
		{
			set { Font = value; }
            get { return Font; }
		}

	    /// <summary>
	    /// Buttons of FAMess
	    /// </summary>
	    [Browsable(false)]
		public List<FAMessageBoxButton> Buttons
		{
			get { return _buttons; }
		}

	    /// <summary>
	    /// If set to <para>true</para>, messagebox control will save it's selected value.
	    /// </summary>
		public bool AllowSaveResponse
		{
			get { return _allowSaveResponse; }
			set { _allowSaveResponse = value; }
		}

	    /// <summary>
	    /// User has checked the checkbox control of <see cref="FAMessageBox"/> control.
	    /// </summary>
		public bool SavedResponse
		{
			get { return _chbSaveResponse.Checked; }
		}

	    /// <summary>
	    /// Text response of <see cref="FAMessageBox"/> control.
	    /// </summary>
		public string SaveResponseText
		{
			set { _chbSaveResponse.Text = value; }
            get { return _chbSaveResponse.Text; }
		}

	    /// <summary>
	    /// Icons of the <see cref="FAMessageBox"/> control.
	    /// </summary>
		public MessageBoxIcon StandardIcon
		{
			set 
			{ 
			    if(_standardIcon == value)
                    return;
			    
			    _standardIcon = value;
			    SetStandardIcon();
			}
            get { return _standardIcon; }
		}

	    /// <summary>
	    /// Custom icon that will be displayed in the <see cref="FAMessageBox"/> control.
	    /// </summary>
		public Icon CustomIcon
		{
			set
			{
				_standardIcon = MessageBoxIcon.None;
				_pbIcon.Image = value.ToBitmap();
			}
	        get
	        {
	            return Icon.FromHandle(_pbIcon.Handle);
	        }
		}

	    /// <summary>
	    /// Custom cancel button that will be used as default cancel button.
	    /// </summary>
		public FAMessageBoxButton CustomCancelButton
		{
			set { _cancelButton = value; }
		}

	    /// <summary>
	    /// Result of the <see cref="FAMessageBox"/> control as string.
	    /// </summary>
		public string Result
		{
			get { return _result; }
		}

	    /// <summary>
	    /// Play sound when displaying the <see cref="FAMessageBox"/> control.
	    /// </summary>
		public bool PlaySound
		{
			get { return _playSound; }
			set { _playSound = value; }
		}
	    
		#endregion

		#region Ctor & Dispose
	    
		public FAMessageBoxForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_maxWidth = (int)(SystemInformation.WorkingArea.Width * 0.60);
			_maxHeight = (int)(SystemInformation.WorkingArea.Height * 0.90);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_components != null)
					_components.Dispose();
			}
		    
			base.Dispose(disposing);
		}
	    
		#endregion

		#region Windows Form Designer generated code
	    
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._components = new System.ComponentModel.Container();
			this._lblMessage = new System.Windows.Forms.Label();
			this._pbIcon = new System.Windows.Forms.PictureBox();
			this._chbSaveResponse = new System.Windows.Forms.CheckBox();
			this.buttonToolTip = new System.Windows.Forms.ToolTip(this._components);
			this.SuspendLayout();
			// 
			// _lblMessage
			// 
			this._lblMessage.Location = new System.Drawing.Point(64, 8);
			this._lblMessage.Name = "_lblMessage";
			this._lblMessage.Size = new System.Drawing.Size(88, 24);
			this._lblMessage.TabIndex = 1;
            this._lblMessage.AutoSize = false;
			this._lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._lblMessage.Visible = false;
			// 
			// _pbIcon
			// 
			this._pbIcon.Location = new System.Drawing.Point(168, 8);
			this._pbIcon.Name = "_pbIcon";
			this._pbIcon.Size = new System.Drawing.Size(32, 32);
			this._pbIcon.TabIndex = 3;
			this._pbIcon.TabStop = false;
			// 
			// _chbSaveResponse
			// 
			this._chbSaveResponse.Location = new System.Drawing.Point(56, 56);
			this._chbSaveResponse.Name = "_chbSaveResponse";
		    this._chbSaveResponse.TextAlign = ContentAlignment.MiddleLeft;
			this._chbSaveResponse.Size = new System.Drawing.Size(104, 16);
			this._chbSaveResponse.TabIndex = 0;
			// 
			// FAMessageBoxForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(210, 103);
			this.Controls.Add(this._chbSaveResponse);
			this.Controls.Add(this._pbIcon);
			this.Controls.Add(this._lblMessage);
			this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FAMessageBoxForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.ResumeLayout(false);

		}

		#endregion

		#region Overrides
	    
		protected override void OnLoad(EventArgs e)
		{
			//Reset result
			_result = null;

			Size = new Size(_maxWidth, _maxHeight);

			//This is the rectangle in which all items will be layed out
			_maxLayoutWidth = ClientSize.Width - DEF_LEFT_PADDING - DEF_RIGHT_PADDING;
			//_maxLayoutHeight = ClientSize.Height - DEF_TOP_PADDING - DEF_BOTTOM_PADDING;

			AddOkButtonIfNoButtonsPresent();
			DisableCloseIfMultipleButtonsAndNoCancelButton();

			SetIconSizeAndVisibility();
			SetMessageSizeAndVisibility();
			SetCheckboxSizeAndVisibility();

			SetOptimumSize();

			LayoutControls();

		    if(StartPosition == FormStartPosition.Manual)
		        CenterForm();

            if (_playSound)
            {
                switch (_standardIcon)
                {
                    case MessageBoxIcon.Error:
                        User32.MessageBeep((int)MessageBeepType.Error);
                        break;

                    case MessageBoxIcon.Information:
                        User32.MessageBeep((int)MessageBeepType.Information);
                        break;

                    case MessageBoxIcon.Question:
                        User32.MessageBeep((int)MessageBeepType.Question);
                        break;
                        
                    case MessageBoxIcon.Warning:
                        User32.MessageBeep((int)MessageBeepType.Warning);
                        break;

                    default:
                    case MessageBoxIcon.None:
                        User32.MessageBeep((int)MessageBeepType.Ok);
                        break;
                }
            }

			base.OnLoad(e);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((int)keyData == (int)(Keys.Alt | Keys.F4) && !_allowCancel)
				return true;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_result == null)
			{
				if (_allowCancel)
				{
					_result = _cancelButton.Value;
				}
				else
				{
					e.Cancel = true;
					return;
				}
			}

			base.OnClosing(e);
		}

		protected override void OnActivated(EventArgs e)
		{
			if (_defaultButton != null)
				_defaultButton.Select();
		
		    base.OnActivated(e);
		}

		#endregion

		#region Methods
	    
		/// <summary>
		/// Measures DrawTab string using the Graphics object for this form with
		/// the specified font
		/// </summary>
		/// <param name="str">The string to measure</param>
		/// <param name="maxWidth">The maximum DEF_BUTTON_WIDTH available to display the string</param>
		/// <param name="font">The font with which to measure the string</param>
		/// <returns></returns>
		private Size MeasureString(string str, int maxWidth, Font font)
		{
            SizeF strRectSizeF;
            using (Graphics g = CreateGraphics())
                strRectSizeF = g.MeasureString(str, font, maxWidth);
            strRectSizeF.Width += 20;
            if (strRectSizeF.Height != 0)
                strRectSizeF.Height += 5;
			
		    return new Size((int)Math.Ceiling(strRectSizeF.Width), (int)Math.Ceiling(strRectSizeF.Height));
		}

		/// <summary>
		/// Measures DrawTab string using the Graphics object for this form and the
		/// font of this form
		/// </summary>
		/// <param name="str"></param>
		/// <param name="maxWidth"></param>
		/// <returns></returns>
		private Size MeasureString(string str, int maxWidth)
		{
			return MeasureString(str, maxWidth, Font);
		}

		/// <summary>
		/// Gets the longest button text
		/// </summary>
		/// <returns></returns>
		private string GetLongestButtonText()
		{
			int maxLen = 0;
			string maxStr = null;
			foreach (FAMessageBoxButton button in _buttons)
			{
				if (button.Text != null && button.Text.Length > maxLen)
				{
					maxLen = button.Text.Length;
					maxStr = button.Text;
				}
			}

			return maxStr;
		}

		/// <summary>
		/// Sets the size and visibility of the Message
		/// </summary>
		private void SetMessageSizeAndVisibility()
		{
			if (string.IsNullOrEmpty(_lblMessage.Text))
			{
				_lblMessage.Size = Size.Empty;
				_lblMessage.Visible = false;
			}
			else
			{
				int maxWidth = _maxLayoutWidth;
				if (_pbIcon.Size.Width != 0)
					maxWidth -= _pbIcon.Size.Width + DEF_ICON_MESSAGE_PADDING;

				Size messageRectSize = MeasureString(_lblMessage.Text, maxWidth);
                _lblMessage.Size = messageRectSize;
				_lblMessage.Height = Math.Max(_pbIcon.Height, _lblMessage.Height);

				_lblMessage.Visible = true;
			}
		}

		/// <summary>
		/// Sets the size and visibility of the Icon
		/// </summary>
		private void SetIconSizeAndVisibility()
		{
			if (_pbIcon.Image == null)
			{
				_pbIcon.Visible = false;
				_pbIcon.Size = Size.Empty;
			}
			else
			{
				_pbIcon.Size = new Size(32, 32);
				_pbIcon.Visible = true;
			}
		}

		/// <summary>
		/// Sets the size and visibility of the save response checkbox
		/// </summary>
		private void SetCheckboxSizeAndVisibility()
		{
			if (!AllowSaveResponse)
			{
				_chbSaveResponse.Visible = false;
				_chbSaveResponse.Size = Size.Empty;
			}
			else
			{
				Size saveResponseTextSize = MeasureString(_chbSaveResponse.Text, _maxLayoutWidth);
				saveResponseTextSize.Width += DEF_CHECKBOX_WIDTH;
				_chbSaveResponse.Size = saveResponseTextSize;
				_chbSaveResponse.Visible = true;
			}
		}

		/// <summary>
		/// Calculates the button size based on the text of the longest
		/// button text
		/// </summary>
		/// <returns></returns>
		private Size GetButtonSize()
		{
			string longestButtonText = GetLongestButtonText();
			if (longestButtonText == null)
			{
				//TODO:Handle this case
			}

			Size buttonTextSize = MeasureString(longestButtonText, _maxLayoutWidth);
			Size buttonSize = new Size(buttonTextSize.Width + DEF_BUTTON_LEFT_PADDING + DEF_BUTTON_RIGHT_PADDING,
				buttonTextSize.Height - 5 + DEF_BUTTON_TOP_PADDING + DEF_BUTTON_BOTTOM_PADDING);

			if (buttonSize.Width < DEF_MIN_BUTTON_WIDTH)
				buttonSize.Width = DEF_MIN_BUTTON_WIDTH;
			if (buttonSize.Height < DEF_MIN_BUTTON_HEIGHT)
				buttonSize.Height = DEF_MIN_BUTTON_HEIGHT;

			return buttonSize;
		}

		/// <summary>
		/// Gets a <see cref="SystemIcons"/> instance based on the selected <see cref="MessageBoxIcon"/> value.
		/// </summary>
		private void SetStandardIcon()
		{
			switch (_standardIcon)
			{
				case MessageBoxIcon.Exclamation:
                    _pbIcon.Image = SystemIcons.Exclamation.ToBitmap();
                    break;
				case MessageBoxIcon.Question:
                    _pbIcon.Image = SystemIcons.Question.ToBitmap();
			        break;
			    case MessageBoxIcon.Error:
                    _pbIcon.Image = SystemIcons.Error.ToBitmap();
			        break;
			    case MessageBoxIcon.Information:
                    _pbIcon.Image = SystemIcons.Information.ToBitmap();
			        break;
			    case MessageBoxIcon.None:
                    _pbIcon.Image = null;
			        break;
			}
		}

		private void AddOkButtonIfNoButtonsPresent()
		{
			if (_buttons.Count == 0)
				_buttons.Add(new FAMessageBoxButton(FAMessageBoxResult.GetFAMessageBoxButton("OK"), FAMessageBoxButtons.Ok.ToString()));
		}


		/// <summary>
		/// Centers the form on the screen
		/// </summary>
		private void CenterForm()
		{
			int x = (SystemInformation.WorkingArea.Width - Width) / 2;
			int y = (SystemInformation.WorkingArea.Height - Height) / 2;

			Location = new Point(x, y);
		}

		/// <summary>
		/// Sets the optimum size for the form based on the controls that
		/// need to be displayed
		/// </summary>
		private void SetOptimumSize()
		{
			int ncWidth = Width - ClientSize.Width;
			int ncHeight = Height - ClientSize.Height;

			int iconAndMessageRowWidth = _lblMessage.Width + DEF_ICON_MESSAGE_PADDING + _pbIcon.Width;
			int saveResponseRowWidth = _chbSaveResponse.Width + (_pbIcon.Width / 2);
			int buttonsRowWidth = GetWidthOfAllButtons();
			int captionWidth = GetCaptionWidth().Width;

			int maxItemWidth = Math.Max(saveResponseRowWidth, Math.Max(iconAndMessageRowWidth, buttonsRowWidth));

			int requiredWidth = DEF_LEFT_PADDING + maxItemWidth + DEF_RIGHT_PADDING + ncWidth;
			//Since Caption DEF_BUTTON_WIDTH is not client DEF_BUTTON_WIDTH, we do the check here
			if (requiredWidth < captionWidth)
				requiredWidth = captionWidth;

			int requiredHeight = DEF_TOP_PADDING + Math.Max(_lblMessage.Height, _pbIcon.Height) + DEF_ITEM_PADDING + _chbSaveResponse.Height + DEF_ITEM_PADDING + GetButtonSize().Height + DEF_BOTTOM_PADDING + ncHeight;

			int height = Math.Min(requiredHeight, _maxHeight);
			int width = Math.Min(requiredWidth, _maxWidth);
			Size = new Size(width, height);
		}

		/// <summary>
		/// Returns the DEF_BUTTON_WIDTH that will be occupied by all buttons including
		/// the inter-button padding
		/// </summary>
		private int GetWidthOfAllButtons()
		{
			Size buttonSize = GetButtonSize();
			int allButtonsWidth = buttonSize.Width * _buttons.Count + DEF_BUTTON_PADDING * (_buttons.Count - 1);

			return allButtonsWidth;
		}

		/// <summary>
		/// Gets the DEF_BUTTON_WIDTH of the caption
		/// </summary>
		private Size GetCaptionWidth()
		{
			Font captionFont = GetCaptionFont();
			if (captionFont == null)
			{
				//some error occured while determining system font
				captionFont = new Font("Tahoma", 9);
			}

			int availableWidth = _maxWidth - SystemInformation.CaptionButtonSize.Width - SystemInformation.Border3DSize.Width * 2;
			Size captionSize = MeasureString(Text, availableWidth, captionFont);

			captionSize.Width += SystemInformation.CaptionButtonSize.Width + SystemInformation.Border3DSize.Width * 2;
			return captionSize;
		}

		/// <summary>
		/// Layout all the controls 
		/// </summary>
		private void LayoutControls()
		{
			if (RightToLeft == RightToLeft.Yes)
			{
				_pbIcon.Location = new Point(ClientSize.Width - _pbIcon.Width - DEF_RIGHT_PADDING, DEF_TOP_PADDING);
				_lblMessage.Location = new Point(_pbIcon.Left -_lblMessage.Size.Width - 5 + DEF_RIGHT_PADDING - DEF_ICON_MESSAGE_PADDING, DEF_TOP_PADDING);
				_chbSaveResponse.Location = new Point(ClientSize.Width - _chbSaveResponse.Width - _pbIcon.Width, DEF_TOP_PADDING + Math.Max(_pbIcon.Height, _lblMessage.Height) + DEF_ITEM_PADDING);
			}
			else
			{
				_pbIcon.Location = new Point(DEF_LEFT_PADDING, DEF_TOP_PADDING);
				_lblMessage.Location = new Point(DEF_LEFT_PADDING + _pbIcon.Width + DEF_ICON_MESSAGE_PADDING, DEF_TOP_PADDING);
				_chbSaveResponse.Location = new Point(DEF_LEFT_PADDING + (_pbIcon.Width / 2), DEF_TOP_PADDING + Math.Max(_pbIcon.Height, _lblMessage.Height) + DEF_ITEM_PADDING);
			}

			Size buttonSize = GetButtonSize();
			int allButtonsWidth = GetWidthOfAllButtons();

			int firstButtonX = (ClientSize.Width - allButtonsWidth) / 2;
			int firstButtonY = ClientSize.Height - DEF_BOTTOM_PADDING - buttonSize.Height;
			Point nextButtonLocation = new Point(firstButtonX, firstButtonY);

			bool foundDefaultButton = false;
			foreach (FAMessageBoxButton button in _buttons)
			{
				Button buttonCtrl = new Button();
				buttonCtrl.Size = buttonSize;
				buttonCtrl.Text = button.Text;
				buttonCtrl.TextAlign = ContentAlignment.MiddleCenter;
				if (button.HelpText != null && button.HelpText.Trim().Length != 0)
				{
					buttonToolTip.SetToolTip(buttonCtrl, button.HelpText);
				}
				buttonCtrl.Location = nextButtonLocation;
				buttonCtrl.Click += new EventHandler(OnInternalButtonClicked);
				buttonCtrl.Tag = button.Value;
				Controls.Add(buttonCtrl);

				if (!foundDefaultButton)
				{
					_defaultButton = buttonCtrl;
					foundDefaultButton = true;
				}

				nextButtonLocation.X += buttonSize.Width + DEF_BUTTON_PADDING;
			}
		}

		private void DisableCloseIfMultipleButtonsAndNoCancelButton()
		{
			if (_buttons.Count > 1)
			{
				if (_cancelButton != null)
					return;

				//See if standard cancel button is present
				foreach (FAMessageBoxButton button in _buttons)
				{
					if (button.Text == FAMessageBoxButtons.Cancel.ToString() && button.Value == FAMessageBoxButtons.Cancel.ToString())
					{
						_cancelButton = button;
						return;
					}
				}

				//Standard cancel button is not present, Disable
				//close button
				DisableCloseButton(this);
				_allowCancel = false;

			}
			else if (_buttons.Count == 1)
			{
				_cancelButton = _buttons[0];
			}
			else
			{
				//This condition should never get called
				_allowCancel = false;
			}
		}
	    
		#endregion

		#region EventHandlers
	    
		private void OnInternalButtonClicked(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			if (btn == null || btn.Tag == null)
				return;

			_result = btn.Tag as string;

			DialogResult = DialogResult.OK;
		}
	    
		#endregion

		#region Unmanaged Code

	    #region Constants

	    private const int SPI_GETNONCLIENTMETRICS = 41;
	    //private const int LF_FACESIZE = 32;
	    private const int SC_CLOSE = 0xF060;
	    private const int MF_BYCOMMAND = 0x0;
	    private const int MF_GRAYED = 0x1;
	    //private const int MF_ENABLED = 0x0;

	    #endregion

	    #region Private Methods

	    private void DisableCloseButton(Form form)
	    {
	        try
	        {
	            User32.EnableMenuItem(User32.GetSystemMenu(form.Handle, false), SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
	        }
	        catch (Exception)
	        {
	            //System.Console.WriteLine(ex.Message);
	        }
	    }

	    private Font GetCaptionFont()
	    {

	        NONCLIENTMETRICS ncm = new NONCLIENTMETRICS();
	        ncm.cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS));
	        try
	        {
	            bool result = User32.SystemParametersInfo(SPI_GETNONCLIENTMETRICS, ncm.cbSize, ref ncm, 0);

	            if (result)
	            {
	                return Font.FromLogFont(ncm.lfCaptionFont);

	            }
	            else
	            {
	                Marshal.GetLastWin32Error();
	                return null;
	            }
	        }
	        catch (Exception /*ex*/)
	        {
	            //System.Console.WriteLine(ex.Message);
	        }

	        return null;
	    }

	    #endregion
	    
		#endregion
	}
}
