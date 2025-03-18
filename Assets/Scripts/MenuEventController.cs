using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter.MenuEvents
{

    public static class MenuEventController
    {
        public static event Action<bool> TogglePurchasePanel;

        public static void RaisePurchasePanel(bool active) => TogglePurchasePanel?.Invoke(active);
    }

}

