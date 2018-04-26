using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public class Point
    {
        public int Id { get; set; }

        public int MainId { get; set; }

        private bool _isBoundaryNode = false;

        public bool IsBoundaryNode
        {
            get { return _isBoundaryNode; }
            set { _isBoundaryNode = value; }
        }

        public double ExactXVelocity { get; set; }
        public double ExactYVelocity { get; set; }
        public double ExactPressure { get; set; }
        public double XVelocity { get; set; }

        public double YVelocity { get; set; }

        public double Pressure { get; set; }

        public double Temprature { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public System.Drawing.Color Color { get; set; }
        public Point(int id,double x, double y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;

        }

        public Point(int mainId, int id, double x, double y)
        {
            this.MainId = mainId;
            this.Id = id;
            this.X = x;
            this.Y = y;

        }
        public Point(double x, double y)
        {
           
            this.X = x;
            this.Y = y;

        }

        public void Draw(BUSL.PointValue valueType, System.Drawing.Graphics graphic, double Scale)
        {
            float diameter = graphic.DpiX / UTL.Constants.ScalingFactor;
            System.Drawing.PointF point = new System.Drawing.PointF((float)(this.X * Scale + diameter), (float)(this.Y * Scale + diameter));
            var nodefontFamily = new System.Drawing.FontFamily("Times New Roman");
            var nodefont = new System.Drawing.Font(nodefontFamily, 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var nodesolidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            //graphic.DrawString(this.Id.ToString() + "(" + this.X.ToString() + "," + this.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, point.X, point.Y);
            switch (valueType)
            {
                case PointValue.Temperature:
                    graphic.DrawString(String.Format("{0:0.00}", this.Temprature), nodefont, System.Drawing.Brushes.Brown, point.X, point.Y);
                    break;
                case PointValue.Pressure:
                    graphic.DrawString(String.Format("{0:0.00}", this.Pressure), nodefont, System.Drawing.Brushes.Brown, point.X, point.Y);
                    break;
                case PointValue.XVelocity:
                    graphic.DrawString(String.Format("{0:0.00}", this.XVelocity), nodefont, System.Drawing.Brushes.Brown, point.X, point.Y);
                    break;
                case PointValue.YVelocity:
                    graphic.DrawString(String.Format("{0:0.00}", this.YVelocity), nodefont, System.Drawing.Brushes.Brown, point.X, point.Y);
                    break;
                default:
                    break;
            }
           
        }
      

    }
}
