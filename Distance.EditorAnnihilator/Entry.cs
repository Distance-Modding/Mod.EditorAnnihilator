using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Events.MainMenu;
using Events.QuitLevelEditor;
using HarmonyLib;
using LevelEditorTools;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Mod.EditorAnnihilator
{
    [ModEntryPoint("com.github.PredatoryBalloon/Distance.EditorAnnihilator")]
    public class Mod : MonoBehaviour
    {
        public static Mod Instance;

        public IManager Manager { get; set; }

        public static Log Logger { get; private set; }

        public static ConfigurationLogic Config { get; private set; }

        public static bool ModEnabled { get; set; }

        public static bool SpecialObj { get; set; }

        public static string SpecialObjName { get; set; }

        public static bool HasExitedEditor { get; set; }

        public static bool PrintResourceDict { get; set; }

        public static bool DevBuildForCreatorName { get; set; }

        public void Initialize(IManager manager)
        {
            Instance = this;
            Logger = LogManager.GetForCurrentAssembly();
            Manager = manager;
            Config = gameObject.AddComponent<ConfigurationLogic>();
            SpecialObj = false;
            SpecialObjName = "";
            HasExitedEditor = false;
            PrintResourceDict = false;
            DevBuildForCreatorName = false;

            CreateSettingsMenu();

            RuntimePatcher.AutoPatch();


            
            
        }

        public void CreateSettingsMenu()
	    {
            MenuTree settingsMenu;
            settingsMenu = new MenuTree("menu.mod.EditorAnnihilator", "Editor Annihilator Settings")
            {
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_mode_req", "ENABLE MODE REQUIREMENTS")
            .WithGetter(() => Config.ModeReq)
            .WithSetter((x) => Config.ModeReq = x)
            .WithDescription("Enables the requirements set for saving a level with certain modes under certain conditions (Reverse tag with no bubble, 2 start zones, ect.)")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_dpmt1c", "DISABLE PLACING MORE THAN 1 CARSPAWNER")
            .WithGetter(() => Config.dpmt1c)
            .WithSetter((x) => Config.dpmt1c = x)
            .WithDescription("Disables the ability to place more than one carspawner.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_snms", "ENABLE SAME NAME MATERIAL SEPARATION")
            .WithGetter(() => Config.SNMS)
            .WithSetter((x) => Config.SNMS = x)
            .WithDescription("Enables the separation of materials with the same name on an object in the editor menus. This is good for things like coloring the individual shards of an archaic object, for example.")
            ,
            new Centrifuge.Distance.GUI.Controls.IntegerSlider(MenuDisplayMode.MainMenu, "setting:enable_disable_edp", "SET EDITOR DECIMAL PRECISION")
            .WithGetter(() => Config.edp)
            .WithSetter((x) => Config.edp = x)
            .LimitedByRange(0,10)
            .WithDescription("Sets the precision of decimals displayed in the editor. Range 0-10.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_ccwbl", "DISABLE CURSOR COLLIDING WITH BACKGROUND LAYERS")
            .WithGetter(() => Config.ccwbl)
            .WithSetter((x) => Config.ccwbl = x)
            .WithDescription("Disables the editor cursor colliding with background layers.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_hf", "ENABLE HIDDEN FOLDER")
            .WithGetter(() => Config.hf)
            .WithSetter((x) => Config.hf = x)
            .WithDescription("When enabled, adds a folder titled \"Hidden\" to the level editor that contains otherwise hidden objects, which are not found anywhere else; Not even in the dev folder.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_em", "DISABLE EXTRA MODES")
            .WithGetter(() => Config.em)
            .WithSetter((x) => Config.em = x)
            .WithDescription("Disables the extra modes. Might be a good idea cause what use are they anyways? Keeps Trackmog though, since that\'s a real thing.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_voic", "ENABLE VISIBILITY OF IGNORED COMPONENTS")
            .WithGetter(() => Config.voic)
            .WithSetter((x) => Config.voic = x)
            .WithDescription("Enables the visibility of components that normally are ignored. The box trigger on a killgridbox for example.")
            ,
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_voic", "DISABLE VISIBILITY OF CREATOR NAME FIELD")
            .WithGetter(() => Config.cnf)
            .WithSetter((x) => Config.cnf = x)
            .WithDescription("Disables the visibility of the Level Creator Name field in the level settings. Maybe a good idea, since the game only uses that field in the event that your level is an official tier Community level.")
            };

        Menus.AddNew(MenuDisplayMode.Both, settingsMenu, "Editor Annihilator", "Settings for the Editor Annihilator mod.");
        }

        public static bool ModeReq => Config.ModeReq;
        public static bool AnimUnbounded => Config.AnimUnbounded;
        public static bool SMNS => Config.SNMS;
        public static int EDP => Config.edp;
        public static bool CCWBL => Config.ccwbl;
        public static bool DPM1C => Config.dpmt1c;
        public static bool HF => Config.hf;
        public static bool EM => Config.em;
        public static bool VOIC => Config.voic;
        public static bool CNF => Config.cnf;

        private void OnMainMenuInitialized(Initialized.Data data)
        {
            Resource.CreateLevelEditorPrefabDirInfo();
        }

        private void OnMainMenuInitialized2(Quit.Data data)
        {
            Resource.CreateResourceList();
        }

        public static bool CheckWhichIsHiddenFolder(LevelPrefabFileInfo info)
        {
            if((info.name_.IsNullOrWhiteSpace() ? "null" : info.name_).Equals("Hidden"))
            {
                return true;
            }
            return false;
        }
    }
}