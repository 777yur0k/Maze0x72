using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform attackPos;
    public float attackRangeX = 1f, attackRangeY = 1f, cooldown = 0.5f;
    float lastSwing;

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0 && Time.time - lastSwing > cooldown) Attack();
    }

    void SetAttackTrigger() => GetComponent<Animator>().SetTrigger("Attack");

    void Attack()
    {
        lastSwing = Time.time;
        SetAttackTrigger();
        Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemiesHit.Length; i++) enemiesHit[i].GetComponent<EnemyBehaviour>().GetHit();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY));
    }
}