using System;

using UnityEngine;
using UnityEngine.Tilemaps;

using Utilities;

public class GridManager : Singleton<GridManager> {
    [SerializeField] private TowerBase[,] towers;
    [SerializeField] private bool[,] validPoints;
    [SerializeField] private TowerMenu towerMenu;
    [SerializeField] private TowerBase heldTower = null;
    [SerializeField] private SpriteRenderer placementIndicator;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector3Int checkPos;
    const string TILE_NONE = "PathNone";

    public void Start() {
        towers = new TowerBase[size.y, size.x];
        validPoints = new bool[size.y, size.x];
        for (int y = size.y - 1; y >= 0; y--) {
            for (int x = 0; x < size.x; x++) {
                validPoints[y, x] = (tileMap.GetSprite(new Vector3Int(x, y, 0)).OrNull()?.name ?? string.Empty) == TILE_NONE;
            }
        }

        towerMenu = FindFirstObjectByType<TowerMenu>();
        placementIndicator = Instantiate(indicatorPrefab, -Vector3.one, Quaternion.identity).GetComponent<SpriteRenderer>();
        placementIndicator.gameObject.SetActive(false);
        Debug.Log($"Cell Bounds: {tileMap.cellBounds.ToString()}, Size: {tileMap.size}");
    }

    public void InitializePlacement(TowerBase tower) {
        heldTower = tower;
        SpriteRenderer towerRenderer = heldTower.GetComponent<SpriteRenderer>();
        Color fade = towerRenderer.color;
        fade.a = 0.5f;
        towerRenderer.color = fade;
        placementIndicator.gameObject.SetActive(true);
        heldTower.enabled = false;
    }

    private void Update() {
        if (heldTower != null) {
            Vector3Int gridPos = Vector3Int.RoundToInt(Helpers.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            gridPos.z = 0;
            heldTower.transform.position = new Vector3(gridPos.x, gridPos.y, 0f);
            placementIndicator.transform.position = heldTower.transform.position;

            bool validPos = validPoints[Mathf.Clamp(gridPos.y, 0, size.y - 1), Mathf.Clamp(gridPos.x, 0, size.x - 1)];
            placementIndicator.color = validPos ? Color.green : Color.red;
            if (Input.GetMouseButtonDown(0) && validPos) {
                towers[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = heldTower;
                validPoints[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = false;
                towerMenu.Place(heldTower);
                SpriteRenderer towerRenderer = heldTower.GetComponent<SpriteRenderer>();
                Color fade = towerRenderer.color;
                fade.a = 1f;
                towerRenderer.color = fade;
                placementIndicator.gameObject.SetActive(false);
                heldTower.enabled = true;
                heldTower = null;
            } else if (Input.GetMouseButtonDown(1)) {
                towerMenu.Cancel(heldTower);
                placementIndicator.gameObject.SetActive(false);
                heldTower = null;
            }
        }
    }

    public void RemoveTower(TowerBase towerBase) {
        validPoints[(int) towerBase.transform.position.y, (int) towerBase.transform.position.x] = true;
        towers[(int) towerBase.transform.position.y, (int) towerBase.transform.position.x] = null;
    }
}