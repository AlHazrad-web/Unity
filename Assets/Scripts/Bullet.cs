using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 _pos;
    Enemy _Enemy;
    float _damage;
    public void Shot(Vector3 pos, Enemy enemy, float damage) {
        _pos = pos;
        _Enemy = enemy;
        _damage = damage;
    }

 
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, _pos, 200.0f * Time.deltaTime);
        if (Vector3.Distance(transform.position, _pos) <= 0.05f) {
            if (_Enemy != null) {
                _Enemy.Shot(_damage);
            }
            Destroy(gameObject);
        }
    }


}
