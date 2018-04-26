using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMProject.UTL
{
    public static class FileIO
    {
        public static void WriteToFile(MathNet.Numerics.LinearAlgebra.Matrix<double> matrix, string fileName)
        {
            using (System.IO.StreamWriter writer =  new System.IO.StreamWriter(fileName+".txt"))
            {
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnCount; j++)
                    {
                        writer.Write(matrix[i, j].ToString()+ ",");

                    }
                    writer.Write(Environment.NewLine);
                }
            }
            
        }

        public static void WriteToFile(MathNet.Numerics.LinearAlgebra.Vector<double> vector, string fileName)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName + ".txt"))
            {
               
                    for (int j = 0; j < vector.Count; j++)
                    {
                        writer.Write(vector[j].ToString() + Environment.NewLine);
                    }
                   
               
            }

        }
    }
}
