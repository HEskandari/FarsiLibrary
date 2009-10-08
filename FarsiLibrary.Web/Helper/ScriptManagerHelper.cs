using System;
using System.Text;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Web.Controls;

namespace FarsiLibrary.Web.Helper
{
    internal class ScriptManagerHelper
    {
        public static void RegisterControlScripts(FADatePicker control)
        {
            Guard.Against<ArgumentNullException>(control == null, "Control is null.");
            Guard.Against<ArgumentException>(control.Page == null, "Control must be on a page.");

            new RecieveServerDataScript(control).Register();
            new EventHandlerScript(control).Register();
            new ShowHideControlScript(control).Register();
            new ClickEventScript(control).Register();
            new OpenClientScript(control).Register();
        }

        public static void RegisterCallbackScripts(FADatePicker control)
        {
            new CallServerScript(control).Register();
        }

        #region ScriptRegistererBase

        private abstract class ScriptRegistererBase
        {
            private readonly FADatePicker ctrl;

            protected ScriptRegistererBase(FADatePicker control)
            {
                this.ctrl = control;
            }

            protected string ClientID
            {
                get { return ctrl.ClientID; }
            }

            protected FADatePicker Control
            {
                get { return ctrl; }
            }

            protected abstract string ScriptName();

            protected abstract string CreateScript();

            public virtual void Register()
            {
                var script = CreateScript();
                Guard.Against(string.IsNullOrEmpty(script), "should generate a javascript to register");

                Control.Page.ClientScript.RegisterClientScriptBlock(Control.GetType(), ScriptName() + "_" + ClientID, script, true);
            }
        }

        #endregion

        #region StartupScriptRegisterer

        private abstract class StartupScriptRegisterer : ScriptRegistererBase
        {
            protected StartupScriptRegisterer(FADatePicker control) : base(control)
            {
            }

            public override void Register()
            {
                var script = CreateScript();
                Guard.Against(string.IsNullOrEmpty(script), "should generate a javascript to register");

                Control.Page.ClientScript.RegisterStartupScript(Control.GetType(), ClientID + "_JSDatePickerBlock", script, true);
            }
        }

        #endregion

        #region EventHandlerScript

        private class EventHandlerScript : ScriptRegistererBase
        {
            public EventHandlerScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "EventHandler";
            }

            protected override string CreateScript()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("function FADatePickerAddEvent(obj, evType, fn)");
                sb.AppendLine("{");
                sb.AppendLine("   if (obj.addEventListener)");
                sb.AppendLine("   {");
                sb.AppendLine("      obj.addEventListener(evType, fn, false);");
                sb.AppendLine("      return true;");
                sb.AppendLine("   }");
                sb.AppendLine("   else if (obj.attachEvent)");
                sb.AppendLine("   {");
                sb.AppendLine("      var r = obj.attachEvent('on'+evType, fn);");
                sb.AppendLine("      return r;");
                sb.AppendLine("   }");
                sb.AppendLine("   else");
                sb.AppendLine("   {");
                sb.AppendLine("      return false;");
                sb.AppendLine("   }");
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        #endregion

        #region ShowHideControlScript

        private class ShowHideControlScript : ScriptRegistererBase
        {
            public ShowHideControlScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "ShowHideControl";
            }

