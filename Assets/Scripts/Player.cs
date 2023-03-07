using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameCore _GameCore;

    public float speed;
    public float rotspeed;
    Quaternion rot;
    public GameObject target;

    public GameObject gun;

   public void Init(GameCore GameCore) {
        _GameCore = GameCore;
        target = new GameObject("target");
        speed = 0.2f;
        rotspeed = 5.0f;       
    }

    void Update() {
        if (_GameCore == null) {
            return;
        }
        if (!_GameCore.inited) {
            return;
        }
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50.0f);
        Vector3 worldPosition = _GameCore._Camera.ScreenToWorldPoint(mousePosition);
        target.transform.position = worldPosition;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rot = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotspeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            PlayerShor(worldPosition);
        }
    }

    void PlayerShor(Vector3 worldPosition) {
        GameObject bullet = Instantiate(Resources.Load("Prefabs/Effects/Bullet"), gun.transform.position, Quaternion.identity) as GameObject;
        Bullet _Bullet = bullet.GetComponent<Bullet>();
        _GameCore._SoundsSource.PlayOneShot(_GameCore.Sounds[0]);
        RaycastHit[] hits;
        Ray ray = _GameCore._Camera.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 100.0f);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider.gameObject.tag == "Enemy") {
                _Bullet.Shot(worldPosition, hits[i].collider.gameObject.GetComponent<Enemy>(), 40.0f);
                return;
                //Debug.Log("Hit");                  
            }
        }
        _Bullet.Shot(worldPosition, null, 0.0f);
    }



}
