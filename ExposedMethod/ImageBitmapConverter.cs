using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomControl.ExposedMethod
{
    /// <summary>
    /// Image与Bitmap之间相互转换
    /// </summary>
    public sealed class ImageBitmapConverter
    {
        [DllImport("Gdi32.dll")]
        private static extern bool DeleteObject(IntPtr intPtr);

        /// <summary>
        /// 转为ImageSource
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageSource ToImageSource(Bitmap bitmap)
        {
            IntPtr intPtr = bitmap.GetHbitmap();
            try
            {
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return imageSource;
            }
            finally
            {
                DeleteObject(intPtr);
            }
        }

        /// <summary>
        /// 转化为Bitmap
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(ms);
                Bitmap bitmap = new Bitmap(ms);
                //ms.Close();
                //bitmap.Dispose();
                return bitmap;
            }
        }
    }
}
