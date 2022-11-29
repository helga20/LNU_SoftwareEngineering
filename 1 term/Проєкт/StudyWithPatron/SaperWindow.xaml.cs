﻿using StudyWithPatron.BLL;
using StudyWithPatron.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StudyWithPatron
{
    /// <summary>
    /// Interaction logic for SaperWindow.xaml
    /// </summary>
    public partial class SaperWindow : Window
    {
        readonly ApplicationContext db;
        readonly DispatcherTimer timer = new ();
        private int i = 11;
        readonly DispatcherTimer gameTimer = new ();
        int speed = 5;
        int intervals = 90;
        readonly Random rand = new ();
        readonly List<Rectangle> itemRemover = new();
        int bombs;
        int j;
        int missedBombs;
        bool gameIsActive;
        int score2;
        public SaperWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(200);
        }
        int counter = 0;
        int max = 0;

        private void GameEngine(object sender, EventArgs e)
        {
            intervals -= 10;
            if (intervals < 1)
            {
                ImageBrush bombImage = new ();
                bombs += 1;
                if (bombs > 1)
                {
                    bombs = 1;
                }
                switch (bombs)
                {
                    case 1:
                        bombImage.ImageSource = new BitmapImage(new Uri(@"../net6.0-windows/Images/Bomb.png", UriKind.Relative));
                        break;
                }

                Rectangle newBomb = new()
                {
                    Tag = "bomb",
                    Height = 100,
                    Width = 100,
                    Fill = bombImage
                };

                Canvas.SetLeft(newBomb, rand.Next(50, 400));
                Canvas.SetTop(newBomb, 600);
                MyCanvas.Children.Add(newBomb);
                intervals = rand.Next(90, 150);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "bomb")
                {
                    j = rand.Next(-5, 5);
                    Canvas.SetTop(x, Canvas.GetTop(x) - speed);
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - (j * -1));
                }

                if (Canvas.GetTop(x) < 20)
                {
                    itemRemover.Add(x);
                    missedBombs += 1;
                }
            }
            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);
            }
            if (missedBombs > 10)
            {
                gameIsActive = false;
                gameTimer.Stop();

                RestartGame();
            }
        }

        private void PopBalloons(object sender, MouseButtonEventArgs e)
        {
            if (gameIsActive)
            {
                if (e.OriginalSource is Rectangle)
                {
                    Rectangle activeRec = (Rectangle)e.OriginalSource;
                    MyCanvas.Children.Remove(activeRec);
                    score2 += 1;
                }
            }
        }

        private void StartGame()
        {
            gameTimer.Start();

            missedBombs = 0;
            score2 = 0;
            intervals = 7;
            gameIsActive = true;
            speed = 15;
        }

        private void RestartGame()
        {
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                itemRemover.Add(x);
            }
            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);
            }
            itemRemover.Clear();
            StartGame();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Слідкуй за часом");

            Check_Hearts();
            counter--;
            if (counter < 0)
            {
                counter = 0;
            }
            score.Content = "Рахунок - " + counter;

            NextSol();
        }

        private void Result_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tString = result_TextBox.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Будь ласка, введи число");
                    result_TextBox.Text = "";
                    return;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.clear);
                playSound.Play();
            }
            result_TextBox.Text = "";
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "1";
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "2";
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "3";
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "4";
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "5";
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "6";
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "7";
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "8";
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "9";
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            string temp = result_TextBox.Text;
            result_TextBox.Text = temp + "0";
        }

        public static char GetModul()
        {
            Random rnd = new ();
            string chars = "+/-*";
            int num = rnd.Next(0, 4);
            char modul = chars[num];
            return modul;
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            i--;
            timer_time.Content = i.ToString();
            if (i == 0)
            {
                if (Globals.Checks_Sound == true)
                {
                    SoundPlayer playSound = new (Properties.ResourcesSounds.error);
                    playSound.Play();
                }

                MessageBox.Show("Час вийшов");
                timer.Stop();
                Check_Hearts();
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.sound1);
                playSound.Play();
            }
            RestartGame();
            timer.IsEnabled = true;

            start.Visibility = Visibility.Collapsed;
            max = 10;
            Random rnd = new ();
            int a = rnd.Next(1, max);
            int b = rnd.Next(1, max);
            char modul = GetModul();
            int temp;
            if (a < b && modul == '-')
            {
                temp = a;
                a = b;
                b = temp;
            }
            number_1.Content = a.ToString();

            modul_.Content = modul.ToString();
            number_2.Content = b.ToString();
            eq.Content = "=";
        }
        private void NextSol()
        {
            result_TextBox.Text = "";
            Random rnd = new ();
            int a_1 = rnd.Next(1, max);
            int b_1 = rnd.Next(1, max);
            char modul = GetModul();
            int temp;
            if (a_1 < b_1 && modul == '-')
            {
                temp = a_1;
                a_1 = b_1;
                b_1 = temp;
            }
            if (a_1 % b_1 != 0 && modul == '/')
            {
                modul = '+';
            }
            number_1.Content = a_1.ToString();
            number_2.Content = b_1.ToString();
            modul_.Content = modul.ToString();
        }

        private void Check_Hearts()
        {
            if (heart3.Visibility == Visibility.Visible)
            {
                heart3.Visibility = Visibility.Collapsed;
                timer.IsEnabled = true;
                timer.Start();
                i = 11; // для таймеру 
            }
            else if (heart2.Visibility == Visibility.Visible)
            {
                heart2.Visibility = Visibility.Collapsed;
                timer.IsEnabled = true;
                timer.Start();
                i = 11; // для таймеру 
            }
            else if (heart1.Visibility == Visibility.Visible)
            {
                heart1.Visibility = Visibility.Collapsed;

                if (Globals.Checks_Sound == true)
                {
                    SoundPlayer playSound = new (Properties.ResourcesSounds.gameover);
                    playSound.Play();
                }

                MessageBox.Show("Не залишилося спроб. Гра завершена");
                UserScores user_score = new (Globals.Name, Globals.Result);
                db.UserScore.Add(user_score);
                db.SaveChanges();

                MenuWindow m_win = new ();
                this.Visibility = Visibility.Hidden;
                m_win.Show();
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            int total_score = counter + score2;

            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.check);
                playSound.Play();
            }

            i = 11;// для  часу

            if (result_TextBox.Text == "")
            {
                MessageBox.Show("Введи свою відповідь)");
            }
            else
            {
                string var; var = result_TextBox.Text;
                int res = Convert.ToInt32(var);
                int a = Convert.ToInt32(number_1.Content);
                int b = Convert.ToInt32(number_2.Content);

                if (res == a + b || res == a - b || res == a / b || res == a * b)
                {
                    NextSol();

                    counter++;
                    score.Content = "Рахунок - " + total_score;
                }
                else
                {
                    if (Globals.Checks_Sound == true)
                    {
                        SoundPlayer playSound1 = new (Properties.ResourcesSounds.error);
                        playSound1.Play();
                    }

                    MessageBox.Show("Обережно, ти відповів неправильно");
                    Check_Hearts();
                    counter--;
                    if (counter < 0)
                    {
                        counter = 0;
                    }
                    score.Content = "Рахунок - " + total_score;
                    NextSol();
                }
                if (counter >= 15)
                {
                    max = 50;
                }
                if (counter >= 25)
                {
                    max = 75;
                }
                if (counter >= 32)
                {
                    max = 130;
                }
            }
            Globals.Result = total_score;

        }

        private void Back_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.back);
                playSound.Play();
            }

            RegistrationWindow reg_win = new ();
            this.Visibility = Visibility.Hidden;
            reg_win.Show();
            db.SaveChanges();
        }

        private void Exit_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.close);
                playSound.Play();
            }

            Close();
            db.SaveChanges();
        }
        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Check_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
