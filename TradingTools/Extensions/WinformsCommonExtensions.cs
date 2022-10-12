using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools.Extensions
{
    public static class WinformsCommonExtensions
    {
        public static DateTimePicker SafeValueAssignment(this DateTimePicker dtp, DateTime value)
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

        public static T Replace<T>(this BindingList<T> bindingList, T item, Func<T, bool> predicate) where T : class
        {
            var updated = bindingList.FirstOrDefault(predicate);
            if (updated == default) return null;
            var index = bindingList.IndexOf(updated);
            bindingList.RemoveAt(index);
            bindingList.Insert(index, item);

            return item;
        }
    }
}
