using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public abstract class IntentHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The AWS name for the intent this handler is processing.
        /// </summary>
        public abstract string IntentName { get; }

        #endregion

        #region Intent Handler Abstract Functions

        /// <summary>
        /// Returns true if this handler is the correct handler for the inputted Intent.
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public bool IsHandlerForIntent(Intent intent) { return intent.Name == IntentName; }

        /// <summary>
        /// Handle the inputted intent and return an appropriate reponse.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="session"></param>
        /// <param name="lambdaContext"></param>
        /// <returns></returns>
        public abstract SkillResponse HandleIntent(Intent intent);

        #endregion
    }
}
