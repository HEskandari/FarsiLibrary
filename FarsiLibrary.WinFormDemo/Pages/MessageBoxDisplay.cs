using System;
using System.Windows.Forms;
using FarsiLibrary.Win.Controls;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class MessageBoxDisplay : DemoBase
    {
        private const string DEF_BASIC_MESSAGEBOX = "BasicMessageBox";
        private const string DEF_ADVBUTTON_MESSAGEBOX = "AdvancedButtonMessageBox";
        
        #region Ctor

        public MessageBoxDisplay()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void button1_Click(object sender, EventArgs e)
        {
            FAMessageBox msg;
            
            msg = FAMessageBoxManager.GetMessageBox(DEF_BASIC_MESSAGEBOX);
            if (FAMessageBoxManager.GetMessageBox(DEF_BASIC_MESSAGEBOX) == null)
            {
                msg = FAMessageBoxManager.CreateMessageBox(DEF_BASIC_MESSAGEBOX, true);
                msg.AddButtons(MessageBoxButtons.YesNo);
            }
            
            msg.AllowSaveResponse = true;
            msg.SaveResponseText = "سوال را تکرار نکن";
            msg.Text = "ایا موافق هستید";
            msg.Caption = "سوال";
            msg.Icon = FarsiMessageBoxExIcon.Exclamation;
            msg.PlaySound = true;
            string value = msg.Show();
            lblMessage.Text = string.Format("You've selected {0} in Basic MessageBox.", value);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            FAMessageBox msg;

            msg = FAMessageBoxManager.GetMessageBox(DEF_ADVBUTTON_MESSAGEBOX);
            if (FAMessageBoxManager.GetMessageBox(DEF_ADVBUTTON_MESSAGEBOX) == null)
            {
                msg = FAMessageBoxManager.CreateMessageBox(DEF_ADVBUTTON_MESSAGEBOX);
                msg.AddButton(new FAMessageBoxButton("Yes", "YES"));
                msg.AddButton(new FAMessageBoxButton("No", "NO"));
                msg.AddButton(new FAMessageBoxButton("Yes To All", "YESTOALL"));
                msg.AddButton(new FAMessageBoxButton("No To All", "NOTOALL"));
            }

            msg.AllowSaveResponse = true;
            msg.SaveResponseText = "Don't ask this question again.";
            msg.Text = "This file already exists. Overwrite?";
            msg.Icon = FarsiMessageBoxExIcon.Question;
            string value = msg.Show(this);
            lblMessage.Text = string.Format("You've selected {0} in Advanced MessageBox.", value);
        }

        private void btnGetBasic_Click(object sender, EventArgs e)
        {
            FAMessageBox msg = FAMessageBoxManager.GetMessageBox(DEF_BASIC_MESSAGEBOX);

            if (msg == null || !msg.SavedResponse)
            {
                lblMessage.Text = "Basic MessageBox is not shown yet or has not its value saved.";
            }
            else
            {
                lblMessage.Text = string.Format("You've selected {0} from Basic MessageBox before.", msg.Show());
            }
        }

        private void btnGetAdv_Click(object sender, EventArgs e)
        {
            FAMessageBox msg = FAMessageBoxManager.GetMessageBox(DEF_ADVBUTTON_MESSAGEBOX);

            if (msg == null || !msg.SavedResponse)
            {
                lblMessage.Text = "Advanced MessageBox is not shown yet or has not its value saved.";
            }
            else
            {
                lblMessage.Text = string.Format("You've selected {0} from Advanced MessageBox before.", msg.Show());
            }
        }

        #endregion
    }
}