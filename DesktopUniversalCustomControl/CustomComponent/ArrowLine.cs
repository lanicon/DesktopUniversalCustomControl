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
    /// ArrowLine
    /// 直线带图标(如箭头、圆、长方形)
    /// </summary>
    public class ArrowLine : Shape
    {
        private const double arrowAngle = 22.5 * Math.PI / 180; //弧度
        private double roateAngle = 0.0;  //旋转角度
        private double ABlength = 0.0; //两点距离
        private double nX2; //新的X2
        private double nY2; //新的Y2
        private StreamGeometry geometry;

        protected override Geometry DefiningGeometry 
        {
            get
            {
                //geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using(StreamGeometryContext context = geometry.Open())
                {
                    DrawArrowLine(context, this);
                }
                //geometry.Freeze();
                return geometry;
            }
        }
        
        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arrowLine"></param>
        /// <remarks>返回Geometry类型</remarks>
        private void DrawArrowLine(StreamGeometryContext context, ArrowLine arrowLine)
        {
            Point startPoint = new Point(arrowLine.X1, arrowLine.Y1);
            context.BeginFigure(startPoint, true, false);
            Point endPoint = new Point(arrowLine.nX2, arrowLine.nY2);
            context.LineTo(endPoint, true, true);

            context.BeginFigure(arrowLine.ArrowPoints[0], true, false);
            context.PolyLineTo(arrowLine.ArrowPoints, true, true);

            context.Close();       
        }


        static ArrowLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ArrowLine), new FrameworkPropertyMetadata(typeof(ArrowLine)));            
        }

        public ArrowLine()
        {           
            geometry = new StreamGeometry();
        }


        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register("X1", typeof(double), typeof(ArrowLine), new PropertyMetadata(0.0, OnPointChanged));


        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2", typeof(double), typeof(ArrowLine), new PropertyMetadata(0.0, OnPointChanged));


        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1", typeof(double), typeof(ArrowLine), new PropertyMetadata(0.0, OnPointChanged));


        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2", typeof(double), typeof(ArrowLine), new PropertyMetadata(0.0, OnPointChanged));

        //获取箭头新坐标集合
        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is ArrowLine)
            {
                var arrowLine = d as ArrowLine;
                if (arrowLine.ArrowPoints != null)
                {
                    //计算角度
                    double angle = Math.Atan2(arrowLine.Y2 - arrowLine.Y1, arrowLine.X2 - arrowLine.X1);
                    double sinA = Math.Sin(angle);
                    double cosA = Math.Cos(angle);

                    arrowLine.ABlength = Math.Sqrt(Math.Pow(arrowLine.X2 - arrowLine.X1, 2) + Math.Pow(arrowLine.Y2 - arrowLine.Y1, 2));
                    arrowLine.nX2 = arrowLine.X1 + arrowLine.ABlength;
                    arrowLine.nY2 = arrowLine.Y1;

                    //弧长=nπr/180; 弧度=nπ/180; n为角度
                    //Console.WriteLine(180 * angle / Math.PI);
                    arrowLine.roateAngle = 180 * angle / Math.PI;
                    SetRotateAngle(arrowLine, arrowLine.roateAngle);

                    arrowLine.ArrowPoints = arrowLine.ArrowPoints.Clone();
                    arrowLine.ArrowPoints.Clear();
                    if(arrowLine.X1 == arrowLine.X2 && arrowLine.Y1 == arrowLine.Y2)
                    {
                        arrowLine.ArrowPoints = new PointCollection(new Point[3] { new Point(0, 0), new Point(0, 0), new Point(0, 0) });
                    }
                    else
                    {
                        arrowLine.ArrowPoints.Add(new Point(Math.Abs(arrowLine.nX2 - arrowLine.ArrowSize), Math.Abs(arrowLine.Y1 - arrowLine.ArrowSize)));
                        arrowLine.ArrowPoints.Add(new Point(arrowLine.nX2 + 2, arrowLine.Y1));
                        arrowLine.ArrowPoints.Add(new Point(Math.Abs(arrowLine.nX2 - arrowLine.ArrowSize), arrowLine.Y1 + arrowLine.ArrowSize));
                    }
                    arrowLine.DrawArrowLine(arrowLine.geometry.Open(), arrowLine);
                }
            }
        }


        /// <summary>
        /// ArrowPoints
        /// 绘制箭头坐标集合
        /// </summary>
        public PointCollection ArrowPoints
        {
            get { return (PointCollection)GetValue(ArrowPointsProperty); }
            set { SetValue(ArrowPointsProperty, value); }
        }

        public static readonly DependencyProperty ArrowPointsProperty =
            DependencyProperty.Register("ArrowPoints", typeof(PointCollection), typeof(ArrowLine), new PropertyMetadata(new PointCollection(new Point[3] { new Point(0, 0), new Point(0, 0), new Point(0, 0) })));


        /// <summary>
        /// ArrowSize
        /// 箭头大小
        /// </summary>
        public double ArrowSize
        {
            get { return (double)GetValue(ArrowSizeProperty); }
            set { SetValue(ArrowSizeProperty, value); }
        }

        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.Register("ArrowSize", typeof(double), typeof(ArrowLine), new PropertyMetadata(10.0, OnPointChanged));


        /// <summary>
        /// ArrowOrientation
        /// 箭头方向
        /// </summary>
        public ArrowOrientation ArrowOrientation
        {
            get { return (ArrowOrientation)GetValue(ArrowOrientationProperty); }
            set { SetValue(ArrowOrientationProperty, value); }
        }

        public static readonly DependencyProperty ArrowOrientationProperty =
            DependencyProperty.Register("ArrowOrientation", typeof(ArrowOrientation), typeof(ArrowLine), new PropertyMetadata(ArrowOrientation.Right, OnPointChanged));


        public static double GetRotateAngle(DependencyObject obj)
        {
            return (double)obj.GetValue(RotateAngleProperty);
        }

        public static void SetRotateAngle(DependencyObject obj, double value)
        {
            obj.SetValue(RotateAngleProperty, value);
        }
        //旋转角度
        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.RegisterAttached("RotateAngle", typeof(double), typeof(ArrowLine), new PropertyMetadata(0.0));
    }

    /// <summary>
    /// 箭头方向
    /// </summary>
    public enum ArrowOrientation
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
    }
}
