using System;
using System.ComponentModel.DataAnnotations;


namespace PacManWeb.Models
{
    public class HighScoreViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
