
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(CreditsNameOrbLogic), "Visit")]
    internal static class CreditsNameOrbLogic__Visit
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(CreditsNameOrbLogic __instance, IVisitor visitor, ISerializable prefabComp, int version)
        {
            if (version == 0)
            {
                
            }
            else
            {
                visitor.Visit("name_", ref __instance.name_);
                if (visitor is ISerializer)
                {
                    visitor.Visit("key_", ref __instance.key_);
                    visitor.Visit("linkedKey_", ref __instance.linkedKey_);
                }
            }
            if(!__instance.initialized_ && !G.Sys.GameManager_.IsLevelEditorMode_)
            {
                //Mod.Logger.Info("BOOYAE");
                //__instance.Initialize();
            }
            return false;
        }
    }
}
