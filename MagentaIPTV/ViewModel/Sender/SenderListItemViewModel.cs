
using LibVLCSharp.Shared;
using System;
using System.Windows;
using System.Windows.Input;

namespace MagentaIPTV
{
    public class SenderListItemViewModel
    {
        public event EventHandler SelectionChanged;

        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsHD { get; set; }
        public string LogoName { get; set; }

        /// <summary>
        /// A Channel was picked
        /// </summary>
        public ICommand OpenSender { get; set; }

        public SenderListItemViewModel()
        {
            OpenSender = new RelayCommand(SwitchChannel);
        }

        public void SwitchChannel()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
