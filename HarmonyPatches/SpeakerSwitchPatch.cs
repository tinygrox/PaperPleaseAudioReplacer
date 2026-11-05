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
        // 只要是跟玩家对话就用普通语气
        if (DialogUIManager.Instance.isSecMonologuePlaying)
        {
            PlayOnce("speech-entrant");
            return;
        }

        // 抓卢时大气也不敢喘 可惜 pp 没有合适的音效，只能用普通语气代替
        if (PlayerStore.Instance.currentClientInstance.GetClientBlueprint().identifier == "wanted5")
        {
            PlayOnce("speech-entrant");
            return;
        }

        // 其他时候一概用抓人语气
        PlayOnce("speech-out");
    }

    private static void PlayOnce(string soundKey)
    {
        SuppressTyping = ModMain.Sounds.TryGetValue(soundKey, out AudioClip clip);
        // ModMain.Logger.Log($"SuppressTyping :{SuppressTyping}");
        StoreSoundPlayer.Instance.typewriterAudioSource.PlayOneShot(clip);
    }
}
