﻿namespace MusicReviewAppAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        //NP
        public Reviewer Reviewer { get; set; }
        public Album Album { get; set; }
    }
}
