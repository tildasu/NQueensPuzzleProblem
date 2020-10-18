using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueenPuzzleProblem
{
    public class SolutionChecker
    {
        public SolutionChecker(int nQueens, bool bPrintResults)
        {
            m_solutions = new List<List<int>>();
            N = nQueens;
            m_bPrint = bPrintResults;
        }

        public int GetNQueensSolutions()
        {
            // record the queen pos in first row
            int queen_pos = 0;

            m_solutions.Clear();

            while (queen_pos < N)
            {
                // record the queen pos in each row
                List<int> queens_idx = Enumerable.Repeat(-1, N).ToList();

                // record the offset vales of valid columns to put queen into each row
                int[] valid_col = Enumerable.Repeat(0, N).ToArray();

                // flag to go back row
                bool back_flag = false;

                // record current row
                int current_row = 1;

                // record the first queen position
                queens_idx[0] = queen_pos;

                #region GET SOLUTION
                while (current_row < N && current_row > 0)
                {
                    // initialize;
                    for (int r = current_row; r < N; r++)
                    {
                        queens_idx[r] = -1;
                        if (r == current_row) continue;
                        valid_col[r] = 0;
                    }

                    int col;
                    for (col = valid_col[current_row]; col < N; col++)
                    {
                        if (!IsSafePos(queens_idx, current_row, col))
                            continue;

                        // Check if backtracking and go next column if yes
                        if (back_flag)
                        {
                            back_flag = false;
                            continue;
                        }

                        valid_col[current_row] = col;

                        queens_idx[current_row] = col;
                        break;
                    }

                    // if no valid pos found to place queen in current_row, back to the previous row
                    back_flag = (col >= N) || (queens_idx[current_row] == -1);

                    // if solution valid
                    if ( (!back_flag) && (current_row == (N - 1)) )
                    {
                        m_solutions.Add(queens_idx.ToList());
                        back_flag = true;
                    }

                    if (back_flag)
                    {
                        // reset valid column record of the current row
                        valid_col[current_row] = 0;

                        // back to the previous row
                        current_row--;

                        continue;
                    }

                    current_row++;
                }
                #endregion

                // go next position of the first queen
                queen_pos++;
            }

            if (m_bPrint)
                PrintAllSolutions();

            return m_solutions.Count;
        }

        public void PrintAllSolutions()
        {
            Console.WriteLine();
            for (int i = 0; i < m_solutions.Count; i++)
            {
                PrintQueensBoardSolution(m_solutions[i], i + 1);
            }
        }

        List<List<int>> m_solutions;

        int N = 0;

        bool m_bPrint = true;

        bool IsSafePos(List<int> queens_idx, int current_row, int current_col)
        {
            // Check if there are any queens on the same rows
            if (queens_idx.GetRange(0, current_row).Contains(current_col))
                return false;

            // Check if there are any queens on the diagonal
            for (int row = 0; row < current_row; row++)
            {
                int offset = Math.Abs(row - current_row);
                if ((current_col - offset) >= 0 && queens_idx[row] == Math.Abs(current_col - offset)) return false;
                if ((current_col + offset) < N && queens_idx[row] == Math.Abs(current_col + offset)) return false;
            }

            return true;
        }

        void PrintQueensBoardSolution(List<int> queens, int solution_cnt)
        {
            Console.WriteLine($"Solution {solution_cnt}:");
            foreach (int idx in queens)
            {
                string[] row = Enumerable.Repeat(".", N).ToArray();
                row[idx] = "Q";
                Console.WriteLine(string.Join(" ", row));
            }
            Console.WriteLine();
        }

    }
}
