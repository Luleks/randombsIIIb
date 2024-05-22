using System;
using Avalonia.Controls;

namespace SBP2.Views.RaseUserControls;

public partial class CovekUserControl : UserControl {
    public CovekUserControl(CovekBasic cb) {
        InitializeComponent();

        Random rnd = new Random();
        cb.UspesnostUSkrivanju = rnd.Next(1, 999);

        this.SkrivanjeTextBox.Text = cb.UspesnostUSkrivanju.ToString();
    }
}