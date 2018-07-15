using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public interface ISkillRequestHandlerFactory
    {
        #region Properties and Fields

        /// <summary>
        /// All of the available handlers for the base alexa request types.
        /// </summary>
        List<SkillRequestHandler> SkillRequestHandlers { get; }

        #endregion
    }
}
