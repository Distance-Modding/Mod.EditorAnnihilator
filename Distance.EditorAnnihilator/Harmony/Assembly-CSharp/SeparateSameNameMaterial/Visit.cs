
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(MaterialWrapper), "ISerializable.Visit")]
    internal static class MaterialWrapper__ISerializableVisit
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(MaterialWrapper __instance, IVisitor visitor, ISerializable prefab, int version)
        {
            if (Mod.SMNS) return true;
                bool flag = visitor is NGUIComponentInspector;
            if (visitor.Direction_ == VisitDirection.Out || visitor.Direction_ == VisitDirection.Both)
                __instance.ReadFromMaterials();
            for (int index = 0; index < __instance.matInfo_.colors_.Length; ++index)
            {
                if (!flag || __instance.matInfo_.supportedColorsFlag_.IsFlagSetAt(index))
                {
                    visitor.Visit(__instance.matInfo_.colors_[index].name_, ref __instance.matInfo_.colors_[index].color_);
                }
                else if(!flag)
                {
                    visitor.Visit(__instance.matInfo_.colors_[index].name_+"!", ref __instance.matInfo_.colors_[index].color_);
                }
            }
            if (visitor.Direction_ != VisitDirection.In && visitor.Direction_ != VisitDirection.Both)
                return false;
            __instance.ApplyToMaterials();
            return false;
        }
    }
}