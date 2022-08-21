using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools.Extensions
{
    public static class WinformsCommonExtensions
    {
        public static DateTimePicker MaxDate_SafeAssign(this DateTimePicker dtp, DateTime value)
        {
            dtp.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtp.Value = value;

            return dtp;
        }

        public static void AppendLine(this TextBox source, string value)
        {
            if (source.IsDisposed) return;
            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText("\r\n" + value);
        }
    }
}
