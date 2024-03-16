using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace MagentaIPTV
{
    public static class FrameworkElementAnimations
    {
        #region Fades

        /// <summary>
        /// Fades an element in
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="durationSeconds">The time the animation will take</param>
        /// <param name="firstLoad">Indicates if this is the first load</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, bool firstLoad, float durationSeconds, bool useFrom = true)
        {
            // Create the storyboard
            Storyboard storyBoard = new Storyboard();

            // Add fade in animation
            storyBoard.AddFadeIn(durationSeconds, useFrom);

            // Start animating
            storyBoard.Begin(element);

            // Make page visible only if we are animating or its the first load
            if (durationSeconds != 0 || firstLoad)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(durationSeconds * 1000));
        }

        /// <summary>
        /// Fades an element out
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="durationSeconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float durationSeconds)
        {
            // Create the storyboard
            Storyboard storyBoard = new Storyboard();

            // Add fade in animation
            storyBoard.AddFadeOut(durationSeconds);

            // Start animating
            storyBoard.Begin(element);

            // Make page visible only if we are animating or its the first load
            if (durationSeconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(durationSeconds * 1000));

            // Fully hide the element
            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Slides

        /// <summary>
        /// Slides an element in
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="direction">The direction of the slide</param>
        /// <param name="durationSeconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <param name="size">The animation width/height to animate to. If not specified the elements size is used</param>
        /// <param name="firstLoad">Indicates if this is the first load</param>
        /// <returns></returns>
        public static async Task SlideInAsync(this FrameworkElement element, AnimationSlideInDirection direction, bool firstLoad, float durationSeconds, bool keepMargin = true, int size = 0)
        {
            // Create the storyboard
            Storyboard storyBoard = new Storyboard();

            // Slide in the correct direction
            switch (direction)
            {
                // Add slide from left animation
                case AnimationSlideInDirection.Left:
                    storyBoard.AddSlideFromLeft(durationSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide from right animation
                case AnimationSlideInDirection.Right:
                    storyBoard.AddSlideFromRight(durationSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide from top animation
                case AnimationSlideInDirection.Top:
                    storyBoard.AddSlideFromTop(durationSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                // Add slide from bottom animation
                case AnimationSlideInDirection.Bottom:
                    storyBoard.AddSlideFromBottom(durationSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
            }

            // Start animating
            storyBoard.Begin(element);

            // Make page visible only if we are animating or its the first load
            if (durationSeconds != 0 || firstLoad)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(durationSeconds * 1000));
        }

        /// <summary>
        /// Slides an element out
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="direction">The direction of the slide (this is for the reverse slide out action, so Left would slide out to left)</param>
        /// <param name="durationSeconds">The time the animation will take</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        /// <param name="size">The animation width/height to animate to. If not specified the elements size is used</param>
        /// <returns></returns>
        public static async Task SlideOutAsync(this FrameworkElement element, AnimationSlideInDirection direction, float durationSeconds, bool keepMargin = true, int size = 0)
        {
            // Create the storyboard
            Storyboard storyBoard = new Storyboard();

            // Slide in the correct direction
            switch (direction)
            {
                // Add slide to left animation
                case AnimationSlideInDirection.Left:
                    storyBoard.AddSlideToLeft(durationSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide to right animation
                case AnimationSlideInDirection.Right:
                    storyBoard.AddSlideToRight(durationSeconds, size == 0 ? element.ActualWidth : size, keepMargin: keepMargin);
                    break;
                // Add slide to top animation
                case AnimationSlideInDirection.Top:
                    storyBoard.AddSlideToTop(durationSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
                // Add slide to bottom animation
                case AnimationSlideInDirection.Bottom:
                    storyBoard.AddSlideToBottom(durationSeconds, size == 0 ? element.ActualHeight : size, keepMargin: keepMargin);
                    break;
            }

            // Start animating
            storyBoard.Begin(element);

            // Make page visible only if we are animating
            if (durationSeconds != 0)
            {
                element.Visibility = Visibility.Visible;
            }

            // Wait for it to finish
            await Task.Delay((int)(durationSeconds * 1000));

            // Make element invisible
                element.Visibility = Visibility.Hidden;

        }

        #endregion


    }
}
