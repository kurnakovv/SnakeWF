using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeWindowsForm
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private Button button = new Button();

        private PictureBox apple;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private int score = 0;
        private int dirX, dirY;
        private int WigthForms = 900;
        private int HeightForms = 800;
        private int SizeOfSides = 40;
        public Form1()
        {
            InitializeComponent();
            dirX = 1;
            dirY = 0;
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyValue == (char)Keys.Space) Button1_Click(button, null); };


            this.Width = WigthForms;
            this.Height = HeightForms;

            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(SizeOfSides - 1, SizeOfSides - 1);
            snake[0].BackColor = Color.Green;
            Controls.Add(snake[0]);

            apple = new PictureBox();
            apple.BackColor = Color.Red;
            apple.Size = new Size(SizeOfSides, SizeOfSides);
            GenerateApple();


            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.ForeColor = Color.White;
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);

            this.pictureBox1.BackColor = System.Drawing.Color.Blue;
            this.pictureBox1.Location = new System.Drawing.Point(800, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(5, 900);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;



            timer.Tick += new EventHandler(Update);
            timer.Interval = 200;
            timer.Start();

            KeyDown += new KeyEventHandler(OKP);
        }





        private void GenerateApple()
        {
            Random r = new Random();
            rI = r.Next(0, HeightForms - SizeOfSides);
            int tempI = rI % SizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, HeightForms - SizeOfSides);
            int tempJ = rJ % SizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            apple.Location = new Point(rI, rJ);
            this.Controls.Add(apple);
        }

        private void EatFrute()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                //snake[score].Location = new Point(snake[score - 1].Location.X * dirX), snake[score - 1].Location.Y + 40 * dirY);
                snake[score].Location = new Point(snake[score - 1].Location.X * dirY - 1000, snake[score - 1].Location.Y * dirX + 1000); // Костыль для нормального появления хвоста
                snake[score].Size = new Size(SizeOfSides - 1, SizeOfSides - 1);
                snake[score].BackColor = Color.Lime;
                this.Controls.Add(snake[score]);
                GenerateApple();
                timer.Interval = timer.Interval - 5;
            }
        }

        private void CheckBorders()
        {
            if (snake[0].Location.X < 0)
            {

                timer.Stop();
                MessageBox.Show("Вы проиграли", $"Ваш счет {score}");
                timer.Start();
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
                timer.Interval = 200;
            }
            if (snake[0].Location.X > HeightForms)
            {

                timer.Stop();
                MessageBox.Show("Вы проиграли", $"Ваш счет {score}");
                timer.Start();
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
                timer.Interval = 200;
            }
            if (snake[0].Location.Y < 0)
            {
                timer.Stop();
                MessageBox.Show("Вы проиграли", $"Ваш счет {score}");
                timer.Start();
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
                timer.Interval = 200;
            }
            if (snake[0].Location.Y > 750)
            {
                timer.Stop();
                MessageBox.Show("Вы проиграли", $"Ваш счет {score}");
                timer.Start();
                for (int _i = 1; _i <= score; _i++)
                {
                    Controls.Remove(snake[_i]);

                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
                timer.Interval = 200;
            }
        }

        private void EatItself()
        {
            for (int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                    {
                        this.Controls.Remove(snake[j]);
                        timer.Interval = 200;


                        snake[0].Location = new Point(-60, -10);
                        snake[0] = new PictureBox();
                        snake[0].Location = new Point(201, 201);
                        snake[0].Size = new Size(SizeOfSides - 1, SizeOfSides - 1);
                        snake[0].BackColor = Color.Green;
                    }
                    timer.Stop();
                    MessageBox.Show("Вы проиграли", $"Ваш счет {score}");
                    timer.Start();
                    score = score - (score - i + 1);
                    Controls.Add(snake[0]);
                    labelScore.Text = "Score: " + score;
                }

            }
        }
        private void Update(Object obj, EventArgs e)
        {
            MoveSnake();
            CheckBorders();
            EatFrute();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer.Stop();
            MessageBox.Show("Нажмите OK что-бы продолжить", "Пауза");
            timer.Start();
        }

        private void MoveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (SizeOfSides), snake[0].Location.Y + dirY * (SizeOfSides));
            EatItself();
        }



        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case ("W"):
                    dirY = -1;
                    dirX = 0;
                    break;
                case "S":
                    dirY = 1;
                    dirX = 0;
                    break;
                case "A":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "D":
                    dirX = 1;
                    dirY = 0;
                    break;


                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }

        }

    }
}

