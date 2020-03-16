﻿using System;
using System.IO;

namespace BuildBot
{
    /// <summary>
    ///     Application configuration helpers
    /// </summary>
    public static class ApplicationConfig
    {
        // really should be using AppContext.BaseDirectory, but this seems to break sometimes when running unit tests with dotnet-xuint.

        /// <summary>
        ///     The base path of the folder with the configuration files in them.
        /// </summary>
        public static string ConfigurationFilesPath { get; } = LookupConfigurationFilesPath();

        private static string LookupConfigurationFilesPath()
        {
            string? path = LookupAppSettingsLocationByAssemblyName();

            if (path == null)
            {
                // https://stackoverflow.com/questions/57222718/how-to-configure-self-contained-single-file-program
                return Environment.CurrentDirectory;
            }

            return path;
        }

        private static string? LookupAppSettingsLocationByAssemblyName()
        {
            string location = typeof(ApplicationConfig).Assembly.Location;

            string? path = Path.GetDirectoryName(location);

            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            if (!File.Exists(Path.Combine(path, path2: @"appsettings.json")))
            {
                return null;
            }

            return path;
        }
    }
}