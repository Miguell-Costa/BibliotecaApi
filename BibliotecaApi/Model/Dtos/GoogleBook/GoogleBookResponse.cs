namespace BibliotecaApi.Model.Dtos.GoogleBook
{
	public class GoogleBookResponse
	{
		public int TotalItems { get; set; }

		public List<GoogleBookItem>? Items { get; set; }
	}

	public class GoogleBookItem
	{
		public GoogleVolumeInfo VolumeInfo { get; set; } = null!;
	}

	public class GoogleVolumeInfo
	{
		public string? Title { get; set; }

		public List<string>? Authors { get; set; }

		public string? Description { get; set; }

		public List<string>? Categories { get; set; }

		public int? PageCount { get; set; }

		public string? PublishedDate { get; set; }

		public GoogleImageLinks? ImageLinks { get; set; }
	}

	public class GoogleImageLinks
	{
		public string? SmallThumbnail { get; set; }

		public string? Thumbnail { get; set; }
	}
}
