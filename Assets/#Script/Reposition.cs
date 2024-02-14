using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area")) // Area 태그가 아니면
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
                float diffY = Mathf.Abs(playerPosition.y - myPosition.y);
                float directionX = diffX < 0 ? -1 : 1;
                float directionY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                if (diffX > diffY) // x축 이동
                {
                    transform.Translate(Vector3.right * directionX * 40); // 2칸 가기 때문에 40 
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * directionY * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled)
                {   
                   Vector3 dist = playerPosition - myPosition;
                   Vector3 ran = new Vector3(Random.Range(-3, 3),Random.Range(-3, 3),0);
                    transform.Translate(ran + dist * 2 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }

                break;
        }
    }
}
