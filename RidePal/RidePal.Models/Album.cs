using System;

namespace RidePal.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
