using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class DefaultSkillRequestHandlerFactory : ISkillRequestHandlerFactory
    {
        public List<SkillRequestHandler> SkillRequestHandlers { get; } = new List<SkillRequestHandler>();
    }
}
