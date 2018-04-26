using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public abstract class Exporter
    {
        public abstract bool Export(Geometry geometry, string file);

        public abstract bool Export(List<Element> elementsList,string fileName);

        public abstract bool Export(List<Point> elementsList, string fileName, bool withSolution);

        public abstract bool Export(List<Point> nodesList, string fileName);

        public abstract bool Export(List<Element> elementsList, List<Point> nodesList, string fileName, bool withSolution);

        public abstract bool Export(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix, string fileName);

        public abstract bool Export(MathNet.Numerics.LinearAlgebra.Vector<double> vector, string fileName);


    }
}
