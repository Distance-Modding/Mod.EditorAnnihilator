
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
    [HarmonyPatch(typeof(GameManager), "GetModeShowInLevelEditor")]
    internal static class GameManager__GetModeShowInLevelEditor
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result, GameModeID ID)
        {
            if(ID == GameModeID.Trackmogrify)
            {
                __result = true;
            }
            if (Mod.EM)
            {

            }
            else
            {
                __result = true;
            }
            

        }
    }
}
