
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(StuntBubbleLogic), "Awake")]
    internal static class StuntBubbleLogic__Awake
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(StuntBubbleLogic __instance)
        {
            
            __instance.col_ = __instance.GetComponent<SphereCollider>();
            __instance.color_ = __instance.bubbleRend_.material.GetColor("_Color");
            __instance.alpha_ = __instance.color_.a;
            __instance.emitColor_ = __instance.bubbleRend_.material.GetColor("_EmitColor");
            __instance.emitAlpha_ = __instance.emitColor_.a;
            __instance.outlineColor_ = __instance.bubbleOutlineRend_.material.GetColor("_Color");
            __instance.scale_ = __instance.transform.localScale;
            __instance.SetAlpha(0.0f);
            return false;
        }
    }
}
