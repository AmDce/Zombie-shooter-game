using System;

public static class GameEvents
{
    public static event Action<int, int> OnAmmoUpdate;
    public static event Action<bool> OnReload;
    public static event Action<bool, string> OnGameOver;

    public static void UpdateAmmo(int currentAmmo, int maxAmmo) => OnAmmoUpdate?.Invoke(currentAmmo, maxAmmo);
    public static void TriggerReload(bool isReloading) => OnReload?.Invoke(isReloading);
    public static void TriggerGameOver(bool active, string message) => OnGameOver?.Invoke(active, message);
}