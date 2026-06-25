namespace Terrasoft.Configuration.FileImport
{
	using System;

	public interface IFileImportColumnProcessError
	{ 

		event EventHandler<ColumnProcessErrorEventArgs> ProcessError;

	}
}
