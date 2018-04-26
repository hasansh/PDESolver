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
    class TransientNavierStokesEquationSolver : Solver
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

        private Matrix<double> _globalCMatrix;

        public Matrix<double> GlobalCMatrix
        {
            get { return _globalCMatrix; }
            set { _globalCMatrix = value; }
        }

        private List<Point> NodesListN_1 = new List<Point>();
        private List<Point> NodesListN_2 = new List<Point>();

        private Matrix<double> _globalCMatrixN_1;
        public Matrix<double> GlobalCMatrixN_1
        {
            get { return _globalCMatrixN_1; }
            set { _globalCMatrixN_1 = value; }
        }

        private Matrix<double> _globalCMatrixN_2;
        public Matrix<double> GlobalCMatrixN_2
        {
            get { return _globalCMatrixN_2; }
            set { _globalCMatrixN_2 = value; }
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
        private Vector<double> _solutionN_1;
        public Vector<double> SolutionN_1
        {
            get { return _solutionN_1; }
            set { _solutionN_1 = value; }
        }

        private Vector<double> exactSolution;

        public Vector<double> ExactRHS;

        private Matrix<double> _tmpstiffnessMatrix;

        private MathNet.Numerics.LinearAlgebra.Factorization.LU<double> DecomposedLU;
        
        public Matrix<double> TMPStiffnessMatrix
        {
            get { return _tmpstiffnessMatrix; }
            set { _tmpstiffnessMatrix = value; }
        }
        int numberOfAllNodes;
        int numberOfMainNodds;
        #region Ctors

        public TransientNavierStokesEquationSolver(Mesh mesh)
        {
            this._mesh = mesh;
            numberOfAllNodes = (2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1);
            numberOfMainNodds = (Mesh.XNodesNumber * Mesh.YNodesNumber);
            SysEqDimension = 2 * numberOfAllNodes + numberOfMainNodds;
            NodesListN_1 = this.CopyListM(mesh.NodesList);
            NodesListN_2 = this.CopyListM(mesh.NodesList);
            GlobalCMatrixN_1 = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfAllNodes, numberOfAllNodes]);
            GlobalCMatrixN_2 = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfAllNodes, numberOfAllNodes]);
            SolutionN_1 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            Solution = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
        }
        #endregion


        //private double CalculateFirstFunction(double x, double y, double t)
        //{
        //    double force =
        //        Math.PI * Math.Sin(2.00 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2.00) * Math.Cos(t)
        //        + 2.00 * Math.Pow(Math.PI, 3.00) * Math.Pow(Math.Sin(2.00 * Math.PI * y), 2.00) * Math.Pow(Math.Sin(Math.PI * x), 3.00) * Math.Pow(Math.Sin(t), 2.00) * Math.Cos(Math.PI * x)
        //        - 2.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2.00) * Math.Pow(Math.Sin(t), 2.00) * Math.Cos(2.00 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2.00)
        //        - 2.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * y) * Math.Pow(Math.Cos(Math.PI * x), 2.00) * Math.Sin(t)
        //        + 6.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2.00) * Math.Sin(t)
        //        - Math.Sin(Math.PI * x) * Math.PI * Math.Sin(Math.PI * y) * Math.Sin(t);
        //    return force;
        //}

        //private double CalculateSecondFunction(double x, double y, double t)
        //{
        //    double force =
        //       -Math.PI * Math.Sin(2.00 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2.00) * Math.Cos(t)
        //       - 2.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2.00) * Math.Pow(Math.Sin(t), 2.00) * Math.Cos(2.00 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2.00)
        //       + 2.00 * Math.Pow(Math.PI, 3.00) * Math.Pow(Math.Sin(2.00 * Math.PI * x), 2.00) * Math.Pow(Math.Sin(Math.PI * y), 3.00) * Math.Pow(Math.Sin(t), 2.00) * Math.Cos(Math.PI * y)
        //       - 6.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * x) * Math.Pow(Math.Sin(Math.PI * y), 2.00) * Math.Sin(t)
        //       + 2.00 * Math.Pow(Math.PI, 3.00) * Math.Sin(2.00 * Math.PI * x) * Math.Pow(Math.Cos(Math.PI * y), 2.00) * Math.Sin(t)
        //       + Math.Cos(Math.PI * x) * Math.Cos(Math.PI * y) * Math.PI * Math.Sin(t);
        //    return force;

        //}


        private double CalculateFirstFunction(double x, double y, double t)
        {
            return 0.0;

        }
        private double CalculateSecondFunction(double x, double y, double t)
        {
            return 0.0;

        }

        protected override void CreateSolverMatrices()
        {
            this.CalculateStiffnessAndMassMatrices();
            this.CalculateGlobalD1AndD2Matrices();
            this.CalculateCMatrix();
            this.CalculateRightHandSideVector();
            Matrix<double> firstGroupMatrix = ((GlobalMassMatrix/UTL.Constants.DT) + (GlobalStiffnessMatrix/UTL.Constants.Reynolds)).Append(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalStiffnessMatrix.RowCount, GlobalStiffnessMatrix.ColumnCount])).Append(-GlobalD1Matrix.Transpose());
            Matrix<double> secondGroupMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalStiffnessMatrix.RowCount, GlobalStiffnessMatrix.ColumnCount]).Append(((GlobalMassMatrix / UTL.Constants.DT) + (GlobalStiffnessMatrix / UTL.Constants.Reynolds))).Append(-GlobalD2Matrix.Transpose());
            Matrix<double> thirdGroupMatrix = (-GlobalD1Matrix).Append(-GlobalD2Matrix).Append(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[GlobalD1Matrix.RowCount, GlobalD1Matrix.RowCount]));                        
            CoeficientMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[SysEqDimension, SysEqDimension]);
            CoeficientMatrix = ((firstGroupMatrix.Transpose().Append(secondGroupMatrix.Transpose())).Append(thirdGroupMatrix.Transpose())).Transpose();
            firstGroupMatrix = null;
            secondGroupMatrix = null;
            thirdGroupMatrix = null;
            GC.Collect();                    
            this.ApplyBoundaryConditions(1.0,0.0);            
        }

        protected override void SolveMatrixEquation()
        {

            GlobalCMatrixN_1.CopyTo(GlobalCMatrixN_2);
            NodesListN_2 = this.CopyListM(NodesListN_1);            
            GlobalCMatrix.CopyTo(GlobalCMatrixN_1);
            NodesListN_1 = this.CopyListM(Mesh.NodesList);
            Solution.CopyTo(SolutionN_1);
            if (UTL.Constants.Time == UTL.Constants.DT)
            {
                DecomposedLU = CoeficientMatrix.LU();
            }            
            Solution = DecomposedLU.Solve(RightHandSide);
            //Solution = CoeficientMatrix.LU().Solve(RightHandSide);
           
            //if (UTL.Constants.Time == 0.0)
            //{
            //    this.LMatrix = CoeficientMatrix.LU().L;
            //    this.UMatrix = CoeficientMatrix.LU().U;
            //    CoeficientMatrix.LU().P;

            //}

           
            
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
            UTL.GlobalObjects.SteadyStateError = Math.Abs((Solution - SolutionN_1).SubVector(0,numberOfAllNodes).Max());
            //GlobalCMatrix.CopyTo(GlobalCMatrixN_1);
            //UTL.GlobalObjects.SteadyStateError = Mesh.NodesList.FirstOrDefault(t => ((t.X-0.5)<UTL.Constants.Accuracy) && ((t.Y - 0.5) < UTL.Constants.Accuracy)).XVelocity - NodesListN_1.FirstOrDefault(t => t.X - 0.5 < UTL.Constants.Accuracy && t.Y - 0.5 < UTL.Constants.Accuracy).XVelocity;

        }

        private Vector<double> SetDataFromMeshToVector(BUSL.Mesh mesh)
        {
            Vector<double> meshData = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            for (int i = 0; i < mesh.NodesList.Count; i++)
            {
                meshData[i] = mesh.NodesList[i].XVelocity;               
            }
            for (int i = 0; i < mesh.NodesList.Count; i++)
            {
                meshData[i + mesh.NodesList.Count] = mesh.NodesList[i].YVelocity;
            }
            for (int i = 0; i < mesh.MainNodesList.Count; i++)
            {
                meshData[i + 2 * mesh.NodesList.Count] = mesh.NodesList[Mesh.MainNodesList[i].Id - 1].Pressure;
            }
            return meshData;
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

        public void CalculateCMatrix()
        {
            GlobalCMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfArray(new double[numberOfAllNodes, numberOfAllNodes]);
            foreach (P2TriangularElement item in Mesh.ElementsList)
            {
                for (int alpha = 0; alpha < item.PointsList.Count; alpha++)
                {
                    int i = item.PointsList[alpha].Id - 1;
                    for (int beta = 0; beta < item.PointsList.Count; beta++)
                    {
                        int j = item.PointsList[beta].Id - 1;
                        GlobalCMatrix[i, j] = GlobalCMatrix[i, j]
                                + item.Gx_1[alpha, beta] * Mesh.NodesList[item.MainNode1.Id-1].XVelocity
                                + item.Gy_1[alpha, beta] * Mesh.NodesList[item.MainNode1.Id-1].YVelocity
                                + item.Gx_2[alpha, beta] * Mesh.NodesList[item.MainNode2.Id-1].XVelocity
                                + item.Gy_2[alpha, beta] * Mesh.NodesList[item.MainNode2.Id-1].YVelocity
                                + item.Gx_3[alpha, beta] * Mesh.NodesList[item.MainNode3.Id-1].XVelocity
                                + item.Gy_3[alpha, beta] * Mesh.NodesList[item.MainNode3.Id-1].YVelocity
                                + item.Gx_4[alpha, beta] * Mesh.NodesList[item.MiddlePoint1.Id-1].XVelocity
                                + item.Gy_4[alpha, beta] * Mesh.NodesList[item.MiddlePoint1.Id-1].YVelocity
                                + item.Gx_5[alpha, beta] * Mesh.NodesList[item.MiddlePoint2.Id-1].XVelocity
                                + item.Gy_5[alpha, beta] * Mesh.NodesList[item.MiddlePoint2.Id-1].YVelocity
                                + item.Gx_6[alpha, beta] * Mesh.NodesList[item.MiddlePoint3.Id-1].XVelocity
                                + item.Gy_6[alpha, beta] * Mesh.NodesList[item.MiddlePoint3.Id-1].YVelocity;
                    }
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
            Vector<double> FirstForceVectorN_1 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> SecondForceVectorN_1 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> FirstForceVectorN_2 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> SecondForceVectorN_2 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> TmpRighHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[(2 * Mesh.XNodesNumber - 1) * (2 * Mesh.YNodesNumber - 1)]);
            Vector<double> MF = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            Vector<double> MVF = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            Vector<double> RC = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);
            RightHandSide = MathNet.Numerics.LinearAlgebra.Double.DenseVector.OfArray(new double[SysEqDimension]);

            for (int i = 0; i < FirstForceVector.Count; i++)
            {
                FirstForceVector[i] = this.CalculateFirstFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y, UTL.Constants.Time/* + UTL.Constants.DT*/);
            }
            TmpRighHandSide = GlobalMassMatrix * FirstForceVector;
            TmpRighHandSide.CopySubVectorTo(MF, 0, 0, TmpRighHandSide.Count);
            for (int i = 0; i < SecondForceVector.Count; i++)
            {
                SecondForceVector[i] = this.CalculateSecondFunction(Mesh.NodesList[i].X, Mesh.NodesList[i].Y, UTL.Constants.Time/* + UTL.Constants.DT*/);
            }
            TmpRighHandSide = GlobalMassMatrix * SecondForceVector;
            TmpRighHandSide.CopySubVectorTo(MF, 0, FirstForceVector.Count, TmpRighHandSide.Count);
            
            TmpRighHandSide.Clear();
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                FirstForceVector[i] = Mesh.NodesList[i].XVelocity;
            }
            TmpRighHandSide = (GlobalMassMatrix/UTL.Constants.DT) * FirstForceVector;
            TmpRighHandSide.CopySubVectorTo(MVF, 0, 0, TmpRighHandSide.Count);
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                SecondForceVector[i] = Mesh.NodesList[i].YVelocity;
            }
            TmpRighHandSide = (GlobalMassMatrix/UTL.Constants.DT) * SecondForceVector;
            TmpRighHandSide.CopySubVectorTo(MVF, 0, FirstForceVector.Count, TmpRighHandSide.Count);
            TmpRighHandSide.Clear();
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                FirstForceVector[i] = Mesh.NodesList[i].XVelocity;
                FirstForceVectorN_1[i] = NodesListN_1[i].XVelocity;
                FirstForceVectorN_2[i] = NodesListN_2[i].XVelocity;
            }
            if (UTL.Constants.Time ==0.0 || UTL.Constants.Time == UTL.Constants.DT)
            {
                TmpRighHandSide = (1.0 / 12.0) * (23 * GlobalCMatrix * FirstForceVector/* - 16 * GlobalCMatrixN_1 * FirstForceVectorN_1*/);
            }
            else
            {
                TmpRighHandSide = (1.0 / 12.0) * (23 * GlobalCMatrix * FirstForceVector - 16 * GlobalCMatrixN_1 * FirstForceVectorN_1 + 5 * GlobalCMatrixN_2 * FirstForceVectorN_2);
            }
            
            TmpRighHandSide.CopySubVectorTo(RC, 0, 0, TmpRighHandSide.Count);
            for (int i = 0; i < Mesh.NodesList.Count; i++)
            {
                SecondForceVector[i] = Mesh.NodesList[i].YVelocity;
                SecondForceVectorN_1[i] = NodesListN_1[i].YVelocity;
                SecondForceVectorN_2[i] = NodesListN_2[i].YVelocity;
            }
            if (UTL.Constants.Time == 0.0 || UTL.Constants.Time == UTL.Constants.DT)
            {
                TmpRighHandSide = (1.0 / 12.0) * (23 * GlobalCMatrix * SecondForceVector/* - 16 * GlobalCMatrixN_1 * FirstForceVectorN_1*/);
            }
            else
            {
                TmpRighHandSide = (1.0 / 12.0) * (23 * GlobalCMatrix * SecondForceVector - 16 * GlobalCMatrixN_1 * SecondForceVectorN_1 + 5 * GlobalCMatrixN_2 * SecondForceVectorN_2);
            }
            TmpRighHandSide.CopySubVectorTo(RC, 0, FirstForceVector.Count, TmpRighHandSide.Count);
            RightHandSide = MVF + MF - RC ;
            


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
                if (Math.Abs(boundaryNode.Y- 1) <= UTL.Constants.Accuracy)
                {
                    if (boundaryNode.X == 0 || /*boundaryNode.X==1.0*/(Math.Abs(boundaryNode.X - 1) <= UTL.Constants.Accuracy))
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
                //if (boundaryNode.Y == 0.0/* && boundaryNode.X == 0.0*/)
                //{
                //    RightHandSide[2 * Mesh.NodesList.Count + boundaryNode.MainId - 1] = 0;
                //}
                if (boundaryNode.Id == 1)
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
            double xvelocity = Math.PI * Math.Sin(2 * Math.PI * y) * Math.Pow(Math.Sin(Math.PI * x), 2) * Math.Sin(UTL.Constants.EndT);
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

        private List<T> CopyListM<T>(List<T> list)
        {
            List<T> newList = new List<T>();
            foreach (var item in list)
            {
                newList.Add(item);
            }
            return newList;
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

