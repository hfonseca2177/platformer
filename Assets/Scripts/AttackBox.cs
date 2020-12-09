using UnityEngine;

public class AttackBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(Player.Instance.attackPower);
        }
    }
}
