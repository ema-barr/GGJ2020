using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    [SerializeField]
    private int initialHealth = 1;

    private float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
                health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

            }

    public void InitializeHealth()
    {
        health = (float)initialHealth;
    }
}
