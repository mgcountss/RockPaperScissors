using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RockPaperScissors
{
    public partial class RockPaperScissors : Form
    {
        public RockPaperScissors()
        {
            InitializeComponent();
        }

        public string selected;
        public int wins = 0;
        public int losses = 0;
        public int ties = 0;
        public int total = 0;
        
        public void saveData()
        {
            string data = "" + wins + "," + losses + "," + ties + "";
            //convert data to base 64
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);
            string base64 = Convert.ToBase64String(dataBytes);
            //write data to file
            File.WriteAllText("rps.bin", base64);
        }

        public string loadData()
        {
            if (File.Exists("rps.bin"))
            {
                string base64 = File.ReadAllText("rps.bin");
                byte[] dataBytes = Convert.FromBase64String(base64);
                string data = Encoding.ASCII.GetString(dataBytes);
                return data;
            }
            else
            {
                return "0,0,0";
            }
        }

        public string cpuSelection()
        {
            Random rnd = new Random();
            int cpu = rnd.Next(1, 4);
            if (cpu == 1)
            {
                return "rock";
            }
            else if (cpu == 2)
            {
                return "paper";
            }
            else
            {
                return "scissors";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //load stats
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            label2.Text = "Wins: " + wins;
            label3.Text = "Losses: " + losses;
            label4.Text = "Ties: " + ties;
            label5.Text = "Total: " + total;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //unload stats
            selected = "";
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //play game
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = true;
            button4.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string data = loadData();
            string[] split = data.Split(',');
            wins = Convert.ToInt32(split[0]);
            losses = Convert.ToInt32(split[1]);
            ties = Convert.ToInt32(split[2]);
            total = wins + losses + ties;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            selected = "scissors";
            pictureBox3.BackColor = Color.Red;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selected = "paper";
            pictureBox2.BackColor = Color.Red;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selected = "rock";
            pictureBox1.BackColor = Color.Red;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("You must selected an option to play.");
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox1.BackColor = Color.Transparent;
                pictureBox2.BackColor = Color.Transparent;
                pictureBox3.BackColor = Color.Transparent;
                button4.Visible = false;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                var cpu = cpuSelection();
                if (cpu == "rock")
                {
                    pictureBox5.Image = Properties.Resources.rock;
                }
                else if (cpu == "paper")
                {
                    pictureBox5.Image = Properties.Resources.paper;
                }
                else
                {
                    pictureBox5.Image = Properties.Resources.scissors;
                }

                if (selected == "rock")
                {
                    pictureBox4.Image = Properties.Resources.rock;
                }
                else if (selected == "paper")
                {
                    pictureBox4.Image = Properties.Resources.paper;
                }
                else
                {
                    pictureBox4.Image = Properties.Resources.scissors;
                }
                //compare selections
                if (selected == cpu)
                {
                    label9.Text = "It's a tie!";
                    ties++;
                }
                else if (selected == "rock" && cpu == "paper")
                {
                    label9.Text = "You lose!";
                    losses++;
                }
                else if (selected == "rock" && cpu == "scissors")
                {
                    label9.Text = "You win!";
                    wins++;
                }
                else if (selected == "paper" && cpu == "rock")
                {
                    label9.Text = "You win!";
                    wins++;
                }
                else if (selected == "paper" && cpu == "scissors")
                {
                    label9.Text = "You lose!";
                    losses++;
                }
                else if (selected == "scissors" && cpu == "rock")
                {
                    label9.Text = "You lose!";
                    losses++;
                }
                else if (selected == "scissors" && cpu == "paper")
                {
                    label9.Text = "You win!";
                    wins++;
                }
                total++;
                button5.Visible = true;
                //save stats into database file
                saveData();
                selected = "";
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = true;
            button4.Visible = true;
            button5.Visible = false;
            label9.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}

//add updating stats by saving them to a file