using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    class MyTracksPageViewModel : ViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private readonly ObservableCollection<TrackViewModel> tracks;
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
                    tracks.Add(new TrackViewModel(track));
            }
        }
    }
}
