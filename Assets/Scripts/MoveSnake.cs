using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnake : MonoBehaviour
{
    public float changeSpeed = 2;
    private float changeTime = 0;
    public Camera camera;
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
        segments[0].transform.localScale = new Vector3(1,1,1);
    }

    // Update is called once per frame
    //new Vector3(-5, 2, 0)
    void Update()
    {
        changeTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            currentDirection *= Quaternion.Euler(0, -90, 0);
            addNewHeadSegment(getHeadPosition());
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            currentDirection *= Quaternion.Euler(0, 90, 0);
            addNewHeadSegment(getHeadPosition());
        }
        segments[segments.Count-1].transform.localScale += new Vector3(speed*Time.deltaTime, 0, 0);
        segments[0].transform.localScale -= new Vector3(speed*Time.deltaTime, 0, 0);
        segments[0].transform.position += segments[0].transform.rotation * new Vector3(2*speed*Time.deltaTime, 0, 0);
        if(segments[0].transform.localScale.x<0){
            Destroy(segments[0]);
            segments.Remove(segments[0]);
        }        
        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(-5, 2, 0), changeTime/changeSpeed);
        camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, Quaternion.Euler(0,90,0), changeTime/changeSpeed);
    }

    void addNewHeadSegment(Vector3 position)
    {
        GameObject newSegment = Instantiate(segmentPrefab, position, currentDirection);
        segments.Add(newSegment);
        newSegment.transform.parent = transform;
        camera.transform.parent = newSegment.transform;
        changeTime = 0;
    }

    Vector3 getHeadPosition()
    {
        return segments[segments.Count-1].transform.position + segments[segments.Count-1].transform.rotation * new Vector3(2*segments[segments.Count-1].transform.localScale.x, 0, 0);
    }
}
