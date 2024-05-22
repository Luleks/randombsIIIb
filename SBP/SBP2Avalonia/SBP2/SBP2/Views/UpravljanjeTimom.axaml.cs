using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace SBP2.Views;

public partial class UpravljanjeTimom : Window {

    private ObservableCollection<TimPregled> TimPregled { get; set; } = [];
    
    private int IgracId { get; set; }
    
    public UpravljanjeTimom(TimPregled timPregled, int igracId) {
        InitializeComponent();
        MainContainer.Width = this.Width;
        MainContainer.Height = this.Height;
        
        if (timPregled.Id != -1) {
            TimPregled.Add(timPregled);
        } 
        CurrentTimGrid.ItemsSource = TimPregled;
        IgracId = igracId;

        Random rnd = new Random();
        BonusXpTextBox.Text = rnd.Next(1, 999).ToString();
    }

    public async void BoriSe(object sender, RoutedEventArgs e) {
        if (string.IsNullOrEmpty(BoriSeTextBox.Text)) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Prazno polje protivnicki tim").ShowAsync();
            BoriSeTextBox.Text = string.Empty;
            return;
        }

        if (!TimPregled.Any()) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Niste clan niti jednog tima").ShowAsync();
            BoriSeTextBox.Text = string.Empty;
            return;
        }

        int pobednik = await DTOManager.NovaBorba(TimPregled[0].Id, BoriSeTextBox.Text);
        if (pobednik == 1) {
            await MessageBoxManager.GetMessageBoxStandard("Pobeda", "Pobedili ste").ShowAsync();
            var t = TimPregled[0];
            TimPregled.RemoveAt(0);
            t.BrojPobeda += 1;
            TimPregled.Add(new TimPregled(t.Id, t.Ime, t.MaxIgraca, t.BonusXp, t.VremeOd, t.BrojClanova, t.BrojPobeda, t.BrojPoraza));
        }
        else if (pobednik == -1) {
            await MessageBoxManager.GetMessageBoxStandard("Poraz", "Izgubili ste").ShowAsync();
            var t = TimPregled[0];
            TimPregled.RemoveAt(0);
            t.BrojPoraza += 1;
            TimPregled.Add(new TimPregled(t.Id, t.Ime, t.MaxIgraca, t.BonusXp, t.VremeOd, t.BrojClanova, t.BrojPobeda, t.BrojPoraza));
        }
        BoriSeTextBox.Text = string.Empty;

    }
    
    public async void JoinTeam(object sender, RoutedEventArgs e) {
        var result = await MessageBoxManager.GetMessageBoxStandard("Upit",
            "Pridruzivanje novom timu ce vas automatski izbaciti iz starog", ButtonEnum.YesNo).ShowAsync();
        if (result == ButtonResult.No) {
            return;
        }

        if (string.IsNullOrEmpty(ImeToJoinTextBox.Text)) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Prazno ime time").ShowAsync();
            return;
        }

        TimPregled? uspeh = await DTOManager.PridruziSeTimu(ImeToJoinTextBox.Text!, IgracId);
        if (uspeh != null) {
            TimPregled.Clear();
            TimPregled.Add(uspeh);
            await MessageBoxManager.GetMessageBoxStandard("Uspeh", "Uspesno ste se pridruzili timu").ShowAsync();
            ImeToJoinTextBox.Text = string.Empty;
        }
    }

    private async void NapustiTim(object sender, RoutedEventArgs e) {
        var result = await MessageBoxManager.GetMessageBoxStandard("Upit",
            "Napusti tim", ButtonEnum.YesNo).ShowAsync();
        if (result == ButtonResult.No) {
            return;
        }

        if (TimPregled.Count == 0) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Niste clan niti jednog tima").ShowAsync();
            return;
        }

        if (await DTOManager.NapustiTim(TimPregled[0].Id, IgracId)) {
            TimPregled.Clear();
            await MessageBoxManager.GetMessageBoxStandard("Uspeh", "Uspesno ste napustili tim").ShowAsync();
        }
        else {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Greska prilikom napustanja tima").ShowAsync();
        }
    } 
    
    private async void NapraviTim(object sender, RoutedEventArgs e) {
        var result = await MessageBoxManager.GetMessageBoxStandard("Upit",
            "Kreiranje novog tima ce vas automatski izbaciti iz starog", ButtonEnum.YesNo).ShowAsync();
        if (result == ButtonResult.No) {
            return;
        }

        if (String.IsNullOrEmpty(ImeTextBox.Text) || ImeTextBox.Text.Length > 15) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Nevalidno ili predugacko ime time").ShowAsync();
            return;
        }

        if (!int.TryParse(MaxClanova.Value.ToString(), out int trash) || trash > 999 || trash < 0) {
            await MessageBoxManager.GetMessageBoxStandard("Greska", "Max igraci error").ShowAsync();
            return;
        }
        TimPregled? tp =
            await DTOManager.NapraviTim(new TimBasic(ImeTextBox.Text!, (int)MaxClanova.Value!, int.Parse(BonusXpTextBox.Text!), IgracId));
        if (tp == null)
            return;
        TimPregled.Clear();
        TimPregled.Add(tp);
        ImeTextBox.Text = string.Empty;
        MaxClanova.Value = 20;
    }
}