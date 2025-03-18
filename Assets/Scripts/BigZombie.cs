using UnityEngine;
using UnityEngine.UI;

public class BigZombie : ZombieBase
{
    protected override void Start()
    {
        MaxHealth = 120;
        moveSpeed = 0.5f;
        damage = 20;
        attackCooldown = 2.5f;
        base.Start();
    }
}
