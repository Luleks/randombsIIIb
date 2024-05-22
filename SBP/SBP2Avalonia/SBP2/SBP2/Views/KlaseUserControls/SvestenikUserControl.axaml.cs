using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SBP2.Views.KlaseUserControls;

public partial class SvestenikUserControl : UserControl {
    
    private SvestenikBasic SvestenikBasic { get; set; }
    
    public SvestenikUserControl(SvestenikBasic sb) {
        InitializeComponent();

        SvestenikBasic = sb;
        SvestenikBasic.Blagoslovi = RandomBlagoslovi();

        BlagosloviTextBox.Text = SvestenikBasic.Blagoslovi;
    }
    
    private static string RandomBlagoslovi() {
        List<string> options = new List<string> {
            "Blessing of the Eternal Flame",
            "Blessing of the Celestial Light",
            "Blessing of the Nature's Embrace",
            "Blessing of the Mystic Winds",
            "Blessing of the Guardian Spirit"
        };
        Random rnd = new Random();
        int numberOfMagics = rnd.Next(1, 2);

        return string.Join(",", options.OrderBy(x => rnd.Next()).Take(numberOfMagics).ToList());
    }

    private void ReligijaCbx_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
        if (ReligijaCbx != null && ReligijaCbx.SelectedItem != null)
            SvestenikBasic.Religija = ReligijaCbx.SelectedItem!.ToString()!;
    }

    private void KoristiStitCbx_OnIsCheckedChanged(object? sender, RoutedEventArgs e) {
        SvestenikBasic.CanHeal = (bool)CanHealCbx.IsChecked! ? 1 : 0;
    }
}