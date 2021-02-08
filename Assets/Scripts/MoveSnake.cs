using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnake : MonoBehaviour
{
    public GameObject segmentPrefab;
    public int speed;
    private List<GameObject> segments;
    private Quaternion currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        currentDirection = Quaternion.identity;
        addNewHeadSegment(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            currentDirection *= Quaternion.Euler(0, -90, 0);
            addNewHeadSegment(getHeadPosition());
        }
        segments[segments.Count-1].transform.localScale += new Vector3(speed*Time.deltaTime, 0, 0);
        
    }

    void addNewHeadSegment(Vector3 position)
    {
        GameObject newSegment = Instantiate(segmentPrefab, position, currentDirection);
        segments.Add(newSegment);
        newSegment.transform.parent = transform;
    }

    Vector3 getHeadPosition()
    {
        return segments[segments.Count-1].transform.position + new Vector3(segments[segments.Count-1].transform.localScale.x, 0, 0);
    }
}
