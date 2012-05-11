using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Hanssens.Applications.HandleThis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            if (e.Args != null && e.Args.Count() > 0)
            {
                foreach (var arg in e.Args)
                {
                    // Read out arguments
                    this.Properties["Filename"] = e.Args[0];
                }
            }

            base.OnStartup(e);
        }
    }

}
