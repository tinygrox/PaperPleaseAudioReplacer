using HarmonyLib;
using UnityEngine;

namespace PaperPleaseAudioReplacer.HarmonyPatches;

// 下一位
[HarmonyPatch]
public class StoreBellRingPatch
{
    [HarmonyPatch(typeof(StoreBell), nameof(StoreBell.RingBell))]
    [HarmonyPrefix]
    public static void RingBellPrefix(StoreBell __instance)
    {
        if (!ModMain.Sounds.TryGetValue("speech-announce", out AudioClip clip))
        {
            return;
        }

        __instance.audioSource.clip = clip;
    }
}
