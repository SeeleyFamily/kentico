using Launchpad.Core.Attributes;

namespace Launchpad.Core.Enums
{
	public enum Sequence
	{
		[CodeDisplayNameType(nameof(Primary), "Primary")] Primary = 1,
		[CodeDisplayNameType(nameof(Secondary), "Secondary")] Secondary = 2,
		[CodeDisplayNameType(nameof(Tertiary), "Tertiary")] Tertiary = 3,
		[CodeDisplayNameType(nameof(Quaternary), "Quaternary")] Quaternary = 4,
		[CodeDisplayNameType(nameof(Quinary), "Quinary")] Quinary = 5,
		[CodeDisplayNameType(nameof(Senary), "Senary")] Senary = 6,
		[CodeDisplayNameType(nameof(Septenary), "Septenary")] Septenary = 7,
		[CodeDisplayNameType(nameof(Octonary), "Octonary")] Octonary = 8,
		[CodeDisplayNameType(nameof(Nonary), "Nonary")] Nonary = 9,
		[CodeDisplayNameType(nameof(Denary), "Denary")] Denary = 10		
	}
}
