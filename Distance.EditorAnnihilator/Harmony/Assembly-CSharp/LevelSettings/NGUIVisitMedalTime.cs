
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
    [HarmonyPatch(typeof(LevelSettings), "NGUIVisitMedalTime")]
    internal static class LevelSettings__NGUIVisitMedalTime
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref LevelSettings __instance, IVisitor visitor, string timeName, ref float time)
        {
            string str = "";
            
            if(float.IsNegativeInfinity(time))
            {
                str = "-I";
            }
            else if (float.IsPositiveInfinity(time))
            {
                str = "+I";
            }
            else if (float.IsNaN(time))
            {
                str = "NaN";
            }
            else if (time < 0)
            {
                str = "-" + __instance.MedalTimeSpanToString(TimeSpan.FromMilliseconds((double)Mathf.Abs(time)));
            }
            else
            {
                str = __instance.MedalTimeSpanToString(TimeSpan.FromMilliseconds((double)Mathf.Max(time, 0.0f)));
            }
            
            string val = str;
            visitor.Visit(timeName, ref val, LevelSettings.medalTimeOptions_);
            TimeSpan span;
            if (!(!(val != str) || !__instance.MedalTimeSpanTryParse(val, out span)))
            { 
                time = span.TotalMilliseconds <= 0.0 ? 0.0f : (float)span.TotalMilliseconds;
            }
            else if(!(!(val != str)) && val.StartsWith("-") && __instance.MedalTimeSpanTryParse(val.Substring(1,val.Length-1), out span))
            {
                time = -(float)span.TotalMilliseconds;
            }
            else if ((val != str) && val.StartsWith("$") && val.EndsWith("$") && val.Length > 1)
            {
                try
                {
                    time = float.Parse(val.Substring(1, val.Length - 2));
                }
                catch
                {
                    time = float.NaN;
                }
            }
            else if (!(!(val != str)) && (val.StartsWith("I") || val.StartsWith("+I")))
            {
                time = float.PositiveInfinity;
            }
            else if (!(!(val != str)) && val.StartsWith("-I"))
            {
                time = float.NegativeInfinity;
            }
            else if (!(!(val != str)) && val.StartsWith("N"))
            {
                time = float.NaN;
            }
            return false;
        }
    }
}