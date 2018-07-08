using Alexa.NET.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Twinary.StorySystem
{
    public class StoryRuntime
    {
        #region Properties and Fields

        /// <summary>
        /// The current intent that the runtime is processing.
        /// </summary>
        public Intent Intent { get; set; }

        /// <summary>
        /// The current session that the runtime is maintaining.
        /// </summary>
        public Session Session { get; set; }

        #endregion

        public StoryRuntime(Intent intent, Session session)
        {
            Intent = intent;
            Session = session;
        }
    }
}
