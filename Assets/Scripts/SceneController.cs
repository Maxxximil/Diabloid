using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;//Спеацилизированная переменная для связи с объктом-шаблоном
    private GameObject _enemy;//Переменная для слежения за экземпляром врага в сцене


    void Update()
    {
        if (_enemy == null)//Создание нового врага, если врагов в сцене нет
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;//Метод копирующий объект шаблон
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
    }
}
