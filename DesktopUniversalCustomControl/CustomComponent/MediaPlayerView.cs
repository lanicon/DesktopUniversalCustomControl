using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// 媒体播放器控件
    /// </summary>
    public class MediaPlayerView : Control
    {
        static MediaPlayerView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaPlayerView), new FrameworkPropertyMetadata(typeof(MediaPlayerView)));
        }

        public MediaPlayerView()
        {

        }

        private void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());//反射
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }


        /// <summary>
        /// 资源路径
        /// </summary>
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(MediaPlayerView), new PropertyMetadata(default(Uri), OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var media = d as MediaPlayerView;
            if (media == null) return;
            media.Source = e.NewValue as Uri;
        }


        /// <summary>
        /// 视频资源集合
        /// </summary>
        public IEnumerable<Uri> ItemSource
        {
            get { return (IEnumerable<Uri>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable<Uri>), typeof(MediaPlayerView), new PropertyMetadata(null));


        [Description("音视频状态")]
        public MediaPlayerState MediaState
        {
            get { return (MediaPlayerState)GetValue(MediaStateProperty); }
            set { SetValue(MediaStateProperty, value); }
        }

        public static readonly DependencyProperty MediaStateProperty =
            DependencyProperty.Register("MediaState", typeof(MediaPlayerState), typeof(MediaPlayerView), new PropertyMetadata(MediaPlayerState.Pause, OnMediaStateValueChanged));

        private static void OnMediaStateValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is MediaPlayerView)
            {
                var media = d as MediaPlayerView;
                media.OnMediaStateChanged((MediaPlayerState)e.OldValue, (MediaPlayerState)e.NewValue);
            }
        }


        [Description("音视频状态值更改事件")]
        public event RoutedEventHandler MediaStateChanged
        {
            add
            {
                this.AddHandler(MediaStateChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(MediaStateChangedEvent, value);
            }
        }

        public static readonly RoutedEvent MediaStateChangedEvent =
            EventManager.RegisterRoutedEvent("MediaStateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MediaPlayerView));

        protected virtual void OnMediaStateChanged(MediaPlayerState oldString, MediaPlayerState newString)
        {
            RoutedPropertyChangedEventArgs<MediaPlayerState> arg = new RoutedPropertyChangedEventArgs<MediaPlayerState>(oldString, newString, MediaStateChangedEvent);
            this.RaiseEvent(arg);
        }


        public static bool GetIsPause(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPauseProperty);
        }

        public static void SetIsPause(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPauseProperty, value);
        }

        //是否暂停
        public static readonly DependencyProperty IsPauseProperty =
            DependencyProperty.RegisterAttached("IsPause", typeof(bool), typeof(MediaPlayerView), new PropertyMetadata(false));
    }

    /// <summary>
    /// 播放的音视频顺序
    /// </summary>
    public enum MediaPlayerState
    {
        Last = 0, //上一集
        Next = 1, //下一集
        Pause = 2, //停止
        Play = 3, //播放
        Restart = 4, //重放
    }
}
