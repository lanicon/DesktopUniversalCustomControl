﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DesktopUniversalCustomControl.NotifycationObject
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }

        #region PublicData


        private Point _clickPoint;
        /// <summary>
        /// 鼠标点击坐标
        /// </summary>
        public Point ClickPoint
        {
            get { return _clickPoint; }
            set => SetProperty(ref _clickPoint, value);
        }

        #endregion     
    }
}
