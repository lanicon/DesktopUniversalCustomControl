﻿using DesktopUniversalCustomControl.CustomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// 轮播控件
    /// </summary>
    public class CarouselControl : Selector
    {
        static CarouselControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CarouselControl), new FrameworkPropertyMetadata(typeof(CarouselControl)));
        }

        private Canvas Canvas;
        private ImageAnimation ImageNavigate;

        public CarouselControl()
        {
            this.Loaded += CarouselControl_Loaded;
            this.Unloaded += CarouselControl_Unloaded;
        }

        private void CarouselControl_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        private void CarouselControl_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas = this.Template.FindName("imgCanvas", this) as Canvas;
            GetElements();
            GetTopMost();

            this.MouseMove += CarouselControl_MouseMove;
            this.MouseDown += CarouselControl_MouseDown;
            this.MouseUp += CarouselControl_MouseUp;
        }

        //获取最上层的图片索引
        private void GetTopMost()
        {
            List<int> list = new List<int>();
            foreach (var item in elementList)
            {
                list.Add(Panel.GetZIndex(item));
            }

            int index = elementList.FindIndex(p => Panel.GetZIndex(p) == list.Max());
            TopMostIndex = index;
        }

        #region DragMove

        private double originX = 0;
        private double moveDistanceX = 0;
        private double currentX = 0;
        private double inertiaAngle = 0;

        private void CarouselControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            moveDistanceX = 0;
            originX = e.GetPosition(this).X;
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        private void CarouselControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GetTopMost();
            ImageNavigate = null;
            if (inertiaAngle != 0)
            {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
        }

        private void CarouselControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentX = e.GetPosition(this).X;
                moveDistanceX = currentX - originX;
                inertiaAngle = 5 * moveDistanceX;

                for (int i = 0; i < elementList.Count; i++)
                {
                    ImageAnimation imageAnimation = elementList[i];
                    imageAnimation.Angle += moveDistanceX;
                }

                UpdateLocaltion();
                originX = currentX;
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            double dIntervalDegree = inertiaAngle * 0.1;
            for (int i = 0; i < this.elementList.Count; i++)
            {
                ImageAnimation item = this.elementList[i];
                item.Angle += dIntervalDegree;
            }
            UpdateLocaltion();

            inertiaAngle -= dIntervalDegree;
            if (Math.Abs(inertiaAngle) < 0.1)
                CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
        }

        #endregion

        #region ImageItems

        private const double averageAngle = 360D / 6;
        private const double radius = 300D;
        private List<ImageAnimation> elementList;
        private double totalAngle = 0;
        private double centerAngle = 180D;

        private void GetElements()
        {
            totalAngle = Items.Count * averageAngle;
            elementList = new List<ImageAnimation>();
            for (int i = 0; i < Items.Count; i++)
            {
                ImageItemInfo imgInfo = Items[i] as ImageItemInfo;
                ImageAnimation imgItem = new ImageAnimation();
                imgItem.WholeScene.FontSize = 20D;
                imgItem.MouseLeftButtonDown += ImgItem_MouseLeftButtonDown;
                imgItem.MouseLeftButtonUp += ImgItem_MouseLeftButtonUp;
                imgItem.Width = ImageItemWidth;
                imgItem.Height = ImageItemHeight;
                imgItem.Source = new BitmapImage(new Uri(imgInfo.imgUri));
                imgItem.ImageTagName = imgInfo.title;
                imgItem.WholeScenePath = imgInfo.scenePath;
                imgItem.WholeScene.MouseDown += WholeScene_MouseDown; ;
                imgItem.Y = 1D;
                imgItem.Angle = i * averageAngle;
                elementList.Add(imgItem);
            }

            UpdateLocaltion();
        }

        private void WholeScene_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ImgItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ImageNavigate == sender)
            {
                GetTopMost();
                inertiaAngle = centerAngle - ImageNavigate.Angle;
                ImageNavigate = null;
                if (inertiaAngle != 0)
                    CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
                e.Handled = true;
            }
        }

        private void ImgItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageNavigate = sender as ImageAnimation;
        }

        private void UpdateLocaltion()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                ImageAnimation item = elementList[i];
                if (item.Angle - centerAngle >= totalAngle / 2D)
                    item.Angle -= totalAngle;
                else if (centerAngle - item.Angle >= totalAngle / 2D)
                    item.Angle += totalAngle;

                if (item.Angle >= 90D && item.Angle <= 270D)
                    SetElementVisiable(item);
                else
                    SetElementInvisiable(item);
            }
        }

        private void SetElementVisiable(ImageAnimation item)
        {
            if (item == null)
                return;
            if (!Canvas.Children.Contains(item))
            {
                Canvas.Children.Add(item);
            }

            double rad = item.Angle / 180D * Math.PI;
            double centerX = ActualWidth / 2;
            double dX = -radius * Math.Sin(rad);
            item.X = (dX + centerX - ImageItemWidth / 2d);
            double dScale = 0.5 - Math.Cos(rad) / 2D;
            item.ScaleX = dScale;
            item.ScaleY = dScale;
            int nZIndex = (int)(360 * 1000 * (0.5 - Math.Cos(rad) / 2D));
            Canvas.SetZIndex(item, nZIndex);
        }

        private void SetElementInvisiable(ImageAnimation item)
        {
            if (item != null && Canvas.Children.Contains(item))
                Canvas.Children.Remove(item);
        }

        #endregion

        public static readonly DependencyProperty ImageItemWidthProperty =
            DependencyProperty.Register("ImageItemWidth", typeof(double), typeof(CarouselControl), new PropertyMetadata(400D));
        public static readonly DependencyProperty ImageItemHeightProperty =
            DependencyProperty.Register("ImageItemHeight", typeof(double), typeof(CarouselControl), new PropertyMetadata(300D));
        public static readonly DependencyProperty TopMostIndexProperty =
            DependencyProperty.Register("TopMostIndex", typeof(int), typeof(CarouselControl), new PropertyMetadata(0));

        /// <summary>
        /// ImageItem的宽度
        /// </summary>
        public double ImageItemWidth
        {
            get { return (double)GetValue(ImageItemWidthProperty); }
            set { SetValue(ImageItemWidthProperty, value); }
        }

        /// <summary>
        /// ImageItem的长度
        /// </summary>
        public double ImageItemHeight
        {
            get { return (double)GetValue(ImageItemHeightProperty); }
            set { SetValue(ImageItemHeightProperty, value); }
        }

        public int TopMostIndex
        {
            get { return (int)GetValue(TopMostIndexProperty); }
            set { SetValue(TopMostIndexProperty, value); }
        }
    }

    public class ImageItemInfo
    {
        public string imgUri { get; set; }

        public string title { get; set; }

        public string scenePath { get; set; }
    }
}
