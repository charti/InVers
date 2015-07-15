using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Control.Controllers
{
    public class ConfigViewControl : BaseController
    {
        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
