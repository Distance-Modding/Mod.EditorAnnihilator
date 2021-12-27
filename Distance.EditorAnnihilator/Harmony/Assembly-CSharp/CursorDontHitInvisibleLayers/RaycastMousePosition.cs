using System;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(MoveCursorTool), "Run")]
    internal static class MoveCursorTool__Run
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref bool __result)
        {
            LevelEditor levelEditor = G.Sys.LevelEditor_;
            Transform transform1 = levelEditor.Cursor_.transform;
            RaycastHit hitInfo;
            int layamask = (int)LayerEx.CreateExcludeMask(Layers.IgnoreRaycast, Layers.TriggerVolume, Layers.EditorGizmo, Layers.EditorObject);
            if(Mod.CCWBL)
            {
                layamask = (int)LayerEx.CreateExcludeMask(Layers.IgnoreRaycast, Layers.TriggerVolume, Layers.EditorGizmo, Layers.EditorObject, Layers.Background, Layers.BackgroundInvis);
            }
            if (levelEditor.RaycastMousePosition(out hitInfo, layamask))
            {
                transform1.localRotation = MoveCursorTool.GetRayCastHitOrientation(hitInfo);
                transform1.localPosition = hitInfo.point + hitInfo.normal * 0.01f;
            }
            else
            {
                Camera camera = G.Sys.LevelEditor_.Camera_;
                Transform transform2 = camera.transform;
                float nearClipPlane = camera.nearClipPlane;
                Vector3 vector3 = transform2.InverseTransformPoint(transform1.localPosition);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = nearClipPlane + vector3.z;
                transform1.localPosition = camera.ScreenToWorldPoint(mousePosition);
                transform1.up = Vector3.up;
            }
            LevelEditorTool.PrintMessage("Moved the cursor to: " + (object)transform1.localPosition);
            __result = false;
            return false;
        }
    }
}
