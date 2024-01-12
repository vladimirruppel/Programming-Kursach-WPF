using System;

namespace prog3_kursach.Model
{
    public class Album : MediaCollectionBase
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsAdded { get; set; } 
    }
}
