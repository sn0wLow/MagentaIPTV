using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MagentaIPTV
{
    public class OverviewViewModel : BaseViewModel
    {


        #region Private Properties

        private WindowState previousState;

        #endregion

        #region Public Properties

        public List<SenderListItemViewModel> FilteredItems { get; set; }
        public List<SenderListItemViewModel> SenderItems { get; set; }
        public List<Sender> SenderList { get; set; }
        public bool OverviewMenuExpanded { get; set; } = true;
        public double OverviewMenuOpacity { get; set; } = 1;
        public double SidePanelWidth { get; set; } = 48;

        public bool IsLoadingSenderListe { get; set; }
        public bool IsChangingChannel { get; set; } = false;
        public bool IsPaused { get; set; } = true;
        public string FilterText { get; set; } = "";
        public bool IsHDOnly { get; set; } = false;
        public bool IsFullscreen { get; set; } = false;
        public bool IsMuted { get; set; } = false;
        public bool HasMedia { get; set; } = false;
        public string MediaName { get; set; } = "MediaName";


        #endregion

        #region Constructor

        public OverviewViewModel()
        {
            LoadSenderList();

            this.ExpandToggle = new RelayCommand(ToggleMenu);
            this.ToggleMute = new RelayCommand(() => { VLCMagentaPlayer.Instance.ToggleMute(); this.IsMuted = VLCMagentaPlayer.Instance.IsMuted(); });
            this.Fullscreen = new RelayCommand(SetFullscreen);
            this.Fullscreen = new RelayCommand(SetFullscreen);
            this.Pause = new RelayCommand(() => { VLCMagentaPlayer.Instance.Pause(); });
            this.SetVolume = new RelayParameterizedCommand((parameter) => VLCMagentaPlayer.Instance.Volume = (int)parameter);
            this.UpdateFilter = new RelayParameterizedCommand((parameter) => { this.FilterText = (parameter.ToString()); this.FilteredItems = this.IsHDOnly ? this.SenderItems?.FindAll(x => x.Name.ToLower().Contains(FilterText.ToLower()) && x.IsHD) : this.SenderItems?.FindAll(x => x.Name.ToLower().Contains(FilterText.ToLower())); });
            this.UpdateHDOnly = new RelayParameterizedCommand((parameter) => { this.IsHDOnly = (bool)parameter; this.FilteredItems = this.IsHDOnly ? this.FilteredItems?.FindAll(x => x.IsHD) : this.SenderItems?.FindAll(x => x.Name.ToLower().Contains(FilterText.ToLower())); });

            if (VLCMagentaPlayer.Instance != null)
            {
                VLCMagentaPlayer.Instance.PlaybackChanged += (s, e) =>
                {
                    this.IsPaused = (s as VLCMagentaPlayer).IsPaused();
                };
            }
        }

        #endregion

        #region Public Commands

        public ICommand ExpandToggle { get; set; }
        public ICommand Pause { get; set; }
        public ICommand SetVolume { get; set; }
        public ICommand UpdateFilter { get; set; }
        public ICommand UpdateHDOnly { get; set; }
        public ICommand Fullscreen { get; set; }
        public ICommand ToggleMute { get; set; }

        #endregion

        private async void LoadSenderList()
        {
            await RunCommandAsync(() => IsLoadingSenderListe, async () =>
            {
                var pathToJson = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "JSON", "channelList.json");
                this.SenderList = await SenderJSON.Deserialize<List<Sender>>(pathToJson);
                this.SenderItems = new List<SenderListItemViewModel>();
                this.FilteredItems = new List<SenderListItemViewModel>();
                this.SenderList.ForEach(sender => this.SenderItems.Add(new SenderListItemViewModel() { Name = sender.Name, Address = sender.Address, IsHD = sender.IsHD, LogoName = sender.LogoName }));
                //this.SenderList.ForEach(sender => this.FilteredItems.Add(new SenderListItemViewModel() { Name = sender.Name, Address = sender.Address, IsHD = sender.IsHD, LogoName = sender.LogoName }));
                foreach (var item in this.SenderItems)
                {
                    item.SelectionChanged += Sender_SelectionChanged;
                    this.FilteredItems.Add(item);
                }
            });



        }

        public void ToggleMenu()
        {
            OverviewMenuExpanded = !OverviewMenuExpanded;

            if (OverviewMenuExpanded)
            {
                OverviewMenuOpacity = 1;
            }
            else
            {
                OverviewMenuOpacity = 0;
            }
        }



        public void SetFullscreen()
        {
            if (!IsFullscreen && VLCMagentaPlayer.Instance.HasMedia())
            {
                if (OverviewMenuExpanded)
                {
                    ToggleMenu();
                }
                SidePanelWidth = 0;
                Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                previousState = Application.Current.MainWindow.WindowState;
                Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Normal;
                Application.Current.MainWindow.WindowState = WindowState.Maximized;

                //WindowMaximizer.FixMaximizedWindowSize(Application.Current.MainWindow);
                IsFullscreen = true;
            }
            else if (IsFullscreen)
            {
                SidePanelWidth = 48;
                Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                Application.Current.MainWindow.WindowState = previousState;
                IsFullscreen = false;
            }
        }

        private void Sender_SelectionChanged(object sender, EventArgs e)
        {
            ToggleMenu();
            this.HasMedia = true;
            ChangeChannel(new Media(VLCMagentaPlayer.Instance.LibVLC, (sender as SenderListItemViewModel).Address, FromType.FromLocation, VLCMagentaPlayer.SDP));
        }

        public async void ChangeChannel(Media media)
        {
            await RunCommandAsync(() => IsChangingChannel, async () =>
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    VLCMagentaPlayer.Instance.PlayMedia(media);
                }));

                await Task.Delay(650);
            });

            this.MediaName = VLCMagentaPlayer.Instance.GetMedia().Meta(MetadataType.ShowName);
        }

    }
}
