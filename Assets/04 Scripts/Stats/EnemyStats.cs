using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
    EnemyController enemyController;

    public HealthBar healthBar;
    public Text armorText;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();

        armorText.text = armor.getValue().ToString();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        healthBar.SetHealth(currentHealth);
    }

    public override void Die()
    {
        base.Die();

        // death animation
        enemyController.OnDeath();

        // additional
        healthBar.TurnOff();
        QuestManager.instance.EnemyKilled();

        // loot

        //StartCoroutine(DeathTimer(5f));
    }

    IEnumerator DeathTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
