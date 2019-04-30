using System;
using System.IO;

using Jun.IO;

namespace AsyncGetFiles {
	class Program {
		private static void GetFilesHandler(string fileName)
		{
			Console.WriteLine($"Async somthing to do ... {fileName}");
		}

		static void Main(string[] args)
		{
			DirectoryInfo di = new DirectoryInfo("c:\\workspace");
			var _task = di.AsyncGetFiles(GetFilesHandler);
			var _files = _task.Result;

			foreach(var _file in _files) {
				Console.WriteLine($"result: {_file}");
			}

			Console.ReadKey();
		}
	}
}
