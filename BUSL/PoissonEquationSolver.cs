using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace FEMProject.BUSL
{
    class PoissonEquationSolver : Solver
    {
        private BUSL.Mesh _mesh;

        public BUSL.Mesh Mesh
        {
            get { return _mesh; }
            set { _mesh = value; }
        }

        private int SysEqDimension;

        private Matrix<double> _globalStiffnessMatrix;

        public Matrix<double> GlobalStiffnessMatrix
        {
            get { return _globalStiffnessMatrix; }
            set { _globalStiffnessMatrix = value; }
        }

        private Matrix<double> _globalMassMatrix;

        public Matrix<double> GlobalMassMatrix
        {
            get { return _globalMassMatrix; }
            set { _globalMassMatrix = value; }
        }

        private Vector<double> _rightHandSide;

        public Vector<double> RightHandSide
        {
            get { return _rightHandSide; }
            set { _rightHandSide = value; }
        }

        private Vector<double> _solution;

        public Vector<double> Solution
        {
            get { return _solution; }
            set { _solution = value; }
        }

        private Matrix<double> _tmpstiffnessMatrix;

        public Matrix<double> TMPStiffnessMatrix
        {
            get { return _tmpstiffnessMatrix; }
            set { _tmpstiffnessMatrix = value; }
        }

        #region Ctors

        public PoissonEquationSolver(Mesh mesh)
        {
            this._mesh = mesh;
            SysEqDimension = (2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1);
            GlobalMassMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[SysEqDimension, SysEqDimension]);
            GlobalStiffnessMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[SysEqDimension, SysEqDimension]);
            RightHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            
        }
        #endregion

        private double CalculateFunction(double x, double y)
        {
            double force = 2.00 * Math.Sin(Math.PI * x) * Math.Pow(Math.PI, 2) * Math.Sin(Math.PI * y);
            return force;
        }
        

        protected override void CreateSolverMatrices()
        {
            Vector<double> ForceVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1)* (2 * Mesh.YNodesNumber - 1)]);
            for (int i = 0; i < ForceVector.Count; i++)
            {
                ForceVector[i] = this.CalculateFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }

            this.CalculateStiffnessAndMassMatrices();           
            RightHandSide = GlobalMassMatrix * ForceVector;
            TMPStiffnessMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfMatrix(GlobalStiffnessMatrix);
            this.ApplyBoundaryConditions(0.0);
            //this.RemoveBoundaryNodes();
            //UTL.FileIO.WriteToFile(GlobalStiffnessMatrix, "GlobalStiffnessMatrix_Removed");
            //UTL.FileIO.WriteToFile(GlobalMassMatrix, "GlobalMassMatrix_Removed");
            //UTL.FileIO.WriteToFile(RightHandSide, "RightHandSize_Removed");
        }

        protected override void SolveMatrixEquation()
        {
            Solution = GlobalStiffnessMatrix.Solve(RightHandSide);
            //UTL.FileIO.WriteToFile(Solution, "RemovedSolution");
        }

        protected override void SetSolutionResults()
        {
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                Mesh.NodesList[i].Temprature = Solution[i];
            }

        }

        private void ApplyBoundaryConditions(double boundaryValue)
        {
            foreach (var boundaryNode in Mesh.BoundryNodes)
            {
                for (int i = 0; i < GlobalStiffnessMatrix.ColumnCount; i++)
                {
                    GlobalStiffnessMatrix[boundaryNode.Id - 1, i] = 0;
                    GlobalStiffnessMatrix[boundaryNode.Id - 1, boundaryNode.Id - 1] = 1;
                }
                RightHandSide[boundaryNode.Id - 1] = boundaryValue;
            }


        }



        public void CalculateStiffnessAndMassMatrices()
        {
            foreach (P2TriangularElement item in Mesh.ElementsList)
            {
                for (int alpha = 0; alpha < item.PointsList.Count; alpha++)
                {
                    int i = item.PointsList[alpha].Id - 1;
                    for (int beta = 0; beta < item.PointsList.Count; beta++)
                    {
                        int j = item.PointsList[beta].Id - 1;
                        GlobalStiffnessMatrix[i, j] = GlobalStiffnessMatrix[i, j] + item.StiffnessMatrix[alpha, beta];
                        GlobalMassMatrix[i, j] = GlobalMassMatrix[i, j] + item.MassMatrix[alpha, beta];
                    }
                }
            }

            
            double sum = 0.0;
            for (int i = 0; i < GlobalMassMatrix.RowCount; i++)
            {
                for (int j = 0; j < GlobalMassMatrix.ColumnCount; j++)
                {
                    sum = sum + GlobalMassMatrix[i, j];
                }
            }
        }

        public void RemoveBoundaryNodes()
        {
            Vector<double> temproary = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[Mesh.NodesList.Count - Mesh.BoundryNodes.Count]);
            List<double> lsit = new List<double>();

            lsit = RightHandSide.ToList();
            int i = 0;
            foreach (var boundaryNode in Mesh.BoundryNodes)
            {
                GlobalStiffnessMatrix = GlobalStiffnessMatrix.RemoveRow(Math.Abs(boundaryNode.Id - 1 - i));
                GlobalStiffnessMatrix = GlobalStiffnessMatrix.RemoveColumn(Math.Abs(boundaryNode.Id - 1 - i));
                //lsit.RemoveAt(Math.Abs(Mesh.BoundryNodes.IndexOf(boundaryNode) - 1 - i));
                lsit.RemoveAt(Math.Abs(boundaryNode.Id - 1 - i));
                i++;
            }
            RightHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(lsit.ToArray());
            

        }

        public override void WriteSolutionToFile()
        {

        }
    }
}
