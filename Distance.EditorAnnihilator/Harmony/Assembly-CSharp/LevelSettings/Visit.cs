
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;
using System.Reflection;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(LevelSettings), "Visit")]
    internal static class LevelSettings__Visit
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static void Prefix(LevelSettings __instance, IVisitor visitor, ISerializable prefabComp, int version)
        {
            if (!Mod.CNF)
            {
                Mod.DevBuildForCreatorName = true;
            }
        }
    }
}