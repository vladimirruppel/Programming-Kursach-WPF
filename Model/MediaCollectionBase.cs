using System.Collections.ObjectModel;

namespace prog3_kursach.Model
{
    public class MediaCollectionBase
    {
        public string Name { get; set; }
        public ObservableCollection<Track> Tracks { get; set; }
    }
}
