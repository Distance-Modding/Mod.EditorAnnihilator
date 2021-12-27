
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Events.Stunt;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(StuntCollectibleLogic), "Awake")]
    internal static class StuntCollectibleLogic__Awake
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(StuntCollectibleLogic __instance)
        {
            StaticEvent<StuntCollectibleSpawned.Data>.Subscribe(new StaticEvent<StuntCollectibleSpawned.Data>.Delegate(__instance.OnEventStuntCollectibleSpawned));
            StaticEvent<HitTagStuntCollectible.Data>.Subscribe(new StaticEvent<HitTagStuntCollectible.Data>.Delegate(__instance.OnHitTagStuntCollectible));
            __instance.col_ = __instance.GetComponent<SphereCollider>();
            __instance.col_.enabled = false;
            __instance.phantom_ = __instance.GetComponent<Phantom>();
            return false;
        }
    }
}
