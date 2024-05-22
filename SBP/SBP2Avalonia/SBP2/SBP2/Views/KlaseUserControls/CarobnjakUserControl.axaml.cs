using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace SBP2.Views.KlaseUserControls;

public partial class CarobnjakUserControl : UserControl {
    public CarobnjakUserControl(CarobnjakBasic cb) {
        InitializeComponent();

        cb.Magije = RandomMagicAttacks();
        MagijeTextBox.Text = cb.Magije;
    }

    private static string RandomMagicAttacks() {
        List<string> options = new List<string> {
            "Fireball",
            "Ice Spike",
            "Lightning Bolt",
            "Earthquake",
            "Wind Slash",
            "Water Blast"
        };
        Random rnd = new Random();
        int numberOfMagics = rnd.Next(1, 4);

        return string.Join(",", options.OrderBy(x => rnd.Next()).Take(numberOfMagics).ToList());
    }
}