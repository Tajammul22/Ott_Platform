namespace PROJECT.Models
{
        public class SCardModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImgSrc { get; set; }
            public List<Episode> Episodes { get; set; }
            public string Director { get; set; }
            public string Genre { get; set; }
            public string Cast { get; set; }
            public string? Desc { get; set; }
        }

        public class Episode
        {

            public int Id { get; set; }
            public string EpisodeName { get; set; }
            public string VideoSource { get; set; }
        }

    public class MyViewModel
    {
        public SCardModel Series { get; set; }
        public Episode Episode { get; set; }
    }
}
