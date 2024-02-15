using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Pong_Game
{
    public partial class Form1 : Form
    {
        bool p1goUp;
        bool p1goDown;
        bool p2goUp;
        bool p2goDown;
        bool isGameStarted = false;
        bool isBallOut = false;
        string p1;
        string p2;

        int scoreP1;
        int scoreP2;
        int scoreTotalP1;
        int scoreTotalP2;
        int ballx;
        int bally;
        int lastBallX;
        int lastBallY;
        int playersSpeed;
        int xMidpoint;
        int yMidpoint;
        int len = 0;
        int heartOutP1 = 0;
        int heartOutP2 = 0;
        int matchNr = 1;

        int[] matchesP1 = new int[5];
        int[] matchesP2 = new int[5];
       
        private PictureBox[] p1heart;
        private PictureBox[] p2heart;

        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            countdown.Visible = false;
            final.Visible = false;
            matchLbl.Visible = false;
            MtchWnr.Visible = false;
            points.Visible = false;
            ballOutMsg.Visible = false;

            xMidpoint = ClientSize.Width / 2;
            yMidpoint = ClientSize.Height / 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CustomInputDialog inputDialog = new CustomInputDialog();

            if (inputDialog.ShowDialog() == DialogResult.OK)
            {
                p1 = inputDialog.player1Inp;
                p2 = inputDialog.player2Inp;

            }
            else
            {
                inputDialog.Close();
            }

            
            p1heart = new PictureBox[] { p1h1, p1h2, p1h3 };
            p2heart = new PictureBox[] { p2h1, p2h2, p2h3 };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if(len == 1)
            {
                countdown.Visible = true;
                countdown.Text = "3";
                len++;
            }
            else if(len == 2)
            {
                countdown.Text = "2";
                len++;
            }
            else if(len == 3)
            {
                countdown.Text = "1";
                len++;
            }
            else if(len == 4)
            {
                countdown.Text = "Start!";
                len++;
                
            }
            else if(len == 5)
            {
                countdown.Visible = false;
                timer1.Stop();
                len = 1;
                SetupGame();
            }
            
        }      
       
        private void SetupGame()
        {

            if (!isBallOut)
            {
                gameTimer.Start();

                matchLbl.Text = "Match " + matchNr;


                ballx = 15;
                bally = 15;
                playersSpeed = 10;

                ballx = rnd.Next(2) == 0 ? -rnd.Next(15, 20) : rnd.Next(15, 20);
                bally = rnd.Next(2) == 0 ? -rnd.Next(15, 20) : rnd.Next(15, 20);

                ball.Left = xMidpoint;
                ball.Top = yMidpoint;

                player1.Top = 321;
                player2.Top = 321;
            }       

        }
        private void pointsBoard()
        {
            if(points.Visible == false)
            {
                points.Visible = true;

                points.AppendText(p1 + "\n");

                for (int i = 0; i < matchesP1.Length; i++)
                {
                    points.AppendText($"Match {i + 1}: {matchesP1[i]} points\n");
                }

                points.AppendText("Total points earned: " + scoreTotalP1 + "\n\n");

                points.AppendText(p2 + "\n");

                for (int i = 0; i < matchesP2.Length; i++)
                {
                    points.AppendText($"Match {i + 1}: {matchesP2[i]} points\n");
                }

                points.AppendText("Total points earned: " + scoreTotalP2 + "\n\n");
            }
            else
            {
                points.Visible = false;
                points.Clear();
            }                 
        }
        private void ballIsOut()
        {
            gameTimer.Stop();
            isGameStarted = false;

            
            label2.Visible = true;
            matchLbl.Visible = false;
            ballOutMsg.Visible = true;

            lastBallX = ball.Left;
            lastBallY = ball.Top;

        }
        private void scoring()
        {
            if(scoreP1 == 5 || scoreP2 == 5)
            {              
                if(scoreP1 == 5)
                {
                    p2heart[heartOutP2].Visible = false;
                    heartOutP2++;

                    if(heartOutP2 == 3)
                    {
                        gameTimer.Stop();
                        final.Visible = true;
                        final.Text = p1 + " has won the game with " + scoreTotalP1 + " points";
                        label2.Text = "Press P to see match points";
                        matchesP1[matchNr - 1] = scoreP1;
                        matchesP2[matchNr - 1] = scoreP2;
                    }
                    else
                    {
                        matchesP1[matchNr - 1] = scoreP1;
                        matchesP2[matchNr - 1] = scoreP2;
                        MtchWnr.Visible = true;
                        MtchWnr.Text = p1 + " has won match " + matchNr;
                    }
                }
                else if(scoreP2 == 5)
                {
                    p1heart[heartOutP1].Visible = false;
                    heartOutP1++;

                    if(heartOutP1 == 3)
                    {
                        gameTimer.Stop();
                        final.Visible = true;
                        final.Text = p2 + " has won the game with " + scoreTotalP2 + " points";
                        label2.Text = "Press P to see match points\nPress R to start a new game\nESC to exit game";
                        matchesP2[matchNr - 1] = scoreP2;
                        matchesP1[matchNr - 1] = scoreP1;
                    }
                    else
                    {
                        matchesP2[matchNr - 1] = scoreP2;
                        matchesP1[matchNr - 1] = scoreP1;
                        MtchWnr.Visible = true;
                        MtchWnr.Text = p2 + " has won match " + matchNr;
                    }
                }
                matchNr++;

                scoreP1 = 0;
                scoreP2 = 0;
            }
            else if (scoreP1 < 3 || scoreP2 < 3)
            {
                label2.Text = "Press ENTER to continue the game!";
                SetupGame();
            }
        }

        private void mainGameTimeEvent(object sender, EventArgs e)
        {
            
            player1scr.Text = p1 + ": " + scoreP1;
            player2scr.Text = p2 + ": " + scoreP2;
           

            if (p1goUp == true && player1.Top > 0)
            {
                player1.Top -= playersSpeed;
            }

            if (p1goDown == true && player1.Top < 620)
            {
                player1.Top += playersSpeed;
            }
            if (p2goUp == true && player2.Top > 0)
            {
                player2.Top -= playersSpeed;
            }
            if(p2goDown == true && player2.Top < 620)
            {
                player2.Top += playersSpeed;
            }

           
            ball.Left += ballx;
            ball.Top += bally;

                     
            if (ball.Bounds.IntersectsWith(player1.Bounds) || ball.Bounds.IntersectsWith(player2.Bounds))
            {
                ballx = -ballx;
            }


            if(ball.Left < 0)
            {
                scoreP2 += 1;
                scoreTotalP2 += 1;
                isBallOut = true;
                scoring();

                ballIsOut();

                ballOutMsg.Location = new Point(lastBallX + 3, lastBallY);

            }

            if(ball.Left + ball.Width > ClientSize.Width)
            {
                scoreP1 += 1;
                scoreTotalP1 += 1;
                isBallOut = true;
                scoring();

                ballIsOut();

                ballOutMsg.Location = new Point(lastBallX - 115, lastBallY);
            }

          
            foreach (Control x in this.Controls)
            {             
                if(x is PictureBox && (string)x.Tag == "wallBtm")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        ball.Top = 690;
                        bally = rnd.Next(5, 12) * -1;

                        if (ballx < 0)
                        {
                            ballx = rnd.Next(5, 12) * -1;
                        }
                        else
                        {
                            ballx = rnd.Next(5, 12);
                        }
                    }
                }
                if(x is PictureBox && (string)x.Tag == "wallTop")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        ball.Top = 0;
                        bally = rnd.Next(5, 12) * 1;

                        if(ballx < 0)
                        {
                            ballx = rnd.Next(5, 12) * -1;
                        }
                        else
                        {
                            ballx = rnd.Next(5, 12);
                        }
                    }
                }
            }

           
        }
       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                p2goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                p2goDown = true;
            }
            if(e.KeyCode == Keys.W)
            {
                p1goUp = true;
            }
            if(e.KeyCode == Keys.S)
            {
                p1goDown = true; 
            }

            if (e.KeyCode == Keys.Enter)
            {
                dir1.Visible = false;
                dir2.Visible = false;
                dir3.Visible = false;
                dir4.Visible = false;

                if (!isGameStarted)
                {
                    isGameStarted = true;
                    label2.Visible = false;
                    MtchWnr.Visible = false;
                    matchLbl.Visible = true;
                    ballOutMsg.Visible = false;
                    isBallOut = false;

                    ball.Left = xMidpoint;
                    ball.Top = yMidpoint;

                    player1.Top = 321;
                    player2.Top = 321;

                    len = 1;
                    timer1.Start();
                }
                
            }
            if (e.KeyCode == Keys.P)
            {
                pointsBoard();
            }
            if(e.KeyCode == Keys.R)
            {
                Application.Exit();

                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }

            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                p2goUp = false;
            }
            if(e.KeyCode == Keys.Down)
            {
                p2goDown = false;
            }
            if(e.KeyCode == Keys.W)
            {
                p1goUp = false;
            }
            if(e.KeyCode == Keys.S)
            {
                p1goDown = false;
            }
        }

    }
}
