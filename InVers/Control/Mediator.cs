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
        Dictionary<string, List<IColleague>> _internalList
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
                if (!_internalList.ContainsKey(message))
                {
                    _internalList[message] = new List<IColleague>(1);
                }
                else
                {
                    if (_internalList[message] == null)
                        _internalList[message] = new List<IColleague>(1);
                }

                _internalList[message].Add(colleague);
            }
        }

        /// <summary>
        /// Notify all colleagues that are registed to the specific message
        /// </summary>
        /// <param name="message">The message for the notify by</param>
        /// <param name="args">The arguments for the message</param>
        public void NotifyColleagues(string message, object args)
        {
            if (_internalList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (IColleague colleague in _internalList[message])
                    colleague.MessageNotification(message, args);
            }
        }

    }
}
