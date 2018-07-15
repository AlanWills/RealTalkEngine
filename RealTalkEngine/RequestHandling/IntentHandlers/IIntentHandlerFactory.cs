using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.IntentHandlers
{
    public interface IIntentHandlerFactory
    {
        #region Properties and Fields

        /// <summary>
        /// A list of custom intent handlers that will process incoming intents.
        /// </summary>
        List<IntentHandler> CustomIntentHandlers { get; }

        #endregion
    }
}
