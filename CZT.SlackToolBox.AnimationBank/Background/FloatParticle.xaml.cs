using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CZY.SlackToolBox.AnimationBank.Background
{

    /// <summary>
    /// FloatParticle.xaml 的交互逻辑
    /// </summary>
    public partial class FloatParticle : UserControl
    {


        /// <summary>
        /// 粒子坐标
        /// </summary>
        internal class GrainBase
        {
            public double? x { get; set; }
            public double? y { get; set; }

            public double xa { get; set; }

            public double ya { get; set; }

            public double max { get; set; }

        }

        #region 依赖属性
        public static readonly DependencyProperty ParticleCountProperty =
          DependencyProperty.Register(nameof(ParticleCount), typeof(int), typeof(FloatParticle));


        public static readonly DependencyProperty EllipseColorProperty =
            DependencyProperty.Register(nameof(EllipseColor), typeof(Color), typeof(FloatParticle));


        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register(nameof(StrokeColor), typeof(Color), typeof(FloatParticle), new PropertyMetadata(StrokeColorChanged));


        public static readonly DependencyProperty ShowStrokeProperty =
            DependencyProperty.Register(nameof(ShowStroke), typeof(bool), typeof(FloatParticle));

        public static readonly DependencyProperty MouseDrawProperty =
            DependencyProperty.Register(nameof(MouseDraw), typeof(bool), typeof(FloatParticle), new PropertyMetadata(MouseDrawChanged));
        public int ParticleCount
        {
            get => (int)GetValue(ParticleCountProperty);
            set => SetValue(ParticleCountProperty, value);
        }

        public Color EllipseColor
        {
            get => (Color)GetValue(EllipseColorProperty);
            set => SetValue(EllipseColorProperty, value);
        }


        public Color StrokeColor
        {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }
        public bool ShowStroke
        {
            get => (bool)GetValue(ShowStrokeProperty);
            set => SetValue(ShowStrokeProperty, value);
        }
        public bool MouseDraw
        {
            get => (bool)GetValue(MouseDrawProperty);
            set => SetValue(MouseDrawProperty, value);
        }
        private static void StrokeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                FloatParticle control = d as FloatParticle;
                Color temp = (Color)e.NewValue;
                control.StrokeColorR = temp.R;
                control.StrokeColorG = temp.G;
                control.StrokeColorB = temp.B;
            }
        }
        private static void MouseDrawChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                FloatParticle control = d as FloatParticle;

                bool old = (bool)e.OldValue; //避免重复设置导致的事件内存溢出
                if (old)
                {
                    control.DrawPanel.MouseDown -= DrawPanel_MouseDown;
                    control.DrawPanel.MouseLeave -= DrawPanel_MouseLeave;
                    control.DrawPanel.MouseMove -= DrawPanel_MouseMove;
                }

                bool temp = (bool)e.NewValue;
                if (temp)
                {
                    control.DrawPanel.MouseDown += DrawPanel_MouseDown;
                    control.DrawPanel.MouseLeave += DrawPanel_MouseLeave;
                    control.DrawPanel.MouseMove += DrawPanel_MouseMove;
                }

            }
        }

        #endregion


        //动画时间器
        private DispatcherTimer updateTimer;
        static List<GrainBase> grains = new List<GrainBase>();
        static GrainBase mousePoint = new GrainBase();
        static Random rand = new Random();


        private byte StrokeColorR { get; set; }
        private byte StrokeColorG { get; set; }
        private byte StrokeColorB { get; set; }
        public FloatParticle()
        {

            InitializeComponent();


            //启动刷新动画
            updateTimer = new System.Windows.Threading.DispatcherTimer();
            updateTimer.Tick += new EventHandler(DrawingAY);
            updateTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            updateTimer.Start();
        }


        private void DrawingAY(object sender, EventArgs e)
        {
            AyKeyFrame();
        }
        #region 手动点击添加粒子

        private static void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas DrawPanel = sender as Canvas;
            var ui = e.GetPosition(DrawPanel);
            mousePoint.x = ui.X;
            mousePoint.y = ui.Y;
        }

        private static void DrawPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            mousePoint.x = null;
            mousePoint.y = null;
        }
        private static void DrawPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas DrawPanel = sender as Canvas;
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                GrainBase gb = new GrainBase();
                var ui = e.GetPosition(DrawPanel);
                gb.x = ui.X;
                gb.y = ui.Y;

                gb.xa = rand.NextDouble() * 2 - 1;
                gb.ya = rand.NextDouble() * 2 - 1;
                gb.max = 8000;
                grains.Add(gb);
            }
        }

        #endregion

        /// <summary>
        /// 枚举可视对象的所有后代。
        /// </summary>
        /// <param name="myVisual">要枚举的父容器</param>
        public static void EnumVisual(Visual myVisual)
        {
            //遍历可视对象所有子项
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // 检索指定索引值处的子可视值
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                // 处理子可视对象 
                //清空文本框
                //if (childVisual is TextBox)
                //{
                //    (childVisual as TextBox).Clear();
                //}
                ////清空密码框
                //if (childVisual is PasswordBox)
                //{
                //    (childVisual as PasswordBox).Clear();
                //}
                //// 枚举子可视对象的子对象。
                EnumVisual(childVisual);
            }
        }
        List<Ellipse> ellipses = new List<Ellipse>();
        List<Line> lines = new List<Line>();
        private void AyKeyFrame()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].StrokeThickness = 0;
            }
            //DrawPanel=null;
            //DrawPanel = new Canvas();
            //DrawPanel.Width = 1920;
            //DrawPanel.Height = 1080;
            //DrawPanel.Background = null;
            //DrawPanel.ClipToBounds = true; 
            //DrawPanel.Children.Clear();

            //this.RemoveLogicalChild(DrawPanel);
            //this.RemoveVisualChild(DrawPanel);
            //this.Content = null;
            //this.Content = DrawPanel;
            //for (int i = 0; i < ellipses.Count; i++)
            //{
            //    ellipses[i] = null;
            //    ellipses.RemoveAt(i);
            //}
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    DrawPanel.Children.Remove(lines[i]);
            //    Line t=lines[i] ; 
            //    lines.RemoveAt(i);
            //    t = null;
            //}
            // EnumVisual(DrawPanel);
            //this.UpdateLayout();
            //DrawPanel
            //var w = DrawPanel.Width;
            //var h = DrawPanel.Height;
            //DrawPanel.Width = w;
            //DrawPanel.Width = h;
            //this.UpdateLayout();
            //GC.Collect(); // This should pick up the control removed at a previous MouseDown 
            //GC.WaitForPendingFinalizers(); // Doesn't help either 

            for (int i = 0; i < grains.Count; i++)
            {
                GrainBase dot = grains[i];
                if (dot.x == null || dot.y == null) continue;
                #region 创建碰撞粒子
                // 粒子位移
                dot.x += dot.xa;
                dot.y += dot.ya;
                // 遇到边界将加速度反向
                dot.xa *= (dot.x.Value > DrawPanel.ActualWidth || dot.x.Value < 0) ? -1 : 1;
                dot.ya *= (dot.y.Value > DrawPanel.ActualHeight || dot.y.Value < 0) ? -1 : 1;
                // 绘制点
                Ellipse elip = ellipses[i];
                Canvas.SetLeft(elip, dot.x.Value - 0.5);
                Canvas.SetTop(elip, dot.y.Value - 0.5);
                elip.Fill = new SolidColorBrush(EllipseColor);
                #endregion 

                if (ShowStroke)
                {
                    //判断是不是最后一个，就不用两两比较了
                    int endIndex = i + 1;
                    if (endIndex == grains.Count) continue;
                    for (int j = endIndex; j < grains.Count; j++)
                    {
                        GrainBase d2 = grains[j];
                        double xc = dot.x.Value - d2.x.Value;
                        double yc = dot.y.Value - d2.y.Value;
                        // 两个粒子之间的距离
                        double dis = xc * xc + yc * yc;
                        // 距离比
                        double ratio;
                        // 如果两个粒子之间的距离小于粒子对象的max值，则在两个粒子间画线
                        if (dis < d2.max)
                        {
                            // 计算距离比 依据计算距离计算是否显示连接线
                            ratio = (d2.max - dis) / d2.max;
                            Line line = null;
                            if (j >= lines.Count)
                                line = new Line();
                            else
                                line = lines[j];
                            double opacity = ratio + 0.2;
                            if (opacity > 1) { opacity = 1; }
                            byte ar = (byte)(opacity * 255);
                            line.Stroke = new SolidColorBrush(Color.FromArgb(ar, StrokeColorR, StrokeColorG, StrokeColorB));
                            line.StrokeThickness = ratio / 2;
                            line.X1 = dot.x.Value;
                            line.Y1 = dot.y.Value;
                            line.X2 = d2.x.Value;
                            line.Y2 = d2.y.Value;
                            if (j >= lines.Count)
                            {
                                lines.Add(line);
                                DrawPanel.Children.Add(line);
                            }
                        }
                    }
                }
            }

            //this.grid.Children.Remove(child); // Remove the child from visual tree 
            //NameScope.GetNameScope(this).UnregisterName("child"); // remove the keyed name 
            //this.child = null; // null the field 

            //this.UpdateLayout();
            //GC.Collect(); // This should pick up the control removed at a previous MouseDown 
            //GC.WaitForPendingFinalizers(); // Doesn't help either 
            //this.Content = DrawPanel;
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            mousePoint.max = 200;
            grains.Add(mousePoint);

            Ellipse t = new Ellipse();
            ellipses.Add(t);
            DrawPanel.Children.Add(t);


            if (ParticleCount == 0)
                ParticleCount = 100;
            //// 添加粒子
            //// x，y为粒子坐标，xa, ya为粒子xy轴加速度，max为连线的最大距离     
            for (int i = 0; i < ParticleCount; i++)
            {
                GrainBase gb = new GrainBase();
                gb.x = rand.NextDouble() * DrawPanel.ActualWidth;
                gb.y = rand.NextDouble() * DrawPanel.ActualHeight;
                gb.xa = rand.NextDouble() * 2 - 1;
                gb.ya = rand.NextDouble() * 2 - 1;

                gb.max = 8000;
                grains.Add(gb);
                Ellipse elip = new Ellipse();
                ellipses.Add(elip);
                DrawPanel.Children.Add(elip);

            }
        }
    }
}
