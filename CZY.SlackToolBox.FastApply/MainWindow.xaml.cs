using CZY.SlackToolBox.FastExtend;
using CZY.SlackToolBox.FastExtend.Communication;
using CZY.SlackToolBox.FastExtend.Window;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace CZY.SlackToolBox.FastApply
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WinHotKeyWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Height = 1080;
            this.Top = 0;
            this.Topmost = true;

            //string server = "http://localhost:8080/";
            //string para = "?text={{speakText}}&spk=派蒙&emotion=平静-好耶！《特尔克西的奇幻历险》出发咯！&speed={{speakSpeed}}&pitch=1.0&top_k=5&top_p=0.75&temperature=1.0&batch_size=20&text_split_method=cut5&text_lang=zh&seed=1480456663&split_bucket=True&batch_threshold=0.75&format=wav&parallel_infer=True&repetition_penalty=1.35&fragment_interval=0.3";

            //para = para.Replace("{{speakText}}","你好朋友");
            //para = para.Replace("{{speakSpeed}}", "1");
            //var addr = server + para;
            //PlayMp3FromUrl(addr);
            // addr.WebRequestGetResponseStream();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //string server = "http://localhost:8080/";
            //string para = "?spk=晓秋&text_split_method=cut2&speed=1.1&emotion=&lang=zh&text=123123";
            //var addr = server + para;

            //await PlayAudioStreamAsync(addr);
        }

        public static void PlayMp3FromUrl(string url)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url).GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    { ms.Write(buffer, 0, read); }
                }
                ms.Position = 0;
                using (var mp3 = new WaveFileReader(ms))
                {
                    using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(mp3)))
                    {
                        using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                        {
                            waveOut.Init(blockAlignedStream); waveOut.Play();
                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            { System.Threading.Thread.Sleep(100); }
                        }
                    }
                }
            }
        }

        private void WinHotKeyWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                MaxWin();
            }
        }

        private void MaxWin()
        {
            this.ResizeMode = ResizeMode.NoResize;

            System.Drawing.Rectangle screenResolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            //宽度*缩放   
            double screenWidth = screenResolution.Width ;
            double screenHeight = screenResolution.Height;

            double screenWidthInInches = screenWidth;
            double screenHeightInInches = screenHeight;
            this.Width = screenWidthInInches;
            this.Height = screenHeightInInches;


            this.Top = 0;
            this.Left = 0;
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.None;
            this.Topmost = true;
        }
        private void MinWin()
        {
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            this.Topmost = false;
        }
        private void WinHotKeyWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MinWin();
            }
            if (e.Key == Key.F11)
            {
                if (this.Topmost == false)
                    MaxWin();
                else
                    MinWin();
            }
        }
    }
}
