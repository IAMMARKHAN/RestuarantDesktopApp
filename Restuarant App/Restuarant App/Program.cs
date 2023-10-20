using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>InvalidOperationException: 'Chart Area Axes - The chart area contains incompatible chart types. For example, bar charts and column charts cannot exist in the same chart area.'
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdminMain("","")) ;
        }
    }
}
