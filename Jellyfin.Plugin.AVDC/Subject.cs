using System;
using System.Collections.Generic;

namespace Jellyfin.Plugin.AVDC
{
    public static class ApiPath
    {
        public const string Actress = "/actress/";
        public const string Metadata = "/metadata/";
        public const string ActressImage = "/image/actress/";
        public const string PrimaryImage = "/image/primary/";
        public const string BackdropImage = "/image/backdrop/";
    }

    public class Actress
    {
        public string Name { get; set; }
        public List<string> Images { get; set; }
    }

    public class Metadata
    {
        public string Vid { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Label { get; set; }
        public string Series { get; set; }
        public string Studio { get; set; }
        public string Director { get; set; }
        public DateTime Release { get; set; }
        public List<string> Actresses { get; set; }
        public List<string> Genres { get; set; }
    }
}