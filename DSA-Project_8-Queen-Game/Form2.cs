using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSA_Project_8_Queen_Game
{
    public partial class GameForm : Form
    {
        private string difficulty;
        private int N; 
        private int[,] board; 

        public GameForm(string selectedDifficulty)
        {
            InitializeComponent();
            difficulty = selectedDifficulty;
            SetupGame();
        }

        private void SetupGame()
        {
            if (difficulty == "Easy")
            {
                N = 4;
            }
            else if (difficulty == "Medium")
            {
                N = 8;
            }
            else if (difficulty == "Hard")
            {
                N = 12;
            }

            InitializeBoard();
            CreateChessboard();
        }

        private void InitializeBoard()
        {
            board = new int[N, N];
        }

        private void CreateChessboard()
        {
            int squareSize = chessboardPanel.Width / N;

            chessboardPanel.Controls.Clear();

            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    Button square = new Button
                    {
                        Width = squareSize,
                        Height = squareSize,
                        Location = new Point(col * squareSize, row * squareSize),
                        BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray,
                        Tag = $"{row},{col}"
                    };

                    square.Click += Square_Click;
                    chessboardPanel.Controls.Add(square);
                }
            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            Button square = sender as Button;
            string[] position = square.Tag.ToString().Split(',');
            int row = int.Parse(position[0]);
            int col = int.Parse(position[1]);

            if (square.Text == "Q") 
            {
                board[row, col] = 0;
                square.Text = "";
                square.BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
            }
            else  
            {
                if (CountQueens() >= N)
                {
                    MessageBox.Show($"Only {N} queens are allowed.");
                    return;
                }

                if (IsSafe(row, col))
                {
                    board[row, col] = 1;
                    square.Text = "Q";
                    square.BackColor = Color.Yellow;

                    CheckWinCondition();
                }
                else
                {
                    MessageBox.Show("Invalid placement. Queens threaten each other.");
                }
            }
        }

        private bool IsSafe(int row, int col)
        {
            for (int i = 0; i < N; i++)
            {
                if (board[i, col] == 1) return false;  
            }
            for (int j = 0; j < N; j++)
            {
                if (board[row, j] == 1) return false;  
            }
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j] == 1) return false; 
            }
            for (int i = row - 1, j = col + 1; i >= 0 && j < N; i--, j++)
            {
                if (board[i, j] == 1) return false;  
            }
            for (int i = row + 1, j = col - 1; i < N && j >= 0; i++, j--)
            {
                if (board[i, j] == 1) return false; 
            }
            for (int i = row + 1, j = col + 1; i < N && j < N; i++, j++)
            {
                if (board[i, j] == 1) return false;  
            }
            return true;
        }

        private int CountQueens()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board[i, j] == 1) count++;
                }
            }
            return count;
        }

        private void CheckWinCondition()
        {
            if (CountQueens() == N)
            {
                CongratulationsForm congratulationsForm = new CongratulationsForm(difficulty);
                congratulationsForm.Show();
                this.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetBoard();
            MessageBox.Show("Board reset!");
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            SolveBoard();
        }

        private void SolveBoard()
        {
            Random random = new Random();
            if (PlaceQueens(0, random))
            {
                MessageBox.Show("Solution found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateBoardUI();
            }
            else
            {
                MessageBox.Show("No solution found.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PlaceQueens(int row, Random random)
        {
            if (row == N)
                return true;

            List<int> columns = Enumerable.Range(0, N).OrderBy(c => random.Next()).ToList();

            foreach (int col in columns)
            {
                
                if (IsSafe(row, col))
                {
                    board[row, col] = 1;  
                    if (PlaceQueens(row + 1, random))  
                        return true;

                    board[row, col] = 0;  
                }
            }
            return false;  
        }

        private void UpdateBoardUI()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button square = (Button)chessboardPanel.Controls[i * N + j];
                    if (board[i, j] == 1)
                    {
                        square.Text = "Q";
                        square.BackColor = Color.Yellow;
                    }
                    else
                    {
                        square.Text = "";
                        square.BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                    }
                }
            }
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            GiveHint();
        }

        private void GiveHint()
        {
            int[] hintPosition = GetHint();

            if (hintPosition != null)
            {
                int row = hintPosition[0];
                int col = hintPosition[1];

                Button square = (Button)chessboardPanel.Controls[row * N + col];

                if (square.Text == "")
                {
                    square.BackColor = Color.LightBlue; 
                    MessageBox.Show($"Hint: You can place a queen at ({row + 1},{col + 1}).", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No valid hint available (all squares are blocked or filled).", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No valid moves available. The puzzle may be unsolvable.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int[] GetHint()
        {
            switch (difficulty)
            {
                case "Easy":
                    return GetEasyHint();
                case "Medium":
                    return GetMediumHint();
                case "Hard":
                    return GetHardHint();
                default:
                    return null;
            }
        }

        private int[] GetFirstAvailableSafeSpot()
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    if (board[row, col] == 0 && IsSafe(row, col))
                    {
                        return new int[] { row, col };
                    }
                }
            }
            return null;
        }

        private int[] GetEasyHint()
        {
            int[] firstAvailable = GetFirstAvailableSafeSpot();
            if (firstAvailable != null) return firstAvailable;

            return null;
        }

        private int[] GetMediumHint()
        {
            int[] firstAvailable = GetFirstAvailableSafeSpot();
            if (firstAvailable != null) return firstAvailable;

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int row = random.Next(0, N);
                int col = random.Next(0, N);

                if (board[row, col] == 0 && IsSafe(row, col))
                {
                    return new int[] { row, col };
                }
            }
            return null;
        }

        private int[] GetHardHint()
        {
            int[] firstAvailable = GetFirstAvailableSafeSpot();
            if (firstAvailable != null) return firstAvailable;

            int[] bestHint = null;
            int maxThreats = -1;

            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    if (board[row, col] == 0 && IsSafe(row, col))
                    {
                        int threatCount = CountThreats(row, col);
                        if (threatCount > maxThreats)
                        {
                            maxThreats = threatCount;
                            bestHint = new int[] { row, col };
                        }
                    }
                }
            }
            return bestHint;
        }

        private int CountThreats(int row, int col)
        {
            int threats = 0;

            for (int i = 0; i < N; i++)
            {
                if (board[row, i] == 1) threats++;
                if (board[i, col] == 1) threats++;
            }

            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--) threats++;
            for (int i = row + 1, j = col + 1; i < N && j < N; i++, j++) threats++;
            for (int i = row - 1, j = col + 1; i >= 0 && j < N; i--, j++) threats++;
            for (int i = row + 1, j = col - 1; i < N && j >= 0; i++, j--) threats++;

            return threats;
        }


        private void ResetBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    board[i, j] = 0;
                }
            }
            CreateChessboard();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void SaveGame()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveFileDialog.Title = "Save Board State";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder boardState = new StringBuilder();

                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                boardState.Append(board[i, j]);
                                if (j < N - 1) boardState.Append(",");
                            }
                            boardState.AppendLine();
                        }
                        System.IO.File.WriteAllText(saveFileDialog.FileName, boardState.ToString());
                        MessageBox.Show("Board state saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving board state: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

        private void LoadGame()
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                    openFileDialog.Title = "Open Saved Game";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                        if (lines.Length == N)
                        {

                            ResetBoard();

                            for (int i = 0; i < N; i++)
                            {
                                string[] columns = lines[i].Split(',');
                                for (int j = 0; j < N; j++)
                                {
                                    if (columns[j] == "1") 
                                    {
                                        board[i, j] = 1;
                                        Button square = (Button)chessboardPanel.Controls[i * N + j];
                                        square.Text = "Q";
                                        square.BackColor = Color.Yellow;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            this.Close();
        }
    }
}