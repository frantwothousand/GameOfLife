using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLifeClient.Utils
{
    public static class Log
    {
        public static void Add(Brush color, string message, ListBox component)
        {
            component.Dispatcher.Invoke(() =>
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = message;
                itm.Foreground = color;
                itm.Focusable = false;
                if(itm.IsMouseOver)
                    itm.Background = Brushes.Transparent;
                component.Items.Add(itm);
            });
        }
    }
}
