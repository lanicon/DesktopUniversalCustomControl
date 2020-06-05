using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.CustomView.MsgDlg;
using DesktopUniversalCustomControl.ExposedMethod;
using DesktopUniversalCustomControl.NotifycationObject;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace DesktopUniversalCustomControl.CustomView.MediaView
{
    /// <summary>
    /// UserMediaView.xaml 的交互逻辑
    /// </summary>
    public partial class UserMediaView : UserControl
    {
        MediaViewModel mediaViewModel = new MediaViewModel();
        DispatcherTimer timer; //播放进度时钟
        DispatcherTimer controlTimer; //控制界面显示时钟

        public UserMediaView()
        {
            InitializeComponent();

            this.DataContext = mediaViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer = TimerClock.IntervalMultiSeconds(timer, 1, RefreshSlider);
            controlTimer = TimerClock.IntervalMultiSeconds(controlTimer, 3, ControlPopupIsShow);

            //mediaElement.MouseLeftButtonUp += MediaElement_MouseLeftButtonUp;
            MouseDoubleClick += UserMediaView_MouseDoubleClick;
        }

        #region 控制界面显示与否

        private void ParentGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!ControlGrid.IsMouseOver)
                controlTimer.Start();
        }

        private void ParentGrid_MouseMove(object sender, MouseEventArgs e)
        {
            mediaViewModel.IsControlOpen = true;
            controlTimer.Start();
        }

        private void ControlGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            controlTimer.Start();
        }

        private void ControlPopupIsShow()
        {
            controlTimer.Stop();
            mediaViewModel.IsControlOpen = false;
        }

        #endregion

        #region 更改播放媒体位置

        private void playSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
        }

        private void playSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            mediaElement.Position = TimeSpan.FromSeconds(playSlider.Value);
        }

        private void playSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            timer.Stop();
        }

        private void playSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            timer.Start();
            mediaElement.Position = TimeSpan.FromSeconds(playSlider.Value);
        }

        #endregion

        #region 播放、暂停、重放

        private void PlayPause_Control(object sender, RoutedEventArgs e)
        {          
            if (!play_pause.IsChecked.Value)
                Pause();
            else
                Play();
        }

        private void MediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (play_pause.IsChecked.Value)
            {
                Pause();
                play_pause.IsChecked = false;                             
            }
            else
            {
                Play();
                play_pause.IsChecked = true;                              
            }                
        }

        //播放
        private void Play()
        {
            mediaElement.Play();
            timer.Start();
        }

        //暂停
        private void Pause()
        {
            mediaElement.Pause();
            timer.Stop();
        }

        //重放
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            Play();
            play_pause.IsChecked = true;
        }

        #endregion

        #region 音量控制

        private void volume_MouseEnter(object sender, MouseEventArgs e)
        {
            mediaViewModel.IsVolumeOpen = true;
        }

        private void volume_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(volume).Y >= 30 || e.GetPosition(volume).X <= 0 || e.GetPosition(volume).X >= 30)
                mediaViewModel.IsVolumeOpen = false;
        }

        private void VolumePopup_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(volume_grid).Y <= 0 || e.GetPosition(volume_grid).X <= 0 || e.GetPosition(volume_grid).X >= 30)
                mediaViewModel.IsVolumeOpen = false;
        }

        private double mutedVolumeValue = 0.0;
        //静音模式
        private void Volume_Click(object sender, RoutedEventArgs e)
        {
            if (volume.Kind == IconType.VolumeOff)
            {
                mediaElement.IsMuted = false;
                volumeSlider.Value = mutedVolumeValue;
            }
            else
            {
                mediaElement.IsMuted = true;
                mutedVolumeValue = volumeSlider.Value;
                volumeSlider.Value = 0;
            }
        }

        //音量值
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            volume_value.Text = ((int)(volumeSlider.Value * 100)).ToString();

            if (volumeSlider.Value > 0)
                mediaElement.IsMuted = false;
            else
                mediaElement.IsMuted = true;

            if (volumeSlider.Value > 0.5 && volumeSlider.Value <= 1.0)
            {
                volume.Kind = IconType.VolumeHigh;
            }
            else if (volumeSlider.Value > 0 && volumeSlider.Value <= 0.5)
            {
                volume.Kind = IconType.VolumeMedium;
            }
            else
            {
                volume.Kind = IconType.VolumeOff;
            }
        }

        #endregion

        #region 倍速控制

        private void playSpeed_MouseEnter(object sender, MouseEventArgs e)
        {
            mediaViewModel.IsSpeedOpen = true;
        }

        private void playSpeed_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(playSpeed).Y >= 30 || e.GetPosition(playSpeed).X <= 0 || e.GetPosition(playSpeed).X >= 45)
                mediaViewModel.IsSpeedOpen = false;
        }

        private void SpeedPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(speed_grid).Y <= 0 || e.GetPosition(speed_grid).X <= 0 || e.GetPosition(speed_grid).X >= 45)
                mediaViewModel.IsSpeedOpen = false;
        }

        Double[] speed = new double[7] { 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0 };
        //播放速度
        private void speedSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.RemovedItems.Count > 0)
            {
                mediaElement.SpeedRatio = speed[speedSlider.SelectedIndex];
            }
        }

        #endregion

        #region 其它控制

        //更新SliderValue
        private void RefreshSlider()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                playSlider.Value = mediaElement.Position.TotalSeconds;
            }));
        }

        //媒体加载完
        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            playSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            volumeSlider.Value = mediaElement.Volume;
            timer.Start();
        }

        //媒体播放结束
        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(0);
            playSlider.Value = 0;
            Pause();
            play_pause.IsChecked = false;
            mediaElement.LoadedBehavior = MediaState.Manual;
        }

        //媒体播放错误
        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageDialog.Show("播放错误：" + e.ErrorException.Message);
        }

        //全屏、退出全屏
        private void FullScreent_Click(object sender, RoutedEventArgs e)
        {
            if (fullScreent.IsChecked == true)
            {
                Window.GetWindow(this).WindowState = WindowState.Maximized;
            }
            else
            {
                Window.GetWindow(this).WindowState = WindowState.Normal;
            }
        }

        //双击全屏
        private void UserMediaView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            fullScreent.IsChecked = true;
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        //上一个
        private void Last_Click(object sender, RoutedEventArgs e)
        {
            var mediaPlayerView = userMedia.TemplatedParent as MediaPlayerView;
            int index = mediaPlayerView.ItemSource.ToList().IndexOf(mediaPlayerView.Source);
            index -= 1;
            if (index >= 0)
            {
                mediaPlayerView.Source = mediaPlayerView.ItemSource.ElementAt(index);
            }
        }

        //下一个
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            var mediaPlayerView = userMedia.TemplatedParent as MediaPlayerView;
            int index = mediaPlayerView.ItemSource.ToList().IndexOf(mediaPlayerView.Source);
            index += 1;
            if (index < mediaPlayerView.ItemSource.Count())
            {
                mediaPlayerView.Source = mediaPlayerView.ItemSource.ElementAt(index);
            }
        }

        #endregion

        /// <summary>
        /// ControlGrid显示与隐藏
        /// </summary>
        /// <param name="isControlPop">是否打开</param>
        /// <param name="times">动画时间</param>
        private void CollapsedControl(bool isControlPop, double times = 3.0)
        {
            //ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
            //Duration duration = new Duration(TimeSpan.FromSeconds(3));
            //DiscreteObjectKeyFrame discreteObjectKeyFrame = new DiscreteObjectKeyFrame(Visibility.Collapsed);
            //objectAnimationUsingKeyFrames.Duration = duration;
            //objectAnimationUsingKeyFrames.KeyFrames.Add(discreteObjectKeyFrame);
            //AnimationClock clock = objectAnimationUsingKeyFrames.CreateClock();
            //ControlGrid.ApplyAnimationClock(VisibilityProperty, clock);

            BooleanAnimationUsingKeyFrames booleanAnimationUsingKeyFrames = new BooleanAnimationUsingKeyFrames();
            Duration duration = new Duration(TimeSpan.FromSeconds(times));
            DiscreteBooleanKeyFrame discreteBooleanKeyFrame = new DiscreteBooleanKeyFrame(isControlPop);
            booleanAnimationUsingKeyFrames.Duration = duration;
            booleanAnimationUsingKeyFrames.KeyFrames.Add(discreteBooleanKeyFrame);
            AnimationClock clock = booleanAnimationUsingKeyFrames.CreateClock();
            //ControlPop.ApplyAnimationClock(CustomPopupEx.IsOpenProperty, clock);
        }
    }
}
