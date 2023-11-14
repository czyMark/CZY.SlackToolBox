using System.Windows;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.NimbleMenu
{
    public class MenuModel
    {
        public string MenuItemId { get; set; }
        public string Title { get; set; }
        public double Angle { get; set; }
        public Brush FillColor { get; set; }
        public ImageSource IconImage { get; set; }
        public Visibility ShowFill { get; set; }
    }
}
