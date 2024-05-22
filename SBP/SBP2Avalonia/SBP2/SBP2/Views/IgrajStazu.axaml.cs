using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;

namespace SBP2.Views;

public partial class IgrajStazu : Window {
    
    private ObservableCollection<StazaPregled> Staze { get; set; }
    
    private int IgracId { get; set; }

    public IgrajStazu(int igracId, ObservableCollection<StazaPregled> staze) {
        InitializeComponent();
        MainContainer.Width = this.Width;
        MainContainer.Height = this.Height;

        Staze = staze;
        IgracId = igracId;

        StazeDataGrid.ItemsSource = Staze;
        var nazivColumn = new DataGridTextColumn() { Header = "Naziv", Binding = new Binding("Naziv") };
        var bonusColumn = new DataGridTextColumn() { Header = "BonusXp", Binding = new Binding("BonusXp") };
        var timskaColumn = new DataGridCheckBoxColumn() { IsReadOnly = true, Header = "TimskaStaza", Binding = new Binding("TimskaStaza") };
        var raseColumn = new DataGridTextColumn() { Header = "Potrebne rase", Binding = new Binding("RaseRepr") };
        var klaseColumn = new DataGridTextColumn() { Header = "Potrebne klase", Binding = new Binding("KlaseRepr") };
        StazeDataGrid.Columns.Add(nazivColumn);
        StazeDataGrid.Columns.Add(bonusColumn);
        StazeDataGrid.Columns.Add(timskaColumn);
        StazeDataGrid.Columns.Add(raseColumn);
        StazeDataGrid.Columns.Add(klaseColumn);

    }

    private async void StazeDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e) {
        var selectedStaza = StazeDataGrid.SelectedItem as StazaPregled;
        if (selectedStaza == null)
            return;
        var sviClanovi = new ObservableCollection<ClanTimaPregled>(await DTOManager.VratiClanoveIgracevogTima(IgracId));
        var stazaDetalji = new IgrajStazu1(IgracId, selectedStaza.Id, selectedStaza.TimskaStaza, selectedStaza.Rase, selectedStaza.Klase, sviClanovi);
        await stazaDetalji.ShowDialog(this);
    }
}