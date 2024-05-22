using System;
using Avalonia.Controls;

namespace SBP2.Views.RaseUserControls;

public partial class VilenjakUserControl : UserControl {
    public VilenjakUserControl(VilenjakBasic vb) {
        InitializeComponent();
        
        Random rnd = new Random();
        vb.NivoPotrebneMagije = rnd.Next(1, 999);

        this.NivoMagijeTextBox.Text = vb.NivoPotrebneMagije.ToString();
    }
}