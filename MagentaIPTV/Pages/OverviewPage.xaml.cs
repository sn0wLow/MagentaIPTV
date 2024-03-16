using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagentaIPTV
{
    /// <summary>
    /// Interaktionslogik für OverviewPage.xaml
    /// </summary>
    public partial class OverviewPage : BasePage<OverviewViewModel>
    {
        private bool currentlyFading = false;
        private bool currentlyMoving = false;
        private bool wasFullscreen = false;
        private object lockObject = new object();
        private DateTime lastMouseMove;
        public OverviewPage()
        {
            InitializeComponent();
        }

        public OverviewPage(OverviewViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            OverviewViewModel viewModel = this.DataContext as OverviewViewModel;
            viewModel.SetVolume.Execute((int)(sender as Slider).Value);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            OverviewViewModel viewModel = this.DataContext as OverviewViewModel;
            viewModel.UpdateFilter.Execute((sender as TextBox).Text);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            OverviewViewModel viewModel = this.DataContext as OverviewViewModel;
            viewModel.UpdateHDOnly.Execute((sender as CheckBox).IsChecked);
        }

        private void BasePage_MouseMove(object sender, MouseEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            currentlyMoving = true;
            FadeOutPlayerControls();

            /*if ((this.DataContext as OverviewViewModel).IsFullscreen)
            {
                currentlyMoving = true;
                FadeOutPlayerControls();
            }
            else
            {
                if (Cursor != Cursors.Arrow || GridPlayerControls.Visibility != Visibility.Visible)
                {
                    Debug.WriteLine("Bug");
                    Cursor = Cursors.Arrow;
                    GridPlayerControls.FadeInAsync(false, 0.1f, false);
                }
            }*/
        }

        private async void FadeOutPlayerControls()
        {
            if (!currentlyFading)
            {
                currentlyFading = true;
                currentlyMoving = false;


                Cursor = Cursors.Arrow;
                await GridPlayerControls.FadeInAsync(false, 0.5f, false);

                await Task.Delay(3000);

                if (currentlyMoving)
                {
                    currentlyFading = false;
                    FadeOutPlayerControls();
                    return;
                }

                Cursor = Cursors.None;
                await GridPlayerControls.FadeOutAsync(0.5f);

                if (currentlyMoving)
                {
                    Cursor = Cursors.Arrow;
                    await GridPlayerControls.FadeInAsync(false, 0.5f, false);
                }

                Debug.WriteLine("Fading");


                currentlyFading = false;
            }
        }

    }
}
