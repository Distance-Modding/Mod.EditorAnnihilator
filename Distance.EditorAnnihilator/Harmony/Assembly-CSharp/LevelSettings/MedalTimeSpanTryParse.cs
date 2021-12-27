using System;
using System;
using Centrifuge.Distance.Data;
using Centrifuge.Distance.Game;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LevelEditorTools;
using UnityEngine;


//Makes level saveable even with the wrong modes. (Reverse tag with no bubble, 2 start zones, ect.)
namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(LevelSettings), "MedalTimeSpanTryParse")]
    internal static class LevelSettings__MedalTimeSpanTryParse
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref bool __result, string timeSpanStr, out TimeSpan span)
        {
            if (string.IsNullOrEmpty(timeSpanStr))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            if(timeSpanStr.StartsWith("-") || timeSpanStr.StartsWith("$") || timeSpanStr.StartsWith("I") || timeSpanStr.StartsWith("+"))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int length = timeSpanStr.IndexOf(':');
            int num1 = timeSpanStr.LastIndexOf(':');
            if (length == -1 || length != num1)
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int num2 = timeSpanStr.IndexOf('.');
            int num3 = timeSpanStr.LastIndexOf('.');
            if (num2 == -1 || num2 != num3 || (num2 < length || length == timeSpanStr.Length - 1))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int result1;
            if (!int.TryParse(timeSpanStr.Substring(0, length), out result1))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int result2;
            if (!int.TryParse(timeSpanStr.Substring(length + 1, num2 - length - 1), out result2))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int result3;
            if (!int.TryParse(timeSpanStr.Substring(num2 + 1, Mathf.Min(timeSpanStr.Length - num2 - 1, 2)), out result3))
            {
                span = TimeSpan.Zero;
                __result = false;
                return false;
            }
            int minutes = result1;
            result2 = Mathf.Min(Mathf.Max(result2, 0), 59);
            result3 = Mathf.Min(Mathf.Max(result3, 0), 99) * 10;
            span = new TimeSpan(0, 0, minutes, result2, result3);
            __result = true;
            return false;
        }
    }
}