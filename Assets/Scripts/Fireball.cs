using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 16.0f;
    public float timer = 2f;
    public int damage = 10;

    private void Start()
    {
        Destroy(this.gameObject, timer);
    }

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)//Эта функция вызывается когда с триггером сталкивается другой объект
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(damage);
            Destroy(this.gameObject);
        }
    }


}
