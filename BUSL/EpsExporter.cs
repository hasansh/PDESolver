using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace FEMProject.BUSL
{
    class EpsExporter : Exporter
    {
        private string _extension = ".eps";

        public string Extension
        {
            get { return _extension; }
        }

        private string _fileHeader = string.Empty;

        public string FileHeader
        {
            get { return _fileHeader; }

        }

        #region Cosntants
        //double AVE_X;
        //double AVE_Y;
        int CIRCLE_SIZE;
        int DELTA;
 
        double X_MAX;
        double X_MIN;
        //int X_PS;
        int X_PS_MAX = 576;
        int X_PS_MAX_CLIP = 594;
        int X_PS_MIN = 36;
        int X_PS_MIN_CLIP = 18;
        double X_SCALE;
        double Y_SCALE;
        double Y_MAX;
        double Y_MIN;
        //int Y_PS;
        int Y_PS_MAX = 666;
        int Y_PS_MAX_CLIP = 684;
        int Y_PS_MIN = 126;
        int Y_PS_MIN_CLIP = 108;
        #endregion

        public EpsExporter()
        {
            _fileHeader += "%!PS-Adobe-3.0 EPSF-3.0\n";
            _fileHeader += "%%Creator: " + UTL.Constants.SoftwareVersion;
            _fileHeader += "%%Title: " + " " + "\n";
            _fileHeader += "%%Pages: 1\n";
            _fileHeader += "%%BoundingBox:  ";
            _fileHeader += X_PS_MIN + " ";
            _fileHeader += Y_PS_MIN + " ";
            _fileHeader += X_PS_MAX + " ";
            _fileHeader += Y_PS_MAX + "\n";
            _fileHeader += "%%Document-Fonts: Times-Roman\n";
            _fileHeader += "%%LanguageLevel: 1\n";
            _fileHeader += "%%EndComments\n";
            _fileHeader += "%%BeginProlog\n";
            _fileHeader += "/inch {72 mul} def\n";
            _fileHeader += "%%EndProlog\n";
            _fileHeader += "%%Page:      1     1\n";
            _fileHeader += "save\n";
            _fileHeader += "%\n";
            _fileHeader += "% Set the RGB line color to very light gray.\n";
            _fileHeader += "%\n";
            _fileHeader += " 0.9000 0.9000 0.9000 setrgbcolor\n";
            _fileHeader += "%\n";
            _fileHeader += "% Draw a gray border around the page.\n";
            _fileHeader += "%\n";
            _fileHeader += "newpath\n";
            _fileHeader += X_PS_MIN + "  " + Y_PS_MIN + "  moveto\n";
            _fileHeader += X_PS_MAX + "  " + Y_PS_MIN + "  lineto\n";
            _fileHeader += X_PS_MAX + "  " + Y_PS_MAX + "  lineto\n";
            _fileHeader += X_PS_MIN + "  " + Y_PS_MAX + "  lineto\n";
            _fileHeader += X_PS_MIN + "  " + Y_PS_MIN + "  lineto\n";
            _fileHeader += "stroke\n";
            _fileHeader += "%\n";
            _fileHeader += "% Set RGB line color to black.\n";
            _fileHeader += "%\n";
            _fileHeader += " 0.0000 0.0000 0.0000 setrgbcolor\n";
            _fileHeader += "%\n";
            _fileHeader += "%  Set the font and its size:\n";
            _fileHeader += "%\n";
            _fileHeader += "/Times-Roman findfont\n";
            _fileHeader += "0.50 inch scalefont\n";
            _fileHeader += "setfont\n";
            _fileHeader += "%\n";
            _fileHeader += "%  Print a title:\n";
            _fileHeader += "%\n";
            _fileHeader += "%  210  702 moveto\n";
            _fileHeader += "%(Pointset) show\n";
            _fileHeader += "%\n";
            _fileHeader += "% Define a clipping polygon\n";
            _fileHeader += "%\n";
            _fileHeader += "newpath\n";

        }

        public override bool Export(Geometry geometry, string fileName)
        {
            bool _isExported = false;
            CIRCLE_SIZE = 5;
            this.CacluateScaleParameters();
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MIN_CLIP + "  moveto\n";
            _fileHeader += X_PS_MAX_CLIP + "  " + Y_PS_MIN_CLIP + "  lineto\n";
            _fileHeader += X_PS_MAX_CLIP + "  " + Y_PS_MAX_CLIP + "  lineto\n";
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MAX_CLIP + "  lineto\n";
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MIN_CLIP + "  lineto\n";
            _fileHeader += "clip newpath\n";
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                if (true)
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    writer.Write("%  Draw filled dots at each node:\n");
                    writer.Write("%\n");
                    writer.Write("%  Set the color to blue:\n");
                    writer.Write("%\n");
                    writer.Write("0.258  0.521  0.957  setrgbcolor\n");
                    writer.Write("%\n");
                    writer.Write(DrawNode(((Square)geometry).Point1));
                    writer.Write(DrawNode(((Square)geometry).Point2));
                    writer.Write(DrawNode(((Square)geometry).Point3));
                    writer.Write(DrawNode(((Square)geometry).Point4));
                    writer.Write(DrawGeometry((Square)geometry));
                    writer.Write("%\n");
                    writer.Write("restore showpage\n");
                    writer.Write("%\n");
                    writer.Write("% End of page\n");
                    writer.Write("%\n");
                    writer.Write("%%Trailer\n");
                    writer.Write("%%EOF\n");
                    _isExported = true;
                }
            }
                return _isExported;
        }

        public override bool Export(List<Element> elementsList, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(List<Point> elementsList, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(List<Point> elementsList, string fileName, bool withSolution)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Matrix<double> matrix, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Vector<double> vector, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(List<Element> elementsList, List<Point> nodesList, string fileName, bool withSolution)
        {
            bool _isExported = false;
            if (nodesList.Count <= 200)
            {
                CIRCLE_SIZE = 5;
            }
            else if (nodesList.Count <= 500)
            {
                CIRCLE_SIZE = 4;
            }
            else if (nodesList.Count <= 1000)
            {
                CIRCLE_SIZE = 3;
            }
            else if (nodesList.Count <= 5000)
            {
                CIRCLE_SIZE = 2;
            }
            else
            {
                CIRCLE_SIZE = 1;
            }
            this.CacluateScaleParameters();
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MIN_CLIP + "  moveto\n";
            _fileHeader += X_PS_MAX_CLIP + "  " + Y_PS_MIN_CLIP + "  lineto\n";
            _fileHeader += X_PS_MAX_CLIP + "  " + Y_PS_MAX_CLIP + "  lineto\n";
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MAX_CLIP + "  lineto\n";
            _fileHeader += X_PS_MIN_CLIP + "  " + Y_PS_MIN_CLIP + "  lineto\n";
            _fileHeader += "clip newpath\n";
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                if (true)
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    writer.Write("%  Draw filled dots at each node:\n");
                    writer.Write("%\n");
                    writer.Write("%  Set the color to blue:\n");
                    writer.Write("%\n");
                    writer.Write("0.258  0.521  0.957  setrgbcolor\n");
                    writer.Write("%\n");
                    foreach (var item in nodesList)
                    {
                        writer.Write(this.DrawNode(item)); ;
                    }
                }

                //Labaling
                if (true)
                {
                    writer.Write("%\n");
                    writer.Write("%  Label the nodes:\n");
                    writer.Write("%\n");
                    writer.Write("%  Set the color to darker blue:\n");
                    writer.Write("%\n");
                    writer.Write("0.984  0.737  0.0196  setrgbcolor\n");
                    writer.Write("/Times-Roman findfont\n");
                    writer.Write("0.20 inch scalefont\n");
                    writer.Write("setfont\n");
                    writer.Write("%\n");
                    foreach (var item in nodesList)
                    {
                        writer.Write(this.ShwoNodesNumber(item));
                    }
                    
                }

                if (true)
                {
                    writer.Write("%\n");
                    writer.Write("%  Set the RGB color to red.\n");
                    writer.Write("%\n");
                    writer.Write("0.203  0.6588  0.325 setrgbcolor\n");
                    writer.Write("%\n");
                    writer.Write("%  Draw the triangles.\n");
                    writer.Write("%\n");
                    foreach (P2TriangularElement item in elementsList)
                    {
                        writer.Write(this.DrawElement(item));
                    }
                }
                //
                //  Label the triangles.
                //
                if (true)
                {
                    writer.Write("%\n");
                    writer.Write("%  Label the triangles.\n");
                    writer.Write("%\n");
                    writer.Write("%  Set the RGB color to darker red.\n");
                    writer.Write("%\n");
                    writer.Write("0.9176  0.2627  0.2078 setrgbcolor\n");
                    writer.Write("/Times-Roman findfont\n");
                    writer.Write("0.20 inch scalefont\n");
                    writer.Write("setfont\n");
                    writer.Write("%\n");
                    foreach (P2TriangularElement item in elementsList)
                    {
                        writer.Write(this.LabelElement(item));
                    }
                }

                writer.Write("%\n");
                writer.Write("restore showpage\n");
                writer.Write("%\n");
                writer.Write("% End of page\n");
                writer.Write("%\n");
                writer.Write("%%Trailer\n");
                writer.Write("%%EOF\n");
                _isExported = true;
            }
            return _isExported;
        }

        private void CacluateScaleParameters()
        {
            X_MAX = ((Square)UTL.GlobalObjects.Geometry).Point2.X;
            X_MIN = ((Square)UTL.GlobalObjects.Geometry).Point1.X;
            Y_MAX = ((Square)UTL.GlobalObjects.Geometry).Point4.Y;
            Y_MIN = ((Square)UTL.GlobalObjects.Geometry).Point1.Y;
            DELTA = (int)(((X_PS_MAX - X_PS_MIN) * Math.Abs(Y_SCALE - X_SCALE) / (2.0 * Y_SCALE)));
            X_SCALE = X_MAX - X_MIN;
            X_MAX = X_MAX + 0.05 * X_SCALE;
            X_MIN = X_MIN - 0.05 * X_SCALE;
            X_SCALE = X_MAX - X_MIN;
            Y_SCALE = Y_MAX - Y_MIN;
            Y_MAX = Y_MAX + 0.05 * Y_SCALE;
            Y_MIN = Y_MIN - 0.05 * Y_SCALE;
            Y_SCALE = Y_MAX - Y_MIN;
            if (X_SCALE < Y_SCALE)
            {
                DELTA = (int)((X_PS_MAX - X_PS_MIN)
                  * (Y_SCALE - X_SCALE) / (2.0 * Y_SCALE));

                X_PS_MAX = X_PS_MAX - DELTA;
                X_PS_MIN = X_PS_MIN + DELTA;

                X_PS_MAX_CLIP = X_PS_MAX_CLIP - DELTA;
                X_PS_MIN_CLIP = X_PS_MIN_CLIP + DELTA;

                X_SCALE = Y_SCALE;
            }
            else if (Y_SCALE < X_SCALE)
            {
                DELTA = (int)((double)(Y_PS_MAX - Y_PS_MIN)
                  * (X_SCALE - Y_SCALE) / (2.0 * X_SCALE));

                Y_PS_MAX = Y_PS_MAX - DELTA;
                Y_PS_MIN = Y_PS_MIN + DELTA;

                Y_PS_MAX_CLIP = Y_PS_MAX_CLIP - DELTA;
                Y_PS_MIN_CLIP = Y_PS_MIN_CLIP + DELTA;

                Y_SCALE = X_SCALE;
            }
        }
        private string DrawNode(BUSL.Point node)
        {
            string epsCommand = string.Empty;
            int x_ps = ((int)(
                          ((X_MAX - node.X) * (double)(X_PS_MIN)
                          + (+node.X - X_MIN) * (double)(X_PS_MAX))
                          / (X_MAX - X_MIN)));

           int  y_ps = (int)(
              ((Y_MAX - node.Y) * (double)(Y_PS_MIN)
              + (node.Y - Y_MIN) * (double)(Y_PS_MAX))
              / (Y_MAX - Y_MIN));

            epsCommand = "newpath  " + x_ps.ToString() + "  " + y_ps.ToString() + "  " + CIRCLE_SIZE + " 0 360 arc closepath fill\n";
            return epsCommand;
        }

        private string ShwoNodesNumber(BUSL.Point node)
        {
            string epsCommand = string.Empty;
            int x_ps = (int)(
                  ((X_MAX - node.X) * (double)(X_PS_MIN)
                  + (+node.X - X_MIN) * (double)(X_PS_MAX))
                  / (X_MAX - X_MIN));

                int y_ps = (int)(
                  ((Y_MAX - node.Y) * (double)(Y_PS_MIN)
                  + (node.Y - Y_MIN) * (double)(Y_PS_MAX))
                  / (Y_MAX - Y_MIN));

            epsCommand= "newpath  " + x_ps.ToString() + "  " + (y_ps + 5).ToString() + "  moveto (" + node.Id.ToString()  + ") show\n";
            return epsCommand;
            
        }

        private string DrawElement(BUSL.P2TriangularElement elemnt)
        {
            string epsCommand = string.Empty;
            int x_ps = ((int)(
                           ((X_MAX - elemnt.MainNode1.X) * (double)(X_PS_MIN)
                           + (+elemnt.MainNode1.X - X_MIN) * (double)(X_PS_MAX))
                           / (X_MAX - X_MIN)));

            int y_ps = (int)(
               ((Y_MAX - elemnt.MainNode1.Y) * (double)(Y_PS_MIN)
               + (elemnt.MainNode1.Y - Y_MIN) * (double)(Y_PS_MAX))
               / (Y_MAX - Y_MIN));

            epsCommand += "newpath  " + x_ps + "  " + y_ps + "  moveto\n";                                 
           x_ps = ((int)(
                            ((X_MAX - elemnt.MainNode1.X) * (double)(X_PS_MIN)
                            + (+elemnt.MainNode1.X - X_MIN) * (double)(X_PS_MAX))
                            / (X_MAX - X_MIN)));

           y_ps = (int)(
                   ((Y_MAX - elemnt.MainNode1.Y) * (double)(Y_PS_MIN)
                   + (elemnt.MainNode1.Y - Y_MIN) * (double)(Y_PS_MAX))
                   / (Y_MAX - Y_MIN));

                epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            x_ps = ((int)(
                            ((X_MAX - elemnt.MainNode2.X) * (double)(X_PS_MIN)
                            + (+elemnt.MainNode2.X - X_MIN) * (double)(X_PS_MAX))
                            / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - elemnt.MainNode2.Y) * (double)(Y_PS_MIN)
                    + (elemnt.MainNode2.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            x_ps = ((int)(
                            ((X_MAX - elemnt.MainNode3.X) * (double)(X_PS_MIN)
                            + (+elemnt.MainNode3.X - X_MIN) * (double)(X_PS_MAX))
                            / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - elemnt.MainNode3.Y) * (double)(Y_PS_MIN)
                    + (elemnt.MainNode3.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            epsCommand += "closepath\nstroke\n";


            return epsCommand;                    

        }

        private string DrawGeometry(BUSL.Square geometry)
        {
            string epsCommand = string.Empty;
            int x_ps = ((int)(
                           ((X_MAX - geometry.Point1.X) * (double)(X_PS_MIN)
                           + (+geometry.Point1.X - X_MIN) * (double)(X_PS_MAX))
                           / (X_MAX - X_MIN)));

            int y_ps = (int)(
               ((Y_MAX - geometry.Point1.Y) * (double)(Y_PS_MIN)
               + (geometry.Point1.Y - Y_MIN) * (double)(Y_PS_MAX))
               / (Y_MAX - Y_MIN));

            epsCommand += "newpath  " + x_ps + "  " + y_ps + "  moveto\n";
            x_ps = ((int)(
                             ((X_MAX - geometry.Point1.X) * (double)(X_PS_MIN)
                             + (+geometry.Point1.X - X_MIN) * (double)(X_PS_MAX))
                             / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - geometry.Point1.Y) * (double)(Y_PS_MIN)
                    + (geometry.Point1.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            x_ps = ((int)(
                            ((X_MAX - geometry.Point2.X) * (double)(X_PS_MIN)
                            + (+geometry.Point2.X - X_MIN) * (double)(X_PS_MAX))
                            / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - geometry.Point2.Y) * (double)(Y_PS_MIN)
                    + (geometry.Point2.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            x_ps = ((int)(
                            ((X_MAX - geometry.Point3.X) * (double)(X_PS_MIN)
                            + (+geometry.Point3.X - X_MIN) * (double)(X_PS_MAX))
                            / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - geometry.Point3.Y) * (double)(Y_PS_MIN)
                    + (geometry.Point3.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            x_ps = ((int)(
                           ((X_MAX - geometry.Point4.X) * (double)(X_PS_MIN)
                           + (+geometry.Point4.X - X_MIN) * (double)(X_PS_MAX))
                           / (X_MAX - X_MIN)));

            y_ps = (int)(
                    ((Y_MAX - geometry.Point4.Y) * (double)(Y_PS_MIN)
                    + (geometry.Point4.Y - Y_MIN) * (double)(Y_PS_MAX))
                    / (Y_MAX - Y_MIN));

            epsCommand += x_ps + "  " + y_ps + "  lineto\n";
            epsCommand += "closepath\nstroke\n";


            return epsCommand;

        }

        private string LabelElement(BUSL.P2TriangularElement elemnt)
        {
            string epsCommand = string.Empty;
            double ave_x = 0.0;
            double ave_y = 0.0;

            foreach (Point item in elemnt.PointsList)
            {
                ave_x = ave_x + item.X;
                ave_y = ave_y + item.Y;
            }

            ave_x = ave_x / 6.0;
            ave_y = ave_y / 6.0;

            int x_ps = (int)(
              ((X_MAX - ave_x) * (double)(X_PS_MIN)
              + (+ave_x - X_MIN) * (double)(X_PS_MAX))
              / (X_MAX - X_MIN));

            int y_ps = (int)(
              ((Y_MAX - ave_y) * (double)(Y_PS_MIN)
              + (ave_y - Y_MIN) * (double)(Y_PS_MAX))
              / (Y_MAX - Y_MIN));

            epsCommand+= String.Format("{0,10:D}", x_ps) + "  "
                      + String.Format("{0,10:D}", y_ps) + "  "
                      + "moveto (" + elemnt.Id.ToString() + ") show\n";
            return epsCommand;
        }
              
        

    }
}
