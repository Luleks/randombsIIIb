using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using SBP2.Models.Entiteti;

namespace SBP2.Views;

public partial class CreatePomocnik : Window {
    private Pomocnik _pomocnik;
    
    public CreatePomocnik(Pomocnik pomocnik) {
        InitializeComponent();

        _pomocnik = pomocnik;

        Random rnd = new Random();
        BonusZastitaTextBox.Text = rnd.Next(1, 999).ToString();
    }

    public async void Napravi(object sender, RoutedEventArgs e) {
        if (string.IsNullOrEmpty(ImeTextBox.Text) || ImeTextBox.Text.Length > 15) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Nevalidna duzina imena").ShowAsync();
            return;
        }
        
        var pomocnikBasic = new PomocnikBasic(ImeTextBox.Text!, RasaComboBox.SelectionBoxItem!.ToString()!.ToUpper(),
                KlasaComboBox.SelectionBoxItem!.ToString()!.ToUpper(), int.Parse(BonusZastitaTextBox.Text!), _pomocnik.Igrac.Id);
        var i = await DTOManager.DodajPomocnika(pomocnikBasic);
        if (i == null) {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Neuspelo dodavanje, pokusajte ponovo kasnije").ShowAsync();
            return;
        }

        _pomocnik.Id = i.Id;
        _pomocnik.Ime = i.Ime;
        _pomocnik.Klasa = i.Klasa;
        _pomocnik.Rasa = i.Rasa;
        _pomocnik.BonusZastita = i.BonusZastita;
        _pomocnik.Igrac = i.Igrac;

        await MessageBoxManager.GetMessageBoxStandard("Uspeh", "Uspesno dodat novi pomocnik").ShowAsync();
        Close();
    }

    public void Odustani(object seneder, RoutedEventArgs e) {
        Close();
    }
}