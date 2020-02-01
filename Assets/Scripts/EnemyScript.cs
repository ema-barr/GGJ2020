using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    private Transform EnTrans;

    // Start is called before the first frame update
    void Start()
    {
        EnTrans = GetComponent<Transform>();
        Debug.Log(EnTrans.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet,EnTrans);
        }*/
    }

    public void Attack()
  {
    Instantiate(bullet, EnTrans);
  }
}
