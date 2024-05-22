using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using SBP2.Models.Entiteti;
using SBP2.Views.KlaseUserControls;
using SBP2.Views.RaseUserControls;

namespace SBP2.Views;

public partial class RegisterLikWindow : Window {
    private readonly IgracBasic _igrac;
    public RegisterLikWindow(IgracBasic ib, string klasa, string rasa) {
        _igrac = ib;
        InitializeComponent();
        
        Random rnd = new Random();

        _igrac.Lik = new LikBasic(0, rnd.Next(1, 9), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 10000), null!,
            null!);

        this.StepenZamoraTextBox.Text = ib.Lik.StepenZamora.ToString();
        this.IskustvoTextBox.Text = ib.Lik.Iskustvo.ToString();
        this.ZlatoTextBox.Text = ib.Lik.Zlato.ToString();
        this.NivoZdravljaTextBox.Text = ib.Lik.NivoZdravlja.ToString();
        
        AddKlasaControl(ib.Lik, klasa);
        AddRasaControl(ib.Lik, rasa);
    }

    private void AddKlasaControl(LikBasic lb, string klasa) {
        if (klasa == "Lopov") {
            lb.Klasa = new LopovBasic(0, 0, 0);
            var lopovControl = new LopovUserControl((LopovBasic)lb.Klasa);
            
            MainGrid.Children.Add(lopovControl);
            Grid.SetRow(lopovControl, 0);
            Grid.SetColumn(lopovControl, 0);
        }
        else if (klasa == "Strelac") {
            lb.Klasa = new StrelacBasic(0, 0);
            var strelacControl = new StrelacUserControl((StrelacBasic)lb.Klasa);
            
            MainGrid.Children.Add(strelacControl);
            Grid.SetRow(strelacControl, 0);
            Grid.SetColumn(strelacControl, 0);
        }
        else if (klasa == "Oklopnik") {
            lb.Klasa = new OklopnikBasic(0, 0);
            var oklopnikControl = new OklopnikUserControl((OklopnikBasic)lb.Klasa);
            
            MainGrid.Children.Add(oklopnikControl);
            Grid.SetRow(oklopnikControl, 0);
            Grid.SetColumn(oklopnikControl, 0);
        }
        else if (klasa == "Carobnjak") {
            lb.Klasa = new CarobnjakBasic(0, "");
            var carobnjakControl = new CarobnjakUserControl((CarobnjakBasic)lb.Klasa);
            
            MainGrid.Children.Add(carobnjakControl);
            Grid.SetRow(carobnjakControl, 0);
            Grid.SetColumn(carobnjakControl, 0);
        }
        else if (klasa == "Borac") {
            lb.Klasa = new BoracBasic(0, 0, 0);
            var boracControl = new BoracUserControl((BoracBasic)lb.Klasa);
            
            MainGrid.Children.Add(boracControl);
            Grid.SetRow(boracControl, 0);
            Grid.SetColumn(boracControl, 0);
        }
        else if (klasa == "Svestenik") {
            lb.Klasa = new SvestenikBasic(0, "Luminism", "", 0);
            var svestenikControl = new SvestenikUserControl((SvestenikBasic)lb.Klasa);
            
            MainGrid.Children.Add(svestenikControl);
            Grid.SetRow(svestenikControl, 0);
            Grid.SetColumn(svestenikControl, 0);
        }
    }

    private void AddRasaControl(LikBasic lb, string rasa) {
        if (rasa == "Covek") {
            lb.Rasa = new CovekBasic(0, 0);
            var covekControl = new CovekUserControl((CovekBasic)lb.Rasa);
            
            MainGrid.Children.Add(covekControl);
            Grid.SetRow(covekControl, 0);
            Grid.SetColumn(covekControl, 1);
        }
        else if (rasa == "Patuljak") {
            lb.Rasa = new PatuljakBasic(0, "Sekira");
            var patuljakControl = new PatuljakUserControl((PatuljakBasic)lb.Rasa);
            
            MainGrid.Children.Add(patuljakControl);
            Grid.SetRow(patuljakControl, 0);
            Grid.SetColumn(patuljakControl, 1);
        }
        else if (rasa == "Ork") {
            lb.Rasa = new OrkBasic(0, "Sekira");
            var orkControl = new OrkUserControl((OrkBasic)lb.Rasa);
            
            MainGrid.Children.Add(orkControl);
            Grid.SetRow(orkControl, 0);
            Grid.SetColumn(orkControl, 1);
        }
        else if (rasa == "Vilenjak") {
            lb.Rasa = new VilenjakBasic(0, 0);
            var vilenjakControl = new VilenjakUserControl((VilenjakBasic)lb.Rasa);
            
            MainGrid.Children.Add(vilenjakControl);
            Grid.SetRow(vilenjakControl, 0);
            Grid.SetColumn(vilenjakControl, 1);
        }
        else if (rasa == "Demon") {
            lb.Rasa = new DemonBasic(0, 0);
            var demonControl = new DemonUserControl((DemonBasic)lb.Rasa);
            
            MainGrid.Children.Add(demonControl);
            Grid.SetRow(demonControl, 0);
            Grid.SetColumn(demonControl, 1);
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e) {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private async void Register(object sender, RoutedEventArgs e) {
        bool success = await DTOManager.DodajIgraca(_igrac);
        if (success) {
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno ste se registrovali").ShowAsync();
        }
        else {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Neuspesna registracija (potencijalno zauzet username").ShowAsync();
        }
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
    
}