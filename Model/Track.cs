using prog3_kursach.MVVM;
using System;

namespace prog3_kursach.Model
{
    public class Track : ViewModelBase
    {
        public int Id { get; set; }

        private string artistName = "";
        public string ArtistName
        {
            get { return artistName; }
            set 
            { 
                artistName = value;
                OnPropertyChanged();
            }
        }

        private string trackName = "";
        public string TrackName
        {
            get { return trackName; }
            set 
            { 
                trackName = value; 
                OnPropertyChanged();
            }
        }

        private int releaseYear;
        public int ReleaseYear
        {
            get { return releaseYear; }
            set 
            { 
                releaseYear = value; 
                OnPropertyChanged();
            }
        }

        private string path = "";
        public string Path
        {
            get { return path = ""; }
            set 
            { 
                path = value; 
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

        public DateTime DateAdded { get; set; }
        public int AlbumId { get; set; }

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

        public Track Clone()
        {
            var clone = new Track();

            clone.ArtistName = artistName;
            clone.TrackName = trackName;
            clone.ReleaseYear = releaseYear;
            clone.Duration = duration;
            clone.DateAdded = DateAdded;
            clone.IsAdded = isAdded;

            return clone;
        }
    }
}
