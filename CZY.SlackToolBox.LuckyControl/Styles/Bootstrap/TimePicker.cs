using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CZY.SlackToolBox.LuckyControl.Styles.Bootstrap
{
    public class ValueItem : INotifyPropertyChanged
    {
        public delegate void ChangeDelegate(ValueItem obj);
        public event ChangeDelegate Change;
        public ValueItem() {
            ChangeCommand = new RelayCommand(ExecuteChangeCommand);
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

         

        /// <summary>
        /// 选中变更
        /// </summary>
        public RelayCommand ChangeCommand { get; private set; }

        //选中变更
        void ExecuteChangeCommand(object obj)
        {
            if (Change != null)
            {
                Change(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class TimePicker : Control
    {
        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TimePicker), new PropertyMetadata("请选择时间"));


        public string SelectedTime
        {
            get { return (string)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        public static readonly DependencyProperty SelectedTimeProperty =
            DependencyProperty.Register("SelectedTime", typeof(string), typeof(TimePicker), new PropertyMetadata(""));
         

        public ObservableCollection<ValueItem> Hours = new ObservableCollection<ValueItem>();
        public ObservableCollection<ValueItem> Minutes = new ObservableCollection<ValueItem>();
        public ObservableCollection<ValueItem> Seconds = new ObservableCollection<ValueItem>();

        public override void OnApplyTemplate()
        {
            var lastSelectedTime = SelectedTime;
            var popup = GetTemplateChild("PART_Popup") as Popup;
            var txtBox = GetTemplateChild("PART_ContentHost") as TextBox;
            if (popup != null)
            {
                var hourUpBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_HourUp") as Button;
                var hourDownBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_HourDown") as Button;

                var minuteUpBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_MinuteUp") as Button;
                var minuteDownBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_MinuteDown") as Button;

                var secondUpBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_SecondUp") as Button;
                var secondDownBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_SecondDown") as Button;

                var clearBtn = GetTemplateChild("PART_Clear") as Button;

                var submitBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_Submit") as Button;

                var cancelBtn = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_Cancel") as Button;


                var hourItems = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_HourItems") as ItemsControl;
                if (hourItems != null)
                {
                    hourItems.ItemsSource = Hours;
                    hourItems.MouseEnter += (sender, e) =>
                    {
                        txtBox?.Select(0, 2);
                    };  
                }
                var minuteItems = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_MinuteItems") as ItemsControl;
                if (minuteItems != null)
                {
                    minuteItems.ItemsSource = Minutes;
                    minuteItems.MouseEnter += (sender, e) =>
                    {
                        txtBox?.Select(3, 2);
                    };
                }
                var secondItems = LogicalTreeHelper.FindLogicalNode(popup?.Child, "PART_SecondItems") as ItemsControl;
                if (secondItems != null)
                {
                    secondItems.ItemsSource = Seconds;
                    secondItems.MouseEnter += (sender, e) =>
                    {
                        txtBox?.Select(6, 2);
                    };
                }
                if (hourUpBtn != null)
                {
                    hourUpBtn.Click += (sender, e) =>
                    {
                        var temp = Hours.Last();
                        Hours.RemoveAt(Hours.Count - 1);
                        Hours.Insert(0, temp);
                        Hours[1].IsActive = false;
                        Hours[2].IsActive = true;
                        Hours[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(0, 2);
                    };
                }

                if (hourDownBtn != null)
                {
                    hourDownBtn.Click += (sender, e) =>
                    {
                        var temp = Hours.First();
                        Hours.RemoveAt(0);
                        Hours.Add(temp);
                        Hours[1].IsActive = false;
                        Hours[2].IsActive = true;
                        Hours[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(0, 2);
                    };

                }

                if (minuteUpBtn != null)
                {
                    minuteUpBtn.Click += (sender, e) =>
                    {
                        var temp = Minutes.Last();
                        Minutes.RemoveAt(Minutes.Count - 1);
                        Minutes.Insert(0, temp);
                        Minutes[1].IsActive = false;
                        Minutes[2].IsActive = true;
                        Minutes[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(3, 2);
                    };

                }

                if (minuteDownBtn != null)
                {
                    minuteDownBtn.Click += (sender, e) =>
                    {
                        var temp = Minutes.First();
                        Minutes.RemoveAt(0);
                        Minutes.Add(temp);
                        Minutes[1].IsActive = false;
                        Minutes[2].IsActive = true;
                        Minutes[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(3, 2);
                    };

                }

                if (secondUpBtn != null)
                {
                    secondUpBtn.Click += (sender, e) =>
                    {
                        var temp = Seconds.Last();
                        Seconds.RemoveAt(Seconds.Count - 1);
                        Seconds.Insert(0, temp);
                        Seconds[1].IsActive = false;
                        Seconds[2].IsActive = true;
                        Seconds[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(6, 2);
                    };

                }

                if (secondDownBtn != null)
                {
                    secondDownBtn.Click += (sender, e) =>
                    {
                        var temp = Seconds.First();
                        Seconds.RemoveAt(0);
                        Seconds.Add(temp);
                        Seconds[1].IsActive = false;
                        Seconds[2].IsActive = true;
                        Seconds[3].IsActive = false;
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        txtBox?.Select(6, 2);

                    };
                }

                if (submitBtn != null)
                {
                    submitBtn.Click += (sender, e) =>
                    {
                        SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
                        lastSelectedTime = SelectedTime;
                    };
                }

                if (cancelBtn != null)
                {
                    cancelBtn.Click += (sender, e) =>
                    {
                        SelectedTime = lastSelectedTime;
                    };
                }

                if (clearBtn != null)
                {
                    clearBtn.Click += (sender, e) =>
                    {
                        SelectedTime = "";
                        lastSelectedTime = SelectedTime;
                    };
                }

                txtBox.PreviewGotKeyboardFocus += (sender, e) =>
                {
                    txtBox.Select(0, 2);
                };
                popup.Opened += (sender, e) =>
                {
                    Hours.Clear();
                    Minutes.Clear();
                    Seconds.Clear();

                    for (int i = 0; i < 24; i++)
                    {
                        var m= new ValueItem
                        {
                            Value = i.ToString("00"),
                            IsActive = false
                        };
                        m.Change += Hours_Change;
                        Hours.Add(m);
                    }

                    for (int i = 0; i < 60; i++)
                    {
                        Minutes.Add(new ValueItem
                        {
                            Value = i.ToString("00"),
                            IsActive = false
                        });
                        Seconds.Add(new ValueItem
                        {
                            Value = i.ToString("00"),
                            IsActive = false
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(SelectedTime))
                    {
                        var time = SelectedTime.Split(':');
                        var targetHour = Hours.FirstOrDefault(it => it.Value == time[0]);
                        if (targetHour != null)
                        {
                            targetHour.IsActive = true;
                            var hourIndex = Hours.IndexOf(targetHour);
                            for (int i = 0; i < hourIndex - 2; i++)
                            {
                                var hourTemp = Hours[0];
                                Hours.RemoveAt(0);
                                Hours.Add(hourTemp);
                            }
                        }

                        var targetMinute = Minutes.FirstOrDefault(it => it.Value == time[1]);
                        if (targetMinute != null)
                        {
                            targetMinute.IsActive = true;
                            var minuteIndex = Minutes.IndexOf(targetMinute);
                            for (int i = 0; i < minuteIndex - 2; i++)
                            {
                                var minuteTemp = Minutes[0];
                                Minutes.RemoveAt(0);
                                Minutes.Add(minuteTemp);
                            }
                        }

                        var targetSecond = Seconds.FirstOrDefault(it => it.Value == time[2]);
                        if (targetSecond != null)
                        {
                            targetSecond.IsActive = true;
                            var secondIndex = Seconds.IndexOf(targetSecond);
                            for (int i = 0; i < secondIndex - 2; i++)
                            {
                                var secondTemp = Seconds[0];
                                Seconds.RemoveAt(0);
                                Seconds.Add(secondTemp);
                            }
                        }

                    }
                    else
                    {
                        Hours[0].IsActive = true;
                        Minutes[0].IsActive = true;
                        Seconds[0].IsActive = true;
                    }
                };
            }
        }

        private void Hours_Change(ValueItem obj)
        {
            var temp = Hours.First();
            Hours.RemoveAt(0);
            Hours.Add(temp);
            for (int i = 0; i < Hours.Count; i++)
            {
                Hours[i].IsActive = false;
            }
            obj.IsActive = true; 
            SelectedTime = $"{Hours[2].Value}:{Minutes[2].Value}:{Seconds[2].Value}";
            var txtBox = GetTemplateChild("PART_ContentHost") as TextBox;
            txtBox?.Select(0, 2);
        }
    }
}
