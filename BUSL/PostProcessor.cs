using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    class PostProcessor
    {
        private List<Point> _nodesList = new List<Point>();

        public List<Point> NodesList
        {
            get { return _nodesList; }
            set { _nodesList = value; }
        }

        public PostProcessor()
        {

        }        

        public double CalculateCenterError(double centerExactValue, List<Point> nodesList)
        {
            double error;
            error = Math.Abs(centerExactValue - nodesList[(2 * UTL.Constants.XNodesNumber - 1) * (UTL.Constants.YNodesNumber - 1) + UTL.Constants.YNodesNumber-1].Temprature);
            return error;

        }

        public double CalculateMatrixError(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix, MathNet.Numerics.LinearAlgebra.Vector<double> calculatedResults)
        {
            double error;
            double tempError;
            MathNet.Numerics.LinearAlgebra.Vector<double> tempVector;
            MathNet.Numerics.LinearAlgebra.Vector<double> exactSolution = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * UTL.Constants.XNodesNumber - 1) * (2 * UTL.Constants.YNodesNumber - 1)]);
            for (int i = 0; i < exactSolution.Count; i++)
            {
                exactSolution[i] = this.CalculateExactTempretureFunction(UTL.GlobalObjects.Mesh.NodesList[i].X, UTL.GlobalObjects.Mesh.NodesList[i].Y);
            }
            MathNet.Numerics.LinearAlgebra.Vector<double> errorVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * UTL.Constants.XNodesNumber - 1) * (2 * UTL.Constants.YNodesNumber - 1)]);
            for (int i = 0; i < exactSolution.Count; i++)
            {
                errorVector[i] = exactSolution[i] - calculatedResults[i];
            }           
            tempVector = matrix * errorVector;
            tempError = errorVector * tempVector;

            error = Math.Sqrt(Math.Abs(tempError));


            return error;

        }

        public double CalculateMatrixXVelocityError(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix, BUSL.Mesh mesh)
        {
            double error;
            
            MathNet.Numerics.LinearAlgebra.Vector<double> errorVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[mesh.NodesList.Count]);

            for (int i = 0; i < mesh.NodesList.Count; i++)
            {
                errorVector[i] = mesh.NodesList[i].ExactXVelocity - mesh.NodesList[i].XVelocity;
            }

            error = errorVector * matrix * errorVector;
            return Math.Sqrt(error);

        }

        public double CalculateMatrixYVelocityError(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix, BUSL.Mesh mesh)
        {
            double error;

            MathNet.Numerics.LinearAlgebra.Vector<double> errorVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[mesh.NodesList.Count]);

            for (int i = 0; i < mesh.NodesList.Count; i++)
            {
                errorVector[i] = mesh.NodesList[i].ExactYVelocity - mesh.NodesList[i].YVelocity;
            }

            error = errorVector * matrix * errorVector;
            return Math.Sqrt(error); ;

        }

        

        private double CalculateExactTempretureFunction(double x, double y)
        {
            double tempreture = Math.Sin(Math.PI * x) * Math.Sin(Math.PI * y);
            return tempreture;
        }

        public double CaculateExactXVelocityFunction(double x, double y)
        {
            double xvelocity = Math.PI * Math.Sin(2 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x),2)*Math.Sin(UTL.Constants.EndT);
            return xvelocity;

        }

        public double CaculateExactYVelocityFunction(double x, double y)
        {
            double yvelocity = -Math.PI * Math.Sin(2 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2) * Math.Sin(UTL.Constants.EndT);
            return yvelocity;

        }

        public double CaculateExactPressureFunction(double x, double y)
        {
            double pressure = Math.Cos(Math.PI * x) * Math.Sin(Math.PI * y) * Math.Sin(UTL.Constants.EndT);
            return pressure;

        }
    }
}
