﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ModernNotes {

    ///<summary>Program.</summary> 
    public class Program {
        ///<summary>Main.</summary> 
        public static void Main(string[] args) {
            BuildWebHost(args).Run();
        }

        ///<summary>Build web host.</summary> 
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
