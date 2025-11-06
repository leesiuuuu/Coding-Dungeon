using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsuSO", menuName = "Scriptable Objects/EnemyStatsuSO")]
public class EnemyStatusSO : ScriptableObject
{
    public int maxHealth;
    public int attack;
    public int defense;
    public int attackRange;
}
