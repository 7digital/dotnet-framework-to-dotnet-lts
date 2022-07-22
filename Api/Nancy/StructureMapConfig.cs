using StructureMap;
using StructureMap.Graph;

namespace Api.Nancy
{
	public static class StructureMapConfig
	{
		public static void Configure(IContainer container)
		{
			container.Configure(x =>
			{
				x.Scan(s =>
				{
					s.TheCallingAssembly();
					s.WithDefaultConventions();
				});
			});
		}
	}
}