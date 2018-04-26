using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace FEMProject.BUSL
{
    public abstract class TriangularElement:Element
    {
        public abstract int Id { get; set; }       
        public abstract double Area
        {
            get;            
        }
        public abstract Matrix<double> MassMatrix
        {
            get;
        }
        protected abstract Matrix<double> G_1_1
        {
            get;
        }
        protected abstract Matrix<double> G_1_2
        {
            get;
        }
        protected abstract Matrix<double> G_2_1
        {
            get;
        }
        protected abstract Matrix<double> G_2_2
        {
            get;
        }
        protected abstract double D_1_1
        {
            get;          
        }
        protected abstract double D_1_2
        {
            get;
        }
        protected abstract double D_2_1
        {
            get;
        }
        protected abstract double D_2_2
        {
            get;
        }       
        public abstract Matrix<double> StiffnessMatrix
        {
            get;           
        }

        public abstract Point MainNode1 { get; set; }

        public abstract Point MainNode2 { get; set; }

        public abstract Point MainNode3 { get; set; }

        public abstract List<Point> PointsList { get; set; }

        public abstract void Refresh();
        
        public override void Draw(Graphics graphic, double scale)
        {
            
        }



        
    }
}
