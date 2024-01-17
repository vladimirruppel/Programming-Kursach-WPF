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

        public RelayCommand PageLoadedCommand => new RelayCommand(execute => OnPageLoaded());

        public MainPageViewModel()
        {
            tracks = new ObservableCollection<TrackViewModel>();
        }

        private void OnPageLoaded()
        {
            db.Database.EnsureCreated();
            db.Tracks.Load();

            foreach (Track track in db.Tracks)
            {
                tracks.Add(new TrackViewModel(track));
            }
        }
    }
}
