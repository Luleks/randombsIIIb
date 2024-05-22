using Avalonia.Controls;

namespace SBP2.Views.RaseUserControls;

public partial class OrkUserControl : UserControl {
    private OrkBasic OrkBasic { get; set; }
    
    public OrkUserControl(OrkBasic ob) {
        InitializeComponent();

        OrkBasic = ob;
    }

    private void OruzjeCombobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        if (OruzjeCombobox != null && OruzjeCombobox.SelectedItem != null)
            OrkBasic.TipOruzja = OruzjeCombobox.SelectedItem.ToString()!;
    }
}