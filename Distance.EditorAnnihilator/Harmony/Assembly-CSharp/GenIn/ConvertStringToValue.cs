
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(UIExGenericNumericInput<float>), "ConvertStringToValue")]
    internal static class UIExGenericNumericInput__ConvertStringToValue
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref float __result, UIExGenericNumericInput<float> __instance, string s)
        {
            __instance.expressionTree_ = new ExpressionTree(s);
            double num = __instance.expressionTree_.Evaluate(__instance.ConvertValueToDouble(__instance.PreviousValue_), true);
            if (double.IsNaN(num))
            {
                __instance.expressionTree_ = (ExpressionTree)null;
                __result = float.NaN;
            }
            else
            {
                __result = __instance.ValidateValue(__instance.ConvertDoubleToValue(num));
            }
            
        }
    }
}