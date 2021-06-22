﻿namespace DataAccess.Options
{
    public class StorageOptions
    {
        public const string Storage = "Storage";

        public string Path { get; set; } = null!;
        public string Films { get; set; } = null!;
        public string Cinemas { get; set; } = null!;
        public string Halls { get; set; } = null!;
    }
}
