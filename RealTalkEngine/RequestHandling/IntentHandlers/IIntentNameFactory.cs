using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.IntentHandlers
{
    public interface IIntentNameFactory
    {
        #region Properties and Fields

        /// <summary>
        /// A list of all of the available intents for this skill.
        /// </summary>
        List<string> IntentNames { get; }

        #endregion
    }
}
