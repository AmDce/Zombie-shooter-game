using UnityEngine;
using UnityEngine.UI;

public class LittleZombie : ZombieBase
{
    protected override void Start()
    {
        MaxHealth = 50;
        moveSpeed = 1f;
        damage = 5;
        attackCooldown = 1.0f;
        base.Start();
    }
}
