#region

using System;
using System.Collections.Generic;
using System.Drawing;
using HtmlAgilityPack;
using JetBrains.Annotations;
using SimpleCore.Utilities;
using SmartImage.Searching.Model;
using SmartImage.Utilities;

#endregion
#nullable enable
namespace SmartImage.Searching.Engines.Other
{
	public sealed class IqdbClient : BasicSearchEngine
	{
		public IqdbClient() : base("https://iqdb.org/?url=") { }

		public override string Name => "IQDB";
		public override Color Color => Color.Pink;

		public override SearchEngineOptions Engine => SearchEngineOptions.Iqdb;

		private struct IqdbResult : ISearchResult
		{
			public string? Caption { get; set; }

			public string Source { get; }

			public int? Width { get; set; }

			public int? Height { get; set; }

			public string? Url { get; set; }

			public float? Similarity { get; set; }

			public IqdbResult(string caption, string source, string url, int width, int height, float? similarity)
			{
				Caption = caption;
				Url = url;
				Source = source;
				Width = width;
				Height = height;
				Similarity = similarity;
			}

			public override string ToString()
			{
				return
					$"{nameof(Caption)}: {Caption}, {nameof(Source)}: {Source}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}, {nameof(Url)}: {Url}, {nameof(Similarity)}: {Similarity}";
			}
		}

		private static IqdbResult ParseResult(HtmlNodeCollection tr)
		{
			var caption = tr[0];
			var img = tr[1];
			var src = tr[2];

			string url = null;

			var urlNode = img.FirstChild.FirstChild;

			if (urlNode.Name != "img") {
				url = "http:" + urlNode.Attributes["href"].Value;
			}

			int w = 0, h = 0;

			if (tr.Count >= 4) {
				var res = tr[3];
				var wh = res.InnerText.Split("�");

				var wstr = wh[0].SelectOnlyDigits();
				w = int.Parse(wstr);

				// May have NSFW caption, so remove it

				var hstr = wh[1].SelectOnlyDigits();
				h = int.Parse(hstr);
			}

			float? sim;

			if (tr.Count >= 5) {
				var simnode = tr[4];
				var simstr = simnode.InnerText.Split('%')[0];
				sim = float.Parse(simstr);
			}
			else {
				sim = null;
			}


			var i = new IqdbResult(caption.InnerText, src.InnerText, url, w, h, sim);

			return i;
		}

		public override SearchResult GetResult(string url)
		{
			var sr = base.GetResult(url);

			try {
				var html = Network.GetSimpleResponse(sr.RawUrl);

				//Network.WriteResponse(html);

				var doc = new HtmlDocument();
				doc.LoadHtml(html.Content);


				//var tables = doc.DocumentNode.SelectNodes("//table");

				// Don't select other results

				var pages = doc.DocumentNode.SelectSingleNode("//div[@id='pages']");
				var tables = pages.SelectNodes("div/table");

				var images = new List<ISearchResult>();

				foreach (var table in tables) {


					var tr = table.SelectNodes("tr");


					var i = ParseResult(tr);

					images.Add(i);
				}

				// First is original image
				images.RemoveAt(0);

				var best = images[0];
				sr.Url = best.Url;
				sr.Similarity = best.Similarity;

				sr.AddExtendedInfo(images.ToArray());


			}
			catch (Exception) {
				// ...

				sr.ExtendedInfo.Add("Error parsing");
			}

			return sr;
		}
	}
}