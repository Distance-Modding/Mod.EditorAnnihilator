using System;
using System.Collections.Generic;


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
    [HarmonyPatch(typeof(LevelSettings), "LevelName_", MethodType.Setter)]
    internal static class LevelSettings__LevelName_
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref string value)
        {
            if (value.StartsWith("$$") && value.EndsWith("$$"))
            {
                Mod.SpecialObj = true;
                Mod.SpecialObjName = value.Substring(2,value.Length-4);
            }
            else
            {
                Mod.SpecialObj = false;
                Mod.SpecialObjName = "";
            }
        }
    }
}
