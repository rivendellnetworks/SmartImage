using JetBrains.Annotations;

namespace SmartImage.Utilities.Imgur
{
	[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
	internal sealed class ImgurResponse<T>
	{
		public T Data { get; set; }

		public bool Success { get; set; }

		public int Status { get; set; }
	}
}