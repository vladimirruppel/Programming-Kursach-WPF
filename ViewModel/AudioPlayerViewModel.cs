using prog3_kursach.Model;
using prog3_kursach.MVVM;
using prog3_kursach.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    public class AudioPlayerViewModel : ViewModelBase
    {
        public static AudioPlayerViewModel Instance { get; }
            = new AudioPlayerViewModel();

        public AudioPlayer AudioPlayerView;

        private ApplicationContext db = new ApplicationContext();
        private ObservableCollection<Track> Tracklist = new ObservableCollection<Track>();
        private int trackIndex;
        private int TrackIndex
        {
            get { return trackIndex; }
            set
            {
                trackIndex = value;
                Play(Tracklist[trackIndex]);
            }
        }

        private AudioPlayerViewModel() {
            ToggleVolumeSliderCommand = new RelayCommand(execute => ToggleVolumeSlider());
            ToggleIsPlayingCommand = new RelayCommand(execute => ToggleIsPlaying());
            ToggleTrackCommand = new RelayCommand(execute => ToggleTrack());
            NextTrackCommand = new RelayCommand(execute => PlayNextTrack());
            PreviousTrackCommand = new RelayCommand(execute => OnBackwardsButtonClick());
        }

        private Track currentTrack = new Track
        {
            TrackName = "Ничего не играет",
            ArtistName = "Выберите трек",
            IsAdded = false,
            Path = "C:\\\\root\\Скриптонит - 100 поцелуев.mp3"
        };
        public Track CurrentTrack
        {
            get { return currentTrack; }
            set
            {
                currentTrack = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TrackName));
                OnPropertyChanged(nameof(ArtistName));
                OnPropertyChanged(nameof(IsAdded));
                OnPropertyChanged(nameof(PlayerSourcePath));
            }
        }

        public string TrackName => CurrentTrack.TrackName;
        public string ArtistName => CurrentTrack.ArtistName;
        public bool IsAdded => CurrentTrack.IsAdded;

        private int duration;
        public int Duration
        {
            get { return duration; }
            set 
            { 
                duration = value;
                OnPropertyChanged();
            }
        }


        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged();
            }
        }

        private double volume = 0.5;
        public double Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(VolumeOutput));
            }
        }

        public int VolumeOutput
        {
            get { return (int)(Volume * 100); }
        }

        private bool toShowVolumeSlider = false;
        public bool ToShowVolumeSlider
        {
            get { return toShowVolumeSlider; }
            set 
            {
                toShowVolumeSlider = value; 
                OnPropertyChanged();
            }
        }

        public string PlayerSourcePath => CurrentTrack.Path;

        private TimeSpan playerTime;
        public TimeSpan PlayerTime
        {
            get { return playerTime; }
            set 
            { 
                playerTime = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayerTimeSeconds));
            }
        }

        public int PlayerTimeSeconds => (int)PlayerTime.TotalSeconds;

        public RelayCommand ToggleVolumeSliderCommand { get; }
        public RelayCommand ToggleIsPlayingCommand {  get; }
        public RelayCommand ToggleTrackCommand { get; }
        public RelayCommand NextTrackCommand { get; }
        public RelayCommand PreviousTrackCommand { get; }

        public void PlayNewTrackList(List<Track> tracks, int trackIndex)
        {
            Tracklist = new ObservableCollection<Track>(tracks);
            TrackIndex = trackIndex;
        }

        public void Play(Track track)
        {
            CurrentTrack = track;

            AudioPlayerView.mediaElement.Source = new Uri(track.Path);
            AudioPlayerView.mediaElement_SourceUpdated();
            IsPlaying = true;
        }

        public void PlayNextTrack()
        {
            if (TrackIndex + 1 == Tracklist.Count) // если текущий трек - последний в треклисте
                TrackIndex = 0; // играем самый первый
            else 
                TrackIndex++; // иначе играем следующий трек
        }

        public void PlayPreviousTrack()
        {
            if (TrackIndex != 0) // если трек не самый первый в треклисте
                TrackIndex--; // играем предыдущий
            else
                Play(Tracklist[TrackIndex]); // иначе играем тот же самый первый трек сначала
        }

        private void OnBackwardsButtonClick()
        {
            if (PlayerTimeSeconds > 3) // если текущий трек играет уже больше 3 секунд
                Play(CurrentTrack); // играть этот же трек сначала
            else
                PlayPreviousTrack(); // иначе играем следующий трек
        }

        private void ToggleVolumeSlider()
        {
            ToShowVolumeSlider = !ToShowVolumeSlider;
        }

        private void ToggleIsPlaying()
        {
            IsPlaying = !IsPlaying;
        }

        private void ToggleTrack()
        {
            db.Database.EnsureCreatedAsync().Wait();
            db.Database.EnsureCreated();
            Track track = db.Tracks.Find(currentTrack.Id);
            if (track != null)
            {
                track.IsAdded = !track.IsAdded;
                db.SaveChanges();

                CurrentTrack = track;
            }
        }
    }
}
