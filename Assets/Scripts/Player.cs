using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int startLife;
    public Transform initRoom;
    int life;
    float h, v;
    Vector2 dir;
    Transform currentRoom;
    private void Start(){
        dir = new Vector2();
        life = startLife;
        currentRoom = initRoom;

    }
    // Update is called once per frame
    void Update(){
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        dir.Set(transform.position.x + h, transform.position.y + v);
        transform.position = Vector2.MoveTowards(transform.position, dir, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"){
            Debug.Log("enemy");
            life--;
            if (life == 0)
                Reset();
        }
        if (collision.tag == "Door"){
            Debug.Log("door");
            currentRoom = collision.transform;
            StartCoroutine(ChangeRoom(collision.transform));
        }
    }

    private void Reset()
    {
        Debug.Log("Reset");
    }

    IEnumerator ChangeRoom(Transform newRoom)
    {
        while (Camera.main.transform.position.x != newRoom.position.x || Camera.main.transform.position.y != newRoom.position.y)
        {
            Vector2 target = Vector2.MoveTowards(Camera.main.transform.position, newRoom.transform.position, 50 * Time.deltaTime);
            Camera.main.transform.position = new Vector3(target.x, target.y, -10);
            yield return new WaitForEndOfFrame();
        }
          
    }
}
