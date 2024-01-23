using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Linq;

namespace prog3_kursach.ViewModel
{
    public class PlaylistViewModel : TracksContainerViewModelBase
    {
        private ApplicationContext db = new ApplicationContext();

        private Playlist playlist;
        public Playlist Playlist
        {
            get { return playlist; }
            set
            {
                playlist = value;
                OnPropertyChanged();
                tracks = Tracks;
            }
        }

        public string PlaylistName => Playlist.Name;
        public bool IsAdded => Playlist.IsAdded;

        public List<TrackViewModel> Tracks => GetTracksOfPlaylistFromDB(Playlist);

        public RelayCommand TogglePlaylistCommand { get; }

        public PlaylistViewModel(Playlist playlist)
        {
            Playlist = playlist;

            db.Database.EnsureCreated();
            db.Playlists.Load();
            db.Tracks.Load();
            db.PlaylistsTracks.Load();

            TogglePlaylistCommand = new RelayCommand(execute => TogglePlaylist());
        }

        private List<TrackViewModel> GetTracksOfPlaylistFromDB(Playlist playlist)
        {
            List<TrackViewModel> tracks = new List<TrackViewModel>();

            List<PlaylistTrack> playlistTracks = db.PlaylistsTracks
                .Where(el => el.PlaylistId == playlist.Id).ToList();

            foreach (PlaylistTrack playlistTrack in playlistTracks)
            {
                Track track = db.Tracks.Find(playlistTrack.TrackId);
                tracks.Add(new TrackViewModel(track, this));
            }

            return tracks;
        }

        private void TogglePlaylist()
        {
            Playlist playlist = db.Playlists.Find(Playlist.Id);
            playlist.IsAdded = !playlist.IsAdded;
            db.SaveChanges();

            Playlist = playlist;
        }
    }
}
