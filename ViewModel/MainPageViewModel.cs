using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    class MainPageViewModel : TracksContainerViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private readonly new ObservableCollection<TrackViewModel> tracks;
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
                tracks.Add(new TrackViewModel(track, this));
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

        public override int GetIndexOfTrackInCollection(Track track)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                if (tracks[i].Track.Id == track.Id)
                    return i;
            }
            return -1;
        }

        public override List<Track> GetListOfTracks()
        {
            List<Track> list = new List<Track>();

            foreach (TrackViewModel trackViewModel in tracks)
            {
                Track track = trackViewModel.Track;
                list.Add(track);
            }

            return list;
        }
    }
}
