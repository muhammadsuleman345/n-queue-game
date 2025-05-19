using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_Project_8_Queen_Game
{
    public partial class CongratulationsForm : Form
    {
        private string difficulty;

        public CongratulationsForm(string currentDifficulty)
        {
            InitializeComponent();
            difficulty = currentDifficulty;

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(difficulty);
            gameForm.Show();
            this.Close();
        }   

        private void btnStartnewGame_Click(object sender, EventArgs e)
        {
            DifficultyForm difficultyForm = new DifficultyForm();
            difficultyForm.Show();
            this.Hide();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            this.Close();
        }
    }
}
