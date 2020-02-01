using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/EnemyData", order = 1)]
public class EnemyDataScriptableObject : ScriptableObject
{
    public Vector2 forceDirection;
    public float launchTime;
    public Sprite enemySprite;
    public AudioClip enemySound;

}
