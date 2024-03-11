using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0.5f;
    private const float REACHED_DIST = 0.02f;
    private bool _moving;
    public bool Moving => _moving;

    public void Move(List<Vector2Int> path)
    {
        if (path.Count > 0 )
        {
            _moving = true;
            var lastGoal = path[^1];
            if (Vector3.Distance(transform.position, new Vector3(lastGoal.x, lastGoal.y)) > REACHED_DIST) 
            {
                StartCoroutine(MoveToGoal(path));
            }
        }
    }


    private IEnumerator MoveToGoal(List<Vector2Int> pointPos)
    {
        int goalReached = 0;
        Vector3 mostRecent = new Vector3();
        Vector3 currentPoint = new Vector3(pointPos[0].x, pointPos[0].y);
        

        while (true)
        {
            //If reached current target cell
            if (Vector3.Distance(transform.position, currentPoint) < REACHED_DIST)
            {
                //If this target cell is different from previous one
                if (mostRecent == Vector3.zero || (currentPoint != mostRecent))
                {
                    //Increase index to move on next cell
                    goalReached++;

                    if (goalReached >= pointPos.Count) yield break;

                    //Update most recent target cell
                    mostRecent = currentPoint;
                    currentPoint = new Vector3(pointPos[goalReached].x, pointPos[goalReached].y);
                    
                    Vector2 rotDir = currentPoint - transform.position;
                    float angle = Mathf.Atan2(rotDir.y, rotDir.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                }
            }

            Vector3 direction = transform.position - currentPoint;
            direction.Normalize();
            Vector3 pos = transform.position - direction * _movementSpeed * Time.deltaTime;
            transform.position = pos;

            yield return null;
        }
    }
}