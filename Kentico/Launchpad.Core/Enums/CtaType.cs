using Launchpad.Core.Attributes;

namespace Launchpad.Core.Enums
{
	public enum CtaType
	{
		[CodeDisplayNameType(nameof(Default), "Default")] Default = 0,
		[CodeDisplayNameType(nameof(External), "External")] External = 1,
		[CodeDisplayNameType(nameof(Download), "Download")] Download = 2,		
	}
}
