using prog3_kursach.Model;
using prog3_kursach.MVVM;

namespace prog3_kursach.ViewModel
{
    public class TrackViewModel : ViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private Track _track;
        public Track Track
        {
            get { return _track; }
            set
            {
                _track = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TrackName));
                OnPropertyChanged(nameof(ArtistName));
                OnPropertyChanged(nameof(IsAdded));
                OnPropertyChanged(nameof(ReleaseYear));
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string TrackName => Track.TrackName;
        public string ArtistName => Track.ArtistName;
        public bool IsAdded => Track.IsAdded;
        public int ReleaseYear => Track.ReleaseYear;
        public int Duration => Track.Duration;

        public TrackViewModel(Track track)
        {
            Track = track;

            ToggleTrackCommand = new RelayCommand(execute => ToggleTrack());
            PlayCommand = new RelayCommand(execute => Play());
        }

        public RelayCommand ToggleTrackCommand { get; }
        public RelayCommand PlayCommand { get; }

        private void ToggleTrack()
        {
            db.Database.EnsureCreated();
            Track track = db.Tracks.Find(_track.Id);
            if (track != null)
            {
                track.IsAdded = !track.IsAdded;
                db.SaveChanges();

                Track = track;
            }
        }

        private void Play()
        {
            AudioPlayerViewModel.Instance.Play(ArtistName);
        }
    }
}
