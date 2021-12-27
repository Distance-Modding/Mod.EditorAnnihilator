

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(MaterialWrapper), "ComponentName_", MethodType.Getter)]
    internal static class MaterialWrapper__ComponentName_
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, MaterialWrapper __instance)
        {
            if(Mod.SMNS)
            {
                __result = "Material: " + (__instance.matInfo_.matName_ + __instance.materialIndex_).Colorize(Colors.GreenColors.seaGreen);
            }
        }
    }
}