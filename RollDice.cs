using HarmonyLib;
using System.Numerics;

namespace LegendOfMortalPlugin
{
    public class RollDice
    {
        [HarmonyPrefix, HarmonyPatch(typeof(Mortal.Story.CheckPointManager), "Dice")]
        public static bool modifyDiceNumnber(ref int random)
        {
            if (Plugin.dice.Value)
                if(Plugin.diceNumber.Value > 0 && Plugin.diceNumber.Value < 100)
                    random = Plugin.diceNumber.Value;

            return true;
        }
    }
}
