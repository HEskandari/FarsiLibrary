namespace FarsiLibrary.Win.Controls
{
	/// <summary>
	/// Structure used to represent buttons of the <see cref="FAMessageBox"/> control.
	/// </summary>
	public class FAMessageBoxButton
	{
	    #region Fields

	    private string _text = null;
	    private string _value = null;
	    private string _helpText = null;
	    private bool _isCancelButton = false;

	    #endregion

	    #region Props

	    /// <summary>
	    /// Gets or Sets the text of the button
	    /// </summary>
	    public string Text
	    {
	        get { return _text; }
	        set { _text = value; }
	    }

	    /// <summary>
	    /// Gets or Sets the return value when this button is clicked
	    /// </summary>
	    public string Value
	    {
	        get { return _value; }
	        set { _value = value; }
	    }

	    /// <summary>
	    /// Gets or Sets the tooltip that is displayed for this button
	    /// </summary>
	    public string HelpText
	    {
	        get { return _helpText; }
	        set { _helpText = value; }
	    }

	    /// <summary>
	    /// Gets or Sets wether this button is DrawTab cancel button. activeDocumentHighlightColor.documentStripBackgroundColor1. the button
	    /// that will be assumed to have been clicked if the user closes the message box
	    /// without pressing any button.
	    /// </summary>
	    public bool IsCancelButton
	    {
	        get { return _isCancelButton; }
	        set { _isCancelButton = value; }
	    }

	    #endregion

	    #region Ctor

	    public FAMessageBoxButton(string text, string value)
	    {
	        _text = text;
	        _value = value;
	    }

	    public FAMessageBoxButton(string text, string value, string helpText)
	    {
	        _text = text;
	        _value = value;
	        _helpText = helpText;
	    }

	    public FAMessageBoxButton(string text, string value, string helpText, bool isCancelButton)
	    {
	        _text = text;
	        _value = value;
	        _helpText = helpText;
	        _isCancelButton = isCancelButton;
	    }

	    #endregion
	}
}
