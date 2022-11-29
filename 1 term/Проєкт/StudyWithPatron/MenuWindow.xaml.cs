﻿using System.Media;
using System.Windows;

namespace StudyWithPatron
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Sum_Diff_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.sound1);
                playSound.Play();
            }

            AddSubtWindow add_sub_win = new ();
            this.Visibility = Visibility.Hidden;
            add_sub_win.Show();
        }

        private void Mul_Div_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.sound1);
                playSound.Play();
            }

            MultDivWindow mul_div_win = new ();
            this.Visibility = Visibility.Hidden;
            mul_div_win.Show();
        }

        private void Saper_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.sound1);
                playSound.Play();
            }

            RegistrationWindow reg_win = new ();
            this.Visibility = Visibility.Hidden;
            reg_win.Show();
        }

        private void Back_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.back);
                playSound.Play();
            }

            MainWindow main_win = new ();
            this.Visibility = Visibility.Hidden;
            main_win.Show();
        }

        private void Exit_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.Checks_Sound == true)
            {
                SoundPlayer playSound = new (Properties.ResourcesSounds.close);
                playSound.Play();
            }

            Close();
        }
    }
}
