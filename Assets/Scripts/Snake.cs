
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Snake : MonoBehaviour
{
    public GameObject segmentPrefab;
    public Vector3 position;

    [SerializeField]
    private List<Segment> segments;
    public float speed;

    public float expectedLength = 0.5f;
    public Camera mainCamera;

    [SerializeField]
    private Quaternion currentDirection;

    internal void AddLength(float toAdd)
    {
        expectedLength += toAdd;
    }

    public float changeTime = 0; 
    public float changeSpeed = 2; 

    // Start is called before the first frame update

    private Segment Head{
        get{
            return segments[segments.Count - 1];
        }
    }

    private Segment Tail{
        get{
            return segments[0];
        }
    }
    void Start()
    {
        currentDirection = Quaternion.identity;
        segments = new List<Segment>();
        AddNewHead();
        changeTime = 0;//changeSpeed;
    }

    private void AddNewHead()
    {
        Vector3 newPos;
        if(segments.Count == 0){
            newPos = position;
        }else{
            Segment currHead = Head;
            newPos = currHead.GetHeadPosition();
        }
        Segment head = Instantiate(segmentPrefab, newPos, currentDirection).GetComponent<Segment>() ;
        head.snake = this;
        head.transform.parent = transform;
        mainCamera.transform.parent = head.transform;
        changeTime = 0;
        segments.Add(head);
    }

    internal float GetRealLengthDifference()
    {
        return expectedLength - segments.Sum(segment => segment != null ? segment.GetCurrentLength() : 0);
    }

    internal void DestroySegment(Segment segment)
    {
        segments.Remove(segment);
        Destroy(segment.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        bool turn = false;
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            turn = true;
            currentDirection *=  Quaternion.Euler(0, -90, 0);
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            turn = true;
            currentDirection  *=  Quaternion.Euler(0, 90, 0);
        }
        if(turn){
            AddNewHead();
            Head.transform.Find("scalePivot").transform.localScale= new Vector3(0,1,1);
        }
        changeTime += Time.deltaTime;
        mainCamera.transform.localRotation = Quaternion.Slerp(mainCamera.transform.localRotation, Quaternion.Euler(0,90,0), changeTime/changeSpeed);
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, new Vector3(-5,2,0),changeTime/ changeSpeed);
    }

    internal bool isTail(Segment segment){
        return segments.IndexOf(segment) == 0;
    }

        internal bool isHead(Segment segment){
        return segments.IndexOf(segment) == segments.Count - 1;
    }

}
