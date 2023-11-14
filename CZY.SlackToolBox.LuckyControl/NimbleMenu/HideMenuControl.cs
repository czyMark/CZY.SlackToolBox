using System.Windows.Input;

namespace CZY.SlackToolBox.LuckyControl.NimbleMenu
{
	/// <summary>
	/// Windows界面上即使不设置默认也会有一个看不见的系统菜单 使用F11就能进入。
	/// 为了避免F11后使用特殊的快捷键关闭窗口，所以在界面上防止该控制阻止
	/// </summary>
    public class HideMenuControl : System.Windows.Controls.Menu
	{
		public delegate void MenuKeyDown(KeyEventArgs e);
		public event MenuKeyDown MenuKeyDownEvent;
		public HideMenuControl()
		{

		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F4)
			{
				e.Handled = true;
			}
			else
			{
				if (MenuKeyDownEvent != null)
				{
					MenuKeyDownEvent(e);
				}
			}
		}
	}
}
