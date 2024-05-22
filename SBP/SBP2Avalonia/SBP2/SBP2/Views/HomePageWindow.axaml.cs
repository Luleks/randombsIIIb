using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using SBP2.Models.Entiteti;

namespace SBP2.Views;

public partial class HomePageWindow : Window {
    private Igrac Igrac { get; set; }

    private bool DeletedAccount { get; set; } 
    private Sesija CurrentSesija { get; set; }

    private ObservableCollection<SesijaPregled> RecentActivity { get; set; } = [];

    private ObservableCollection<PomocnikPregled> Pomocnici { get; set; } = [];
    
    public HomePageWindow(Igrac igrac, Sesija currenetSesija) {
        InitializeComponent();
        MainContainer.Width = this.Width;
        MainContainer.Height = this.Height;
        
        Igrac = igrac;
        CurrentSesija = currenetSesija;
        LoadRecentActivity(Igrac.Id);
        LoadPomocnici(Igrac.Id);
        
        PasswordTextBox.Text = igrac.Lozinka;
        UsernameTextBox.Text = igrac.Nadimak;
        UzrastTextBox.Value = igrac.Uzrast;
        ImeTextBox.Text = igrac.Ime;
        PrezimeTextBox.Text = igrac.Prezime ?? "";
        PolComboBox.SelectedIndex = igrac.Pol == 'M' ? 0 : 1;
        KlasaTextBox.Text = igrac.Lik.Klasa.GetType().Name;
        RasaTextBox.Text = igrac.Lik.Rasa.GetType().Name;

        PomocniciDataGrid.DoubleTapped += IzmeniPomocnika;
    }
    

    private async void LoadRecentActivity(int igracId) {
        RecentActivity = new ObservableCollection<SesijaPregled>(await DTOManager.VratiSesijeIgraca(igracId));
        RecentActivityDataGrid.ItemsSource = RecentActivity;
    }

    private async void LoadPomocnici(int igracId) {
        Pomocnici = new ObservableCollection<PomocnikPregled>(await DTOManager.VratiPomocnikeIgraca(igracId));
        PomocniciDataGrid.ItemsSource = Pomocnici;
    }

    private async void OpenLeaderboards(object sender, RoutedEventArgs e) {
        var leaderboards = await DTOManager.VratiLeaderboards();
        if (leaderboards == null) {
            return;
        }
        var leaderboardsWindow = new Leaderboards(leaderboards);
        await leaderboardsWindow.ShowDialog(this);
    }

    private async void ShowInventory(object sender, RoutedEventArgs e) {
        var inventory = new ObservableCollection<OrudjePregled>(await DTOManager.VratiIgracevInventory(Igrac.Id));
        var invWindow = new InventoryWindow(Igrac.Id, inventory);
        await invWindow.ShowDialog(this);

    }

    private async void OtvoriShop(object sender, RoutedEventArgs e) {
        var shopItems = new ObservableCollection<ShoppableOrudjePregled>(await DTOManager.VratiShoppableOrudje());
        var shop = new ShopWindow(Igrac.Id, shopItems);
        await shop.ShowDialog(this);
    }
    
    private async void IgrajStazu(object sender, RoutedEventArgs e) {
        ObservableCollection<StazaPregled> staze = new ObservableCollection<StazaPregled>(
            await DTOManager.VratiSveStaze());
        var igrajStazu = new IgrajStazu(Igrac.Id, staze);
        await igrajStazu.ShowDialog(this);
    }
    
    private async void UpravljanjeTimom(object sender, RoutedEventArgs e) {
        TimPregled? tim = await DTOManager.VratiTimIgraca(Igrac.Id);
        if (tim == null) {
            return;
        }
        var upravljanjeTimom = new UpravljanjeTimom(tim, Igrac.Id);
        await upravljanjeTimom.ShowDialog(this);
    }
    
    private async void NoviPomocnik(object sender, RoutedEventArgs e) {
        var pomocnikToBeFilled = new Pomocnik() {
            BonusZastita = 0,
            Id = 0,
            Klasa = null!,
            Rasa = null!,
            Igrac = Igrac,
            Ime = string.Empty
        };
        var newPom = new CreatePomocnik(pomocnikToBeFilled);
        await newPom.ShowDialog(this);
        Pomocnici.Add(new PomocnikPregled(pomocnikToBeFilled.Id, pomocnikToBeFilled.Ime, pomocnikToBeFilled.Rasa,
            pomocnikToBeFilled.Klasa, pomocnikToBeFilled.BonusZastita));
    }

