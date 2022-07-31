using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools.Dialogs
{
    public static class AppMessageBox
    {
        public static DialogResult Question_YesNo(string msg, string title)
        {
            DialogResult objDialog = MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return objDialog;
        }

        internal static void Error(string msg, string title = "Error")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void Inform(string msg, string title = "")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}