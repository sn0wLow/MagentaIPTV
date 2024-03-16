using System.Windows.Controls;


namespace MagentaIPTV
{
    /// <summary>
    /// Interaktionslogik für SenderListItemControl.xaml
    /// </summary>
    public partial class SenderListItemControl : UserControl
    {
        public SenderListItemControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SenderListItemViewModel viewModel = this.DataContext as SenderListItemViewModel;
            viewModel.OpenSender.Execute(null);
        }
    }
}
