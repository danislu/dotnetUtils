using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Utils.WPF
{
    public class OverrideCursor : IDisposable
    {
        private readonly Cursor oldOverrideCursor;

        public OverrideCursor(Cursor newCursor)
        {
            oldOverrideCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = newCursor;
        }

        public void Dispose()
        {
            Mouse.OverrideCursor = oldOverrideCursor;
        }
    }
}
