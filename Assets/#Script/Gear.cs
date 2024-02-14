using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear(); // 레벨업이 되었으면 다시 호출 업그레이드
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>(); // 플레이어가 가진 모든 무기를 업글해야하기 때문에 가져와야함
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = 150 + (150 * rate); // 근거리 무기
                    break;
                default:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = 0.5f * (1f - rate); // 원거리 무기 속도 
                    break;
            }
        }

    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.moveSpeed = speed + speed * rate;
    }

}
