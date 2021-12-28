using Launchpad.Core.Attributes;


namespace Launchpad.Core.Enums
{
	public enum BackgroundType
	{
		[CodeDisplayNameType(nameof(Primary), "Primary")] Primary = 1,
		[CodeDisplayNameType(nameof(Secondary), "Secondary")] Secondary = 2,
		[CodeDisplayNameType(nameof(Tertiary), "Tertiary")] Tertiary = 3,
		[CodeDisplayNameType(nameof(Quaternary), "Quaternary")] Quaternary = 4,
		[CodeDisplayNameType(nameof(Framed), "Framed")] Framed = 5,
	}
}
