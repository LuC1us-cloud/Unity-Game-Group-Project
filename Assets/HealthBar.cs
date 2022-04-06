using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;
    private float hp;
    public GameObject boss;
    private Entity entity;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        entity = boss.GetComponent<Entity>();
        hp = (float)entity.CurrentHealth / (float)entity.MaxHealth;
        localScale.x = hp;
        Debug.Log(localScale.x);
        transform.localScale = localScale;
    }
}