            protected override string CreateScript()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("function ShowCalendar_{0}(e)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendFormat("   if(document.getElementById('{0}').disabled == true)\r\n", ClientID);
                sb.AppendLine("      return;");
                sb.AppendLine();
                sb.AppendFormat("   document.getElementById('FADatePickerCalendar_{0}').style.display = 'inline';\r\n", ClientID);
                sb.AppendLine("   FADatePickerClick(e);");
                sb.AppendFormat("   var textBox = document.getElementById('{0}');\r\n", ClientID);
                sb.AppendFormat("   CallServer_{0}('UpdateDate:' + textBox.Value);\r\n", ClientID);
                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendFormat("function HideCalendar_{0}(e)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendFormat("   document.getElementById('FADatePickerCalendar_{0}').style.display = 'none';\r\n", ClientID);
                sb.AppendLine("}");
                sb.AppendLine();
                sb.AppendFormat("function SetDate_{0}(selectedDate)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendFormat("   var textBox  = document.getElementById('{0}');\r\n", ClientID);
                sb.AppendFormat("   textBox.value = selectedDate;\r\n");
                sb.AppendFormat("   CallServer_{0}('UpdateDate:' + selectedDate);\r\n", ClientID);
                sb.AppendFormat("   HideCalendar_{0}();\r\n", ClientID);
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        #endregion

        #region ClickEventScript

        private class ClickEventScript : ScriptRegistererBase
        {
            public ClickEventScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "ClickEvent";
            }

            protected override string CreateScript()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("function FADatePickerClick(e)");
                sb.AppendLine("{");
                sb.AppendLine("   if (!e) var e = window.event;");
                sb.AppendLine("   e.cancelBubble = true;");
                sb.AppendLine();
                sb.AppendLine("   if (e.stopPropagation)");
                sb.AppendLine("      e.stopPropagation();");
                sb.AppendLine("}");

                sb.AppendFormat("function noLeftClick_{0}(e)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendLine("   if (!e) var e = window.event;");
                sb.AppendLine();
                sb.AppendLine("   if(e.button == 0 || e.button == 1)");
                sb.AppendFormat("      HideCalendar_{0}();\r\n", ClientID);
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        #endregion

        #region RecieveServerDataScript

        private class RecieveServerDataScript : ScriptRegistererBase
        {
            public RecieveServerDataScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "RecieveServerData";
            }

            protected override string CreateScript()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("function ReceiveServerData_{0}(arg, context)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendFormat("   var FADatePicker = document.getElementById('FADatePickerCalendar_{0}');\r\n", ClientID);
                sb.AppendFormat("   if(arg == '')\r\n");
                sb.AppendLine("   {");
                sb.AppendFormat("      HideCalendar_{0}();\r\n", ClientID);
                sb.AppendLine("      return;");
                sb.AppendLine("   }");
                sb.AppendLine("   FADatePicker.innerHTML = arg;");
                sb.AppendFormat("   var CalendarContainer = document.getElementById('FADatePickerDropShadow_{0}');\r\n", ClientID);

                if (Control.PlaceAutomatically)
                {
                    sb.AppendLine("   FADatePicker.style.left = '';");
                    sb.AppendLine();
                    sb.AppendLine("   if((FADatePicker.offsetLeft + CalendarContainer.offsetLeft) < 0)");
                    sb.AppendLine("   {");
                    sb.AppendLine("      FADatePicker.style.left = '0px';");
                    sb.AppendLine("      CalendarContainer.style.left = '2px';");
                    sb.AppendLine("   }");
                    sb.AppendLine("   else if((FADatePicker.offsetLeft + CalendarContainer.offsetLeft + CalendarContainer.offsetWidth) > document.documentElement.clientWidth)");
                    sb.AppendLine("   {");
                    sb.AppendLine("      FADatePicker.style.left = document.documentElement.clientWidth.toString() + 'px';");
                    sb.AppendLine("      CalendarContainer.style.left = (0 - CalendarContainer.offsetWidth - 2).toString() + 'px';");
                    sb.AppendLine("   }");
                    sb.AppendLine();
                    sb.AppendLine("   if((FADatePicker.offsetTop + CalendarContainer.offsetTop + CalendarContainer.offsetHeight) > document.documentElement.clientHeight)");
                    sb.AppendLine("   {");
                    sb.AppendLine("      CalendarContainer.style.top = (0 - CalendarContainer.offsetHeight - 2).toString() + 'px';");
                    sb.AppendLine("   }");
                }

                if (Control.Page.Request.Browser.Type.Equals("IE6") || Control.Page.Request.Browser.Type.Equals("IE5"))
                {
                    sb.AppendFormat("   FADatePicker.insertAdjacentHTML('afterBegin', '<iframe id=FADatePickerIframe_{0} src=javascript:false; scrolling=no frameborder=0 style=\"position:absolute; top:0px; left:0px; filter:alpha(opacity=50);-moz-opacity:.50;opacity:.50; \"> </iframe>');\r\n", ClientID);
                    sb.AppendFormat("   var IFrameRef = document.getElementById('FADatePickerIframe_{0}');\r\n", ClientID);
                    sb.AppendLine();
                    sb.AppendFormat("   // Fix for the nasty IE6 select bug\r\n");
                    sb.AppendFormat("   IFrameRef.style.left = CalendarContainer.offsetLeft;\r\n");
                    sb.AppendFormat("   IFrameRef.style.top = CalendarContainer.offsetTop;\r\n");
                    sb.AppendFormat("   IFrameRef.style.width = CalendarContainer.offsetWidth;\r\n");
                    sb.AppendFormat("   IFrameRef.style.height = CalendarContainer.offsetHeight;\r\n");
                    sb.AppendFormat("   IFrameRef.style.zIndex = CalendarContainer.style.zIndex - 1;\r\n");
                }

                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        #endregion

        #region CallServerScript

        private class CallServerScript : ScriptRegistererBase
        {
            public CallServerScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "CallServer";
            }

            protected override string CreateScript()
            {
                var callbackRef = Control.Page.ClientScript.GetCallbackEventReference(Control, "arg", "ReceiveServerData_" + ClientID, string.Empty);
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("function CallServer_{0}(arg, context)\r\n", ClientID);
                sb.AppendLine("{");
                sb.AppendFormat("   {0}\r\n", callbackRef);
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        #endregion

        #region OpenClientScript

        private class OpenClientScript : StartupScriptRegisterer
        {
            public OpenClientScript(FADatePicker control) : base(control)
            {
            }

            protected override string ScriptName()
            {
                return "OpenClient";
            }

            protected override string CreateScript()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine();
                sb.AppendFormat("FADatePickerAddEvent(document, 'click', noLeftClick_{0});\r\n", ClientID);
                sb.AppendFormat("document.getElementById('FADatePickerCalendar_{0}').onclick = FADatePickerClick;\r\n", ClientID);
                sb.AppendFormat("document.getElementById('{0}').onclick = ShowCalendar_{1};\r\n", Control.openButton.ClientID, ClientID);

                return sb.ToString();
            }
        }

        #endregion
    }
}