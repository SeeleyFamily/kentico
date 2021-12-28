using System;


namespace Launchpad.Core.Enums
{

	[Flags]
	public enum IndexDirective
	{
		Undefined = 0,
		NoIndex = 1,
		Index = 2,
		Follow = 4,
		NoFollow = 8,
		NoImageIndex = 16,

		None = NoIndex | NoFollow,

		NoArchive = 32,
		NoCache = 64,
		NoSnippet = 128
	}

}
