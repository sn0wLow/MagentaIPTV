using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace MagentaIPTV
{
    /// <summary>
    /// A Base Page / UserControl for base functionality
    /// </summary>
    public class BasePage : UserControl
    {
        #region Private Properties

        private object viewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation the play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.FadeIn;

        /// <summary>
        /// The animation the play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.FadeOut;

        /// <summary>
        /// The time any animation takes to complete
        /// </summary>
        public float AnimationSeconds { get; set; } = 0.5f;

        /// <summary>
        /// The associated ViewModel
        /// </summary>
        public object ViewModelObject
        {
            get => viewModel;
            set
            {
                if (viewModel == value) return;

                viewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                this.DataContext = viewModel;
            }
        }


        #endregion

        #region Default Constructor
        public BasePage()
        {
            // Don't animate in DesignTime
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            // Initially hide / collapse when we are animating in
            if (this.PageLoadAnimation != PageAnimation.None)
            {
                this.Visibility = System.Windows.Visibility.Collapsed;
            }

            // Listen to Page/UserControl loading
            this.Loaded += BasePage_LoadedAsync;

        }



        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            // Animate the Page in
            await AnimateInAsync();
        }

        /// <summary>
        /// Animates the Page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            switch (this.PageLoadAnimation)
            {
                // Return if PageAnimation is None
                case PageAnimation.None:
                    return;
                case PageAnimation.FadeIn:
                    await this.FadeInAsync(false, this.AnimationSeconds);
                    break;
                case PageAnimation.FadeOut:
                    await this.FadeOutAsync(this.AnimationSeconds);
                    break;
                case PageAnimation.SlideInFromRight:
                    await this.SlideInAsync(AnimationSlideInDirection.Right, false, AnimationSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimation.SlideInFromLeft:
                    await this.SlideInAsync(AnimationSlideInDirection.Left, false, AnimationSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimation.SlideOutToRight:
                    await this.SlideOutAsync(AnimationSlideInDirection.Right, AnimationSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                case PageAnimation.SlideOutToLeft:
                    await this.SlideOutAsync(AnimationSlideInDirection.Left, AnimationSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
                default:
                    Debugger.Break();
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Fired when the view model changes
        /// </summary>
        protected virtual void OnViewModelChanged()
        {

        }
    }

    /// <summary>
    /// A base page with added ViewModel support
    /// </summary>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        #region Public Properties

        /// <summary>
        /// The view model associated with this page
        /// </summary>
        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage() : base()
        {
            // If in design time mode...
            if (DesignerProperties.GetIsInDesignMode(this))
                // Just use a new instance of the VM
                ViewModel = new VM();
            else
                // Create a default view model
                ViewModel = ViewModel ?? new VM();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use, if any</param>
        public BasePage(VM specificViewModel = null) : base()
        {
            // Set specific view model
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
            {
                // If in design time mode...
                if (DesignerProperties.GetIsInDesignMode(this))
                    // Just use a new instance of the VM
                    ViewModel = new VM();
                else
                {
                    // Create a default view model
                    ViewModel = ViewModel ?? new VM();
                }
            }
        }

        #endregion
    }
}
