using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameCore _GameCore;

    public float CurHP;
    public float MaxHP;


    public void Init(GameCore GameCore) {
        _GameCore = GameCore;
        GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/Spawn"), transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 2);
    }

    void Update() {
        if (_GameCore == null) {
            return;
        }
        if (!_GameCore.inited) {
            return;
        }
        transform.Translate(-Vector3.forward * _GameCore.enemyspeed * Time.deltaTime);

        if (transform.position.z <= 0.0f) {
            _GameCore.MissEnemy++;
        }
    }


    public void Shot(float damage) {
        GameObject effectD = Instantiate(Resources.Load("Prefabs/Effects/Damage"), transform.position, Quaternion.identity) as GameObject;
        Destroy(effectD, 2);
        CurHP -= damage;
        if (CurHP <= 0.0f) {
            GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/Explosion"), transform.position, Quaternion.identity) as GameObject;
            _GameCore._SoundsSource.PlayOneShot(_GameCore.Sounds[1]);

            if (_GameCore.Enemyes.Contains(this)) {
                _GameCore.Enemyes.Remove(this);
            }
            
            Destroy(effect, 2);
            Destroy(gameObject);
            _GameCore.KilledEnemy++;
            _GameCore._GlobalUI._PanelGame.AddScore();
        }

    }

    public void DestroyEnd() {
        GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/Explosion"), transform.position, Quaternion.identity) as GameObject;
        _GameCore._SoundsSource.PlayOneShot(_GameCore.Sounds[1]);
        Destroy(effect, 2);
        Destroy(gameObject);
    }

}
