using prog3_kursach.MVVM;

namespace prog3_kursach.ViewModel
{
    public class ChooseTrackViewModel : ViewModelBase
    {
        private TrackViewModel trackViewModel;
        public TrackViewModel TrackViewModel
        {
            get { return trackViewModel; }
            set
            {
                trackViewModel = value;
                OnPropertyChanged();
            }
        }

        private TrackListChooseViewModel parent;

        public RelayCommand AddTrackCommand { get; }
        public RelayCommand RemoveTrackCommand { get; }

        public ChooseTrackViewModel(TrackViewModel trackViewModel, TrackListChooseViewModel parent)
        {
            TrackViewModel = trackViewModel;
            this.parent = parent;

            AddTrackCommand = new RelayCommand(execute => AddTrack());
            RemoveTrackCommand = new RelayCommand(execute => RemoveTrack());
        }

        private void AddTrack()
        {
            parent.AddTrack(this);
        }

        private void RemoveTrack()
        {
            parent.RemoveTrack(this);
        }
    }
}
