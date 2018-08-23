using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix{

	public float[,] mat;
	public Matrix(float[,] M){
			mat = M;
	}


    public Matrix Solve(){
		int rowCount = mat.GetUpperBound(0) + 1;
        if (mat == null || mat.Length != rowCount * (rowCount + 1))
          Debug.Log("The algorithm must be provided with a (n x n+1) matrix.");
        if (rowCount < 1)
          Debug.Log("The matrix must at least have one row.");

        // pivoting
        for (int col = 0; col + 1 < rowCount; col++) if (mat[col, col] == 0)
        // check for zero coefficients
        {
            // find non-zero coefficient
            int swapRow = col + 1;
            for (;swapRow < rowCount; swapRow++) if (mat[swapRow, col] != 0) break;

            if (mat[swapRow, col] != 0) // found a non-zero coefficient?
            {
                // yes, then swap it with the above
                float[] tmp = new float[rowCount + 1];
                for (int i = 0; i < rowCount + 1; i++)
                  { tmp[i] = mat[swapRow, i]; mat[swapRow, i] = mat[col, i]; mat[col, i] = tmp[i]; }
            }
            else return null; // no, then the matrix has no unique solution
        }

        // elimination
        for (int sourceRow = 0; sourceRow + 1 < rowCount; sourceRow++)
        {
            for (int destRow = sourceRow + 1; destRow < rowCount; destRow++)
            {
                float df = mat[sourceRow, sourceRow];
                float sf = mat[destRow, sourceRow];
                for (int i = 0; i < rowCount + 1; i++)
                  mat[destRow, i] = mat[destRow, i] * df - mat[sourceRow, i] * sf;
            }
        }

        // back-insertion
        for (int row = rowCount - 1; row >= 0; row--)
        {
            float f = mat[row,row];
            if (f == 0) return null;

            for (int i = 0; i < rowCount + 1; i++) mat[row, i] /= f;
            for (int destRow = 0; destRow < row; destRow++)
              { mat[destRow, rowCount] -= mat[destRow, row] * mat[row, rowCount]; mat[destRow, row] = 0; }
        }
        return this;
    }

	public override string ToString(){
		string s = mat[0,0].ToString() + ", ";
		s += mat[0,1].ToString() + ", ";
		s += mat[0,2].ToString() + ", ";
		s += "\n";
		s += mat[1,0].ToString() + ", ";
		s += mat[1,1].ToString() + ", ";
		s += mat[1,2].ToString() + ", ";
		return s;
	}
}
