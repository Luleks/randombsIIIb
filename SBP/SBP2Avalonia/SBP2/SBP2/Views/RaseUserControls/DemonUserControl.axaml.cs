using System;
using Avalonia.Controls;

namespace SBP2.Views.RaseUserControls;

public partial class DemonUserControl : UserControl {
    public DemonUserControl(DemonBasic db) {
        InitializeComponent();
        
        Random rnd = new Random();
        db.NivoPotrebneMagije = rnd.Next(1, 999);

        this.NivoMagijeTextBox.Text = db.NivoPotrebneMagije.ToString();
    }
}