using DesktopUniversalCustomControl.ExposedMethod;
using QRCoder;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// QRCodeControl二维码控件
    /// </summary>
    public class QRCodeControl : Control
    {
        static QRCodeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QRCodeControl), new FrameworkPropertyMetadata(typeof(QRCodeControl)));
        }

        public QRCodeControl()
        {
            //this.QrIcon = new BitmapImage(new Uri(GetPathHelper.GetBinParentPath() + "\\Resource\\Images\\msgIcon.png"));
            //this.QrCodeContent = "She is my wife";
            //GetQRCodeImage(this, QrCodeContent, null);

            GetQRCodeImage(this);
        }

        /// <summary>
        /// 得到二维码
        /// </summary>
        /// <param name="qRCodeControl"></param>
        /// <param name="content">二维码内容</param>
        /// <param name="icon">二维码图标</param>
        /// <returns></returns>
        private static ImageSource GetQRCodeImage(QRCodeControl qrCodeControl)
        {
            //Console.WriteLine(qrCodeControl.GetValue(ForegroundProperty).ToString() + "|" + qrCodeControl.GetValue(BackgroundProperty).ToString());

            System.Drawing.ColorConverter colorConverter = new System.Drawing.ColorConverter();
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qRCodeData = qrGenerator.CreateQrCode(qrCodeControl.QrCodeContent, QRCodeGenerator.ECCLevel.Q))
                {
                    using (QRCode qrCode = new QRCode(qRCodeData))
                    {
                        Bitmap codeImage = qrCode.GetGraphic(
                            qrCodeControl.QrCodePixelsPerModule,
                            (System.Drawing.Color)colorConverter.ConvertFromString(qrCodeControl.Foreground.ToString()),
                            System.Drawing.Color.FromName(((SolidColorBrush)qrCodeControl.Background).Color.ToString()),
                            ImageBitmapConverter.ToBitmap(qrCodeControl.QrCodeIcon),
                            qrCodeControl.QrCodeIconSizePercent,
                            qrCodeControl.QrCodeIconBorderWidth);
                        qrCodeControl.QRCodeImage = ImageBitmapConverter.ToImageSource(codeImage);
                        //qrCodeControl.QRCodeImage = Imaging.CreateBitmapSourceFromHBitmap(codeImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        return qrCodeControl.QRCodeImage;
                    }
                }
            }
        }


        /// <summary>
        /// CornerRadius      
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(QRCodeControl), new PropertyMetadata());


        /// <summary>
        /// QRCodeImage
        /// </summary>
        public ImageSource QRCodeImage
        {
            get { return (ImageSource)GetValue(QRCodeImageProperty); }
            set { SetValue(QRCodeImageProperty, value); }
        }

        public static readonly DependencyProperty QRCodeImageProperty =
            DependencyProperty.Register("QRCodeImage", typeof(ImageSource), typeof(QRCodeControl), new PropertyMetadata(OnQRCodeImageChanged));

        private static void OnQRCodeImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var qrCodeControl = d as QRCodeControl;
            if (qrCodeControl.QRCodeImage == null || qrCodeControl.QRCodeImage.ToString() == string.Empty)
                qrCodeControl.QRCodeImage = GetQRCodeImage(qrCodeControl);
        }


        /// <summary>
        /// 二维码内容
        /// </summary>
        public string QrCodeContent
        {
            get { return (string)GetValue(QrCodeContentProperty); }
            set { SetValue(QrCodeContentProperty, value); }
        }

        public static readonly DependencyProperty QrCodeContentProperty =
            DependencyProperty.Register("QrCodeContent", typeof(string), typeof(QRCodeControl), new PropertyMetadata("**我是写代码的厨子**", GetChangedQRCodeImage));


        /// <summary>
        /// 二维码图标
        /// </summary>
        public BitmapSource QrCodeIcon
        {
            get { return (BitmapSource)GetValue(QrCodeIconProperty); }
            set { SetValue(QrCodeIconProperty, value); }
        }

        public static readonly DependencyProperty QrCodeIconProperty =
            DependencyProperty.Register("QrCodeIcon", typeof(BitmapSource), typeof(QRCodeControl), new PropertyMetadata(GetChangedQRCodeImage));


        /// <summary>
        /// 图标百分比大小
        /// </summary>
        public int QrCodeIconSizePercent
        {
            get { return (int)GetValue(QrCodeIconSizePercentProperty); }
            set { SetValue(QrCodeIconSizePercentProperty, value); }
        }

        public static readonly DependencyProperty QrCodeIconSizePercentProperty =
            DependencyProperty.Register("QrCodeIconSizePercent", typeof(int), typeof(QRCodeControl), new PropertyMetadata(15, GetChangedQRCodeImage));


        /// <summary>
        /// 图标外边框Border
        /// </summary>
        public int QrCodeIconBorderWidth
        {
            get { return (int)GetValue(QrCodeIconBorderWidthProperty); }
            set { SetValue(QrCodeIconBorderWidthProperty, value); }
        }

        public static readonly DependencyProperty QrCodeIconBorderWidthProperty =
            DependencyProperty.Register("QrCodeIconBorderWidth", typeof(int), typeof(QRCodeControl), new PropertyMetadata(6, GetChangedQRCodeImage));


        /// <summary>
        /// 二维码前置颜色
        /// </summary>
        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public new static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(QRCodeControl), new PropertyMetadata(Brushes.Red, GetChangedQRCodeImage));


        /// <summary>
        /// 二维码背景颜色
        /// </summary>
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(QRCodeControl), new PropertyMetadata(Brushes.PaleGreen, GetChangedQRCodeImage));


        /// <summary>
        /// 二维码像素
        /// </summary>
        public int QrCodePixelsPerModule
        {
            get { return (int)GetValue(QrCodePixelsPerModuleProperty); }
            set { SetValue(QrCodePixelsPerModuleProperty, value); }
        }

        public static readonly DependencyProperty QrCodePixelsPerModuleProperty =
            DependencyProperty.Register("QrCodePixelsPerModule", typeof(int), typeof(QRCodeControl), new PropertyMetadata(30, GetChangedQRCodeImage));

        //属性更改时
        private static void GetChangedQRCodeImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var qrCodeControl = d as QRCodeControl;
                if (qrCodeControl.QrCodeIconSizePercent < 0)
                    qrCodeControl.QrCodeIconSizePercent = 0;
                if (qrCodeControl.QrCodeIconSizePercent > 20)
                    qrCodeControl.QrCodeIconSizePercent = 20;
                if (qrCodeControl.QrCodeIconBorderWidth <= 0)
                    qrCodeControl.QrCodeIconBorderWidth = 1;
                if (qrCodeControl.QrCodePixelsPerModule <= 0)
                    qrCodeControl.QrCodePixelsPerModule = 1;
                if (qrCodeControl.QrCodePixelsPerModule > 200)
                    qrCodeControl.QrCodePixelsPerModule = 200;

                //if (e.Property == ForegroundProperty)
                //    qrCodeControl.SetValue(ForegroundProperty, e.NewValue);
                //if (e.Property == BackgroundProperty)
                //    qrCodeControl.SetValue(BackgroundProperty, e.NewValue);

                qrCodeControl.QRCodeImage = GetQRCodeImage(qrCodeControl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "------");
            }
        }
    }
}
