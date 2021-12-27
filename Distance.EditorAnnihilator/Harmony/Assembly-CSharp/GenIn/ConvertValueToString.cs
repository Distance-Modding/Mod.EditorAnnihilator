using System;
using System.Collections.Generic;
using System.Linq;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(UIExNumericInput), "ConvertValueToString")]
    internal static class UIExNumericInput__ConvertValueToString
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, float val)
        {
            String precisionStringer = "0.0";
            for(int i = 0; i<Mod.EDP; i++)
            {
                precisionStringer = precisionStringer + "#";
            }
            __result = val.ToString(precisionStringer);
        }
    }
}
