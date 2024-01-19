using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System;

namespace prog3_kursach.ViewModel
{
    public class AudioPlayerViewModel : ViewModelBase
    {
        public static AudioPlayerViewModel Instance { get; }
            = new AudioPlayerViewModel();

        private AudioPlayerViewModel() {
            ToggleVolumeSliderCommand = new RelayCommand(execute => ToggleVolumeSlider());
            ToggleIsPlayingCommand = new RelayCommand(execute => ToggleIsPlaying());
        }

        private string trackName = "Ничего не играет";
        public string TrackName
        {
            get { return trackName; }
            set
            {
                trackName = value;
                OnPropertyChanged();
            }
        }

        private string artistName = "Выберите трек";
        public string ArtistName
        {
            get { return artistName; }
            set
            {
                artistName = value;
                OnPropertyChanged();
            }
        }

        private bool isAdded;
        public bool IsAdded
        {
            get { return isAdded; }
            set 
            { 
                isAdded = value;
                OnPropertyChanged();
            }
        }

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

        private string playerSourcePath = "C:\\\\root\\Скриптонит - 100 поцелуев.mp3";

        public string PlayerSourcePath
        {
            get { return playerSourcePath; }
            set 
            { 
                playerSourcePath = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan playerTime;
        public TimeSpan PlayerTime
        {
            get { return playerTime; }
            set 
            { 
                playerTime = value; 
                OnPropertyChanged();
            }
        }

        public RelayCommand ToggleVolumeSliderCommand { get; }
        public RelayCommand ToggleIsPlayingCommand {  get; }

        public void Play(Track track)
        {
            TrackName = track.TrackName;
            ArtistName = track.ArtistName;
            IsAdded = track.IsAdded;

        }

        private void ToggleVolumeSlider()
        {
            ToShowVolumeSlider = !ToShowVolumeSlider;
        }

        private void ToggleIsPlaying()
        {
            IsPlaying = !IsPlaying;
        }
    }
}
