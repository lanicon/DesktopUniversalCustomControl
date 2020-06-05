using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class CoordinateSystem : Shape
    {
        private StreamGeometry streamGeometry;
        private static double XStaticValue = 0.0; //X轴空留距离基准
        private static double YStaticValue = 0.0; //Y轴空留距离基准

        /// <summary>
        /// X轴刻度坐标集合
        /// </summary>
        public Point[] XTicks { get; set; }

        /// <summary> 
        /// Y轴刻度坐标集合
        /// </summary>
        public Point[] YTicks { get; set; } 



        static CoordinateSystem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CoordinateSystem), new FrameworkPropertyMetadata(typeof(CoordinateSystem)));
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                using (StreamGeometryContext context = streamGeometry.Open())
                {
                    DrawCoordinateSystem(context, this);
                }
                return streamGeometry;
            } 
        }

        /// <summary>
        /// 绘制坐标系
        /// </summary>
        /// <param name="context"></param>
        /// <param name="coordinate"></param>
        private void DrawCoordinateSystem(StreamGeometryContext context, CoordinateSystem coordinate)
        {
            //画X轴
            context.BeginFigure(coordinate.CenterPoint, true, false);
            context.LineTo(coordinate.XEndPoint, true, true);            
            Point[] xPoints = new Point[3] { new Point(coordinate.XEndPoint.X - 10, coordinate.XEndPoint.Y - 10), new Point(coordinate.XEndPoint.X + 2, coordinate.XEndPoint.Y), new Point(coordinate.XEndPoint.X - 10, coordinate.XEndPoint.Y + 10) };
            context.BeginFigure(xPoints[0], true, false);
            context.PolyLineTo(xPoints, true, true);

            //画Y轴
            context.BeginFigure(coordinate.CenterPoint, true, false);
            context.LineTo(coordinate.YEndPoint, true, true);            
            Point[] yPoints = new Point[3] { new Point(coordinate.YEndPoint.X - 10, coordinate.YEndPoint.Y + 10), new Point(coordinate.YEndPoint.X, coordinate.YEndPoint.Y), new Point(coordinate.YEndPoint.X + 10, coordinate.YEndPoint.Y + 10) };
            context.BeginFigure(yPoints[0], true, false);
            context.PolyLineTo(yPoints, true, true);

            /***----画刻度----***/
            //X方向
            double XLength = coordinate.XEndPoint.X - coordinate.CenterPoint.X;
            XStaticValue = XLength / coordinate.XCoordinateTicks;
            double XTickInterval = (XLength - XStaticValue / 3) / coordinate.XCoordinateTicks;
            coordinate.XTickValue = XTickInterval;
            XTicks = new Point[coordinate.XCoordinateTicks];
            for (int i = 0; i < XTicks.Length; i++)
            {
                XTicks[i] = new Point(coordinate.CenterPoint.X + (i + 1) * XTickInterval, coordinate.XEndPoint.Y);                   
            }
            DrawTicks(context, XTicks, 9.0, coordinate, "X");

            //Y方向
            double YLength = coordinate.CenterPoint.Y - coordinate.YEndPoint.Y;
            YStaticValue = YLength / coordinate.YCoordinateTicks;
            double YTickInterval = (YLength - YStaticValue / 3) / coordinate.YCoordinateTicks;
            coordinate.YTickValue = YTickInterval;
            YTicks = new Point[coordinate.YCoordinateTicks];
            for (int i = 0; i < YTicks.Length; i++)
            {
                YTicks[i] = new Point(coordinate.CenterPoint.X, coordinate.CenterPoint.Y - (i + 1) * YTickInterval);
            }
            DrawTicks(context, YTicks, 9.0, coordinate, "Y");

            context.Close();
        }

        //画刻度
        private void DrawTicks(StreamGeometryContext context,Point[] beginPoint, double tickHeight, CoordinateSystem coordinate, string axis)
        {
            int index = 0;
            while(index < beginPoint.Length)
            {
                context.BeginFigure(beginPoint[index], true, false);
                if (axis == "X")
                    context.LineTo(new Point(beginPoint[index].X, coordinate.CenterPoint.Y - tickHeight), true, true);
                else if (axis == "Y")
                    context.LineTo(new Point(coordinate.CenterPoint.X + tickHeight, beginPoint[index].Y), true, true);
                else { }
                index++;
            }
        }


        public CoordinateSystem()
        {
            streamGeometry = new StreamGeometry();
        }


        /// <summary>
        /// 坐标原点
        /// </summary>
        public Point CenterPoint
        {
            get { return (Point)GetValue(CenterPointProperty); }
            set { SetValue(CenterPointProperty, value); }
        }

        public static readonly DependencyProperty CenterPointProperty =
            DependencyProperty.Register("CenterPoint", typeof(Point), typeof(CoordinateSystem), new PropertyMetadata(default(Point), OnCoordinateSystemChanged));


        /// <summary>
        /// X轴长度
        /// </summary>
        public Point XEndPoint
        {
            get { return (Point)GetValue(XEndPointProperty); }
            set { SetValue(XEndPointProperty, value); }
        }

        public static readonly DependencyProperty XEndPointProperty =
            DependencyProperty.Register("XEndPoint", typeof(Point), typeof(CoordinateSystem), new PropertyMetadata(default(Point), OnCoordinateSystemChanged));


        /// <summary>
        /// Y轴长度
        /// </summary>
        public Point YEndPoint
        {
            get { return (Point)GetValue(YEndPointProperty); }
            set { SetValue(YEndPointProperty, value); }
        }

        public static readonly DependencyProperty YEndPointProperty =
            DependencyProperty.Register("YEndPoint", typeof(Point), typeof(CoordinateSystem), new PropertyMetadata(default(Point), OnCoordinateSystemChanged));


        /// <summary>
        /// X刻度数
        /// </summary>
        public int XCoordinateTicks
        {
            get { return (int)GetValue(XCoordinateTicksProperty); }
            set { SetValue(XCoordinateTicksProperty, value); }
        }

        public static readonly DependencyProperty XCoordinateTicksProperty =
            DependencyProperty.Register("XCoordinateTicks", typeof(int), typeof(CoordinateSystem), new PropertyMetadata(5, OnCoordinateSystemChanged));


        /// <summary>
        /// Y刻度数
        /// </summary>
        public int YCoordinateTicks
        {
            get { return (int)GetValue(YCoordinateTicksProperty); }
            set { SetValue(YCoordinateTicksProperty, value); }
        }

        public static readonly DependencyProperty YCoordinateTicksProperty =
            DependencyProperty.Register("YCoordinateTicks", typeof(int), typeof(CoordinateSystem), new PropertyMetadata(5, OnCoordinateSystemChanged));


        /// <summary>
        /// X轴平均刻度值
        /// </summary>
        public double XTickValue
        {
            get { return (double)GetValue(XTickValueProperty); }
            set { SetValue(XTickValueProperty, value); }
        }

        public static readonly DependencyProperty XTickValueProperty =
            DependencyProperty.Register("XTickValue", typeof(double), typeof(CoordinateSystem), new PropertyMetadata(default(double)));


        /// <summary>
        /// Y轴平均刻度值
        /// </summary>
        public double YTickValue
        {
            get { return (double)GetValue(YTickValueProperty); }
            set { SetValue(YTickValueProperty, value); }
        }

        public static readonly DependencyProperty YTickValueProperty =
            DependencyProperty.Register("YTickValue", typeof(double), typeof(CoordinateSystem), new PropertyMetadata(default(double)));


        //更新坐标系
        private static void OnCoordinateSystemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d != null && d is CoordinateSystem)
            {
                var coordinate = d as CoordinateSystem;
                coordinate.DrawCoordinateSystem(coordinate.streamGeometry.Open(), coordinate);
            }
        }
    }
}
