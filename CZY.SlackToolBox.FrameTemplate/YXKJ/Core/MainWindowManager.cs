using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.Core
{
    internal class MainWindowManager
    {

        public static void SetMessageTip(string tip, 
            LuckyControl.ElementPanel.TipPanel.TipPanelState panelState =
                LuckyControl.ElementPanel.TipPanel.TipPanelState.Success
            )
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MessageTip("信息修改完成!", panelState);
        }
         
    }
}
