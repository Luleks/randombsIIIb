using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;

namespace SBP2.Views;

public partial class ShopWindow : Window {
    
    private ObservableCollection<ShoppableOrudjePregled> ShopInventory { get; set; }

    private int IgracId { get; set; }
    public ShopWindow(int igracId, ObservableCollection<ShoppableOrudjePregled> shopInventory) {
        InitializeComponent();

        ShopInventory = shopInventory;
        ItemsDataGrid.ItemsSource = ShopInventory;

        IgracId = igracId;
    }

    private async void ItemsDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e) {
        var selectedOrudje = ItemsDataGrid.SelectedItem as ShoppableOrudjePregled;
        if (selectedOrudje == null) {
            return;
        }

        await DTOManager.KupiOrudje(IgracId, selectedOrudje.Id);
    }
}