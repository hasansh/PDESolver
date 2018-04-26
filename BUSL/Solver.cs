using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.BUSL
{
    public abstract class Solver
    {
        public void Solve()
        {
            CreateSolverMatrices();
            SolveMatrixEquation();
            SetSolutionResults();
        }   
       public abstract void WriteSolutionToFile();

        protected abstract void CreateSolverMatrices();

        protected abstract void SolveMatrixEquation();

        protected abstract void SetSolutionResults();
    }
}
