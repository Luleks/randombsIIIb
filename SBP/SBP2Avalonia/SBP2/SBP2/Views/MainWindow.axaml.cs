using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SBP2.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void OpenRegisterWindow(object sender, RoutedEventArgs e) {
        var newWindow = new RegisterWindow();
        newWindow.Show();
        Close();
    }

    private void OpenLoginWindow(object sender, RoutedEventArgs e) {
        var newWindow = new LoginWindow();
        newWindow.Show();
        Close();
    }
    
}