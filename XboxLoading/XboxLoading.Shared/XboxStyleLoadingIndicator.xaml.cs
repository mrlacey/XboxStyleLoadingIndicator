using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace XboxLoading
{
    public sealed partial class XboxStyleLoadingIndicator : UserControl
    {
        private bool play = false;
        private bool running = false;

        public XboxStyleLoadingIndicator()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public new static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(XboxStyleLoadingIndicator), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.White)));


        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(XboxStyleLoadingIndicator), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Green)));

        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public new static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(XboxStyleLoadingIndicator), new PropertyMetadata(false, OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XboxStyleLoadingIndicator indicator = d as XboxStyleLoadingIndicator;
            if (indicator != null)
            {
                indicator.IsEnabledChanged();
            }
        }

        private new void IsEnabledChanged()
        {
            if (this.IsEnabled)
            {
                this.Start();
            }
            else
            {
                this.Stop();
            }
        }

        public void Start()
        {
            play = true;
            StartAnimation();
        }

        public void Stop()
        {
            // Don't stop immediately, just let the current storyboard finish
            play = false;
        }

        private void StartAnimation()
        {
            if (!running)
            {
                running = true;
                this.ShowActivity.Begin();
            }
        }

        private void StoryboardCompleted(object sender, object e)
        {
            running = false;

            if (play)
            {
                this.StartAnimation();
            }
        }
    }
}
