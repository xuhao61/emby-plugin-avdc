using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Providers;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.AVDC.Providers
{
    public class MovieProvider : BaseProvider, IRemoteMetadataProvider<Movie, MovieInfo>, IHasOrder
    {
        public MovieProvider(IHttpClientFactory httpClientFactory,
            IJsonSerializer jsonSerializer,
            ILogger<MovieProvider> logger) : base(httpClientFactory, jsonSerializer, logger)
        {
            // Empty
        }

        public int Order => 1;

        public string Name => Plugin.Instance.Name;

        public async Task<MetadataResult<Movie>> GetMetadata(MovieInfo info,
            CancellationToken cancellationToken)
        {
            Logger.LogInformation($"[AVDC] GetMetadata for video: {info.Name}");

            var m = await GetMetadata(info.Name, cancellationToken);
            if (m == null || string.IsNullOrEmpty(m.Vid)) return new MetadataResult<Movie>();

            // Add `中文字幕` Genre
            if (HasChineseSubtitle(info.Path) && !m.Genres.Contains("中文字幕"))
                m.Genres.Add("中文字幕");

            // Create Studios
            var studios = new List<string>();
            if (!string.IsNullOrEmpty(m.Studio)) studios.Add(m.Studio);

            // Use Series or Label as Tagline
            var tagline = !string.IsNullOrEmpty(m.Series) ? m.Series : m.Label;

            var result = new MetadataResult<Movie>
            {
                Item = new Movie
                {
                    Name = $"{m.Vid} {m.Title}",
                    OriginalTitle = m.Title,
                    Overview = m.Overview,
                    Tagline = tagline,
                    Genres = m.Genres.ToArray(),
                    Studios = studios.ToArray(),
                    PremiereDate = m.Release,
                    ProductionYear = m.Release.Year,
                    SortName = m.Vid,
                    ExternalId = m.Vid,
                    OfficialRating = "XXX"
                }
            };

            // Add Director
            if (!string.IsNullOrEmpty(m.Director))
                result.AddPerson(new PersonInfo
                {
                    Name = m.Director,
                    Type = "Director"
                });

            // Add Actresses
            foreach (var name in m.Actresses)
            {
                var actress = await GetActress(name, cancellationToken);

                var url = actress != null && !string.IsNullOrEmpty(actress.Name) && actress.Images.Any()
                    ? $"{Config.AvdcServer}{ApiPath.ActressImage}{actress.Name}"
                    : string.Empty;

                result.AddPerson(new PersonInfo
                {
                    Name = name,
                    Type = "Actor",
                    ImageUrl = url
                });
            }

            result.QueriedById = true;
            result.HasMetadata = true;
            return result;
        }

        public async Task<IEnumerable<RemoteSearchResult>> GetSearchResults(
            MovieInfo info, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"[AVDC] SearchResults for video: {info.Name}");

            var m = await GetMetadata(info.Name, cancellationToken);
            if (m == null || string.IsNullOrEmpty(m.Vid)) return new List<RemoteSearchResult>();

            return new List<RemoteSearchResult>
            {
                new()
                {
                    Name = $"{m.Vid} {m.Title}",
                    ProductionYear = m.Release.Year,
                    ImageUrl = $"{Config.AvdcServer}{ApiPath.PrimaryImage}{m.Vid}"
                }
            };
        }

        private static bool HasChineseSubtitle(string path)
        {
            var filename = Path.GetFileNameWithoutExtension(path);

            // Simply check if filename contains `-C`
            return filename.ToUpper().Replace("CD", "").Contains("-C");
        }
    }
}