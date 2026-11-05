using HarmonyLib;
using UnityEngine;

namespace PaperPleaseAudioReplacer.HarmonyPatches;

// 普通顾客和治安部抓人的分别不同音效
[HarmonyPatch]
public static class SpeakerSwitchPatch
{
    internal static bool SuppressTyping { get; private set; }

    [HarmonyPatch(typeof(DialogUIManager), nameof(DialogUIManager.DisplayClientText))]
    [HarmonyPrefix]
    public static void ClientPrefix()
    {
        PlayOnce("speech-entrant");
    }

    [HarmonyPatch(typeof(DialogUIManager), nameof(DialogUIManager.DisplaySecText))]
    [HarmonyPrefix]
    public static void SecPrefix()
    {
        PlayOnce("speech-out");
    }

    private static void PlayOnce(string soundKey)
    {
        SuppressTyping = ModMain.Sounds.TryGetValue(soundKey, out AudioClip clip);
        // ModMain.Logger.Log($"SuppressTyping :{SuppressTyping}");
        StoreSoundPlayer.Instance.typewriterAudioSource.PlayOneShot(clip);
    }
}
