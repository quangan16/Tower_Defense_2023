using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDemoEnemy : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(inputX, 0, inputY) * speed);;
        
    }
}
