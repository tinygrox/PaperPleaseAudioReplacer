using System.Collections.Generic;
using UnityEngine;

namespace PaperPleaseAudioReplacer;

public class ModMain : IMod
{
    public static ModLog Logger { get; private set; }
    public static string Id { get; private set; }
    public static Dictionary<string, AudioClip> Sounds { get; private set; } = new Dictionary<string, AudioClip>();

    public void Init(ModManifest manifest)
    {
        Id = manifest.ID;
        Logger = new ModLog(manifest);
        Logger.Log("Mod Initialized");
    }

    public void OnEnable()
    {
        ModHook.OnModAssetsLoaded += OnModAssetsLoaded;
    }

    public void OnDisable()
    {
    }

    private void OnModAssetsLoaded(LoadedMod mod)
    {
        if (mod.Manifest.ID != Id) return;
        Sounds = mod.Sounds;
        // Logger.Log($"Sounds loaded: {Sounds.Count}");
        ModHook.OnModAssetsLoaded -= OnModAssetsLoaded;
    }
}
