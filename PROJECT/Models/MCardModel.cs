﻿namespace PROJECT.Models
{
    public class MCardModel
    {
        public string Id { get; set; }
        public string ImgSrc { get; set; }
        public string Name { get; set; }
        public string DateOfRelease { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Cast { get; set; }
        public string Rating { get; set; }
        public string? VideoLink { get; set; }
        public string? Desc { get; set; }
    }
}