using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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

namespace CZY.SlackToolBox.FrameTemplate.SettingWindow.View
{
    /// <summary>
    /// SettingContent.xaml 的交互逻辑
    /// </summary>
    public partial class SettingContent : UserControl
    {
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public SettingContent()
        {
            InitializeComponent();
            PopulateVoices();
        }
        private void PopulateVoices()
        {
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                VoiceSelection.Items.Add(voice.VoiceInfo.Name);
            }
            if (VoiceSelection.Items.Count > 0)
            {
                VoiceSelection.SelectedIndex = 0; // Select the first voice by default
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CaptureApply.MainWindow win = new CaptureApply.MainWindow();
            win.Show();
        }

        private void SpeakText_Click(object sender, RoutedEventArgs e)
        {
            if (VoiceSelection.SelectedItem != null)
            {
                string selectedVoiceName =  VoiceSelection.SelectedItem.ToString();
                synthesizer.SelectVoice(selectedVoiceName);
                synthesizer.Speak(TextInput.Text);
            }
        }
    }
}
