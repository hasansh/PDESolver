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
    class P2TriangularElement : TriangularElement
    {


        #region Properites
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
                return _id;
            }

            set
            {
                _id = value;
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

        #region G
        private Matrix<double> _g_1_1 = DenseMatrix.OfArray(new double[,]
      {
           {.50000000000000000000, .00000000000000000000, .16666666666666666667, .00000000000000000000, .00000000000000000000, -.66666666666666666667},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000},
            {.16666666666666666667, .00000000000000000000, .50000000000000000000, .00000000000000000000, .00000000000000000000, -.66666666666666666667},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, 1.33333333333333333333, -1.33333333333333333333, .00000000000000000000},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, -1.33333333333333333333, 1.33333333333333333333, .00000000000000000000},
            {-.66666666666666666667, .00000000000000000000, -.66666666666666666667, .00000000000000000000, .00000000000000000000, 1.33333333333333333333}
      });
        protected override Matrix<double> G_1_1
        {
            get
            {
                return _g_1_1;
            }
        }

        private Matrix<double> _g_1_2 = DenseMatrix.OfArray(new double[,]
        {
            {.00000000000000000000, -.16666666666666666667, .16666666666666666667, .66666666666666666667, .00000000000000000000, -.66666666666666666667},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000},
            {.00000000000000000000, .16666666666666666667, .50000000000000000000, .00000000000000000000, -.66666666666666666667, .00000000000000000000},
            {.00000000000000000000, .66666666666666666667, .00000000000000000000, .66666666666666666667, -.66666666666666666667, -.66666666666666666667},
            {.00000000000000000000, -.66666666666666666667, .00000000000000000000, -.66666666666666666667, .66666666666666666667, .66666666666666666667},
            {.00000000000000000000, .00000000000000000000, -.66666666666666666667, -.66666666666666666667, .66666666666666666667, .66666666666666666667}
        });

        protected override Matrix<double> G_1_2
        {
            get
            {
                return _g_1_2;
            }
        }

        private Matrix<double> _g_2_1 = DenseMatrix.OfArray(new double[,]
        {
           {.00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000},
            {-.16666666666666666667, .00000000000000000000, .16666666666666666667, .66666666666666666667, -.66666666666666666667, .00000000000000000000},
            {.16666666666666666667, .00000000000000000000, .50000000000000000000, .00000000000000000000, .00000000000000000000, -.66666666666666666667},
            {.66666666666666666667, .00000000000000000000, .00000000000000000000, .66666666666666666667, -.66666666666666666667, -.66666666666666666667},
            {.00000000000000000000, .00000000000000000000, -.66666666666666666667, -.66666666666666666667, .66666666666666666667, .66666666666666666667},
            {-.66666666666666666667, .00000000000000000000, .00000000000000000000, -.66666666666666666667, .66666666666666666667, .66666666666666666667}

        });

        protected override Matrix<double> G_2_1
        {
            get
            {
                return _g_2_1;
            }
        }

        private Matrix<double> _g_2_2 = DenseMatrix.OfArray(new double[,]
      {
           {.00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000, .00000000000000000000},
            {.00000000000000000000, .50000000000000000000, .16666666666666666667, .00000000000000000000, -.66666666666666666667, .00000000000000000000},
            {.00000000000000000000, .16666666666666666667, .50000000000000000000, .00000000000000000000, -.66666666666666666667, .00000000000000000000},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, 1.33333333333333333333, .00000000000000000000, -1.33333333333333333333},
            {.00000000000000000000, -.66666666666666666667, -.66666666666666666667, .00000000000000000000, 1.33333333333333333333, .00000000000000000000},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, -1.33333333333333333333, .00000000000000000000, 1.33333333333333333333}
      });

        protected override Matrix<double> G_2_2
        {
            get
            {
                return _g_2_2;
            }
        }
        #endregion

        private Matrix<double> _massMatrix = DenseMatrix.OfArray(new double[,]
      {
           {.01666666666666666667, -.00277777777777777778, -.00277777777777777778, .00000000000000000000, -.01111111111111111111, .00000000000000000000},
            {-.00277777777777777778, .01666666666666666667, -.00277777777777777778, .00000000000000000000, .00000000000000000000, -.01111111111111111111},
            {-.00277777777777777778, -.00277777777777777778, .01666666666666666667, -.01111111111111111111, .00000000000000000000, .00000000000000000000},
            {.00000000000000000000, .00000000000000000000, -.01111111111111111111, .08888888888888888889, .04444444444444444444, .04444444444444444444},
            {-.01111111111111111111, .00000000000000000000, .00000000000000000000, .04444444444444444444, .08888888888888888889, .04444444444444444444},
            {.00000000000000000000, -.01111111111111111111, .00000000000000000000, .04444444444444444444, .04444444444444444444, .08888888888888888889}
      });

        public Matrix<double> D1Matrix
        {
            get
            {
                return _d1Matrix;
            }
        }

        private Matrix<double> _d1Matrix;
        public Matrix<double> D2Matrix
        {
            get
            {
                return _d2Matrix;
            }
        }

        private Matrix<double> _d2Matrix;


        #region H
        public Matrix<double> H1Matrix
        {
            get
            {
                return _h1Matrix;
            }
        }

        private Matrix<double> _h1Matrix = DenseMatrix.OfArray(new double[,]
      {
            {.16666666666666666667, .00000000000000000000, .00000000000000000000, .16666666666666666667, -.16666666666666666667, -.16666666666666666667},
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, .33333333333333333333, -.33333333333333333333, .00000000000000000000},
            {.00000000000000000000, .00000000000000000000, -.16666666666666666667, .16666666666666666667, -.16666666666666666667, .16666666666666666667}
      });
        public Matrix<double> H2Matrix
        {
            get
            {
                return _h2Matrix;
            }
        }

        private Matrix<double> _h2Matrix = DenseMatrix.OfArray(new double[,]
      {
            {.00000000000000000000, .00000000000000000000, .00000000000000000000, .33333333333333333333, .00000000000000000000, -.33333333333333333333},
            {.00000000000000000000, .16666666666666666667, .00000000000000000000, .16666666666666666667, -.16666666666666666667, -.16666666666666666667},
            {.00000000000000000000, .00000000000000000000, -.16666666666666666667, .16666666666666666667, .16666666666666666667, -.16666666666666666667}
      });
        public override Matrix<double> MassMatrix
        {
            get
            {
                return 2 * _area * _massMatrix;
            }
        }
        #endregion

        
        #region Gy
        private Matrix<double> _gk_1_1 = DenseMatrix.OfArray(new double[,]
       {
            {.030952, .000000, .007143, .009524, -.009524, -.038095 },
            {-.003571, .000000, -.004365, -.006349, .006349, .007936},
            {-.003571, .000000, .003571, .001587, -.001587, .000000},
            {.019048, .000000, .006349, -.012698, .012698, -.025397},
            {.004762, .000000, .007936, -.019048, .019048, -.012698},
            {.019048, .000000, .012698, -.006349, .006349, -.031746}
       });


        private Matrix<double> _gk_1_2 = DenseMatrix.OfArray(new double[,]
        {
            {.000000, -.007143, .007143, .047619, .000000, -.047619},
            {.000000, -.003571, -.004365, -.006349, .007936, .006349},
            {.000000, .004365, .003571, -.006349, -.007936, .006349},
            {.000000, -.012698, .006349, .019048, .006349, -.019048},
            {.000000, -.007936, .007936, -.006349, .000000, .006349},
            {.000000, -.006349, .012698, .019048, -.006349, -.019048}
        });

        private Matrix<double> _gk_2_1 = DenseMatrix.OfArray(new double[,]
        {
            {-.003571, .000000, -.004365, -.006349, .006349, .007936},
            {-.007143, .000000, .007143, .047619, -.047619, .000000},
            {.004365, .000000, .003571, -.006349, .006349, -.007936},
            {-.012698, .000000, .006349, .019048, -.019048, .006349},
            {-.006349, .000000, .012698, .019048, -.019048, -.006349},
            {-.006349, .000000, .012698, .019048, -.019048, -.006349}
        });

        private Matrix<double> _gk_2_2 = DenseMatrix.OfArray(new double[,]
        {
            { .000000, -.003571, -.004365, -.006349, .007936, .006349},
            {.000000, .030952, .007143, .009524, -.038095, -.009524},
            {.000000, -.003571, .003571, .001587, .000000, -.001587},
            {.000000, .019048, .006349, -.012698, -.025397, .012698},
            {.000000, .019048, .012698, -.006349, -.031746, .006349},
            {.000000, .004762, .007936, -.019048, -.012698, .019048}
        });


        private Matrix<double> _gk_3_1 = DenseMatrix.OfArray(new double[,]
         {
            {-.003571, .000000, .003571, .001587, -.001587, .000000},
            {.004365, .000000, .003571, -.006349, .006349, -.007936},
            {-.007143, .000000, -.030952, .009524, -.009524, .038095},
            {-.007936, .000000, -.004762, -.019048, .019048, .012698},
            {-.006349, .000000, -.019048, -.012698, .012698, .025397},
            {-.012698, .000000, -.019048, -.006349, .006349, .031746}
          });

        private Matrix<double> _gk_3_2 = DenseMatrix.OfArray(new double[,]
         {
            {.000000, .004365, .003571, -.006349, -.007936, .006349},
            {.000000, -.003571, .003571, .001587, .000000, -.001587},
            {.000000, -.007143, -.030952, .009524, .038095, -.009524},
            {.000000, -.007936, -.004762, -.019048, .012698, .019048},
            {.000000, -.012698, -.019048, -.006349, .031746, .006349},
            {.000000, -.006349, -.019048, -.012698, .025397, .012698}
          });

        private Matrix<double> _gk_4_1 = DenseMatrix.OfArray(new double[,]
           {
            {.019048, .000000, .006349, -.012698, .012698, -.025397},
            {-.012698, .000000, .006349, .019048, -.019048, .006349},
            {-.007936, .000000, -.004762, -.019048, .019048, .012698},
            {.063492, .000000, .038095, .152381, -.152381, -.101587},
            {.006349, .000000, -.006349, .076190, -.076190, .000000},
            {.031746, .000000, -.006349, .050794, -.050794, -.025397}
           });
        private Matrix<double> _gk_4_2 = DenseMatrix.OfArray(new double[,]
        {
            {.000000, -.012698, .006349, .019048, .006349, -.019048},
            {.000000, .019048, .006349, -.012698, -.025397, .012698},
            {.000000, -.007936, -.004762, -.019048, .012698, .019048},
            {.000000, .063492, .038095, .152381, -.101587, -.152381},
            {.000000, .031746, -.006349, .050794, -.025397, -.050794},
            {.000000, .006349, -.006349, .076190, .000000, -.076190}
        });


        private Matrix<double> _gk_5_1 = DenseMatrix.OfArray(new double[,]
      {
            {.004762, .000000, .007936, -.019048, .019048, -.012698},
            {-.006349, .000000, .012698, .019048, -.019048, -.006349},
            {-.006349, .000000, -.019048, -.012698, .012698, .025397},
            {.006349, .000000, -.006349, .076190, -.076190, .000000},
            {-.038095, .000000, -.063492, .152381, -.152381, .101587},
            {.006349, .000000, -.031746, .050794, -.050794, .025397}
      });

        private Matrix<double> _gk_5_2 = DenseMatrix.OfArray(new double[,]
       {
            {.000000, -.007936, .007936, -.006349, .000000, .006349},
            {.000000, .019048, .012698, -.006349, -.031746, .006349},
            {.000000, -.012698, -.019048, -.006349, .031746, .006349},
            {.000000, .031746, -.006349, .050794, -.025397, -.050794},
            {.000000, .063492, -.063492, .050794, .000000, -.050794},
            {.000000, .006349, -.031746, .050794, .025397, -.050794}
       });

        private Matrix<double> _gk_6_1 = DenseMatrix.OfArray(new double[,]
        {
            {.019048, .000000, .012698, -.006349, .006349, -.031746},
            {-.007936, .000000, .007936, -.006349, .006349, .000000},
            {-.012698, .000000, -.019048, -.006349, .006349, .031746},
            {.031746, .000000, -.006349, .050794, -.050794, -.025397},
            {.006349, .000000, -.031746, .050794, -.050794, .025397},
            {.063492, .000000, -.063492, .050794, -.050794, .000000}
        });
        private Matrix<double> _gk_6_2 = DenseMatrix.OfArray(new double[,]
        {
            {.000000, -.006349, .012698, .019048, -.006349, -.019048},
            {.000000, .004762, .007936, -.019048, -.012698, .019048},
            {.000000, -.006349, -.019048, -.012698, .025397, .012698},
            {.000000, .006349, -.006349, .076190, .000000, -.076190},
            {.000000, .006349, -.031746, .050794, .025397, -.050794},
            {.000000, -.038095, -.063492, .152381, .101587, -.152381}
        });

       
        public Matrix<double> Gx_1 { get; set; }
        public Matrix<double> Gx_2 { get; set; }
        public Matrix<double> Gx_3 { get; set; }
        public Matrix<double> Gx_4 { get; set; }
        public Matrix<double> Gx_5 { get; set; }
        public Matrix<double> Gx_6 { get; set; }

        public Matrix<double> Gy_1 { get; set; }

        public Matrix<double> Gy_2 { get; set; }
        public Matrix<double> Gy_3 { get; set; }
        public Matrix<double> Gy_4 { get; set; }
        public Matrix<double> Gy_5 { get; set; }
        public Matrix<double> Gy_6 { get; set; }


        #endregion



        private List<Point> _pointsList = new List<Point>();

        public override List<Point> PointsList
        {
            get
            {
                return _pointsList;
            }
            set
            {
                _pointsList = value;
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


        public override Point MainNode1 { get; set; }
        public override Point MainNode2 { get; set; }
        public override Point MainNode3 { get; set; }
        public Point MiddlePoint1 { get; set; }

        public Point MiddlePoint2 { get; set; }

        public Point MiddlePoint3 { get; set; }
        #endregion

        #region Ctors
        public P2TriangularElement(Point point1, Point point2, Point point3, Point point4, Point point5, Point point6)

        {
            this.MainNode1 = point1;
            this.MainNode2 = point2;
            this.MainNode3 = point3;
            this.MiddlePoint1 = point4;
            this.MiddlePoint2 = point5;
            this.MiddlePoint3 = point6;
            this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;

            this._d_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            this._d_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            this._d_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            this._d_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);

            this._stiffnessMatrix = (D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_1 * G_1_1) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_2 * D_2_1 * G_1_2) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2);
        }

        public P2TriangularElement()
        {
            //this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;

            //this.D_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            //this.D_1_1 = (MainNode3.X - MainNode2.X) / (2 * _area);
            //this.D_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            //this.D_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            //this.D_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);

            //this._stiffnessMatrix = (D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_1 * G_1_1) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_2 * D_2_1 * G_1_2) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2);
        }
        #endregion

        #region PublicMethods
        public override void Draw(System.Drawing.Graphics graphic, double Scale)
        {

            float diameter = graphic.DpiX / UTL.Constants.ScalingFactor;
            System.Drawing.PointF pint1 = new System.Drawing.PointF((float)(MainNode1.X * Scale + diameter), (float)(MainNode1.Y * Scale + diameter));
            System.Drawing.PointF pint2 = new System.Drawing.PointF((float)(MainNode2.X * Scale + diameter), (float)(MainNode2.Y * Scale + diameter));
            System.Drawing.PointF pint3 = new System.Drawing.PointF((float)(MainNode3.X * Scale + diameter), (float)(MainNode3.Y * Scale + diameter));
            System.Drawing.PointF pint4 = new System.Drawing.PointF((float)(MiddlePoint1.X * Scale + diameter), (float)(MiddlePoint1.Y * Scale + diameter));
            System.Drawing.PointF pint5 = new System.Drawing.PointF((float)(MiddlePoint2.X * Scale + diameter), (float)(MiddlePoint2.Y * Scale + diameter));
            System.Drawing.PointF pint6 = new System.Drawing.PointF((float)(MiddlePoint3.X * Scale + diameter), (float)(MiddlePoint3.Y * Scale + diameter));

            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint1, pint2);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint2, pint3);
            graphic.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), pint3, pint1);

            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint1.X - diameter / 2), (float)(pint1.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint2.X - diameter / 2), (float)(pint2.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint3.X - diameter / 2), (float)(pint3.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint4.X - diameter / 2), (float)(pint4.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint5.X - diameter / 2), (float)(pint5.Y - diameter / 2), diameter, diameter);
            graphic.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Blue), (float)(pint6.X - diameter / 2), (float)(pint6.Y - diameter / 2), diameter, diameter);

            var fontFamily = new System.Drawing.FontFamily("Times New Roman");
            var elemntfont = new System.Drawing.Font(fontFamily, 24, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 0, 0, 255));
            if (this.Id % 2 == 0)
            {
                graphic.DrawString(this.Id.ToString(), elemntfont, System.Drawing.Brushes.Black, (int)((MiddlePoint1.X * Scale + MiddlePoint3.X * Scale) / 2), (int)(MiddlePoint1.Y * Scale + MiddlePoint3.Y * Scale) / 2);
            }
            else
            {
                graphic.DrawString(this.Id.ToString(), elemntfont, System.Drawing.Brushes.Black, (int)((MainNode1.X * Scale + MiddlePoint2.X * Scale) / 2), (int)((MainNode1.Y * Scale + MiddlePoint2.Y * Scale) / 2));
            }
            var nodefontFamily = new System.Drawing.FontFamily("Times New Roman");
            var nodefont = new System.Drawing.Font(fontFamily, 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            var nodesolidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            graphic.DrawString(this.MainNode1.Id.ToString() + "(" + String.Format("{0:0.00}", this.MainNode1.X) + "," + String.Format("{0:0.00}", this.MainNode1.Y) + ")", nodefont, System.Drawing.Brushes.Brown, pint1.X, pint1.Y);
            graphic.DrawString(this.MainNode2.Id.ToString() + "(" + String.Format("{0:0.00}", this.MainNode2.X) + "," + String.Format("{0:0.00}", this.MainNode2.Y) + ")", nodefont, System.Drawing.Brushes.Brown, pint2.X, pint2.Y);
            graphic.DrawString(this.MainNode3.Id.ToString() + "(" + String.Format("{0:0.00}", this.MainNode3.X) + "," + String.Format("{0:0.00}", this.MainNode3.Y) + ")", nodefont, System.Drawing.Brushes.Brown, pint3.X, pint3.Y);
            graphic.DrawString(this.MiddlePoint1.Id.ToString(), nodefont, System.Drawing.Brushes.Green, pint4.X, pint4.Y);
            graphic.DrawString(this.MiddlePoint2.Id.ToString(), nodefont, System.Drawing.Brushes.Green, pint5.X, pint5.Y);
            graphic.DrawString(this.MiddlePoint3.Id.ToString(), nodefont, System.Drawing.Brushes.Green, pint6.X, pint6.Y);

        }
        public override void Refresh()
        {
            this._area = ((MainNode2.X - MainNode1.X) * (MainNode3.Y - MainNode1.Y) - (MainNode2.Y - MainNode1.Y) * (MainNode3.X - MainNode1.X)) / 2;
            this._d_1_1 = (MainNode2.Y - MainNode3.Y) / (2 * _area);
            this._d_1_2 = (MainNode3.X - MainNode2.X) / (2 * _area);
            this._d_2_1 = (MainNode3.Y - MainNode1.Y) / (2 * _area);
            this._d_2_2 = (MainNode1.X - MainNode3.X) / (2 * _area);
            this._stiffnessMatrix = 2 * _area * ((D_1_1 * D_1_1 * G_1_1) + (D_1_2 * D_1_2 * G_1_1) + (D_1_2 * D_2_2 * G_1_2) + (D_1_1 * D_2_1 * G_1_2) + (D_2_1 * D_1_1 * G_2_1) + (D_2_2 * D_1_2 * G_2_1) + (D_2_1 * D_2_1 * G_2_2) + (D_2_2 * D_2_2 * G_2_2));
            this._d1Matrix = 2 * _area * (D_1_1 * _h1Matrix + D_2_1 * _h2Matrix);
            this._d2Matrix = 2 * _area * (D_1_2 * _h1Matrix + D_2_2 * _h2Matrix);
            this.Gx_1 = 2 * _area * (D_1_1 * _gk_1_1 + D_2_1 * _gk_1_2);
            this.Gy_1 = 2 * _area * (D_1_2 * _gk_1_1 + D_2_2 * _gk_1_2);
            this.Gx_2 = 2 * _area * (D_1_1 * _gk_2_1 + D_2_1 * _gk_2_2);
            this.Gy_2 = 2 * _area * (D_1_2 * _gk_2_1 + D_2_2 * _gk_2_2);
            this.Gx_3 = 2 * _area * (D_1_1 * _gk_3_1 + D_2_1 * _gk_3_2);
            this.Gy_3 = 2 * _area * (D_1_2 * _gk_3_1 + D_2_2 * _gk_3_2);
            this.Gx_4 = 2 * _area * (D_1_1 * _gk_4_1 + D_2_1 * _gk_4_2);
            this.Gy_4 = 2 * _area * (D_1_2 * _gk_4_1 + D_2_2 * _gk_4_2);
            this.Gx_5 = 2 * _area * (D_1_1 * _gk_5_1 + D_2_1 * _gk_5_2);
            this.Gy_5 = 2 * _area * (D_1_2 * _gk_5_1 + D_2_2 * _gk_5_2);
            this.Gx_6 = 2 * _area * (D_1_1 * _gk_6_1 + D_2_1 * _gk_6_2);
            this.Gy_6 = 2 * _area * (D_1_2 * _gk_6_1 + D_2_2 * _gk_6_2);
            PointsList.Add(MainNode1);
            PointsList.Add(MainNode2);
            PointsList.Add(MainNode3);
            PointsList.Add(MiddlePoint1);
            PointsList.Add(MiddlePoint2);
            PointsList.Add(MiddlePoint3);
        }
        #endregion





    }
}
