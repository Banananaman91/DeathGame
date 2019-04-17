using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextController : MonoBehaviour {

    
    //string pageText = Variableforgameobject.gameObject.GetComponent<Dialogue>().PageText; //pageText recieves text from component with Dialogue type

    Direction currentDir;
    Vector2 input; bool isMoving = false; //What the player is pressing in terms of vertical and horrizontal keys
    Vector3 startPos;
    Vector3 endPos;
    float t;    //for time

    public float walkSpeed = 3f;

    void Update()
    {
        
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))

                input.y = 0;
            else
                input.x = 0;


            if (input != Vector2.zero)
            {
                StartCoroutine(Move(transform));
            }
        }
    }

    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;


        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

        while (t < 1f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;

        yield return 0;
    }

    enum Direction  //An Enum is a string of items that you can sellect between
    {
        North,
        East,
        South,
        West,
    }
}
