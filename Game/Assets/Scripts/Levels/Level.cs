using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    public Grid grid;

    public Tilemap mainTilemap;
    public TilemapCollider2D mainTilemapCollider2D;

    public Tilemap platformTilemap;
    public TilemapCollider2D platformTilemapCollider2D;
    public Transform projectiles;

    public Camera mainCamera;

    public static Level instance;

    public GameObject playerPrefab;

    public GameObject groundRefs;

    public GameObject respawnPoints;

    public CollectibleContainer collectibles;


    public Transform roomsContainer;
    [NonSerialized] public List<Room> rooms;
     
    [NonSerialized] public Room currentRoom;



    public int totalPresets = 2;
    public int colorPreset = 0;
    public bool forceColorPreset = false;

    void Awake() {
        instance = this;

        if(!forceColorPreset) colorPreset = UnityEngine.Random.Range(0, totalPresets);

        groundRefs.SetActive(true);
        foreach(Renderer rend in groundRefs.GetComponentsInChildren<Renderer>().ToList()) {
            rend.enabled = false;
        }

        rooms = roomsContainer.GetComponentsInChildren<Room>().ToList();
        currentRoom = rooms[0];
        rooms[0].active = true;
        rooms[0].onRoomEntered.Invoke();
    }

    void Start() {
        SetPlayerOnKillListener();
    }

    void SetPlayerOnKillListener() {
        Player.instance.health.onKill.AddListener(() => {

            Player oldPlayer = Player.instance;
            Player.PlayerData data = oldPlayer.GetPlayerData();

            oldPlayer.CleanUp();

            GameObject _newPlayer = Instantiate(playerPrefab);
            Player newPlayer = _newPlayer.GetComponent<Player>();

            newPlayer.SetPlayerData(data);
            newPlayer.transform.position = RespawnPoint.activeRespawnPoint.transform.position;

            Destroy(oldPlayer.gameObject);


            SetPlayerOnKillListener();
        });
    }

}
