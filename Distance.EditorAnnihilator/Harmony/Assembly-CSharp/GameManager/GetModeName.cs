
using System.Linq;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;
using System;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(GameManager), "GetModeName")]
    internal static class GameManager__GetModeName
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, GameModeID ID)
        {
            if(ID == GameModeID.Soccer)
            {
                __result = "Soccer";
            }
            else if(ID == GameModeID.None)
            {
                __result = "None";
            }
            else if (ID == GameModeID.FreeRoam)
            {
                __result = "Free Roam";
            }
            else if (ID == GameModeID.CoopSprint)
            {
                __result = "Coop Sprint";
            }
            else if (ID == GameModeID.SpeedAndStyle)
            {
                __result = "SpeedAndStyle";
            }
            else if (ID == GameModeID.Demo)
            {
                __result = "Demo";
            }
            else if (ID == GameModeID.Count)
            {
                __result = "Count";
            }
        }
    }
}
