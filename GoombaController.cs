using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    public float speed;

    public Transform wallHitBox;
    public float wallHitWidth;
    public float wallHitHeight;
    private bool wallHit;
    public LayerMask isGround;

    void Start ()
    {
		
	}
	
	void Update ()
    {
       
	}

    private void LateUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }
    }

}
