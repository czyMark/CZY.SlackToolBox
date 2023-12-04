using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using System.Windows;

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
