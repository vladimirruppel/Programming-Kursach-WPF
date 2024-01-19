using prog3_kursach.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace prog3_kursach.View
{
    public partial class AudioPlayer : UserControl
    {
        public AudioPlayer()
        {
            InitializeComponent();

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
            }
        }
    }
}
