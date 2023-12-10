using CZY.SlackToolBox.FrameTemplate.AirportCS.Core;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.ViewModel
{
    public class HomeContentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性


        /// <summary>
        /// 版本号
        /// </summary>
        private string versionNumber;
        public string VersionNumber
        {
            get
            {
                return versionNumber;
            }
            set
            {
                versionNumber = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("VersionNumber"));
                }
            }
        }

        #endregion



        public HomeContentViewModel()
        {
            VersionNumber = "V1.0";
          
        }
    }
}
