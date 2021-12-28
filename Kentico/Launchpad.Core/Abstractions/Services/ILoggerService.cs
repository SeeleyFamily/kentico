using System;
using System.Runtime.CompilerServices;


namespace Launchpad.Core.Abstractions.Services
{

	public interface ILoggerService
	{
		void LogError( string message, string eventCode, object data = null, Exception exception = null, [CallerMemberName] string source = "" );
		void LogInformation( string message, string eventCode, object data = null, [CallerMemberName] string source = "" );
		void LogWarning( string message, string eventCode, object data = null, [CallerMemberName] string source = "" );
	}

}
