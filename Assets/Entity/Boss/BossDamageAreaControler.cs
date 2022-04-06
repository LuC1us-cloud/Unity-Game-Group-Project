using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageAreaControler : MonoBehaviour
{
    public enum SpawnState { DAMAGE, WAITING, START};

    public Transform[] damageFields;
    private int nextField = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private SpawnState state = SpawnState.START;

    private Color tranperentGreen;
    private Color tranperentRed;
    private Color tranperentWhite;

    public GameObject boss;
    private Entity entity;

    private void Start()
    {
        tranperentGreen = new Color(0, 1f, 0.05f, 0.2f);
        tranperentRed = new Color(1f, 0, 0, 0.2f);
        tranperentWhite = new Color(1f, 1f, 1f, 0.1f);
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        entity = boss.GetComponent<Entity>();
        if(entity.CurrentHealth <= entity.MaxHealth / 2)
        {
            if (state == SpawnState.WAITING)
            {
                if (waveCountdown >= 0)
                {
                    waveCountdown -= Time.deltaTime;
                    return;
                }
            }

            if (state != SpawnState.DAMAGE)
            {
                StartCoroutine(DamagePhase(nextField));
            }
        }
    }

    // coroutine for spawning enemies in fixed time intervals
    IEnumerator DamagePhase(int field)
    {
        Debug.Log("Damage area " + damageFields[field].name + " " + field);
        state = SpawnState.DAMAGE;

        damageFields[field].gameObject.GetComponent<SpriteRenderer>().color = tranperentRed;


        for (int i = 0; i < damageFields.Length; i++)
        {
            if (i != field)
            {
                damageFields[i].gameObject.GetComponent<SpriteRenderer>().color = tranperentGreen;
            }
        }


        yield return new WaitForSeconds(5f);

        damageFields[field].GetComponent<BossDamageArea>().DamagePlayer(20);

        for (int i = 0; i < damageFields.Length; i++)
        {
            damageFields[i].gameObject.GetComponent<SpriteRenderer>().color = tranperentWhite;
        }

        waveCountdown = timeBetweenWaves;
        state = SpawnState.WAITING;

        if (nextField == damageFields.Length - 1)
        {
            nextField = 0;
        }
        else
        {
            nextField++;
        }

        yield break;

    }

}
