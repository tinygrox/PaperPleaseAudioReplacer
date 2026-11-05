using HarmonyLib;
using UnityEngine;

namespace PaperPleaseAudioReplacer.HarmonyPatches;

// 报告治安部警告
[HarmonyPatch]
public class EmporiumAudioPlayerPatch
{
    [HarmonyPatch(typeof(EmporiumAudioPlayer), "Start")]
    [HarmonyPostfix]
    public static void AlarmPostfix(EmporiumAudioPlayer __instance)
    {
        if (!ModMain.Sounds.TryGetValue("border-callguards", out AudioClip clip))
        {
            return;
        }

        __instance.alarm2.clip = clip;
    }
}
