using Avalonia.Controls;

namespace SBP2.Views.RaseUserControls;

public partial class PatuljakUserControl : UserControl {
    private PatuljakBasic PatuljakBasic { get; set; }
    
    public PatuljakUserControl(PatuljakBasic pb) {
        InitializeComponent();

        PatuljakBasic = pb;
    }

    private void OruzjeCombobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        if (OruzjeCombobox != null && OruzjeCombobox.SelectedItem != null)
            PatuljakBasic.TipOruzja = OruzjeCombobox.SelectedItem.ToString()!;
    }
}