using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform attackPos;
    [SerializeField] float attackRangeX = 1f, attackRangeY = 1f, cooldown = 0.5f;
    float lastSwing = 0;
    bool isFlipped = true;

    void Start() => animator = GetComponent<Animator>();

    void Update()
    {
        if (Time.time - lastSwing < cooldown) return;

        if (Input.GetAxisRaw("Fire1") > 0) Attack();
    }

    void SetAttackTrigger()
    {
        if (isFlipped)
        {
            animator.SetTrigger("FlippedSwingTrigger");
            return;
        }

        animator.SetTrigger("SwingTrigger");
    }

    void Attack()
    {
        lastSwing = Time.time;
        SetAttackTrigger();
        Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemiesHit.Length; i++) enemiesHit[i].GetComponent<EnemyBehaviour>().GetHit();
    }

    public void setFlipped(bool flippedValue) => isFlipped = flippedValue;

    void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY));
    }
}