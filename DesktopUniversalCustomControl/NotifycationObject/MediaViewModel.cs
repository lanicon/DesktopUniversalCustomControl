using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUniversalCustomControl.NotifycationObject
{
    public class MediaViewModel: NotifyPropertyChanged
    {
        private bool _isVolumeOpen = false;
        /// <summary>
        /// VolumePopup是否打开
        /// </summary>
        public bool IsVolumeOpen
        {
            get { return _isVolumeOpen; }
            set { SetProperty(ref _isVolumeOpen, value); }
        }

        private bool _isSpeedOpen = false;
        /// <summary>
        /// SpeedPopup是否打开
        /// </summary>
        public bool IsSpeedOpen
        {
            get { return _isSpeedOpen; }
            set { SetProperty(ref _isSpeedOpen, value); }
        }

        private bool _isControlOpen = false;
        /// <summary>
        /// ControlPopup是否打开
        /// </summary>
        public bool IsControlOpen
        {
            get { return _isControlOpen; }
            set { SetProperty(ref _isControlOpen, value); }
        }
    }
}
