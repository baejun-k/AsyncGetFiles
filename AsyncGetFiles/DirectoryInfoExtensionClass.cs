using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jun.IO {
	public static class DirectoryInfoExtensionClass {
		/// <summary>
		/// 비동기로 파일 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="handler">파일 이름을 얻으면서 할 작업. 파라미터는 파일 이름</param>
		/// <returns>비동기 종료 후 전체 파일 이름 배열</returns>
		public static Task<string[]> AsyncGetFiles(this DirectoryInfo dir, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /a-d/b {dir.FullName}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();
				proc.WaitForExit();

				return result.ToArray();
			});
		}

		/// <summary>
		/// 비동기로 파일 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="pattern">검색 파일 패턴 ("*.*")</param>
		/// <param name="handler">파일 이름을 얻으면서 할 작업. 파라미터는 파일 이름</param>
		/// <returns>비동기 종료 후 전체 파일 이름 배열</returns>
		public static Task<string[]> AsyncGetFiles(this DirectoryInfo dir, string pattern, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /a-d/b {Path.Combine(dir.FullName, pattern)}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();
				proc.WaitForExit();

				return result.ToArray();
			});
		}

		/// <summary>
		/// 취소가능한 비동기로 파일 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="ct">취소할 때 사용할 토큰</param>
		/// <param name="handler">파일 이름을 얻으면서 할 작업. 파라미터는 파일 이름</param>
		/// <returns>비동기 종료 후 전체 파일 이름 배열</returns>
		public static Task<string[]> AsyncGetFiles(this DirectoryInfo dir, CancellationToken ct, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /a-d/b {dir.FullName}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();

				while (!proc.WaitForExit(1)) {
					if (ct.IsCancellationRequested) { break; }
				}
				if (!proc.HasExited) { proc.Close(); }

				return result.ToArray();
			});
		}

		/// <summary>
		/// 취소가능한 비동기로 파일 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="pattern">검색 파일 패턴 ("*.*")</param>
		/// <param name="ct">취소할 때 사용할 토큰</param>
		/// <param name="handler">파일 이름을 얻으면서 할 작업. 파라미터는 파일 이름</param>
		/// <returns>비동기 종료 후 전체 파일 이름 배열</returns>
		public static Task<string[]> AsyncGetFiles(this DirectoryInfo dir, string pattern, CancellationToken ct, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /a-d/b {Path.Combine(dir.FullName, pattern)}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();

				while (!proc.WaitForExit(1)) {
					if (ct.IsCancellationRequested) { break; }
				}
				if (!proc.HasExited) { proc.Close(); }

				return result.ToArray();
			});
		}

		/// <summary>
		/// 비동기로 폴더 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="handler">폴더 이름을 얻으면서 할 작업. 파라미터는 폴더 이름</param>
		/// <returns>비동기 종료 후 전체 폴더 이름 배열</returns>
		public static Task<string[]> AsyncGetDirectories(this DirectoryInfo dir, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /ad/b {dir.FullName}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();
				proc.WaitForExit();

				return result.ToArray();
			});
		}

		/// <summary>
		/// 비동기로 폴더 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="pattern">검색 폴더 패턴 ("abc*")</param>
		/// <param name="handler">폴더 이름을 얻으면서 할 작업. 파라미터는 폴더 이름</param>
		/// <returns>비동기 종료 후 전체 폴더 이름 배열</returns>
		public static Task<string[]> AsyncGetDirectories(this DirectoryInfo dir, string pattern, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /ad/b {Path.Combine(dir.FullName, pattern)}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();
				proc.WaitForExit();

				return result.ToArray();
			});
		}

		/// <summary>
		/// 비동기로 폴더 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="ct">취소할 때 사용할 토큰</param>
		/// <param name="handler">폴더 이름을 얻으면서 할 작업. 파라미터는 폴더 이름</param>
		/// <returns></returns>
		public static Task<string[]> AsyncGetDirectories(this DirectoryInfo dir, CancellationToken ct, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /ad/b {dir.FullName}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();

				while (!proc.WaitForExit(1)) {
					if (ct.IsCancellationRequested) { break; }
				}
				if (!proc.HasExited) { proc.Close(); }

				return result.ToArray();
			});
		}

		/// <summary>
		/// 비동기로 폴더 이름을 얻는다.
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="pattern">검색 폴더 패턴 ("abc*")</param>
		/// <param name="ct">취소할 때 사용할 토큰</param>
		/// <param name="handler">폴더 이름을 얻으면서 할 작업. 파라미터는 폴더 이름</param>
		/// <returns></returns>
		public static Task<string[]> AsyncGetDirectories(this DirectoryInfo dir, string pattern, CancellationToken ct, Action<string> handler = null)
		{
			return Task.Run(() => {
				List<string> result = new List<string>();

				Process proc = new Process();
				ProcessStartInfo procInfo = new ProcessStartInfo();
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/C dir /ad/b {Path.Combine(dir.FullName, pattern)}";
				procInfo.CreateNoWindow = true;
				procInfo.UseShellExecute = false;
				procInfo.RedirectStandardOutput = true;
				proc.StartInfo = procInfo;
				proc.OutputDataReceived += (s, e) => {
					if (string.IsNullOrWhiteSpace(e.Data)) { return; }
					result.Add(e.Data);
					handler?.Invoke(e.Data);
				};
				proc.Start();
				proc.BeginOutputReadLine();

				while (!proc.WaitForExit(1)) {
					if (ct.IsCancellationRequested) { break; }
				}
				if (!proc.HasExited) { proc.Close(); }

				return result.ToArray();
			});
		}
	}
}
