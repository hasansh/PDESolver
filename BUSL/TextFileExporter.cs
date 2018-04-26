using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace FEMProject.BUSL
{
    class TextFileExporter : Exporter
    {

        private string _extension = ".txt";

        public string Extension
        {
            get { return _extension; }         
        }

        private string _fileHeader;

        public string FileHeader
        {
            get { return _fileHeader; }

        }

        public TextFileExporter()
        {
            _fileHeader = "Exported By Finite Element Sovler Version 1.0.0";
        }

        public override bool Export(Geometry geometry, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write(this.FileHeader + Environment.NewLine);
                writer.Write("Id" + "\t\t" + "X" + "\t\t" + "Y" + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point1.Id) + "\t\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point2.Id) + "\t\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point3.Id) + "\t\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.Y) + Environment.NewLine);
                writer.Write(String.Format("{0}", ((Square)geometry).Point4.Id) + "\t\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.Y) + Environment.NewLine);
                writer.Write("EOF");
                _isExported = true;
            }


            return _isExported;
        }
        public override bool Export(List<Point> nodesList, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write(this.FileHeader + Environment.NewLine);
                writer.Write("Id" + "\t\t" + "X" + "\t\t" + "Y" + Environment.NewLine);
                foreach (var item in nodesList)
                {
                    writer.Write(String.Format("{0}", (item.Id)) + "\t\t");
                    writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                    writer.Write(String.Format("{0:0.000000}", (item.Y)) + Environment.NewLine);
                }
                writer.Write("EOF");
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
                    writer.Write(this.FileHeader + Environment.NewLine);
                    writer.Write("Id" + "\t\t" + "X" + "\t\t" + "Y" + Environment.NewLine);
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0}", (item.Id)) + "\t\t");
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + Environment.NewLine);
                    }
                    writer.Write("EOF");
                    _isExported = true;
                }

            }
            else
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName+ this.Extension))
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    writer.Write("Id" + "\t\t" + "X" + "\t\t" + "Y" + "\t\t" + "T" + Environment.NewLine);
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0}", (item.Id)) + "\t\t");
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Temprature) + Environment.NewLine));
                    }
                    writer.Write("EOF");
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
                writer.Write(this.FileHeader);
                writer.Write(Environment.NewLine +"Id" + "\t\t" + "Point 1" + "\t\t" + "Point 2" + "\t\t" + "Point 3" + "\t\t" + "Point 4" + "\t\t" + "Point 5" + "\t\t" + "Point 6" + Environment.NewLine);
                foreach (P2TriangularElement item in elementsList)
                {
                    writer.Write(String.Format("{0}", (item.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MainNode1.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MainNode2.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MainNode3.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MiddlePoint1.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MiddlePoint2.Id)) + "\t\t");
                    writer.Write(String.Format("{0}", (item.MiddlePoint3.Id)) + Environment.NewLine);
                }
                writer.Write("EOF");
                _isExported = true;
            }
            return _isExported;

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
            throw new NotImplementedException();
        }

        public bool Export(List<BUSL.Error> errorsList, string fileName)
        {
            bool _isExported = false;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write(this.FileHeader + Environment.NewLine);
                writer.Write("NodesNumber" + "\t" + "h" + "\t" + "LInfinityError" + "\t" + "L2Error" + "\t" + "H1Error" + "\t" + "Log(h)" + "\t\t" + "Log(LInfinityError)" + "\t\t" + "Log(L2Error)" + "\t" + "LogH1Error" + Environment.NewLine);
                foreach (var item in errorsList)
                {
                    writer.Write(String.Format("{0}", item.NodesNumber) + "\t");
                    writer.Write(String.Format("{0:0.000000}", 1.0 / (item.NodesNumber-1)) + "\t");
                    writer.Write(String.Format("{0:0.000000}", item.LInfinityError) + "\t");
                    writer.Write(String.Format("{0:0.000000}", item.L2Error) + "\t");
                    writer.Write(String.Format("{0:0.000000}", item.H1Error) + "\t");
                    writer.Write(String.Format("{0:0.000000}", Math.Log(1.0 / (item.NodesNumber-1))) + "\t");
                    writer.Write(String.Format("{0:0.000000}", Math.Log(item.LInfinityError)) + "\t");
                    writer.Write(String.Format("{0:0.000000}", Math.Log(item.L2Error)) + "\t");
                    writer.Write(String.Format("{0:0.000000}", Math.Log(item.H1Error)) + Environment.NewLine);
                }



                _isExported = true;
            }
            return _isExported;
        }

    }

       
}
