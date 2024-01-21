using System.Collections.Generic;

namespace prog3_kursach.Model
{
    public class MediaCollectionBase
    {
        public string Name { get; set; } = string.Empty;
        public List<Track> Tracks { get; set; } = new List<Track>();
    }
}
