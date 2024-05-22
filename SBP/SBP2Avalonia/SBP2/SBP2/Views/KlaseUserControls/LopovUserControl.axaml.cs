using System;
using Avalonia.Controls;

namespace SBP2.Views.KlaseUserControls;

public partial class LopovUserControl : UserControl {
    public LopovUserControl(LopovBasic lb) {
        InitializeComponent();

        Random rnd = new Random();
        lb.NivoBuke = rnd.Next(1, 999);
        lb.NivoZamki = rnd.Next(1, 999);

        this.NivoBukeTextBox.Text = lb.NivoBuke.ToString();
        this.NivoZamkiTextBox.Text = lb.NivoZamki.ToString();
    }
}