using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private Transform pivotPoint;
    private float speed = 0.5f;
    // Start is called before the first frame update

    internal Snake snake;
    private Transform segmentHead;
    
    
    private Transform segmentTail;
    void Start()
    {
        pivotPoint = transform.Find("scalePivot");
        segmentHead = transform.Find("segmentHead");
        segmentTail = transform.Find("segmentTail");
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Time.deltaTime * speed;
        
        //Tail.transform.Find("scalePivot").transform.localScale -= new Vector3(Time.deltaTime * speed, 0, 0);
        //Tail.transform.Find("scalePivot").transform.localPosition += new Vector3(Time.deltaTime * speed, 0, 0);
        if(snake.isHead(this)){
            pivotPoint.localScale += new Vector3(diff, 0, 0);
        }
        if(snake.isTail(this)){
            float snakeLengthDifference = snake.GetRealLengthDifference();
            float toChange = Math.Max(diff - snakeLengthDifference, 0);
            pivotPoint.localScale -= new Vector3(toChange, 0, 0);
            transform.localPosition +=  transform.rotation * new Vector3(2*toChange, 0, 0) ;
            if(pivotPoint.localScale.x<=0){
                snake.DestroySegment(this);
            }
        }
        segmentHead.position = CalculateHeadPosition();
        segmentTail.position = transform.position;
    }

    internal float GetCurrentLength()
    {   
        if(pivotPoint != null){
            return pivotPoint.localScale.x;
        }
        return 0;
    }

    internal Vector3 CalculateHeadPosition()
    {
        return transform.position + transform.rotation * new Vector3(2 * pivotPoint.localScale.x, 0, 0);

    }

    internal Vector3 GetHeadPosition()
    {
        return segmentHead.position;
    }

}
