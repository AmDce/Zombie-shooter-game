using System.Collections;
using UnityEngine;
using ZombieShooter.Player;
using ZombieShooter.PlayerController;

public class ZombieBase : CharacterBase
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public int damage;
    [SerializeField] public float attackCooldown;
    [SerializeField] public float attackRange = 1.5f;

    [SerializeField] Animator animator;

    private Transform player;
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else if (canAttack)
            {
                Attack();
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"hit : {collision.gameObject.name}");
        if (collision.collider.CompareTag("Bullet"))
        {
            base.TakeDamage(30);
            collision.gameObject.SetActive(false);
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public override void Attack()
    {
        StartCoroutine(Attacks());
    }

    private IEnumerator Attacks()
    {
        canAttack = false;
        Debug.Log($"{gameObject.name} attacked!");
        animator.SetBool("Attack", true);
        PlayerHandler playerHandler = player.GetComponent<PlayerHandler>();
        if (playerHandler != null)
        {
            playerHandler.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public override void Die()
    {
        animator.SetBool("Attack", false);
        Debug.Log($"{gameObject.name} died!");
        PlayerModular.Instance.AddKill();
        gameObject.SetActive(false);
    }
}
