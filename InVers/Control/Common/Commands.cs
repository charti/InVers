using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InVers.Control
{
    /// <summary>
    /// Where all application commands are defined
    /// </summary>
    public static class Commands
    {
        public static RoutedUICommand Turn
            = new RoutedUICommand("Makes a Turn", "Turn", typeof(Commands));
    }
}
