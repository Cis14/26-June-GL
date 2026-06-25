namespace Terrasoft.Configuration.Translation
{
	using System;
	using Terrasoft.Core.Translation;

	#region Interface: ITranslationLogger

	public interface ITranslationLogger
	{

		#region Methods: Public

		void Debug(object message);
		void Error(Exception e);
		void Error(IActionResult result);
		void Info(object message);

		#endregion

	}

	#endregion

}

