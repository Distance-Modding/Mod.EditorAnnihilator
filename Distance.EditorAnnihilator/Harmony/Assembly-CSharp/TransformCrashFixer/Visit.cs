using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(TransformWrapper), "Visit")]
    internal static class TransformWrapper__Visit
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(TransformWrapper __instance, IVisitor visitor, ISerializable prefab, int version)
        {
            if (!(bool)(UnityEngine.Object)__instance.tComponent_)
                return false;
            Transform componentOrNull = prefab.GetComponentOrNull() as Transform;
            if (visitor.Direction_ == VisitDirection.Out && (UnityEngine.Object)componentOrNull != (UnityEngine.Object)null)
            {
                visitor.SerializeOutIfDifferentFromPrefab("Position", __instance.tComponent_.localPosition, componentOrNull.localPosition);
                visitor.SerializeOutIfDifferentFromPrefab("Rotation", __instance.tComponent_.localRotation, componentOrNull.localRotation);
                visitor.SerializeOutIfDifferentFromPrefab("Scale", __instance.tComponent_.localScale, componentOrNull.localScale);
            }
            else
            {
                Vector3 outVal1;
                if (visitor.VisitPropertyHelper("Position", __instance.tComponent_.localPosition, out outVal1))
                {
                    if (!outVal1.IsValid())
                    {
                        Debug.LogWarning((object)("Invalid position on " + __instance.tComponent_.gameObject.name));
                        outVal1 = Vector3.zero;
                    }
                    __instance.tComponent_.localPosition = outVal1;
                }
                Quaternion outVal2;
                if (visitor.VisitPropertyHelper("Rotation", __instance.tComponent_.localRotation, out outVal2))
                {
                    if (!outVal2.IsValid())
                    {
                        Debug.LogWarning((object)("Invalid rotation on " + __instance.tComponent_.gameObject.name));
                        outVal2 = Quaternion.identity;
                    }
                    __instance.tComponent_.localRotation = outVal2;
                }
                if (visitor.VisitPropertyHelper("Scale", __instance.tComponent_.localScale, out outVal1, TransformWrapper.scaleOptions_))
                {
                    GameObject gameObject1 = __instance.tComponent_.gameObject;
                    GameObject gameObject2 = gameObject1.Root();
                    if (!outVal1.IsValid())
                    {
                        Debug.LogWarning((object)("Invalid scale on " + __instance.tComponent_.gameObject.name));
                        outVal1 = Vector3.one;
                    }
                    else if ((double)outVal1.ElementMin() < 9.99999974737875E-06)
                    {
                        string str = "Negative scale on " + gameObject1.name;
                        if ((UnityEngine.Object)gameObject1 != (UnityEngine.Object)gameObject2)
                            str = str + ", root " + gameObject2.name;
                        Debug.LogWarning((object)str);
                        outVal1.x = Mathf.Max(Mathf.Abs(outVal1.x), 1E-05f);
                        outVal1.y = Mathf.Max(Mathf.Abs(outVal1.y), 1E-05f);
                        outVal1.z = Mathf.Max(Mathf.Abs(outVal1.z), 1E-05f);
                    }
                    MeshCollider coll = gameObject2.GetComponent<MeshCollider>();
                    if(coll != null)
                    {
                        if (float.IsInfinity(outVal1.x) || float.IsNaN(outVal1.x))
                        {
                            outVal1.x = 1;
                        }
                        if (float.IsInfinity(outVal1.y) || float.IsNaN(outVal1.y))
                        {
                            outVal1.y = 1;
                        }
                        if (float.IsInfinity(outVal1.z) || float.IsNaN(outVal1.z))
                        {
                            outVal1.z = 1;
                        }
                    }
                    
                    __instance.tComponent_.localScale = outVal1;
                }
            }
            if (!(visitor is ISerializer))
                return false;
            __instance.VisitChildren(visitor as ISerializer, __instance.tComponent_, componentOrNull);
            return false;
        }
    }
}