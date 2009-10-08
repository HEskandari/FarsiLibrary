using System;
using System.Drawing;
using System.Windows.Forms;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Controls
{
	/// <summary>
	/// An extended MessageBox with lot of customizing capabilities.
	/// </summary>
	public class FAMessageBox : IDisposable
	{
		#region Fields

		private readonly FAMessageBoxForm _msgBox = new FAMessageBoxForm();
		private readonly bool _isRightToLeft;
		private bool _useSavedResponse = true;

	    #endregion

		#region Props

		/// <summary>
        /// Gets or Set this property if the control is DrawTab FAMessageBox. If you want the control to appear in english, set this to false.
		/// </summary>
		public bool IsRightToLeft
		{
			get { return _isRightToLeft; }
		}

	    /// <summary>
	    /// Gets or Sets the Name of the <see cref="FAMessageBox"/> control which <see cref="FAMessageBoxManager"/> will use to manage this instance.
	    /// </summary>
	    internal string Name
	    {
	        get; 
            private set;
	    }

	    /// <summary>
		/// Gets or Sets the caption of the message box
		/// </summary>
		public string Caption
		{
			set { _msgBox.Caption = value; }
            get { return _msgBox.Caption; }
		}

		/// <summary>
        /// Gets or Sets the text of the message box
		/// </summary>
		public string Text
		{
			set { _msgBox.Message = value; }
            get { return _msgBox.Message; }
		}

		/// <summary>
        /// Gets or Sets the icon to show in the message box
		/// </summary>
		public Icon CustomIcon
		{
			set { _msgBox.CustomIcon = value; }
            get { return _msgBox.CustomIcon; }
		}

		/// <summary>
		/// Sets the icon to show in the message box
		/// </summary>
		public FarsiMessageBoxExIcon Icon
		{
			set { _msgBox.StandardIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), value.ToString()); }
		}

		/// <summary>
        /// Gets or Sets the font for the text of the message box
		/// </summary>
		public Font Font
		{
			set { _msgBox.Font = value; }
            get { return _msgBox.Font; }
		}

		/// <summary>
        /// Gets or Sets the ability of the user to save the response
		/// </summary>
		public bool AllowSaveResponse
		{
			get { return _msgBox.AllowSaveResponse; }
			set { _msgBox.AllowSaveResponse = value; }
		}

		/// <summary>
        /// Gets or Sets the text to show to the user when saving the response
		/// </summary>
		public string SaveResponseText
		{
			set { _msgBox.SaveResponseText = value; }
            get { return _msgBox.SaveResponseText; }
		}

		/// <summary>
		/// Sets or Gets wether the saved response if available should be used
		/// </summary>
		public bool UseSavedResponse
		{
			get { return _useSavedResponse; }
			set { _useSavedResponse = value; }
		}

		/// <summary>
		/// Sets or Gets wether an alert sound is played while showing the message box.
		/// The sound played depends on the the Icon selected for the message box
		/// </summary>
		public bool PlaySound
		{
			get { return _msgBox.PlaySound; }
			set { _msgBox.PlaySound = value; }
		}

		/// <summary>
		/// Gets the selected value from checkbox.
		/// </summary>
		public bool SavedResponse
		{
			get { return _msgBox.SavedResponse; }
		}
	    
	    /// <summary>
	    /// Start Position
	    /// </summary>
	    public FormStartPosition StartPosition
	    {
            get { return _msgBox.StartPosition; }
            set { _msgBox.StartPosition = value; }
	    }

	    public bool IsDisposed
	    {
	        get; 
            private set;
	    }

	    #endregion

	    #region Methods
	    
	    /// <summary>
	    /// Shows the message box
	    /// </summary>
	    /// <returns></returns>
	    public string Show()
	    {
	        return Show(null);
	    }

	    /// <summary>
	    /// Shows the messsage box with the specified ownerForm
	    /// </summary>
	    /// <param name="owner"></param>
	    /// <returns></returns>
	    public string Show(IWin32Window owner)
	    {
	        _msgBox.RightToLeft = IsRightToLeft ? RightToLeft.Yes : RightToLeft.No;

	        if (_useSavedResponse)
	        {
	            var savedResponse = FAMessageBoxManager.GetSavedResponse(this);
	            if (savedResponse != null)
	                return savedResponse;
	        }

	        if (owner == null)
	        {
	            _msgBox.ShowDialog();
	        }
	        else
	        {
	            _msgBox.ShowDialog(owner);
	        }

	        if (_msgBox.AllowSaveResponse && _msgBox.SavedResponse)
	        {
	            FAMessageBoxManager.SetSavedResponse(this, _msgBox.Result);
	        }
	        else
	        {
	            FAMessageBoxManager.ResetSavedResponse(Name);
	        }

	        return _msgBox.Result;
	    }

	    /// <summary>
	    /// Add DrawTab custom button to the message box
	    /// </summary>
	    /// <param name="button">The button to add</param>
	    public void AddButton(FAMessageBoxButton button)
	    {
	        if (button == null)
	            throw new ArgumentNullException("button", "A null button cannot be added");

	        _msgBox.Buttons.Add(button);

	        if (button.IsCancelButton)
	        {
	            _msgBox.CustomCancelButton = button;
	        }
	    }

	    /// <summary>
	    /// Add DrawTab custom button to the message box
	    /// </summary>
	    /// <param name="text">The text of the button</param>
	    /// <param name="val">The return value in case this button is clicked</param>
	    public void AddButton(string text, string val)
	    {
	        if (text == null)
	            throw new ArgumentNullException("text", "Text of a button cannot be null");

	        if (val == null)
	            throw new ArgumentNullException("val", "Value of a button cannot be null");

	        var button = new FAMessageBoxButton(text, val);
	        AddButton(button);
	    }

	    /// <summary>
	    /// Add DrawTab standard button to the message box
	    /// </summary>
	    /// <param name="button">The standard button to add</param>
	    public void AddButton(FAMessageBoxButtons button)
	    {
	        if (IsRightToLeft)
	            AddButton(FAMessageBoxResult.GetFAMessageBoxButton(button.ToString()), button.ToString().ToUpper());
	        else
	            AddButton(button.ToString(), button.ToString().ToUpper());
	    }

	    /// <summary>
	    /// Add standard buttons to the message box.
	    /// </summary>
	    /// <param name="buttons">The standard buttons to add</param>
	    public void AddButtons(MessageBoxButtons buttons)
	    {
	        switch (buttons)
	        {
	            case MessageBoxButtons.OK:
	                AddButton(FAMessageBoxButtons.Ok);
	                break;

	            case MessageBoxButtons.AbortRetryIgnore:
	                AddButton(FAMessageBoxButtons.Abort);
	                AddButton(FAMessageBoxButtons.Retry);
	                AddButton(FAMessageBoxButtons.Ignore);
	                break;

	            case MessageBoxButtons.OKCancel:
	                AddButton(FAMessageBoxButtons.Ok);
	                AddButton(FAMessageBoxButtons.Cancel);
	                break;

	            case MessageBoxButtons.RetryCancel:
	                AddButton(FAMessageBoxButtons.Retry);
	                AddButton(FAMessageBoxButtons.Cancel);
	                break;

	            case MessageBoxButtons.YesNo:
	                AddButton(FAMessageBoxButtons.Yes);
	                AddButton(FAMessageBoxButtons.No);
	                break;

	            case MessageBoxButtons.YesNoCancel:
	                AddButton(FAMessageBoxButtons.Yes);
	                AddButton(FAMessageBoxButtons.No);
	                AddButton(FAMessageBoxButtons.Cancel);
	                break;
	        }
	    }
	    
	    #endregion

	    #region Ctor
	    
	    /// <summary>
	    /// Ctor is internal because this can only be created by MBManager
	    /// </summary>
	    internal FAMessageBox(string name, bool isRTL)
	    {
            _isRightToLeft = isRTL;
	        Name = name;
	    }

	    /// <summary>
	    /// Called by the manager when it is _disposed
	    /// </summary>
	    void IDisposable.Dispose()
	    {
	        if (!IsDisposed)
	        {
	            _msgBox.Dispose();
                IsDisposed = true;
	        }
	    }
	    
	    #endregion
	}
}
