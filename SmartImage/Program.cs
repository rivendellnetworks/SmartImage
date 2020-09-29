﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using SmartImage.Searching;
using SimpleCore;
using SimpleCore.Utilities;
using SimpleCore.Win32.Cli;
using SmartImage.Shell;
using SmartImage.Utilities;

#endregion

namespace SmartImage
{
	public static class Program
	{
		//  ____                       _   ___
		// / ___| _ __ ___   __ _ _ __| |_|_ _|_ __ ___   __ _  __ _  ___
		// \___ \| '_ ` _ \ / _` | '__| __|| || '_ ` _ \ / _` |/ _` |/ _ \
		//  ___) | | | | | | (_| | |  | |_ | || | | | | | (_| | (_| |  __/
		// |____/|_| |_| |_|\__,_|_|   \__|___|_| |_| |_|\__,_|\__, |\___|
		//                                                     |___/

		/**
		 * Entry point
		 */
		private static void Main(string[] args)
		{
			/*
			 * Set up console
			 */

			Console.Title = RuntimeInfo.NAME;
			Console.SetWindowSize(120, 45);
			Console.OutputEncoding = Encoding.Unicode;
			CliOutput.EnableVirtualTerminalProcessing();
			Console.Clear();

			/*
			 * Run search
			 */

			try {

				Integration.Setup();
				SearchConfig.ReadSearchConfigArguments(args);

				if (SearchConfig.Config.NoArguments) {
					ConsoleMainMenu.RunMainMenu();
					Console.Clear();
				}

				string img = SearchConfig.Config.Image;

				// Run checks
				if (!SearchClient.IsFileValid(img)) {
					return;
				}

				// Run search

				using var searchClient = new SearchClient(img);
				searchClient.Start();

				// Show results

				ConsoleIO.HandleOptions(searchClient.Results);
			}
			catch (Exception exception) {

#if !DEBUG
				var cr = new CrashReport(exception);

				Console.WriteLine(cr);

				var src = cr.WriteToFile();

				Console.WriteLine("Crash log written to {0}", src);

				Console.WriteLine("Please file an issue and attach the crash log.");

				Network.OpenUrl(RuntimeInfo.Issue);

				ConsoleIO.WaitForInput();
#endif

			}
			finally {
				// Exit
				SearchConfig.UpdateFile();
			}


		}
	}
}