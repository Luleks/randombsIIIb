using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SBP2.Views.KlaseUserControls;

public partial class BoracUserControl : UserControl {
    private BoracBasic BoracBasic { get; set; }
    
    public BoracUserControl(BoracBasic bb) {
        InitializeComponent();

        BoracBasic = bb;
    }

    private void DualWielderCbx_OnIsCheckedChanged(object? sender, RoutedEventArgs e) {
        BoracBasic.DualWilder = (bool)DualWielderCbx.IsChecked! ? 1 : 0;
    }


    private void KoristiStitCbx_OnIsCheckedChanged(object? sender, RoutedEventArgs e) {
        BoracBasic.KoristiStit = (bool)KoristiStitCbx.IsChecked! ? 1 : 0;
    }
}