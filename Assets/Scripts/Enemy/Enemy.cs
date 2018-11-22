using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected int health;
    [SerializeField] protected float speed;

    [Header("Waypoints")]
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    [Header("Loot")]
    [SerializeField] protected int gems;
    public GameObject diamondPrefab;

    protected Vector3 target;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected bool isDead = false;
    protected Player player;
    protected float distance;
    protected Vector3 direction;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        target = pointB.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        if (!isDead)
        {
            Move();  // Move if not dead
        }
    }

    public virtual void Move()
    {

        sprite.flipX = target == pointA.position ? true : false;

        if (transform.position == pointA.position)
        {
            target = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            target = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        // Set enemy out of combat if player is not within distance
        CheckDistance();

        // Face enemy toward player if in combat
        FaceEnemy();

    }

    private void CheckDistance()
    {
        distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
    }

    private void FaceEnemy()
    {
        direction = player.transform.localPosition - transform.localPosition;

        if (anim.GetBool("InCombat"))
        {
            if (direction.x < 0)
            {
                // Face left
                sprite.flipX = true;
            }
            else if (direction.x > 0)
            {
                // Face right
                sprite.flipX = false;
            }
        }
    }
}
