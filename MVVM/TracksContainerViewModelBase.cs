using prog3_kursach.Model;
using prog3_kursach.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prog3_kursach.MVVM
{
    public class TracksContainerViewModelBase : ViewModelBase
    {
        protected List<TrackViewModel> tracks;

        public virtual int GetIndexOfTrackInCollection(Track track)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                if (tracks[i].Track.Id == track.Id)
                    return i;
            }
            return -1;
        }

        public virtual List<Track> GetListOfTracks()
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
