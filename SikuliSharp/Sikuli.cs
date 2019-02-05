﻿using System;
using System.IO;

namespace SikuliSharp
{
	public static class Sikuli
	{
		public static ISikuliSession CreateSession114()
		{
			return new SikuliSession(CreateRuntime(),true);
		}

		public static ISikuliSession CreateSession()
		{
			return new SikuliSession(CreateRuntime());
		}

		public static SikuliRuntime CreateRuntime()
		{
			return new SikuliRuntime(
				new AsyncDuplexStreamsHandlerFactory(),
				new SikuliScriptProcessFactory()
				);
		}

		public static string RunProject(string projectPath)
		{
			return RunProject(projectPath, null);
		}

		public static string RunProject(string projectPath, string args)
		{
			if (projectPath == null) throw new ArgumentNullException("projectPath");

			if (!Directory.Exists(projectPath))
				throw new DirectoryNotFoundException(string.Format("Project not found in path '{0}'", projectPath));

			var processFactory = new SikuliScriptProcessFactory();
			using (var process = processFactory.Start(string.Format("-r {0} {1}", projectPath, args)))
			{
				var output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				return output;
			}
		}
	}
}
