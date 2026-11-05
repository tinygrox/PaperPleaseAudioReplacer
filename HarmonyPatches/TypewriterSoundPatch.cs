using Febucci.UI.Examples;
using HarmonyLib;
using UnityEngine;

namespace PaperPleaseAudioReplacer.HarmonyPatches;

// 取消原版的对话打字机音效
[HarmonyPatch]
public class TypewriterSoundPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(TAnimSoundWriter), "OnCharacter")]
    private static bool Prefix()
    {
        return !SpeakerSwitchPatch.SuppressTyping;
    }
}
