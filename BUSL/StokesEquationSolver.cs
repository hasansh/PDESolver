using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace FEMProject.BUSL
{
    class StokesEquationSolver : Solver
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

        private Matrix<double> _globalD1Matrix;

        public Matrix<double> GlobalD1Matrix
        {
            get { return _globalD1Matrix; }
            set { _globalD1Matrix = value; }
        }
        private Matrix<double> _globalD2Matrix;

        public Matrix<double> GlobalD2Matrix
        {
            get { return _globalD2Matrix; }
            set { _globalD2Matrix = value; }
        }

        private Matrix<double> _coeficientMatrix;

        public Matrix<double> CoeficientMatrix
        {
            get { return _coeficientMatrix; }
            set { _coeficientMatrix = value; }
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

        private Vector<double> exactSolution;

        public Vector<double> ExactRHS;

        private Matrix<double> _tmpstiffnessMatrix;

        public Matrix<double> TMPStiffnessMatrix
        {
            get { return _tmpstiffnessMatrix; }
            set { _tmpstiffnessMatrix = value; }
        }
        int numberOfAllNodes;
        int numberOfMainNodds;
        #region Ctors

        public StokesEquationSolver(Mesh mesh)
        {
            this._mesh = mesh;
            numberOfAllNodes = (2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1);
            numberOfMainNodds = (Mesh.XNodesNumber * Mesh.YNodesNumber);
            SysEqDimension = 2 * numberOfAllNodes + numberOfMainNodds;
        }
        #endregion

        //private double CalculateFirstFunction(double x, double y)
        //{
        //    double force = -2.00 * Math.Pow(Math.PI, 3) * Math.Sin(2*Math.PI * y)  * Math.Cos(2*Math.PI * x) + 4.00 * Math.Pow(Math.PI, 3) * Math.Pow(Math.Sin(Math.PI * x), 2)  * Math.Sin(2 * Math.PI * y) - Math.PI * Math.Sin( Math.PI * x) * Math.Sin(Math.PI * y);
        //    return force;
        //}
        //private double CalculateSecondFunction(double x, double y)
        //{
        //    double force = -4.00 * Math.Pow(Math.PI, 3) * Math.Pow(Math.Sin(Math.PI * y), 2) * Math.Sin(2 * Math.PI * x) + 2.00 * Math.Pow(Math.PI, 3) * Math.Sin(2 * Math.PI * x) * Math.Cos(2 * Math.PI * y) + Math.PI * Math.Cos(Math.PI * x) * Math.Cos(Math.PI * y);
        //    return force;
        //}

        private double CalculateFirstFunction(double x, double y)
        {
            double force = -2.00 * Math.Pow(Math.PI, 3) * Math.Sin(2 * Math.PI * y) * Math.Pow(Math.Cos(Math.PI * x), 2) + 6.00 * Math.Pow(Math.PI, 3) * Math.Pow(Math.Sin(Math.PI * x), 2) * Math.Sin(2 * Math.PI * y) - Math.PI * Math.Sin(Math.PI * x) * Math.Sin(Math.PI * y);
            return force;

        }
        private double CalculateSecondFunction(double x, double y)
        {
            double force = -6.00 * Math.Pow(Math.PI, 3) * Math.Pow(Math.Sin(Math.PI * y), 2) * Math.Sin(2 * Math.PI * x) + 2.00 * Math.Pow(Math.PI, 3) * Math.Sin(2 * Math.PI * x) * Math.Pow(Math.Cos(Math.PI * y), 2) + Math.PI * Math.Cos(Math.PI * x) * Math.Cos(Math.PI * y);
            return force;

        }

        protected override void CreateSolverMatrices()
        {

            this.CalculateStiffnessAndMassMatrices();
            this.CalculateGlobalD1AndD2Matrices();
            this.CalculateRightHandSideVector();
            Matrix<double> firstGroupMatrix = GlobalStiffnessMatrix.Append(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalStiffnessMatrix.RowCount, GlobalStiffnessMatrix.ColumnCount])).Append(-GlobalD1Matrix.Transpose());
            Matrix<double> secondGroupMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalStiffnessMatrix.RowCount, GlobalStiffnessMatrix.ColumnCount]).Append(GlobalStiffnessMatrix).Append(-GlobalD2Matrix.Transpose());
            Matrix<double> thirdGroupMatrix = (-GlobalD1Matrix).Append(-GlobalD2Matrix).Append(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalD1Matrix.RowCount, GlobalD1Matrix.RowCount]));                        
            CoeficientMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[SysEqDimension, SysEqDimension]);
            CoeficientMatrix = ((firstGroupMatrix.Transpose().Append(secondGroupMatrix.Transpose())).Append(thirdGroupMatrix.Transpose())).Transpose();
            firstGroupMatrix = null;
            secondGroupMatrix = null;
            thirdGroupMatrix = null;
            GC.Collect();

            //for (int i = 0; i < firstGroupMatrix.RowCount; i++)
            //{
            //    for (int j = 0; j < firstGroupMatrix.ColumnCount; j++)
            //    {
            //        CoeficientMatrix[i, j] = firstGroupMatrix[i, j];
            //    }
            //}
            //for (int i = 0; i < secondGroupMatrix.RowCount; i++)
            //{
            //    for (int j = 0; j < secondGroupMatrix.ColumnCount; j++)
            //    {
            //        CoeficientMatrix[i+firstGroupMatrix.RowCount, j] = secondGroupMatrix[i, j];
            //    }
            //}
            //for (int i = 0; i < thirdGroupMatrix.RowCount; i++)
            //{
            //    for (int j = 0; j < thirdGroupMatrix.ColumnCount; j++)
            //    {
            //        CoeficientMatrix[i + firstGroupMatrix.RowCount+secondGroupMatrix.RowCount, j] = thirdGroupMatrix[i, j];
            //    }
            //}

            this.ApplyBoundaryConditions(0.0,0.0);
            //this.ApplyZeroBoundaryConditions(0.0);
            //this.CreateExactSolution();

        }

        protected override void SolveMatrixEquation()
        {

            Solution = CoeficientMatrix.LU().Solve(RightHandSide);
            //var iterationCountStopCriterion = new MathNet.Numerics.LinearAlgebra.Solvers.IterationCountStopCriterion<double>(500000);

            //// Stop calculation if residuals are below 1E-10 --> the calculation is considered converged
            //var residualStopCriterion = new MathNet.Numerics.LinearAlgebra.Solvers.ResidualStopCriterion<double>(1e-10);
            //var solver = new CompositeSolver(MathNet.Numerics.LinearAlgebra.Solvers.SolverSetup<double>.LoadFromAssembly( System.Reflection.Assembly.GetExecutingAssembly()));

            //var monitor = new MathNet.Numerics.LinearAlgebra.Solvers.Iterator<double>(iterationCountStopCriterion, residualStopCriterion);
            //Solution = CoeficientMatrix.SolveIterative(RightHandSide, solver, monitor);

        }

        protected override void SetSolutionResults()
        {
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                Mesh.NodesList[i].XVelocity = Solution[i];
                Mesh.NodesList[i].ExactXVelocity = this.CaculateExactXVelocityFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);


            }
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                Mesh.NodesList[i].YVelocity = Solution[i + Mesh.NodesList.Count];
                Mesh.NodesList[i].ExactYVelocity = this.CaculateExactYVelocityFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }
            for (int i = 0; i < Mesh.MainNodesList.Count; i++)
            {
                Mesh.NodesList[Mesh.MainNodesList[i].Id - 1].Pressure = Solution[i + 2 * Mesh.NodesList.Count];
                Mesh.NodesList[Mesh.MainNodesList[i].Id - 1].ExactPressure = this.CaculateExactPressureFunction(Mesh.NodesList[Mesh.MainNodesList[i].Id - 1].X, Mesh.NodesList[Mesh.MainNodesList[i].Id - 1].Y);
            }
        }



        public void CalculateStiffnessAndMassMatrices()
        {
            GlobalMassMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfAllNodes, numberOfAllNodes]);
            GlobalStiffnessMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfAllNodes, numberOfAllNodes]);
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

        public void CalculateGlobalD1AndD2Matrices()
        {
            GlobalD1Matrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfMainNodds, numberOfAllNodes]);
            GlobalD2Matrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfMainNodds, numberOfAllNodes]);
            foreach (P2TriangularElement item in Mesh.ElementsList)
            {
                for (int alpha = 0; alpha < item.PointsList.Count / 2; alpha++)
                {
                    int i = item.PointsList[alpha].MainId - 1;
                    for (int beta = 0; beta < item.PointsList.Count; beta++)
                    {
                        int j = item.PointsList[beta].Id - 1;
                        GlobalD1Matrix[i, j] = GlobalD1Matrix[i, j] + item.D1Matrix[alpha, beta];
                        GlobalD2Matrix[i, j] = GlobalD2Matrix[i, j] + item.D2Matrix[alpha, beta];
                    }
                }
            }

        }

        public void CalculateRightHandSideVector()
        {
            Vector<double> FirstForceVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> SecondForceVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> TmpRighHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            RightHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            for (int i = 0; i < FirstForceVector.Count; i++)
            {
                FirstForceVector[i] = this.CalculateFirstFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }
            TmpRighHandSide = GlobalMassMatrix * FirstForceVector;
            TmpRighHandSide.CopySubVectorTo(RightHandSide, 0, 0, TmpRighHandSide.Count);
            for (int i = 0; i < SecondForceVector.Count; i++)
            {
                SecondForceVector[i] = this.CalculateSecondFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }
            TmpRighHandSide = GlobalMassMatrix * SecondForceVector;
            TmpRighHandSide.CopySubVectorTo(RightHandSide, 0, FirstForceVector.Count, TmpRighHandSide.Count);


        }
        private void ApplyBoundaryConditions(double noneHomogenBoundaryValue, double homogenBoundaryValue)
        {
            foreach (var boundaryNode in Mesh.BoundryNodes)
            {
                if (Math.Abs(boundaryNode.Y - 1) <= UTL.Constants.Accuracy)
                {
                    if (boundaryNode.X == 0 || (Math.Abs(boundaryNode.X - 1) <= UTL.Constants.Accuracy))
                    {
                        RightHandSide = RightHandSide - 0 * noneHomogenBoundaryValue * CoeficientMatrix.Column(boundaryNode.Id);
                        
                    }
                    else
                    {
                        RightHandSide = RightHandSide - noneHomogenBoundaryValue * CoeficientMatrix.Column(boundaryNode.Id);
                    }
                }                
            }
            foreach (var boundaryNode in Mesh.BoundryNodes)
            {                
                CoeficientMatrix.ClearColumn(boundaryNode.Id - 1);
                CoeficientMatrix.ClearRow(boundaryNode.Id - 1);
                CoeficientMatrix.ClearColumn(Mesh.NodesList.Count + boundaryNode.Id - 1);
                CoeficientMatrix.ClearRow(Mesh.NodesList.Count + boundaryNode.Id - 1);
                if (boundaryNode.Y == 0.0/* && boundaryNode.X == 0.0*/)
                {
                    CoeficientMatrix.ClearColumn(2 * Mesh.NodesList.Count + boundaryNode.MainId - 1);
                    CoeficientMatrix.ClearRow(2 * Mesh.NodesList.Count + boundaryNode.MainId - 1);                   
                }
                CoeficientMatrix[boundaryNode.Id - 1, boundaryNode.Id - 1] = 1;
                CoeficientMatrix[Mesh.NodesList.Count + boundaryNode.Id - 1, Mesh.NodesList.Count + boundaryNode.Id - 1] = 1;
                if (boundaryNode.Y == 0.0/* && boundaryNode.X == 0.0*/)
                {                    
                    CoeficientMatrix[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1, 2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 1;                   

                }
                if (/*Math.Abs(*/boundaryNode.Y==1.0 /*- 1) <= UTL.Constants.Accuracy*/)
                {
                    if (boundaryNode.X == 0 || boundaryNode.X==1.0/*(Math.Abs(boundaryNode.X - 1) <= UTL.Constants.Accuracy)*/)
                    {
                        RightHandSide[boundaryNode.Id - 1] = 0 * noneHomogenBoundaryValue;
                    }
                    else
                    {

                        RightHandSide[boundaryNode.Id - 1] = noneHomogenBoundaryValue;
                    }

                }
                else
                {
                    RightHandSide[boundaryNode.Id - 1] = homogenBoundaryValue;
                }
                //if (boundaryNode.Y == 0.0 && boundaryNode.X == 0.0)
                //{
                //    RightHandSide[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;
                //}
                if (boundaryNode.Id ==1)
                {
                    RightHandSide[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;
                }
                RightHandSide[Mesh.NodesList.Count+boundaryNode.Id - 1] = homogenBoundaryValue;
                
            }


        }
        //private void ApplyBoundaryConditions(double boundaryValue)
        //{

        //    foreach (var boundaryNode in Mesh.BoundryNodes)
        //    {


        //        if (Math.Abs(boundaryNode.Y - 1) <= UTL.Constants.Accuracy )
        //        {
        //            if (boundaryNode.X != 0 && !(Math.Abs(boundaryNode.X - 1) <= UTL.Constants.Accuracy))
        //            {
        //                for (int i = 0; i < RightHandSide.Count; i++)
        //                {
        //                    RightHandSide[i] = RightHandSide[i] - boundaryValue * CoeficientMatrix[i, boundaryNode.Id];
        //                }
        //            }
        //            else 
        //            {

        //                for (int i = 0; i < RightHandSide.Count; i++)
        //                {
        //                    RightHandSide[i] = RightHandSide[i] - 0 * (boundaryValue / 2) * CoeficientMatrix[i, boundaryNode.Id];
        //                }
        //            }                            

        //        }

        //    }
        //    foreach (var boundaryNode in Mesh.BoundryNodes)
        //    {

        //        if (Math.Abs(boundaryNode.Y - 1) <= UTL.Constants.Accuracy)
        //        {
        //            if (boundaryNode.X != 0 && !(Math.Abs(boundaryNode.X - 1) <= UTL.Constants.Accuracy))
        //            {
        //                RightHandSide[boundaryNode.Id - 1] = boundaryValue;
        //            }
        //            else
        //            {

        //                RightHandSide[boundaryNode.Id - 1] = 0*boundaryValue / 2;
        //            }

        //        }
        //        else
        //        {
        //            RightHandSide[boundaryNode.Id - 1] = 0.0;
        //        }
        //        for (int i = 0; i < CoeficientMatrix.ColumnCount; i++)
        //        {
        //            CoeficientMatrix[boundaryNode.Id - 1, i] = 0;
        //            CoeficientMatrix[boundaryNode.Id - 1, boundaryNode.Id - 1] = 1;
        //            CoeficientMatrix[Mesh.NodesList.Count + boundaryNode.Id - 1, i] = 0;
        //            CoeficientMatrix[Mesh.NodesList.Count + boundaryNode.Id - 1, Mesh.NodesList.Count + boundaryNode.Id - 1] = 1;
        //            CoeficientMatrix[i, boundaryNode.Id - 1] = 0;
        //            CoeficientMatrix[i, Mesh.NodesList.Count + boundaryNode.Id - 1] = 0;
        //        }
        //        if (boundaryNode.Y == 0.0)
        //        {
        //            for (int i = 0; i < CoeficientMatrix.ColumnCount; i++)
        //            {
        //                CoeficientMatrix[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1, i] = 0;
        //                CoeficientMatrix[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1, 2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 1;
        //                CoeficientMatrix[i,2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;
        //            }                   
        //            RightHandSide[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;

        //        }
        //        RightHandSide[boundaryNode.Id + Mesh.NodesList.Count - 1] = 0;
        //    }


        //}

        private void ApplyZeroBoundaryConditions(double boundaryValue)
        {
            foreach (var boundaryNode in Mesh.BoundryNodes)
            {
                for (int i = 0; i < CoeficientMatrix.ColumnCount; i++)
                {
                    CoeficientMatrix[boundaryNode.Id - 1, i] = 0;
                    CoeficientMatrix[boundaryNode.Id - 1, boundaryNode.Id - 1] = 1;
                    CoeficientMatrix[Mesh.NodesList.Count + boundaryNode.Id - 1, i] = 0;
                    CoeficientMatrix[Mesh.NodesList.Count + boundaryNode.Id - 1, Mesh.NodesList.Count + boundaryNode.Id - 1] = 1;
                }
                if (boundaryNode.Y == 0 && boundaryNode.X == 0.0)
                {
                    for (int i = 0; i < CoeficientMatrix.ColumnCount; i++)
                    {
                        CoeficientMatrix[2 * Mesh.NodesList.Count + boundaryNode.Id - 1, i] = 0;
                        CoeficientMatrix[2 * Mesh.NodesList.Count + boundaryNode.Id - 1, 2 * Mesh.NodesList.Count + boundaryNode.Id - 1] = 1;
                    }

                    RightHandSide[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;

                }
                RightHandSide[boundaryNode.Id - 1] = boundaryValue;
                RightHandSide[boundaryNode.Id + Mesh.NodesList.Count - 1] = 0;
            }
        }

        public void RemoveBoundaryNodes(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix)
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

        public double CaculateExactXVelocityFunction(double x, double y)
        {
            double xvelocity = Math.PI * Math.Sin(2 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2);
            return xvelocity;

        }

        public double CaculateExactYVelocityFunction(double x, double y)
        {
            double yvelocity = -Math.PI * Math.Sin(2 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2);
            return yvelocity;
        }

        public double CaculateExactPressureFunction(double x, double y)
        {
            double pressure = Math.Cos(Math.PI * x) * Math.Sin(Math.PI * y);
            return pressure;
        }


        private void CreateExactSolution()
        {
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                exactSolution[i] = CaculateExactXVelocityFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }

            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                exactSolution[Mesh.NodesList.Count + i] = CaculateExactYVelocityFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y);
            }

            for (int i = 0; i < Mesh.MainNodesList.Count; i++)
            {
                exactSolution[2 * Mesh.NodesList.Count + i] = CaculateExactPressureFunction(Mesh.MainNodesList[i].X, Mesh.MainNodesList[i].Y);
            }
            foreach (var item in Mesh.BoundryNodes)
            {
                if (Math.Abs(item.Y - 1) <= UTL.Constants.Accuracy)
                {
                    exactSolution[item.Id] = 1;
                }
            }
            ExactRHS = CoeficientMatrix * exactSolution;
        }
    }

}

