using System;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Json;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using StructureMap;

namespace Api.Nancy
{
	public class Bootstrapper : StructureMapNancyBootstrapper
	{
		protected override NancyInternalConfiguration InternalConfiguration
		{
			get { return new NancyInternalConfigurationFactory().Build(); }
		}

		protected override IContainer GetApplicationContainer()
		{
			var container = new Container();
			StructureMapConfig.Configure(container);
			return container;
		}
	}

	public class NancyInternalConfigurationFactory
	{
		public NancyInternalConfiguration Build()
		{
			JsonSettings.MaxJsonLength = Int32.MaxValue;

			return NancyInternalConfiguration.WithOverrides(c =>
			{
				c.Serializers.Clear();
				c.Serializers.Add(typeof(DefaultJsonSerializer));
				c.Serializers.Add(typeof(DefaultXmlSerializer));

				c.ResponseProcessors.Clear();
				c.ResponseProcessors.Add(typeof(JsonProcessor));
				c.ResponseProcessors.Add(typeof(XmlProcessor));
			});
		}
	}
}