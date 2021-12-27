using System;
using Centrifuge.Distance.Data;
using Centrifuge.Distance.Game;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//Makes level saveable even with the wrong modes. (Reverse tag with no bubble, 2 start zones, ect.)
namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(GameManager), "GetModeCheckRequirements")]
    internal static class GameManager__GetModeCheckRequirements
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref CheckModeRequirements __result)
        {
            
            if (Mod.ModeReq)
            {
                __result = null;
                return false;
            }
            return true;
        }
    }
}