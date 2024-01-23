using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.ViewModel
{
    public class TrackListChooseViewModel : TracksContainerViewModelBase
    {
        ApplicationContext db = new ApplicationContext();

        private ObservableCollection<ChooseTrackViewModel> addedTracks = new ObservableCollection<ChooseTrackViewModel>();
        public IEnumerable<ChooseTrackViewModel> AddedTracks => addedTracks;

        private ObservableCollection<ChooseTrackViewModel> otherTracks = new ObservableCollection<ChooseTrackViewModel>();
        public IEnumerable<ChooseTrackViewModel> OtherTracks => otherTracks;

        public TrackListChooseViewModel()
        {
            db.Database.EnsureCreated();
            db.Tracks.Load();

            tracks = new List<TrackViewModel>();

            foreach (Track track in db.Tracks)
            {
                TrackViewModel trackViewModel = new TrackViewModel(track, this);
                tracks.Add(trackViewModel);

                otherTracks.Add(new ChooseTrackViewModel(trackViewModel, this));
            }
        }

        public void AddTrack(ChooseTrackViewModel track)
        {
            addedTracks.Add(track);
            otherTracks.Remove(track);
        }

        public void RemoveTrack(ChooseTrackViewModel track)
        {
            otherTracks.Add(track);
            addedTracks.Remove(track);
        }
    }
}
