using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 180;
    void OnTriggerEnter(Collider other){
        Segment segment = other.transform.parent.GetComponent<Segment>();
        if(segment != null){
            segment.snake.AddLength(0.5f);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
