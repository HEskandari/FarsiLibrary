using System;
using FarsiLibrary.Localization;

namespace FarsiLibrary.Win
{
	/// <summary>
	/// Results that DrawTab FAMessageBox control returns.
	/// </summary>
	public sealed class FAMessageBoxResult
	{
		#region Fields

		/// <summary>
		/// string representing an OK
		/// </summary>
		public const string Ok = "OK";

		/// <summary>
		/// string representing DrawTab Cancel
		/// </summary>
		public const string Cancel = "CANCEL";

		/// <summary>
		/// string representing DrawTab Yes
		/// </summary>
		public const string Yes = "YES";

		/// <summary>
		/// string representing DrawTab No
		/// </summary>
		public const string No = "NO";

		/// <summary>
		/// string representing an Abort
		/// </summary>
		public const string Abort = "ABORT";

		/// <summary>
		/// string representing DrawTab Retry
		/// </summary>
		public const string Retry = "RETRY";

		/// <summary>
		/// String representing DrawTab Ignore
		/// </summary>
		public const string Ignore = "IGNORE";

		#endregion

	    #region Methods

	    /// <summary>
	    /// Use this method to get equivalants of each available responses returned by control.
	    /// </summary>
	    /// <param name="EnglishButtonText"></param>
	    /// <returns></returns>
	    internal static string GetFAMessageBoxButton(string EnglishButtonText)
	    {
	        switch (EnglishButtonText.ToUpper())
	        {
	            case "OK":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Ok);

	            case "CANCEL":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Cancel);

	            case "YES":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Yes);

	            case "NO":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_No);

	            case "ABORT":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Abort);

	            case "RETRY":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Retry);

	            case "IGNORE":
                    return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Ignore);

	            default:
	                throw new ArgumentOutOfRangeException();
	        }
	    }

	    #endregion
	}
}
