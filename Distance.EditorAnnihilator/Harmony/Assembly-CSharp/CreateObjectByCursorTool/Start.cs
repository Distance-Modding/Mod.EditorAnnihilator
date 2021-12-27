
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(CreateObjectByCursorTool), "Start")]
    internal static class CreateObjectByCursorTool__Start
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'

        /*This patch is allows creating more than one object which has the OnlyAllowOneInLevel component. This allows multiple TagBoundsSpawners
         in one level, for example. The patch itself is copy of the function code, but replaces the contents of the second if statement to create
        the object, rather than to withhold creation of the object.*/

        [HarmonyPrefix]
        internal static bool Prefix(CreateObjectByCursorTool __instance)
        {
            __instance.state_ = ToolState.Active;
            Level workingLevel = G.Sys.LevelEditor_.WorkingLevel_;
            bool isCustomPrefab = __instance.prefabFileInfo_.IsCustomPrefab_;
            OnlyAllowOneInLevel onlyAllowOneInLevel = !isCustomPrefab ? __instance.prefabFileInfo_.Prefab_.GetComponent<OnlyAllowOneInLevel>() : (OnlyAllowOneInLevel)null;
            if (isCustomPrefab || (bool)(UnityEngine.Object)onlyAllowOneInLevel)
            {
                string name = __instance.prefabFileInfo_.Name_;
                foreach (GameObject gameObject in workingLevel.AllGameObjectsInLevelIEnumerable_)
                {
                    if (gameObject.name == name && gameObject.HasComponent<OnlyAllowOneInLevel>())
                    {
                        __instance.CreateObject();
                        return false;
                    }
                }
                __instance.CreateObject();
                return false;
            }
            else
            {
                __instance.CreateObject();
                return false;
            }
        }
    }
}