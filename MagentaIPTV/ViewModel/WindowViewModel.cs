using System.Windows;
using System.Windows.Controls;

namespace MagentaIPTV
{
    public class WindowViewModel : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// The Window this View Model controls
        /// </summary>
        private readonly Window window;

        private readonly WindowResizer resizer;


        /// <summary>
        /// The window resizer helper that keeps the window size correct in various states
        /// </summary>

        #endregion

        #region Constructors
        /// <summary>
        /// Design time Constructor
        /// </summary>
        public WindowViewModel()
        {
        }

        public WindowViewModel(Window window)
        {
            this.window = window;
            //this.resizer = new WindowResizer(this.window);

            //this.resizer.WindowDockChanged += (s) =>
            //{
            //    if (window.WindowState == WindowState.Maximized)
            //    {
            //        WindowMaximizer.FixMaximizedWindowSize(window);
            //    }
            //};
        }
        #endregion

        #region Public Properties

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Overview;

        /// <summary>
        /// True if the window is currently being moved/dragged
        /// </summary>
        public bool IsMoving { get; set; }

        #endregion



    }
}
