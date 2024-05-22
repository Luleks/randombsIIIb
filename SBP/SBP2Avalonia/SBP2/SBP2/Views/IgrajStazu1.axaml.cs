using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace SBP2.Views;

public partial class IgrajStazu1 : Window {

    private int StazaId { get; set; }
    private int IgracId { get; set; }
    private bool TimskaStaza { get; set; }
    private List<string> Rase { get; set; }
    private List<string> Klase { get; set; }

    private ObservableCollection<ClanTimaPregled> Saigraci { get; set; }
    
    private ObservableCollection<ClanTimaPregled> IzabraniSaigraci { get; set; } = [];
    
    public IgrajStazu1(int igracId, int stazaId, bool timskaStaza, List<string> rase, List<string> klase, ObservableCollection<ClanTimaPregled> sviClanovi) {
        InitializeComponent();

        IgracId = igracId;
        StazaId = stazaId;
        Rase = rase;
        Klase = klase;
        TimskaStaza = timskaStaza;

        Saigraci = sviClanovi;

        SaigraciGrid.ItemsSource = Saigraci;
        IzabraniSaigraciGrid.ItemsSource = IzabraniSaigraci;
    }

    private async void Igraj(object sender, RoutedEventArgs e) {
        if (TimskaStaza && IzabraniSaigraci.Count < 2) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Timska staza zahteva bar 2 igraca").ShowAsync();
            return;
        }
        
        if (IzabraniSaigraci.FirstOrDefault(x => x.Id == IgracId) == null) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Potrebno je dodati sebe u grupu").ShowAsync();
            return;
        }

        if (!Rase.All(x => IzabraniSaigraci.Count(y => y.Rasa.ToUpper() == x) >= 1)) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", $"Za igranje staze potreban je bar jedan primerak sledecih rasa: " +
                                                                    $"{string.Join("\n", Rase)}").ShowAsync();
            return;
        }
        
        if (!Klase.All(x => IzabraniSaigraci.Count(y => y.Klasa.ToUpper() == x) >= 1)) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", $"Za igranje staze potreban je bar jedan primerak sledecih klasa: " +
                                                                    $"{string.Join("\n", Klase)}").ShowAsync();
            return;
        }

        await DTOManager.IgranjeStaze(StazaId, IzabraniSaigraci.Select(x => x.Id).ToList());
    }

    private void DodajIgraca(object? sender, TappedEventArgs e) {
        int selectedIdx = SaigraciGrid.SelectedIndex;
        if (selectedIdx == -1)
            return;
        IzabraniSaigraci.Add(Saigraci[selectedIdx]);
        Saigraci.RemoveAt(selectedIdx);
    }

    private void UkloniIgraca(object? sender, TappedEventArgs e) {
        int selectedIdx = IzabraniSaigraciGrid.SelectedIndex;
        if (selectedIdx == -1)
            return;
        Saigraci.Add(IzabraniSaigraci[selectedIdx]);
        IzabraniSaigraci.RemoveAt(selectedIdx);
    }
}