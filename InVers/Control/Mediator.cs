using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Control
{
    /// <summary>
    /// Mediator for all sub controllers
    /// </summary>
    public class Mediator
    {
        #region Data members
        Dictionary<string, List<IColleague>> internalList
            = new Dictionary<string, List<IColleague>>();
        #endregion

        /// <summary>
        /// Registers a Colleague to a specific message
        /// </summary>
        /// <param name="colleague">The colleague to register</param>
        /// <param name="messages">The message to register to</param>
        public void Register(IColleague colleague, IEnumerable<string> messages)
        {
            foreach (string message in messages)
            {
                if (!internalList.ContainsKey(message))
                {
                    internalList[message] = new List<IColleague>(1);
                }
                else
                {
                    if (internalList[message] == null)
                        internalList[message] = new List<IColleague>(1);
                }

                internalList[message].Add(colleague);
            }
        }

        /// <summary>
        /// Notify all colleagues that are registed to the specific message
        /// </summary>
        /// <param name="message">The message for the notify by</param>
        /// <param name="args">The arguments for the message</param>
        public void NotifyColleagues(string message, object args)
        {
            if (internalList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (IColleague colleague in internalList[message])
                    colleague.MessageNotification(message, args);
            }
        }

    }
}
