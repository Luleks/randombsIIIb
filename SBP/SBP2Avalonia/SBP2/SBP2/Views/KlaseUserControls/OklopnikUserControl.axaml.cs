using System;
using Avalonia.Controls;

namespace SBP2.Views.KlaseUserControls;

public partial class OklopnikUserControl : UserControl {
    public OklopnikUserControl(OklopnikBasic ob) {
        InitializeComponent();

        Random rnd = new Random();
        ob.MaxOklop = rnd.Next(1, 999);

        this.MaxOklopTextBox.Text = ob.MaxOklop.ToString();
    }
}