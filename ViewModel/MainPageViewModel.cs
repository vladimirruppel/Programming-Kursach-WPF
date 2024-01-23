using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    class MainPageViewModel : ViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private readonly ObservableCollection<TrackViewModel> tracks;
        public IEnumerable<TrackViewModel> Tracks => tracks;

        private readonly ObservableCollection<AlbumViewModel> albums;
        public IEnumerable<AlbumViewModel> Albums => albums;

        private readonly ObservableCollection<PlaylistViewModel> playlists;
        public IEnumerable<PlaylistViewModel> Playlists => playlists;

        public RelayCommand PageLoadedCommand => new RelayCommand(execute => OnPageLoaded());

        public MainPageViewModel()
        {
            tracks = new ObservableCollection<TrackViewModel>();
            albums = new ObservableCollection<AlbumViewModel>();
            playlists = new ObservableCollection<PlaylistViewModel>();
        }

        private void OnPageLoaded()
        {
            db.Database.EnsureCreated();
            db.Tracks.Load();
            db.Albums.Load();
            db.Playlists.Load();

            foreach (Track track in db.Tracks)
            {
                tracks.Add(new TrackViewModel(track));
            }

            foreach (Album album in db.Albums)
            {
                albums.Add(new AlbumViewModel(album));
            }

            foreach (Playlist playlist in db.Playlists)
            {
                playlists.Add(new PlaylistViewModel(playlist));
            }
        }
    }
}
