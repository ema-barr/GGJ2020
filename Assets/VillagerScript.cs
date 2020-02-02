using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
  [SerializeField]
  private int initialHealth = 1;

  [SerializeField]
  private Signal villagerDeath;

  [SerializeField]
  private ElementToRem villagerToRem;


    [SerializeField]
    private Sprite[] alive;
    [SerializeField]
    private Sprite[] deadAndHappyHead;
    [SerializeField]
    private Sprite[] justDead;

    private int villagerNumber;



  private SpriteRenderer villagerSprite;

  [SerializeField]
  private float timeHeadStaysUpWhenDeathComes = 2;

  private float health = 1;

  private bool isAlreadyDead = false;

  // Start is called before the first frame update
  void Start()
  {
        villagerNumber = (int)Random.Range(0, alive.Length);
    InitializeHealth();

        villagerSprite = GetComponentInChildren<SpriteRenderer>();
        villagerSprite.sprite = alive[villagerNumber];
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

      villagerToRem.go = this.gameObject;
      villagerDeath.Raise();
      if (!isAlreadyDead)
      {
        //call iEnumerator "DeathAnimation"
        StartCoroutine("DeathAnimation");
        //            Destroy(this.gameObject);
      }
    }

  }

  public void InitializeHealth()
  {
    health = (float)initialHealth;
  }


  private IEnumerator DeathAnimation()
  {
    isAlreadyDead = true;
    
        print("I'm Dying, you cruel world!");
        //head pops up and collider fucks off
        villagerSprite.sprite = deadAndHappyHead[villagerNumber];
        transform.localScale += new Vector3(+0.3f, +0.3f, +0.3f);
        
            yield return new WaitForSeconds(timeHeadStaysUpWhenDeathComes);
            print("I'm dead, you cunt!");
        //dead body pops up
        villagerSprite.sprite = justDead[villagerNumber];
        villagerSprite.color = new Vector4(1f, 1f, 1f, 0.75f);
        transform.localScale -= new Vector3(+0.3f, +0.3f, +0.3f);


    yield return new WaitForSeconds(timeHeadStaysUpWhenDeathComes);
    print("I'm dead, you cunt!");
    //dead body pops up
    villagerSprite.sprite = justDead;
    villagerSprite.color = new Vector4(1f, 1f, 1f, 0.75f);
    transform.localScale -= new Vector3(+0.3f, +0.3f, +0.3f);


  }


}

