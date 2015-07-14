using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Control
{
    public abstract class BaseController : INotifyPropertyChanged, IColleague
    {
        #region Data members
        static Mediator _mediatorInstance = new Mediator();
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event raised to notify that a property has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
        #region IColleague Members

        /// <summary>
        /// Gets the mediator for this controller
        /// </summary>
        public Mediator Mediator { get; private set; }

        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public abstract void MessageNotification(string message, object args);

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseController()
        {
            //set the mediator to be the same one for every controller.
            Mediator = _mediatorInstance;
        }
    }
}
