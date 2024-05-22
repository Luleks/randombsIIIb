using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using SBP2.Models.Entiteti;

namespace SBP2.Views;

public partial class ModifyPomocnik : Window {
    private Pomocnik _pomocnik;
    
    public ModifyPomocnik(Pomocnik pomocnik) {
        InitializeComponent();

        _pomocnik = pomocnik;

        Random rnd = new Random();
        BonusZastitaTextBox.Text = rnd.Next(1, 999).ToString();
        ImeTextBox.Text = _pomocnik.Ime;
    }

    public async void Obrisi(object sender, RoutedEventArgs e) {
        if (await DTOManager.DeletePomocnik(_pomocnik)) {
            _pomocnik.Id = -1;
            Close();
        }
        else {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Neuspelo brisanje, pokusajte ponovo").ShowAsync();
        }

    }

    public async void Sacuvaj(object sender, RoutedEventArgs e) {
        if (string.IsNullOrEmpty(ImeTextBox.Text) || ImeTextBox.Text.Length > 15) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Nevalidna duzina imena").ShowAsync();
            return;
        }
        
        _pomocnik.Ime = ImeTextBox.Text!;
        if (await DTOManager.UpdatePomocnik(_pomocnik)) {
            Close();
        }
        else {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Neuspelo azuriranje, pokusajte ponovo").ShowAsync();
        }
    }

    public void Odustani(object seneder, RoutedEventArgs e) {
        Close();
    }
}