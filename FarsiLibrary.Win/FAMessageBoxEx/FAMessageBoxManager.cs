using System;
using System.Collections.Generic;
using System.IO;

namespace FarsiLibrary.Win.Controls
{
	/// <summary>
	/// Manages various instances of <see cref="FAMessageBox"/> controls. This manager class, manages the saved responses of all <see cref="FAMessageBox"/> controls.
	/// </summary>
	public class FAMessageBoxManager
	{
		#region Fields

        private static readonly Dictionary<string, FAMessageBox> _messageBoxes = new Dictionary<string, FAMessageBox>();
		private static readonly Dictionary<string, string> _savedResponses = new Dictionary<string, string>();
	    
		#endregion

		#region Methods
	    
		/// <summary>
		/// Creates a new <see cref="FAMessageBox"/> with the specified unique name. If null is specified
		/// in the name parameter of the <see cref="FAMessageBox"/> control, the instance is not managed by the Manager and will be 
		/// disposed automatically after call to <see cref="FAMessageBox.Show()"/> method.
		/// </summary>
		/// <param name="name">The name of the message box</param>
		/// <returns>A new message box</returns>
		public static FAMessageBox CreateMessageBox(string name)
		{
		    return CreateMessageBox(name, false);
		}

		/// <summary>
        /// Creates a new <see cref="FAMessageBox"/> with the specified unique name. If null is specified
        /// in the name parameter of the <see cref="FAMessageBox"/> control, the instance is not managed by the Manager and will be 
        /// disposed automatically after call to <see cref="FAMessageBox.Show()"/> method.
        /// Set rtl value to true, if the control is supposed to run a RTL messagebox, else set it to false, or use the overloaded method.
		/// </summary>
		/// <param name="name">The name of the <see cref="FAMessageBox"/> which should be unique.</param>
		/// <param name="rtl">Is the instance has RTL or LTR layout.</param>
		/// <returns>A new message box</returns>
		public static FAMessageBox CreateMessageBox(string name, bool rtl)
		{
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("name can not be empty.");

		    FAMessageBox msgBox;

            if (_messageBoxes.ContainsKey(name))
            {
                msgBox = _messageBoxes[name];
                if(msgBox.IsDisposed)
                {
                    DeleteMessageBox(name);
                }
                else
                {
                    return msgBox;
                }
            }

            msgBox = new FAMessageBox(name, rtl);

            if (msgBox.Name != null)
                _messageBoxes[name] = msgBox;

            return msgBox;
        }

		/// <summary>
		/// Gets the <see cref="FAMessageBox"/> instance with the specified name.
		/// </summary>
		/// <param name="name">The name of the message box to retrieve</param>
		/// <returns>The message box with the specified name or null if the message box with that name does not exist</returns>
		public static FAMessageBox GetMessageBox(string name)
		{
            if (_messageBoxes.ContainsKey(name))
                return _messageBoxes[name];

            return null;
		}

		/// <summary>
		/// Deletes the message box with the specified name
		/// </summary>
		/// <param name="name">The name of the message box to delete</param>
		public static bool DeleteMessageBox(string name)
		{
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("name can not be empty.");

			if (_messageBoxes.ContainsKey(name))
			{
				var msgBox = _messageBoxes[name];
				((IDisposable)msgBox).Dispose();
				_messageBoxes.Remove(name);

                return true;
			}

		    return false;
		}

		/// <summary>
		/// Save message box values to an stream.
		/// </summary>
		/// <param name="stream"></param>
		public static void WriteSavedResponses(Stream stream)
		{
			throw new NotImplementedException("This feature has not yet been implemented");
		}

		/// <summary>
		/// Loads message box values from DrawTab stream. This method is not implemented yet.
		/// </summary>
		/// <param name="stream"></param>
		public static void ReadSavedResponses(Stream stream)
		{
			throw new NotImplementedException("This feature has not yet been implemented");
		}

		/// <summary>
		/// Reset the saved response for the message box with the specified name.
		/// </summary>
		/// <param name="messageBoxName">The name of the message box whose response is to be reset.</param>
		public static void ResetSavedResponse(string messageBoxName)
		{
			if (_savedResponses.ContainsKey(messageBoxName))
				_savedResponses.Remove(messageBoxName);
		}

		/// <summary>
		/// Resets the saved responses for all message boxes that are managed by the manager.
		/// </summary>
		public static void ResetAllSavedResponses()
		{
			_savedResponses.Clear();
		}
	    
		#endregion

		#region Internal Methods
	    
		/// <summary>
		/// Set the saved response for the specified message box
		/// </summary>
		/// <param name="msgBox">The message box whose response is to be set</param>
		/// <param name="response">The response to save for the message box</param>
		internal static void SetSavedResponse(FAMessageBox msgBox, string response)
		{
			_savedResponses[msgBox.Name] = response;
		}

		/// <summary>
		/// Gets the saved response for the specified message box
		/// </summary>
		/// <param name="msgBox">The message box whose saved response is to be retrieved</param>
		/// <returns>The saved response if exists, null otherwise</returns>
		internal static string GetSavedResponse(FAMessageBox msgBox)
		{
			string msgBoxName = msgBox.Name;
			if (_savedResponses.ContainsKey(msgBoxName))
			{
				return _savedResponses[msgBox.Name];
			}
		    
            return null;
		}
	    
		#endregion
	}
}
