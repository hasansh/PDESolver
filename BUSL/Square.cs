using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public class Square : Geometry
    {
        private static Square _instance;
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public Point Point3 { get; set; }
        public Point Point4 { get; set; }
        public BUSL.Shape Shape { get; set; }
        public double Area
        {
            get
            {
                return System.Math.Abs((Point2.X - Point1.X) * (Point2.X - Point1.X));
            }
        }

        private int ScalingFactor = 10;

        public void Draw(System.Drawing.Graphics graphic, double scale)
        {
            float diameter = graphic.DpiX / ScalingFactor;
            
            System.Drawing.PointF pint1 = new System.Drawing.PointF((float)(Point1.X*scale + diameter), (float)(Point1.Y * scale + diameter));
            System.Drawing.PointF pint2 = new System.Drawing.PointF((float)(Point2.X * scale + diameter), (float)(Point2.Y * scale + diameter));
            System.Drawing.PointF pint3 = new System.Drawing.PointF((float)(Point3.X * scale + diameter), (float)(Point3.Y * scale + diameter));
            System.Drawing.PointF pint4 = new System.Drawing.PointF((float)(Point4.X * scale + diameter), (float)(Point4.Y * scale + diameter));

            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint1, pint2);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint2, pint3);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint3, pint4);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint4, pint1);


            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint1.X - diameter / 2), (float)(pint1.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint2.X - diameter / 2), (float)(pint2.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint3.X - diameter / 2), (float)(pint3.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint4.X - diameter / 2), (float)(pint4.Y - diameter / 2), diameter, diameter);

            var nodefontFamily = new System.Drawing.FontFamily("Times New Roman");
            var nodefont = new System.Drawing.Font(nodefontFamily, 15, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var nodesolidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            graphic.DrawString("(" + this.Point1.X.ToString() + "," + this.Point1.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint1.X, pint1.Y);
            graphic.DrawString("(" + this.Point2.X.ToString() + "," + this.Point2.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint2.X, pint2.Y);
            graphic.DrawString("(" + this.Point3.X.ToString() + "," + this.Point3.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint3.X, pint3.Y);
            graphic.DrawString("(" + this.Point4.X.ToString() + "," + this.Point4.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint4.X, pint4.Y);

        }

        protected Square(Point point1, Point point2, Point point3, Point point4)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
            this.Point4 = point4;
        }

        public static Square GetGeometry(Point point1, Point point2, Point point3, Point point4)
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Square(point1, point2, point3, point4);
            }

            return _instance;
        }
    }
}
