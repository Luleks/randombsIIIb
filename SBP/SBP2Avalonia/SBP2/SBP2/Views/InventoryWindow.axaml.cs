using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace SBP2.Views;

public partial class InventoryWindow : Window {
    
    private ObservableCollection<OrudjePregled> Inventory { get; set; }

    private int IgracId { get; set; }
    
    public InventoryWindow(int igracId, ObservableCollection<OrudjePregled> inventory) {
        InitializeComponent();

        Inventory = inventory;
        ItemsDataGrid.ItemsSource = Inventory;

        IgracId = igracId;
    }

    private async void ItemsDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e) {
        var result = await MessageBoxManager
            .GetMessageBoxStandard("Prodaja?", "Da li stvarno zelite da prodate ovo orudje?", ButtonEnum.YesNo)
            .ShowAsync();
        if (result == ButtonResult.No)
            return;
        var selectedItem = ItemsDataGrid.SelectedItem as OrudjePregled;
        if (selectedItem == null)
            return;
        var uspeh = await DTOManager.ProdajOruzje(IgracId, selectedItem.Id);
        if (uspeh) {
            Inventory.RemoveAt(ItemsDataGrid.SelectedIndex);
        }
    }
}