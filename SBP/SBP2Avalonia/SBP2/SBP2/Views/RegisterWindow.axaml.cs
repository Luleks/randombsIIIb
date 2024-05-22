using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace SBP2.Views;

public partial class RegisterWindow : Window {
    public RegisterWindow() {
        InitializeComponent();
    }
    
    private void GoBack(object? sender, RoutedEventArgs e) {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private async void Next(object? seneder, RoutedEventArgs e) {
        bool goNext = await ValidateNext();
        if (!goNext) return;

        IgracBasic ib = new IgracBasic(0, this.UsernameTextBox.Text!, this.PasswordTextBox.Text!,
                this.PolComboBox.SelectionBoxItem!.ToString()!.ToCharArray()[0],
                Int32.Parse(this.UzrastTextBox.Text!), this.ImeTextBox.Text!,
                this.PrezimeTextBox.Text, null!);

        var registerLik = new RegisterLikWindow(ib, this.KlasaComboBox.SelectionBoxItem!.ToString()!,
            this.RasaComboBox.SelectionBoxItem!.ToString()!);
        registerLik.Show();
        Close();
    }

    private async Task<bool> ValidateNext() {
        if (string.IsNullOrEmpty(this.UsernameTextBox.Text)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Polje username je obavezno").ShowAsync();
            return false;
        }
        if (string.IsNullOrEmpty(this.PasswordTextBox.Text)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Polje password je obavezno").ShowAsync();
            return false;
        }
        if (string.IsNullOrEmpty(this.ImeTextBox.Text)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Polje Ime je obavezno").ShowAsync();
            return false;
        }

        if (this.UzrastTextBox != null && !Int32.TryParse(this.UzrastTextBox.Text, out int val)) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Nedozvoljeni karakter u polju uzrast").ShowAsync();
            return false;
        }
        if (Int32.Parse(this.UzrastTextBox!.Text!) < 13) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Morate biti bar 13 godina").ShowAsync();
            return false;
        }

        return true;
    }
}