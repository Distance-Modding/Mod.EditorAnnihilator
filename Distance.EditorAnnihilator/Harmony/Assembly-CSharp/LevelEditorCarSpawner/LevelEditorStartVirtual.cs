
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(LevelEditorCarSpawner), "LevelEditorStartVirtual")]
    internal static class LevelEditorCarSpawner__LevelEditorStartVirtual
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(LevelEditorCarSpawner __instance)
        {
            if(Mod.DPM1C)
            {
                return true;
            }
            LevelEditor levelEditor = G.Sys.LevelEditor_;
            foreach (LevelEditorCarSpawner editorCarSpawner in levelEditor.WorkingLevel_.FindComponentsOfType<LevelEditorCarSpawner>())
            {
                if ((Object)editorCarSpawner != (Object)__instance)
                { 
                    
                }
            }
            return false;
        }
    }
}