using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 changeMovement;
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private float speed = 1;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetMovement();
        if (changeMovement != Vector3.zero)
        {
            Move();
        }
    }

        private void GetMovement()
    {
        changeMovement = Vector3.zero;
        changeMovement.y = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        changeMovement.Normalize();
        myRigidbody.MovePosition(
                transform.position + changeMovement * speed * Time.deltaTime);

        //Play Animation
        changeMovement.x = Mathf.Round(changeMovement.x);
        changeMovement.y = Mathf.Round(changeMovement.y);
    }
}
