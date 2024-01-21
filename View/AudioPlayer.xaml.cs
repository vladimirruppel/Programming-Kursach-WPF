using prog3_kursach.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace prog3_kursach.View
{
    public partial class AudioPlayer : UserControl
    {
        private bool isSliderClicked = false;

        public AudioPlayer()
        {
            InitializeComponent();

            AudioPlayerViewModel.Instance.AudioPlayerView = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaElement.LoadedBehavior == MediaState.Play)
            {
                AudioPlayerViewModel.Instance.PlayerTime = mediaElement.Position;
                if (!isSliderClicked)
                    playerSlider.Value = (int)mediaElement.Position.TotalSeconds;
            }
        }

        public void mediaElement_SourceUpdated()
        {
            while (!mediaElement.NaturalDuration.HasTimeSpan);
            int duration = (int)mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            playerSlider.Maximum = duration;
            playerSlider.Value = 0;
            AudioPlayerViewModel.Instance.Duration = duration;
        }

        private void playerSlider_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isSliderClicked = true;
        }

        private void playerSlider_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(playerSlider.Value);
            playerSlider.Value = (int)mediaElement.Position.TotalSeconds;
            isSliderClicked = false;
        }
    }
}
