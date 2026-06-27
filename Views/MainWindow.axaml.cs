using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Stichpunkt.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
    }
        private void StartGame_Click(object? sender, RoutedEventArgs e)
    {
        PageHost.Content = new GameView();
    }
}