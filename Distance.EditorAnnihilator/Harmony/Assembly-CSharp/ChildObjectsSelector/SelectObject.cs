
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(LevelEditor), "SelectObject")]
    internal static class LevelEditor__SelectObject
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref bool __result, LevelEditor __instance, GameObject newObj)
        {
            if (Mod.SpecialObjName.StartsWith("##"))
            {
                String[] indexes = Mod.SpecialObjName.Substring(2).Split(',');
                int[] chileindexes = new int[indexes.Length];
                for(int i = 0; i<indexes.Length; i++)
                {
                    chileindexes[i] = Int32.Parse(indexes[i]);
                }
                GameObject theob = newObj;
                for(int i = 0; i<chileindexes.Length; i++)
                {
                    GameObject[] chileoblist = theob.GetChildren();
                    if (chileoblist.Length > chileindexes[i] && chileindexes[i] >= 0)
                    {
                        if(i==chileindexes.Length-1)
                        {
                            GameObject chileob = chileoblist[chileindexes[i]];
                            if (!__instance.selectedObjects_.Contains(chileob))
                            {
                                LevelLayer layerOfObject = __instance.workingLevel_.GetLayerOfObject(newObj);
                                if (layerOfObject != null && !layerOfObject.Frozen_)
                                {
                                    __instance.ClearSelectedList();
                                    __instance.AddObjectToSelectedList(chileob);
                                    __result = true;
                                    return false;
                                }
                                else
                                {
                                    __instance.SetActiveObject(chileob);
                                    __result = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            theob = chileoblist[chileindexes[i]];
                        }
                    }
                    else
                    {
                        Mod.Logger.Info("Index " + chileindexes[i] + " out of RANGE!");
                        return false;
                    }
                }
            }
            if ((UnityEngine.Object)newObj == (UnityEngine.Object)null)
            {
                UnityEngine.Debug.LogWarning((object)"Trying to select a null object");
                __result = false;
                return false;
            }
            if (!newObj.transform.IsRoot())
            {
                UnityEngine.Debug.LogWarning((object)"Trying to select a child object");
                __result = false;
                return false;
            }
            if (!__instance.selectedObjects_.Contains(newObj))
            {
                LevelLayer layerOfObject = __instance.workingLevel_.GetLayerOfObject(newObj);
                if (layerOfObject != null && !layerOfObject.Frozen_)
                {
                    __instance.AddObjectToSelectedList(newObj);
                    __result = true;
                    return false;
                }
            }
            else
            {
                __instance.SetActiveObject(newObj);
                __result = false;
            }
            return false;
        }
    }
}