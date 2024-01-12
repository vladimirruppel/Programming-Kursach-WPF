using System;

namespace prog3_kursach.Model
{
    public class Playlist : MediaCollectionBase
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsAdded { get; set; } 
    }
}
