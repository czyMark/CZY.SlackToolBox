using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CZY.SlackToolBox.LuckyControl
{ 
    /// <summary>
    /// 重写wpf事件绑定
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// 判断命令是否可以执行的方法
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        /// <param name="canExecute">判断命令是否能够执行的方法</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 检查命令是否可以执行的事件，在UI事件发生导致控件状态或数据发生变化时触发
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令传入的参数</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute(parameter);
            }
        }

    }

    /// <summary>
    /// 给不支持Command的控件提供基础的绑定
    /// </summary>
    public static class CommandUtil
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandUtil), new PropertyMetadata(null, CommandChanged));

        public static void SetCommand(UIElement element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }



        public static readonly DependencyProperty CommandParaProperty =
            DependencyProperty.RegisterAttached("CommandPara", typeof(object), typeof(CommandUtil), new PropertyMetadata(null));

        public static void SetCommandPara(UIElement element, object value)
        {
            element.SetValue(CommandParaProperty, value);
        }

        public static object GetCommandPara(UIElement element)
        {
            return (object)element.GetValue(CommandParaProperty);
        }

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;
            ICommand command = (ICommand)e.NewValue;
            element.MouseLeftButtonDown += (s, args) => command.Execute(GetCommandPara(element));
        }
    }
}
