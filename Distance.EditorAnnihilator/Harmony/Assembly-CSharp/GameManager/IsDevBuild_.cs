using HarmonyLib;

namespace Mod.EditorAnnihilator.Harmony
{
	[HarmonyPatch(typeof(GameManager), "IsDevBuild_", MethodType.Getter)]
	internal static class GameManager__IsDevBuild_get
	{
		[HarmonyPostfix]
		internal static void Postfix(ref bool __result)
		{
			if (!Mod.CNF)
            {
				if (Mod.DevBuildForCreatorName)
				{
					__result = true;
				}
				Mod.DevBuildForCreatorName = false;
			}
		}
	}
}