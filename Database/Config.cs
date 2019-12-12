using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Database {
	public class Config {
		private static IConfigurationRoot _config;

		public static void Setup() {
			_config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
		}

		public static IConfiguration ConnectionStrings => _config.GetSection("ConnectionStrings");
	}
}
