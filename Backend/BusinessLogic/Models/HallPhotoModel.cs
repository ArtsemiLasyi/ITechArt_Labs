﻿using System.IO;

namespace BusinessLogic.Models
{
    public class HallPhotoModel
    {
        public int HallId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public Stream FileStream { get; set; } = null!;
    }
}
