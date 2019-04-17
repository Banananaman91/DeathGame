using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Linq;

public class Test : MonoBehaviour {
    Direction currentDir;
    Vector2 input;
    public Vector2 rayDir;
    bool isMoving = false;
    Vector3 startPos;
    Vector3 endPos;
    float t; //wtf is this

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public float walkSpeed = 3f;

    public Tilemap wallTilemap; //thank FUCK
    public Tilemap hiddenWallTilemap;
    public Tilemap hiddenRoomTilemap;

    [SerializeField] Camera main, hidden;


    void Start()
    {
        hidden.enabled = false;
    }

    void Update()
    {
        if (!isMoving) //wtf invert this
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if (input != Vector2.zero) //wtf invert this too
            {

                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch (currentDir)
                {
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        rayDir = Vector2.up;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        rayDir = Vector2.right;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        rayDir = Vector2.down;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        rayDir = Vector2.left;
                        break;
                }

                StartCoroutine(Move(transform));
            }

        }

        bool inHiddenRoom = getCell(hiddenRoomTilemap, gameObject.transform.position) != null;

        if (inHiddenRoom)
        {
            main.enabled = false;
            hidden.enabled = true;
        }
        else
        {
            main.enabled = true;
            hidden.enabled = false;
        }
    }

    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;
        

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);
        bool isWall = getCell(wallTilemap, endPos) != null;
        bool isHiddenWall = getCell(hiddenWallTilemap, endPos) != null;

        if (!isWall && !isHiddenWall)
        {
            while (t < 1f)
            {
                t += Time.deltaTime * walkSpeed;
                entity.position = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }
            var resultColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); //this is where the fix starts
            foreach (var collider in resultColliders)
            {
                var component = collider.GetComponent<Floortransition>();
                if (component != null)
                {
                    component.MovePlayerToEndpoint();
                }
            }
        }

        isMoving = false;
    }

    enum Direction
    {
        North,
        East,
        South,
        West
    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

}
