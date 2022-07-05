using System.Windows;

namespace TicTaeToe;

public partial class NotifyWindow : Window
{
    public NotifyWindow()
    {
        InitializeComponent();
    }

    private void Minimize_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }
        
    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}