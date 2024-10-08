﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    public class InputAttach : DependencyObject
    {
        public static readonly DependencyProperty PromptProperty = DependencyProperty.RegisterAttached(
        "Prompt",
        typeof(string),
        typeof(InputAttach),
        new FrameworkPropertyMetadata(string.Empty));

        public static string GetPrompt(DependencyObject obj)
        {
            return (string)obj.GetValue(PromptProperty);
        }

        public static void SetPrompt(DependencyObject obj, string value)
        {
            obj.SetValue(PromptProperty, value);
        }

        #region 过滤输入
        public static readonly DependencyProperty RegexFilterProperty = DependencyProperty.RegisterAttached(
       "RegexFilter", typeof(string), typeof(InputAttach), new PropertyMetadata(null, OnRegexStringChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetRegexString(DependencyObject element, string value)
            => element.SetValue(RegexFilterProperty, value);

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetRegexString(DependencyObject element)
            => (string)element.GetValue(RegexFilterProperty);

        private static void OnRegexStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, false);

                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                textBox.KeyUp += TextBox_KeyUp;
            }
        }

        private static void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Regex re = new Regex(GetRegexString(textBox));
            if (re.IsMatch(textBox.Text))
            {
                textBox.Undo();
            }
        }

        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex re = new Regex(GetRegexString(sender as TextBox));
            e.Handled = re.IsMatch(e.Text);
        }
        #endregion
         
        #region TextBox 代码实现提示文本

        public static readonly DependencyProperty IsTextBoxProperty = DependencyProperty.RegisterAttached(
            "IsTextBox",
            typeof(bool),
            typeof(InputAttach),
            new FrameworkPropertyMetadata(false, IsTextBoxChanged));

        public static bool GetIsTextBox(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTextBoxProperty);
        }

        public static void SetIsTextBox(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTextBoxProperty, value);
        }
        public static void CancelTextBoxPrompt(TextBox textBox)
        {
            textBox.Background = (Brush)textBox.Tag;
            textBox.Foreground = Brushes.Black;
        }
        public static void RefreshTextBoxPrompt(TextBox textBox, string prompt)
        {
            InputAttach.SetPrompt(textBox, prompt);
            UpdateTextBoxPrompt(textBox, prompt);
        }

        private static void IsTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.Loaded += TextBox_Loaded;
                UpdateTextBoxPrompt(textBox);
            }
            else
            {
                textBox.GotFocus -= TextBox_GotFocus;
                textBox.LostFocus -= TextBox_LostFocus;
                textBox.Loaded -= TextBox_Loaded;
                RemoveTextBoxPrompt(textBox);
            }
        }

        private static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }
            UpdateTextBoxPrompt(textBox);

        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }
            RemoveTextBoxPrompt(textBox);
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            UpdateTextBoxPrompt(textBox);
        }

        private static void UpdateTextBoxPrompt(TextBox textBox, string prompt = "")
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Tag = textBox.Background;
                textBox.Background = Brushes.Transparent;
                if (string.IsNullOrEmpty(prompt))
                {
                    textBox.Text = GetPrompt(textBox);
                }
                else
                {
                    textBox.Text = prompt;
                }
                textBox.Foreground = Brushes.Gray;
            }
        }

        private static void RemoveTextBoxPrompt(TextBox textBox)
        {
            if (textBox.Text == GetPrompt(textBox))
            {
                textBox.Text = string.Empty;
                textBox.Background = (Brush)textBox.Tag;
                textBox.Foreground = Brushes.Black;
            }
        }
        #endregion
         
        #region 密码框通知绑定数据源更新密码

        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.RegisterAttached("Password",
           typeof(string), typeof(InputAttach),
           new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));
         
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
            typeof(bool), typeof(InputAttach), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
           typeof(InputAttach));

        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }
        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }
        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }
        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }
        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }
            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
        #endregion

        #region 实现ToggleButton分组内只能选中一个




        #endregion
    }
}
