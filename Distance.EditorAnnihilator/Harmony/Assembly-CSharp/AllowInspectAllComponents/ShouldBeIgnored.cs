

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(DontInspectComponents.Set), "ShouldBeIgnored")]
    internal static class DontInspectComponentsSet__ShouldBeIgnored
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result, Component comp)
        {
            if (Mod.VOIC)
            {
                if ((Object)comp == (Object)null)
                {
                    __result = true;
                }
                else
                {
                    __result = false;
                }
            }    
        }
    }
}