using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area")) // Area 태그가 아니면
            return;

        Vector2 playerPosition = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;
        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        Vector3 playerDirection = GameManager.instance.player.inputVector;
        float directionX = playerDirection.x < 0 ? -1 : 1;
        float directionY = playerDirection.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if(diffX > diffY) // x축 이동
                {
                    transform.Translate(Vector3.right * directionX * 40); // 2칸 가기 때문에 40 
                } 
                else if(diffX <diffY)
                {
                    transform.Translate(Vector3.up * directionY * 40);
                }
            break;

            case "Enemy":

            break;
        }
    }
}