    private async void IzmeniPomocnika(object? sender, TappedEventArgs e) {
        var selectedPomocnik = PomocniciDataGrid.SelectedItem as PomocnikPregled;
        if (selectedPomocnik == null)
            return;
        var pomocnik = new Pomocnik() {
            Id = selectedPomocnik.Id,
            BonusZastita = selectedPomocnik.BonusZastita,
            Ime = selectedPomocnik.Ime,
            Klasa = selectedPomocnik.Klasa,
            Rasa = selectedPomocnik.Rasa,
            Igrac = Igrac
        };
        var izmPom = new ModifyPomocnik(pomocnik);
        await izmPom.ShowDialog(this);
        if (pomocnik.Id == -1) {
            Pomocnici.Remove(selectedPomocnik);
        }
        else {
            Pomocnici.Remove(selectedPomocnik);
            Pomocnici.Add(new PomocnikPregled(pomocnik.Id, pomocnik.Ime, pomocnik.Rasa, pomocnik.Klasa, pomocnik.BonusZastita));
        }
    }
    
    private async Task<bool> ValidateNext() {
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

    private async void UpdateIgrac(object sender, RoutedEventArgs e) {
        var result = await MessageBoxManager
            .GetMessageBoxStandard("Update?", "Da li zelite da azurirate nalog", ButtonEnum.YesNo).ShowAsync();

        if (result == ButtonResult.No)
            return;

        if (!await ValidateNext()) {
            return;
        }

        Igrac.Prezime = PrezimeTextBox.Text!;
        Igrac.Ime = ImeTextBox.Text!;
        Igrac.Uzrast = (int)UzrastTextBox.Value!;
        Igrac.Pol = Convert.ToChar(PolComboBox.SelectionBoxItem!.ToString()!);
        Igrac.Lozinka = PasswordTextBox.Text!;

        if (await DTOManager.UpdateIgrac(Igrac)) {
            await MessageBoxManager.GetMessageBoxStandard("Success", "Uspesno azurirani podaci").ShowAsync();
        }
        else {
            await MessageBoxManager.GetMessageBoxStandard("Error", "Azuriranje podataka nije uspelo, pokusajte ponovo kasnije").ShowAsync();
        }
    }

    private async void DeleteIgrac(object sender, RoutedEventArgs e) {
        var result = await MessageBoxManager
            .GetMessageBoxStandard("Delete?", "Da li zelite da obirsete nalog?", ButtonEnum.YesNo).ShowAsync();

        if (result == ButtonResult.No)
            return;
        
        result = await MessageBoxManager
            .GetMessageBoxStandard("Delete1?", "Da li stvarno zelite da obrisete nalog", ButtonEnum.YesNo).ShowAsync();

        if (result == ButtonResult.No)
            return;
        
        result = await MessageBoxManager
            .GetMessageBoxStandard("Delete2?", "Brisanje naloga je trajna akcija, ne moze biti opozvana. Obrisi?", ButtonEnum.YesNo).ShowAsync();

        if (result == ButtonResult.No)
            return;

        var deleted = await DTOManager.ObrisiIgraca(Igrac.Id);
        if (deleted) {
            DeletedAccount = true;
            await MessageBoxManager
                .GetMessageBoxStandard("Brisanje uspesno", "Nalog je uspesno obrisan").ShowAsync();
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        else {
            await MessageBoxManager
                .GetMessageBoxStandard("Greska", "Neuspesno brisanje naloga, pokusajte ponovo kasnije").ShowAsync();
        }
    }
    
    private async void SaveSession(object? sender, EventArgs eventArgs) {
        if (DeletedAccount) return;
        Random rnd = new Random();
        CurrentSesija.Zlato = rnd.Next(1, 1000);
        CurrentSesija.Xp = rnd.Next(1, 1000);
        CurrentSesija.Duzina = (int)((DateTime.Now - CurrentSesija.Vreme).TotalMinutes);
        await DTOManager.UpdateSesija(CurrentSesija);
    }

    private void Signout(object sender, RoutedEventArgs e) {
        var mainPage = new MainWindow();
        mainPage.Show();
        Close();
    }
}