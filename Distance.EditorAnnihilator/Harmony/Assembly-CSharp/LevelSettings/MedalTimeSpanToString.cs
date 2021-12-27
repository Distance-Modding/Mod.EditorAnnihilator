
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
    [HarmonyPatch(typeof(LevelSettings), "MedalTimeSpanToString")]
    internal static class LevelSettings__MedalTimeSpanToString
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref string __result, TimeSpan timeSpan)
        {
            int num = timeSpan.Milliseconds / 10;
            __result = string.Format("{0:00}:{1:00}.{2:00}", (object)(timeSpan.Minutes + timeSpan.Hours * 60 + timeSpan.Days * 1440), (object)timeSpan.Seconds, (object)num);
            return false;
        }
    }
}