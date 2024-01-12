using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    class MainPageViewModel : ViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private ObservableCollection<Track> tracks = new ObservableCollection<Track>();
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set 
            { 
                tracks = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PageLoadedCommand => new RelayCommand(execute => OnPageLoaded());
        public RelayCommand ToggleTrackCommand => new RelayCommand(trackId => ToggleTrack(trackId));

        private void OnPageLoaded()
        {
            db.Database.EnsureCreated();
            db.Tracks.Load();
            Tracks = db.Tracks.Local.ToObservableCollection();
        }

        private void ToggleTrack(object trackId)
        {
            if (trackId == null) return;

            Track track = db.Tracks.Find((int)trackId);
            if (track != null)
            {
                track.IsAdded = !track.IsAdded;
                db.SaveChanges();
            }
        }
    }
}
