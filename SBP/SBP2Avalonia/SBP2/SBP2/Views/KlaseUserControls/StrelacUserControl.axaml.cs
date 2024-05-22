using Avalonia.Controls;

namespace SBP2.Views.KlaseUserControls;

public partial class StrelacUserControl : UserControl {
    private StrelacBasic StrelacBasic { get; set; }
    
    public StrelacUserControl(StrelacBasic sb) {
        InitializeComponent();

        StrelacBasic = sb;
    }

    private void LukIliSamostrelCbx_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        if (LukIliSamostrelCbx != null && LukIliSamostrelCbx.SelectedItem != null) {
            StrelacBasic.LukIliSamostrel = LukIliSamostrelCbx.SelectedIndex == 0 ? 0 : 1;
        }
    }
}