using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public static class Intents
    {
        #region Utility Intent Names

        /// <summary>
        /// The name of the Yes intent on AWS lambda.
        /// </summary>
        public const string YesIntentName = "AMAZON.YesIntent";

        /// <summary>
        /// The name of the No intent on AWS lambda.
        /// </summary>
        public const string NoIntentName = "AMAZON.NoIntent";

        /// <summary>
        /// The name of the Help intent on AWS lambda.
        /// </summary>
        public const string HelpIntentName = "AMAZON.HelpIntent";

        /// <summary>
        /// The name of the Stop intent on AWS lambda.
        /// </summary>
        public const string StopIntentName = "AMAZON.StopIntent";

        /// <summary>
        /// The name of the Start Over intent on AWS lambda.
        /// </summary>
        public const string StartOverIntentName = "AMAZON.StartOverIntent";

        /// <summary>
        /// The name of the Cancel intent on AWS lambda.
        /// </summary>
        public const string CancelIntentName = "AMAZON.CancelIntent";

        #endregion

        #region Story Start Intent Names

        /// <summary>
        /// The name of the PlayGame intent on AWS lambda.
        /// </summary>
        public const string PlayGameIntentName = "PlayGameIntent";

        /// <summary>
        /// The name of the StartFromNode intent on AWS lambda.
        /// </summary>
        public const string StartFromNodeIntentName = "StartFromNodeIntent";

        #endregion

        #region Story Progression Intent Names

        public const string TellMeWhatsHappenedIntent = "TellMeWhatsHappenedIntent";
        public const string WithThemNowIntent = "WithThemNowIntent";
        public const string HowManyWeeksPregnantIntent = "HowManyWeeksPregnantIntent";
        public const string HowOldIsMotherIntent = "HowOldIsMotherIntent";
        public const string WhereAreYouIntent = "WhereAreYouIntent";
        public const string IsBabyVisibleIntent = "IsBabyVisibleIntent";
        public const string StayOnTheLineIntent = "StayOnTheLineIntent";
        public const string BabyWillBeSlipperyIntent = "BabyWillBeSlipperyIntent";
        public const string SupportBabyIntent = "SupportBabyIntent";
        public const string CheckOkIntent = "CheckOkIntent";
        public const string IsBabyCryingOrBreathingIntent = "IsBabyCryingOrBreathingIntent";
        public const string IsAnythingObviouslyWrongIntent = "IsAnythingObviouslyWrongIntent";
        public const string RubBabysBackInstructionIntent = "RubBabysBackInstructionIntent";
        public const string IsBoyOrGirlIntent = "IsBoyOrGirlIntent";
        public const string CongratulationsIntent = "CongratulationsIntent";
        public const string AreParamedicsWithYouIntent = "AreParamedicsWithYouIntent";
        public const string WellDoneIntent = "WellDoneIntent";

        #endregion
    }
}
