using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Object", order = 1)]
public class DestroyableObject : ScriptableObject
{
    public int id;
    public string Name;
    public string Description;
    public float MaxHealth;
    public float Damage;
    public float attackSpeed;
}
