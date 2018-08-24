using RealTalkEngine.RequestHandling.IntentHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using RealTalkEngine.RequestHandling.RequestHandlers;

namespace RealTalkEngine.Tests.Mocks.RequestHandling.IntentHandlers
{
    public class MockIntentHandlerFactory : IIntentHandlerFactory
    {
        #region Properties and Fields

        public List<IntentHandler> CustomIntentHandlers { get; } = new List<IntentHandler>();

        #endregion

        public MockIntentHandlerFactory(params IntentHandler[] handlers)
        {
            if (handlers != null)
            {
                CustomIntentHandlers.AddRange(handlers);
            }
        }
    }
}