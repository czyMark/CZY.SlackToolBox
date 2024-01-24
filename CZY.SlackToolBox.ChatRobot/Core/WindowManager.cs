namespace CZY.SlackToolBox.ChatRobot.Core
{
    public class WindowManager
    {
        public delegate void GoToAnsyPageDelegate(string PageName);
        public static event GoToAnsyPageDelegate GoToAnsyPage;

        public static void GoToPage(string PageName)
        {
            if (GoToAnsyPage != null)
            {
                GoToAnsyPage(PageName);
            }
        }



        public delegate void ShowWinTipDelegate(string TipText);
        public static event ShowWinTipDelegate WinTipMessage;

        public static void ShowWinTipMessage(string TipText)
        {
            if (WinTipMessage != null)
            {
                WinTipMessage(TipText);
            }
        }




        public delegate void Notification(string NotificationName);
    } 
}
