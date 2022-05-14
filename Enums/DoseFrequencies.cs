using System.Runtime.Serialization;

namespace AntiPsychRRMVC.Enums
{


    public class DoseFrequencies
    {
        /// <summary>
        /// For IM,Oral,Inhaled medication
        /// </summary>
        public enum NonWeeklyFrequencies
        {
            OneDaily = 1,
            TwoDaily = 2,
            ThreeDaily = 3,
            FourDaily = 4
        }

        //For depot, long acting injection medication
        public enum WeeklyFrequencies
        {
            EveryWeek = 1,
            EveryTwoWeeks = 2,
            EveryThreeWeeks = 3,
            EveryFourWeeks = 4,
            EveryFiveWeeks = 5,
            EverySixWeeks = 6,
            EverySevenWeeks = 7,
            EveryEightWeeks = 8,
            EveryNineWeeks = 9,
            EveryTenWeeks = 10,
            EveryElevenWeeks = 11,
            EveryTwelveWeeks = 12
        }

    }
}
