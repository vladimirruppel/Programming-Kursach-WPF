using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    class MyTracksPageViewModel : TracksContainerViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private readonly new ObservableCollection<TrackViewModel> tracks;
        public IEnumerable<TrackViewModel> Tracks => tracks;

        public MyTracksPageViewModel()
        {
            tracks = new ObservableCollection<TrackViewModel>();

            LoadTrackFromDB();
        }

        private void LoadTrackFromDB()
        {
            db.Database.EnsureCreated();
            db.Tracks.Load();

            foreach (Track track in db.Tracks)
            {
                if (track.IsAdded) 
                    tracks.Add(new TrackViewModel(track, this));
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
