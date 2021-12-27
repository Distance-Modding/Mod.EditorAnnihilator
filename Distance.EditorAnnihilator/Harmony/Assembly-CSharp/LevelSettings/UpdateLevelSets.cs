using System;
using System.Linq;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;
using System;

namespace Mod.EditorAnnihilator.Harmony
{
    [HarmonyPatch(typeof(LevelSetsManager), "UpdateLevelSets")]
    internal static class LevelSetsManager__UpdateLevelSets
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPrefix]
        internal static bool Prefix(ref LevelSetsManager __instance, string normalizedAbsoluteLevelPath, LevelInfo levelInfo)
        {
            if (levelInfo.levelType_ == LevelType.Official)
                return true;
            foreach (KeyValuePair<int, bool> mode in levelInfo.modes_)
            {
                GameModeID key = (GameModeID)mode.Key;
                if (!GameManager.IsCampaignMode(key))
                {
                    LevelSet set = __instance.GetSet(key);
                    if (set != null)
                    {
                        if (mode.Value)
                        {
                            string creatorSteamIDString = (string)null;
                            if (levelInfo.workshopCreatorID_ != 0UL)
                                creatorSteamIDString = levelInfo.workshopCreatorID_.ToString();
                            set.AddLevel(levelInfo.levelName_, normalizedAbsoluteLevelPath, levelInfo.levelType_, creatorSteamIDString);
                        }
                        else
                            set.RemoveLevel(normalizedAbsoluteLevelPath, levelInfo.levelType_);
                    }
                    else
                    {
                        
                    }
                }
                
            }
            return false;
        }
    }
}
