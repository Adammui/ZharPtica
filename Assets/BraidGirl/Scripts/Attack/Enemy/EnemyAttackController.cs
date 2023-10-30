using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField]
    private Collider _weaponCollider;

    public void OnDeath()
    {
        _weaponCollider.enabled = false;
    }
}
