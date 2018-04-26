using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace FEMProject.BUSL
{
    class P1TriangularElement : TriangularElement
    {

        private double _area;


        public override double Area
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        private int _id;
        public override int Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private double _d_1_1;

        protected override double D_1_1
        {
            get
            {
                return _d_1_1;
            }
        }
        private double _d_1_2;
        protected override double D_1_2
        {
            get
            {
                return _d_1_2;
            }
        }
        private double _d_2_1;
        protected override double D_2_1
        {
            get
            {
                return _d_2_1;
            }
        }
        private double _d_2_2;
        protected override double D_2_2
        {
            get
            {
                return _d_2_2;
            }
        }

        protected override Matrix<double> G_1_1
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override Matrix<double> G_1_2
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override Matrix<double> G_2_1
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override Matrix<double> G_2_2
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public override Point MainNode1
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override Point MainNode2
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override Point MainNode3
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override Matrix<double> MassMatrix
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override List<Point> PointsList
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private Matrix<double> _stiffnessMatrix;
        public override Matrix<double> StiffnessMatrix
        {
            get
            {
                return _stiffnessMatrix;
            }
        }

        public override void Draw(System.Drawing.Graphics graphic, double Scale)
        {
            float diameter = graphic.DpiX / UTL.Constants.ScalingFactor;
            System.Drawing.PointF pint1 = new System.Drawing.PointF((float)(MainNode1.X * Scale + diameter), (float)(MainNode1.Y * Scale + diameter));
            System.Drawing.PointF pint2 = new System.Drawing.PointF((float)(MainNode2.X * Scale + diameter), (float)(MainNode2.Y * Scale + diameter));
            System.Drawing.PointF pint3 = new System.Drawing.PointF((float)(MainNode3.X * Scale + diameter), (float)(MainNode3.Y * Scale + diameter));


            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint1, pint2);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint2, pint3);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint3, pint1);

            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint1.X - diameter / 2), (float)(pint1.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint2.X - diameter / 2), (float)(pint2.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint3.X - diameter / 2), (float)(pint3.Y - diameter / 2), diameter, diameter);


            var fontFamily = new System.Drawing.FontFamily("Times New Roman");
            var elemntfont = new System.Drawing.Font(fontFamily, 24, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 0, 0, 255));

            var nodefontFamily = new System.Drawing.FontFamily("Times New Roman");
            var nodefont = new System.Drawing.Font(fontFamily, 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var nodesolidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            graphic.DrawString(this.MainNode1.Id.ToString() + "(" + this.MainNode1.X.ToString() + "," + this.MainNode1.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint1.X, pint1.Y);
            graphic.DrawString(this.MainNode2.Id.ToString() + "(" + this.MainNode2.X.ToString() + "," + this.MainNode2.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint2.X, pint2.Y);
            graphic.DrawString(this.MainNode3.Id.ToString() + "(" + this.MainNode3.X.ToString() + "," + this.MainNode3.Y.ToString() + ")", nodefont, System.Drawing.Brushes.Brown, pint3.X, pint3.Y);


        }


        public P1TriangularElement(Point point1, Point point2, Point point3)

        {
            this.MainNode1 = point1;
            this.MainNode2 = point2;
            this.MainNode3 = point3;
            this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;

            this._d_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            this._d_1_1 = (MainNode3.X - MainNode2.X) / (2 * _area);
            this._d_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            this._d_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            this._d_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);

            this._stiffnessMatrix = (D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_1 * G_1_1) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_2 * D_2_1 * G_1_2) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2);
        }

        public P1TriangularElement()
        {
            //this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;

            //this.D_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            //this.D_1_1 = (MainNode3.X - MainNode2.X) / (2 * _area);
            //this.D_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            //this.D_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            //this.D_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);

            //this._stiffnessMatrix = (D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_1 * G_1_1) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_2 * D_2_1 * G_1_2) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2);
        }

        public override void Refresh()
        {
            this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;
            this._d_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            this._d_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            this._d_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            this._d_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);
            this._stiffnessMatrix = (D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_1 * G_1_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_1 * D_2_1 * G_1_2) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2);
            PointsList.Add(MainNode1);
            PointsList.Add(MainNode2);
            PointsList.Add(MainNode3);

        }


    }
}
