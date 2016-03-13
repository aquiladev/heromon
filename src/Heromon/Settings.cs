namespace Heromon
{
	public class Setting
	{
		public Provider[] Providers { get; set; }
		public Metrik[] Metriks { get; set; }
	}

	public class Provider
	{
		public string Name { get; set; }
		public ProviderSetting Setting { get; set; }
	}

	public class ProviderSetting
	{
		public string TenantId { get; set; }
		public string SubscriptionId { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string ResourceGroup { get; set; }
	}

	public class Metrik
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Provider { get; set; }
		public string Group { get; set; }
		public int Priority { get; set; }
		public string Website { get; set; }
	}
}
