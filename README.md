# EditorAnnihilator. ->
<br/>
Things this adds:  (Things with a * are toggleable in the mod options menu.)<br/>
<br/>
*-All modes (Even hidden ones.).<br/>
<br/>
-Unbounded numbers (You can even enter 1/0 for Infintity, -1/0 for -Infinity, and 0/0 for NaN.). (Some numbers are bounded, but they are that way because they don't carry over to people without the mod.)<br/>
<br/>
-Medal times can be in any order. (The Diamond time can be higher than the gold time, for example.)<br/>
<br/>
-Medal times are unbounded. (Their "minutes" can exeed 59 now, allowing for hour long medal times.)<br/>
<br/>
-Weird medal times. (You can put a negative sign at the start of the time to make it negative. Enter 'I' for infinity, '-I' for negative infinity, and 'N' for NaN medal times.)<br/>
<br/>
*-Level is savable no matter what. (Challenge mode can now have checkpoints, for example. Reverse tag with no reverse tag bubble, sprint with no end zone, ect.)<br/>
<br/>
*-Can place multiple level editor car spawners and multiple tag bubbles.<br/>
<br/>*-Hidden objects (which were not in the dev folder or anywhere else) are now useable in a new editor folder titled "Hidden". (Toast object is in here.)<br/>
<br/>-Can apply any component to any object. (Animators can go on dropper drones now, for example.)<br/>
<br/>-The spline replacement tool now has a menu that includes all splines. (Before, it didn't have KillGridSpline available, for example.)<br/>
<br/>*-For objects with sub-textures that are normally all grouped together, you can enable "SAME NAME MATERIAL SEPARATION" in the mod options, which will separate these in the editor menu so that you can edit them individually. (Allowing you to color all the shard fragments of an Archaic individually, for example.)<br/>
<br/>-Credits orbs will now not cause the level to be corrupted when placed!<br/>
<br/>-Improves the precision of decimal values in the editor menu. (Option in mod options to modify the precision value.)<br/>
<br/>*-Option to make background layers not collide with the cursor when clicking to move the cursor.<br/>
<br/>-Reveals some otherwise hidden components on objects. (For example, the kill grid "trigger box" on a kill grid box is a component that was otherwise hidden, but now it is not.)<br/>
<br/>-You can select sub objects. To do this, title the level $$##0$$, select and object and it will select the first child object of that object. $$##1$$ will select the second, $$##2$$ the third and so on. To select a sub object of a child object, use a comma. For example $$##1,0$$ would allow you to select the first child object of the second child object of the main object (if the object you select has such an object). Be careful with this one utility. While it can be exceptionally powerful, do not add any componets to a sub object. It will cause the level to be corrupted. You can however, translate these sub objects, rotate them, scale them, and modify their existing parameters. Just be careful with it though. I recomend creating a "dummy level" where you can make and modify objects in this way, save the level, and then check if it corrupted the dummy level or not by saving and exiting and then re-entering the level. If it didn't corrupt, then it's safe to use in a real level, and all you have to do is copy and paste it over.<br/>
<br/>*-Adds option to edit the hidden 'Level Creator Name' field in the level settings. (Note: Will not work if you use a version of the Editor Additions mod by Reherc that is not the latest.)<br/>
<br/>
Does not:<br/>
<br/>-Enable Dev folder.<br/>
<br/>-Add missing music to the music menus.<br/>
<br/>-Do anything to effect the physics or gameplay of the game.<br/>
<br/>-Crash your game upon scaling splines (Not in my experience at least.).<br/>
<br/>

# Fixes from the version pinned in the Discord:
<br/>
<br/>-Doesnt spew random Logger things in the console that I used to debug like it did before.<br/>
<br/>-Doesnt spew a million lines of "No level set for ______ mode, please add it to the LevelSetsManager." in the console anymore upon starting the game up.<br/>
<br/>-Added toggle in the mod options for the hidden object folder and mode visibility.<br/>
<br/>-Fixed a weird effect of the child object selector functionality (Thanks to Plamsa for showing this to me.).<br/>
<br/>-Added toggle in the mod options for the visibility of hidden components.<br/>
<br/>-Added option to edit 'Level Creator Name' field.<br/>
<br/>-Did a lot of cleaning up and getting rid of unused files.<br/>
