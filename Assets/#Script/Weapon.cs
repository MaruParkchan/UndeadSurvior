using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (GameManager.instance.isLive == false)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();

                }
                break;
        }

        // Test 
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 10);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform; // 플레이어 오브젝트로 들어가야함
        transform.localPosition = Vector3.zero;
        //Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for (int index = 0; index < GameManager.instance.poolManager.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.poolManager.prefabs[index]) // itemData projectile 의 프리팹과 풀 매니저 프리팹과 동일하다면 
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;

            default:
                speed = 0.3f * Character.WeaponRate;
                break;
        }

        // Hand Sprite
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // 플레이어가 가지고 있는 ApplyGear 메소드 다 호출
        // 레벨했을때 기어 호출
        // 웨폰이 생성, 웨폰 업글, 기어 자체 생성, 기어 자체 업글 4번 호출해야함
    }

    void Batch() // 배치 . 물건을 놓다 
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index); // 기존꺼가 있으면 그걸 사용 
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                bullet.parent = transform;
            }
            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVector = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity Per 무한 관통 공격 
        }
    }

    void Fire()
    {
        if (player.scanner.nearestTarget == null)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; // 크기를 1로 변환 
        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
