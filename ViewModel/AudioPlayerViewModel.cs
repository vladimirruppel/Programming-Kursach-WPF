using prog3_kursach.MVVM;

namespace prog3_kursach.ViewModel
{
    public class AudioPlayerViewModel : ViewModelBase
    {
        public static AudioPlayerViewModel Instance { get; } 
            = new AudioPlayerViewModel();

        private AudioPlayerViewModel() {
        
        }

        private string trackName = "null";
        public string TrackName
        {
            get { return trackName; }
            set 
            { 
                trackName = value;
                OnPropertyChanged();
            }
        }

        public void Play(string someString)
        {
            TrackName = someString as string;
        }
    }
}
