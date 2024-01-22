using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Linq;

namespace prog3_kursach.ViewModel
{
    public class AlbumViewModel : ViewModelBase
    {
        private ApplicationContext db = new ApplicationContext();

        private Album album;
        public Album Album
        {
            get { return album; }
            set
            { 
                album = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AlbumName));
                OnPropertyChanged(nameof(ArtistName));
                OnPropertyChanged(nameof(ReleaseYear));
                OnPropertyChanged(nameof(IsAdded));
                OnPropertyChanged(nameof(Tracks));
            }
        }

        public string AlbumName => Album.Name;
        public string ArtistName => Album.ArtistName;
        public int ReleaseYear => Album.ReleaseYear;
        public bool IsAdded => Album.IsAdded;
        public List<TrackViewModel> Tracks => GetTracksOfAlbumFromDB(Album);

        public RelayCommand ToggleAlbumCommand { get; }

        public AlbumViewModel(Album album)
        {
            Album = album;

            db.Database.EnsureCreated();
            db.Albums.Load();
            db.Tracks.Load();
            db.AlbumsTracks.Load();

            ToggleAlbumCommand = new RelayCommand(execute => ToggleAlbum());
        }

        private List<TrackViewModel> GetTracksOfAlbumFromDB(Album album) 
        {
            List<TrackViewModel> tracks = new List<TrackViewModel>();

            List<AlbumTrack> albumTracks = db.AlbumsTracks
                .Where(el => el.AlbumId == album.Id).ToList();

            foreach (AlbumTrack albumTrack in albumTracks)
            {
                Track track = db.Tracks.Find(albumTrack.TrackId);
                tracks.Add(new TrackViewModel(track));
            }

            return tracks;
        }

        private void ToggleAlbum()
        {
            Album album = db.Albums.Find(Album.Id);
            album.IsAdded = !album.IsAdded;
            db.SaveChanges();

            Album = album;
        }
    }
}
