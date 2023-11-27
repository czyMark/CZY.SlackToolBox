using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    public class TextBoxPrompt : DependencyObject
    {
        public static readonly DependencyProperty PromptProperty = DependencyProperty.RegisterAttached(
        "Prompt",
        typeof(string),
        typeof(TextBoxPrompt),
        new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsPromptEnabledProperty = DependencyProperty.RegisterAttached(
            "IsPromptEnabled",
            typeof(bool),
            typeof(TextBoxPrompt),
            new FrameworkPropertyMetadata(false, IsPromptEnabledChanged));

        public static string GetPrompt(DependencyObject obj)
        {
            return (string)obj.GetValue(PromptProperty);
        }

        public static void SetPrompt(DependencyObject obj, string value)
        {
            obj.SetValue(PromptProperty, value);
        }

        public static bool GetIsPromptEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPromptEnabledProperty);
        }

        public static void SetIsPromptEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPromptEnabledProperty, value);
        }

        public static void CancelPrompt(TextBox textBox)
        { 
            textBox.Background = (Brush)textBox.Tag;
            textBox.Foreground = Brushes.Black;
        }
        public static void RefreshPrompt(TextBox textBox, string prompt)
        {
            TextBoxPrompt.SetPrompt(textBox, prompt);
            UpdatePrompt(textBox, prompt);
        }

        private static void IsPromptEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
                UpdatePrompt(textBox);
            }
            else
            {
                textBox.GotFocus -= TextBox_GotFocus;
                textBox.LostFocus -= TextBox_LostFocus;
                RemovePrompt(textBox);
            }
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            RemovePrompt(textBox);
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            UpdatePrompt(textBox);
        }

        private static void UpdatePrompt(TextBox textBox, string prompt = "")
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

        private static void RemovePrompt(TextBox textBox)
        {
            if (textBox.Text == GetPrompt(textBox))
            {
                textBox.Text = string.Empty;
                textBox.Background = (Brush)textBox.Tag;
                textBox.Foreground = Brushes.Black;
            }
        }
    }
}
