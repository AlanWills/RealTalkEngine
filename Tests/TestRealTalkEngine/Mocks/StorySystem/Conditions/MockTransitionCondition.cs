using RealTalkEngine.StorySystem.Conditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestRealTalkEngine.Mocks.StorySystem.Conditions
{
    public class MockTransitionCondition : TransitionCondition
    {
        #region Properties and Fields

        public bool ConditionPasses_Result { get; set; }

        #endregion

        public override bool ConditionPasses()
        {
            return ConditionPasses_Result;
        }
    }
}
