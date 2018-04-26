using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace FEMProject.BUSL
{
    class CsvExporter : Exporter
    {
        private string _extension = ".csv";

        public string Extension
        {
            get { return _extension; }
        }

        public CsvExporter()
        {

        }

        public override bool Export(Geometry geometry, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {

                writer.Write("Id" + "," + "X" + "," + "Y" + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point1.Id) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.X) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point2.Id) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.X) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point3.Id) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.X) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point4.Id) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.X) + ",");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.Y) + Environment.NewLine);

                _isExported = true;
            }


            return _isExported;
        }
        public override bool Export(List<Point> nodesList, string fileName, bool withSolution)
        {
            bool _isExported = false;
            if (!withSolution)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
                {

                    writer.Write("Id" + "," + "X" + "," + "Y" + Environment.NewLine);
                    foreach (var item in nodesList)
                    {

                        if (item.MainId!=0)
                        {
                            writer.Write(String.Format("{0}", (item.Id)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.X)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.Y)) + Environment.NewLine);  
                        }
                        
                    }
                    _isExported = true;
                }
            }
            else
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
                {

                    writer.Write("Id" + "," + "X" + "," + "Y" + "," + "U" + "," + "V" + "," + "P" + "," + "T" + Environment.NewLine);
                    foreach (var item in nodesList)
                    {

                        if (item.MainId !=0)
                        {
                            writer.Write(String.Format("{0}", (item.Id)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.X)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.Y)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.XVelocity)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.YVelocity)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.Pressure)) + ",");
                            writer.Write(String.Format("{0:0.000000}", (item.Temprature)) + Environment.NewLine);   
                        }
                   
                      

                    }
                    _isExported = true;
                }
            }

            return _isExported;

        }
        public override bool Export(List<Element> elementsList, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write("Id" + "," + "Point 1" + "," + "Point 2" + "," + "Point 3" + "," + "Point 4" + "," + "Point 5" + "," + "Point 6" + Environment.NewLine);
                foreach (P2TriangularElement item in elementsList)
                {
                    writer.Write(String.Format("{0}", (item.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MainNode1.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MainNode2.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MainNode3.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MiddlePoint1.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MiddlePoint2.Id)) + ",");
                    writer.Write(String.Format("{0}", (item.MiddlePoint3.Id)) + Environment.NewLine);
                }

                _isExported = true;
            }
            return _isExported;

        }

        public override bool Export(List<Point> elementsList, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(Matrix<double> matrix, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {

                for (int i = 0; i < matrix.RowCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnCount; j++)
                    {
                        writer.Write(String.Format("{0:0.000000}", matrix[i, j]) + ",");
                    }
                    writer.Write(Environment.NewLine);
                }

                _isExported = true;
            }
            return _isExported;
        }

        public  bool Export(List<double> list, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                foreach (var item in list)
                {
                    writer.Write(String.Format("{0:0.000000}", item + Environment.NewLine));
                }                                 
                _isExported = true;
            }
            return _isExported;
        }

        public override bool Export(Vector<double> vector, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {

                for (int i = 0; i < vector.Count; i++)
                {
                    writer.Write(String.Format("{0:0.000000}", vector[i]) + Environment.NewLine);
                }

                _isExported = true;
            }
            return _isExported;
        }

        public override bool Export(List<Element> elementsList, List<Point> nodesList, string fileName, bool withSolution)
        {
            throw new NotImplementedException();
        }

        public bool Export(List<BUSL.Error> errorsList, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write("NodesNumber" + "," + "DT" + "," + "h" + "," + "LInfinityError" + "," + "L2Error" + "," + "H1Error" + "," + "Log(h)" + "," + "Log(LInfinityError)" + "," + "Log(L2Error)" + "," + "LogH1Error" + Environment.NewLine);
                foreach (var item in errorsList)
                {
                    writer.Write(String.Format("{0}", item.NodesNumber) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", item.DT) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", 1.0 / (item.NodesNumber-1)) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", item.LInfinityError) + "," );
                    writer.Write(String.Format("{0:0.00000000000}", item.L2Error) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", item.H1Error) + ",");                    
                    writer.Write(String.Format("{0:0.00000000000}", -Math.Log10(1.0/(item.NodesNumber-1))) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", -Math.Log10(item.LInfinityError)) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", -Math.Log10(item.L2Error)) + ",");
                    writer.Write(String.Format("{0:0.00000000000}", -Math.Log10(item.H1Error)) + Environment.NewLine);
                }
                    
               

                _isExported = true;
            }
            return _isExported;
        }


    }
}
