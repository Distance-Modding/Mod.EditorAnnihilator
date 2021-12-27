
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
    [HarmonyPatch(typeof(LevelSettings), "GetDefaultModesMap")]
    internal static class LevelSettings__GetDefaultModesMap
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref Dictionary<int, bool> __result)
        {
            if(Mod.EM) return;
            __result.Add((int)GameModeID.None, false);
            __result.Add((int)GameModeID.FreeRoam, false);
            __result.Add((int)GameModeID.CoopSprint, false);
            __result.Add((int)GameModeID.Count, false);
        }
    }
}
