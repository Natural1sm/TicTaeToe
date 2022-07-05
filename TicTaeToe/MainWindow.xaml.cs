using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTaeToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player currentPlayer;
 
        List<Button> buttons;
        Random rand = new Random(); 
        int playerWins = 0;
        int computerWins = 0;

        public MainWindow()
        {
            InitializeComponent();
            restartGame();
        }
        

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Player_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            currentPlayer = Player.X;
            button.Content = currentPlayer.ToString();
            button.IsEnabled = false;
            buttons.Remove(button);
            
            AImove();
        }
        
        private void AImove()
        {
            Thread.Sleep(100);
            if (buttons.Count > 0)
            {
                int index = rand.Next(buttons.Count);
                buttons[index].IsEnabled = false;
                
                Task.Delay(200);
                currentPlayer = Player.O;
                buttons[index].Content = currentPlayer.ToString();
                buttons.RemoveAt(index);
                CheckGame();
            }
        }
        
        private void loadbuttons()
        {
            buttons = new List<Button> { Board1, Board2, Board3, Board4, Board5, Board6, Board7, Board9, Board8 };
        }

        private void CheckGame()
        {
            if ((string)Board1.Content == "X" && (string)Board2.Content == "X" && (string)Board3.Content == "X"
                || (string)Board4.Content == "X" && (string)Board5.Content == "X" && (string)Board6.Content == "X"
                || (string)Board7.Content == "X" && (string)Board9.Content == "X" && (string)Board8.Content == "X"
                || (string)Board1.Content == "X" && (string)Board4.Content == "X" && (string)Board7.Content == "X"
                || (string)Board2.Content == "X" && (string)Board5.Content == "X" && (string)Board8.Content == "X"
                || (string)Board3.Content == "X" && (string)Board6.Content == "X" && (string)Board9.Content == "X"
                || (string)Board1.Content == "X" && (string)Board5.Content == "X" && (string)Board9.Content == "X"
                || (string)Board3.Content == "X" && (string)Board5.Content == "X" && (string)Board7.Content == "X")
            {
                createNotifier(Player.X);
                playerWins++;
                playerWinLabel.Content = "Player`s win: " + playerWins;
                restartGame();
            }
            
            else if ((string)Board1.Content == "O" && (string)Board2.Content == "O" && (string)Board3.Content == "O"
                     || (string)Board4.Content == "O" && (string)Board5.Content == "O" && (string)Board6.Content == "O"
                     || (string)Board7.Content == "O" && (string)Board9.Content == "O" && (string)Board8.Content == "O"
                     || (string)Board1.Content == "O" && (string)Board4.Content == "O" && (string)Board7.Content == "O"
                     || (string)Board2.Content == "O" && (string)Board5.Content == "O" && (string)Board8.Content == "O"
                     || (string)Board3.Content == "O" && (string)Board6.Content == "O" && (string)Board9.Content == "O"
                     || (string)Board1.Content == "O" && (string)Board5.Content == "O" && (string)Board9.Content == "O"
                     || (string)Board3.Content == "O" && (string)Board5.Content == "O" && (string)Board7.Content == "O")
            {
                createNotifier(Player.O);
                computerWins++;
                botWinLabel.Content = "Bot`s win: " + computerWins;
                restartGame();
            }
        }

        private void createNotifier(Player player)
        {
            switch (player)
            {
                case Player.O:
                {
                    NotifyWindow nw = new NotifyWindow();
                    nw.winLabel.Content = "Bot Wins";
                    nw.Show();
                    break;
                }

                case Player.X:
                {
                    NotifyWindow nw = new NotifyWindow();
                    nw.winLabel.Content = "Player Wins";
                    nw.Show();
                    break;
                }
            }
        }

        private void restartGame()
        {
            foreach (var button in FindVisualChildren<Button>(this))
            {
                if ((string)button.Tag == "play")
                {
                    button.IsEnabled = true;
                    button.Content = "";
                }
            }
            
            loadbuttons();
        }

        public enum Player
        {
            X, O
        }
        
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }


        private void RestartButton_OnClick(object sender, RoutedEventArgs e)
        {
            restartGame();
        }
    }
}