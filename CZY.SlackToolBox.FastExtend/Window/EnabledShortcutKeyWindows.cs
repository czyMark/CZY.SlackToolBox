﻿using System;
using System.Windows;
using System.Windows.Input;

namespace CZY.SlackToolBox.FastExtend
{
    public class EnabledShortcutKeyWindows : Window
    {
		public bool isCommit = false;
		public Key _systemKey;

		public EnabledShortcutKeyWindows()
		{

		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F4)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Windows && e.SystemKey == Key.D)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Control && e.SystemKey == Key.Tab)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Tab)
			{
				e.Handled = true;
			}
			else
			{
				_systemKey = e.SystemKey;
				base.OnKeyDown(e);
			}
		}
		public string DontCloseWindwosName = "";
		//用来完全退出窗口
		protected override void OnClosed(EventArgs e)
		{
			var collections = Application.Current.Windows;
 
			base.OnClosed(e);
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
			else if (Keyboard.Modifiers == ModifierKeys.Windows && e.SystemKey == Key.D)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Control && e.SystemKey == Key.Tab)
			{
				e.Handled = true;
			}
			else if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Tab)
			{
				e.Handled = true;
			}
			else
			{
				_systemKey = e.SystemKey;
				base.OnKeyDown(e);
			}
		}

	}
}
