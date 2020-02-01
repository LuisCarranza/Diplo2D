using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public SpriteRenderer enemyRenderer;
    public EnemyDataScriptableObject enemyData;
    void Start()
    {
        enemyRenderer.sprite = enemyData.enemySprite;
        Debug.Log(enemyData.forceDirection);
        Debug.Log(enemyData.launchTime);
    }
}
