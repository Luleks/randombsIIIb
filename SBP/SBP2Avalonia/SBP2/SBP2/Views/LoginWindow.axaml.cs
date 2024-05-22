using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using SBP2.Models.Entiteti;

namespace SBP2.Views;

public partial class LoginWindow : Window {
    public LoginWindow() {
        InitializeComponent();
    }

    private async void ButtonLogin(object? sender, RoutedEventArgs e) {
        var username = UsernameTextBox.Text;
        if (string.IsNullOrEmpty(username)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Username length is 0").ShowAsync();
            return;
        }

        var password = PasswordBox.Text;
        if (string.IsNullOrEmpty(password)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Username length is 0").ShowAsync();
            return;
        }

        var igrac = await DTOManager.FindIgracByUsername(username, password);
        if (igrac == null) {
            return;
        }

        Sesija? sesija = await DTOManager.DodajSesiju(new SesijaBasic(0, 0, DateTime.Now, 0, igrac.Id));
        if (sesija == null) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Failed session, please try again later").ShowAsync();
            return;
        }
        
        var hpWindow = new HomePageWindow(igrac, sesija);
        hpWindow.Show();
        Close(); 
    }

    private void GoBack(object? sender, RoutedEventArgs e) {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}