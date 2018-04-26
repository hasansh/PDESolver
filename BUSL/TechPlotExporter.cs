using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace FEMProject.BUSL
{
    class TechPlotExporter:Exporter
    {

        private string _extension = ".dat";

        public string Extension
        {
            get { return _extension; }
        }

        private string _fileHeader;

        public string FileHeader
        {
            get { return _fileHeader; }

        }

        public TechPlotExporter()
        {
            
        }

        public override bool Export(Geometry geometry, string fileName)
        {
            bool _isExported = false;
            this._fileHeader = @"TITLE = ""GEOMETRY""" + Environment.NewLine + @"Variables=""X"",""Y"""+ Environment.NewLine + @"Zone I=    1, J=    1, F=POINT";
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
            {
                writer.Write(this.FileHeader + Environment.NewLine);                
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point1.Y) + Environment.NewLine);              
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point2.Y) + Environment.NewLine);               
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point3.Y) + Environment.NewLine);                
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.X) + "\t");
                writer.Write(String.Format("{0:0.000000}", ((Square)geometry).Point4.Y) + Environment.NewLine);                
                _isExported = true;
            }


            return _isExported;
        }

        public override bool Export(List<Element> elementsList, string fileName)
        {
            bool _isExported = false;            
            return _isExported;
        }

        public override bool Export(List<Point> nodesList, string fileName)
        {
            throw new NotImplementedException();
        }

        

        public override bool Export(Matrix<double> matrix, string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool Export(List<Point> nodesList, string fileName, bool withSolution)
        {
            bool _isExported = false;
            
            if (!withSolution)
            {
                this._fileHeader = @"TITLE = ""Temprature for the whole domain""" + Environment.NewLine + @"Variables=""X"",""Y""" + Environment.NewLine + "Zone I=\t" + UTL.GlobalObjects.Mesh.XNodesNumber + "," + "J=\t" + UTL.GlobalObjects.Mesh.YNodesNumber + "," + "F=POINT";
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
                {
                    writer.Write(this.FileHeader + Environment.NewLine);                    
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + Environment.NewLine);
                    }                    
                    _isExported = true;
                }

            }
            else
            {
                this._fileHeader = @"TITLE = ""Temprature for the whole domain""" + Environment.NewLine + @"Variables=""X"",""Y"",""U"",""V"",""ExactU"",""ExactV"",""ExactP"",""P"",""T""" + Environment.NewLine + "Zone I=\t" + UTL.GlobalObjects.Mesh.XNodesNumber + "," + "J=\t" + UTL.GlobalObjects.Mesh.YNodesNumber + "," + "F=POINT";
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + "_WithSolution" + this.Extension))
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.XVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.YVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactXVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactYVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactPressure) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.Pressure) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.Temprature) + Environment.NewLine));
                    }                   
                    _isExported = true;
                }
            }
            return _isExported;
        }

        public override bool Export(List<Element> elementsList, List<Point> nodesList, string fileName, bool withSolution)
        {
            bool _isExported = false;
            if (!withSolution)
            {
                this._fileHeader = @"TITLE = ""Temprature for the whole domain""" + Environment.NewLine + @"Variables=""X"",""Y""" + Environment.NewLine + "Zone N=\t" + nodesList.Count + "," + "J=\t" + elementsList.Count + "," + "DATAPACKING = POINT, ZONETYPE = FETRIANGLE";
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + Environment.NewLine);
                    }
                    foreach (P2TriangularElement item in elementsList)
                    {
                        writer.Write(String.Format("{0}", (item.MainNode1.Id)) + "\t");
                        writer.Write(String.Format("{0}", (item.MiddlePoint1.Id)) + "\t");
                        writer.Write(String.Format("{0}", (item.MainNode2.Id)) + "\t");
                        writer.Write(String.Format("{0}", (item.MiddlePoint2.Id)) + "\t");
                        writer.Write(String.Format("{0}", (item.MainNode3.Id)) + "\t");
                        writer.Write(String.Format("{0}", (item.MiddlePoint3.Id)) + "\t");
                    }
                    _isExported = true;
                }

            }
            else
            {
                this._fileHeader = @"TITLE = ""Temprature for the whole domain""" + Environment.NewLine + @"Variables=""X"",""Y"",""U"",""V"",""ExactU"",""ExactV"",""ExactP"",""P"",""T""" + Environment.NewLine + "Zone N=\t" + nodesList.Count + "," + "E=\t" + elementsList.Count + "," + "DATAPACKING = POINT, ZONETYPE = FETRIANGLE";
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + this.Extension))
                {
                    writer.Write(this.FileHeader + Environment.NewLine);
                    foreach (var item in nodesList)
                    {
                        writer.Write(String.Format("{0:0.000000}", (item.X)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.Y)) + "\t");
                        writer.Write(String.Format("{0:0.000000}", (item.XVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.YVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactXVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactYVelocity) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.ExactPressure) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.Pressure) + "\t"));
                        writer.Write(String.Format("{0:0.000000}", (item.Temprature) + Environment.NewLine));
                    }

                    foreach (P2TriangularElement item in elementsList)
                    {
                        writer.Write(String.Format("{0}", (item.MainNode1.Id)) + "\t");                       
                        writer.Write(String.Format("{0}", (item.MainNode2.Id)) + "\t");                      
                        writer.Write(String.Format("{0}", (item.MainNode3.Id)) + "\t");
                        
                    }
                    _isExported = true;
                }
            }
            return _isExported;
        }

        public override bool Export(Vector<double> vector, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
