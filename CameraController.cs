using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float speed;
    private Scene SampleScene;
    

	void Start ()
    {
        SampleScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        // Put escape here so you can still exit if Mario dies
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SampleScene.buildIndex);
        }
    }

    void LateUpdate ()
    {
        if (System.Math.Abs(transform.position.x - player.transform.position.x) >= 3)
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                new Vector3 (player.transform.position.x, transform.position.y, transform.position.z)
                , speed);
        }
    }
}
