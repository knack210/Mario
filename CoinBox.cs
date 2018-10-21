using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour {

    public int coinNumber;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mario"))
        {
            coinNumber = coinNumber - 1;

            if (coinNumber == 0)
            {
                anim.SetTrigger("isEmpty");
            }

        }
    }

}
