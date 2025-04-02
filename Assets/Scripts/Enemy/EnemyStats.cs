using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemies/New Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;

    [Header("Vitals")]
    public float maxHealth;
    public float moveSpeed;

    [Header("Combat")]
    public float damage;
    public float attackCooldown;
    public float detectionRange;

    [Header("Rewards")]
    public int goldRewarded;
}
