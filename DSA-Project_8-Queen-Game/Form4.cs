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
    public partial class DifficultyForm : Form
    {

        public string SelectedDifficulty
        {
            get; set;
        }

        public DifficultyForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
            this.Close();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Easy";
            OpenGameForm();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Medium";
            OpenGameForm();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Hard";
            OpenGameForm();
        }

        private void OpenGameForm()
        {
            GameForm gameForm = new GameForm(SelectedDifficulty);
            gameForm.Show();
            this.Close();  
        }
    }
}
