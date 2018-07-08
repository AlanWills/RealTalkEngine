using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine
{
    public static class Logger
    {
        #region Properties and Fields

        /// <summary>
        /// The static logger instance we obtain from amazon when we handle a request.
        /// </summary>
        private static ILambdaLogger m_logger;

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize our local logger with the inputted lambda logger.
        /// </summary>
        /// <param name="logger"></param>
        public static void Initialize(ILambdaLogger logger)
        {
            m_logger = logger;
        }

        #endregion

        #region Logging Functions

        /// <summary>
        /// Log the inputted message as a line to the logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            m_logger?.LogLine(message);
        }

        /// <summary>
        /// If the inputted condition is false, we log the inputted message as a line to the logger.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                Log(message);
            }
        }
        
        #endregion
    }
}
