using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour {

    public GlobalUI _GlobalUI;
    public Camera _Camera;
    Player _Player;
    Bounds bounds;

    public bool inited;
    public bool ingame;

   
    float spawntime;
    public float enemyspeed;

    public AudioSource _MusicSource;
    public AudioSource _SoundsSource;
    public List<AudioClip> Sounds = new List<AudioClip>();
    public AudioClip _GameMusic;

    public int KilledEnemy;
    public int MaxKilledEnemy;
    public int MissEnemy;

    public List<Enemy> Enemyes = new List<Enemy>();

    void Start() {
        Init();
    }

    void Init() {

        if (MaxKilledEnemy == 0) {
            MaxKilledEnemy = 30;
        }

        KilledEnemy = 0;
       

        _GlobalUI = gameObject.transform.Find("GlobalUI").GetComponent<GlobalUI>();
        _GlobalUI._GameCore = this;
        _GlobalUI._PanelMain.OpenPanel();
        _GlobalUI._PanelGame.ClosePanel();
        _GlobalUI._PanelGame.Init();

        _Player = gameObject.transform.Find("Player").GetComponent<Player>();
        _Player.Init(this);

        _Camera = gameObject.transform.Find("GameCamera").GetComponent<Camera>();

        bounds = _Camera.transform.Find("spawnbounds").GetComponent<BoxCollider>().bounds;

        spawntime = Random.Range(0.5f, 2.0f);
        enemyspeed = 4.0f;

      

        inited = true;
    }

    public void StartGame() {
        _Player.gameObject.SetActive(true);

        _GlobalUI._PanelMain.ClosePanel();
        _GlobalUI._PanelGame.OpenPanel();

        _MusicSource.clip = _GameMusic;
        _MusicSource.Play();
        ingame = true;
    }


    void Update() {
        if (!inited) {
            return;
        }

        if (ingame) {
            if (spawntime > 0.0f) {
                spawntime -= 1.0f * Time.deltaTime;
            }
            if (spawntime <= 0.0f) {
                spawntime = Random.Range(0.5f, 2.0f);
                SpawnEnemy();
            }

            if (KilledEnemy >= MaxKilledEnemy) {
                Win();
                return;
            }
            if (MissEnemy != 0) {
                Failure();
                return;
            }
        }

        
    }

    void Win() {
        ingame = false;
        _GlobalUI._PanelGame._PanelEndGame.SetActive(true);
        _GlobalUI._PanelGame._TextWin.SetActive(true);
        _GlobalUI._PanelGame._TextFailure.SetActive(false);
        _Player.gameObject.SetActive(false);

        for (int i = 0; i < Enemyes.Count; i++) {
            Enemyes[i].DestroyEnd();
        }
    }
    void Failure() {
        ingame = false;
        _GlobalUI._PanelGame._PanelEndGame.SetActive(true);
        _GlobalUI._PanelGame._TextWin.SetActive(false);
        _GlobalUI._PanelGame._TextFailure.SetActive(true);
        _Player.gameObject.SetActive(false);
    }

    void SpawnEnemy() {
        int rand = Random.Range(1, 3);
        GameObject enemy = Instantiate(Resources.Load("Prefabs/Ships/Enemy_0" + rand), RandomPointInBounds(), Quaternion.identity) as GameObject;
        Enemy _Enemy = enemy.GetComponent<Enemy>();
        _Enemy.Init(this);
        Enemyes.Add(_Enemy);
    }

    Vector3 RandomPointInBounds() {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
