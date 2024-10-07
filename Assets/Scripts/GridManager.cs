using System;

using Entity;

using ProjectileComponents;

using UnityEngine;
using UnityEngine.Tilemaps;

using Utilities;

public class GridManager : Singleton<GridManager> {
    [SerializeField] private Tower[,] towers;
    [SerializeField] private bool[,] validPoints;
    [SerializeField] private TowerMenu towerMenu;
    [SerializeField] private Tower heldTower = null;
    [SerializeField] private SpriteRenderer placementIndicator;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector3Int checkPos;
    [SerializeField] private SFXEmitter emitter;
    [SerializeField] private UIMouseDetector[] uiObjects;
    const string TILE_NONE = "PathNone";
    private RaycastHit2D hit;

    public void Start() {
        towers = new Tower[size.y, size.x];
        validPoints = new bool[size.y, size.x];
        for (int y = size.y - 1; y >= 0; y--) {
            for (int x = 0; x < size.x; x++) {
                validPoints[y, x] = (tileMap.GetSprite(new Vector3Int(x, y, 0)).OrNull()?.name ?? string.Empty) == TILE_NONE;
            }
        }
        uiObjects = FindObjectsOfType<UIMouseDetector>();
        towerMenu = FindFirstObjectByType<TowerMenu>();
        placementIndicator = Instantiate(indicatorPrefab, -Vector3.one, Quaternion.identity).GetComponent<SpriteRenderer>();
        placementIndicator.gameObject.SetActive(false);
        Debug.Log($"Cell Bounds: {tileMap.cellBounds.ToString()}, Size: {tileMap.size}");
        emitter = GetComponent<SFXEmitter>();
    }

    public void InitializePlacement(Tower tower) {
        heldTower = tower;
        heldTower.enabled = false;
        heldTower.gameObject.layer = 0;
        tower.transform.GetChild(0).gameObject.SetActive(false);
        SpriteRenderer towerRenderer = heldTower.GetComponentInChildren<SpriteRenderer>();
        Color fade = towerRenderer.color;
        fade.a = 0.5f;
        towerRenderer.color = fade;
        placementIndicator.gameObject.SetActive(true);
    }

    private void Update() {
        if (heldTower != null) {
            Vector3Int gridPos = Vector3Int.RoundToInt(Helpers.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            gridPos.z = 0;
            heldTower.transform.position = new Vector3(gridPos.x, gridPos.y, 0f);
            placementIndicator.transform.position = heldTower.transform.position;

            bool validPos = validPoints[Mathf.Clamp(gridPos.y, 0, size.y - 1), Mathf.Clamp(gridPos.x, 0, size.x - 1)];
            placementIndicator.color = validPos ? Color.green : Color.red;
            if (Input.GetMouseButton(0) && validPos) {
                if (MouseTouchingUI()) {
                    return;
                }
                emitter.Play(SoundEffectType.TowerPlace);
                towers[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = heldTower;
                validPoints[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = false;
                SpriteRenderer towerRenderer = heldTower.GetComponentInChildren<SpriteRenderer>();
                Color fade = towerRenderer.color;
                fade.a = 1f;
                towerRenderer.color = fade;
                placementIndicator.gameObject.SetActive(false);
                Instantiate(Assets.instance.towerPlaceParticles, heldTower.transform.position, Quaternion.identity)
                    .GetOrAddComponent<AutoDestroy>().Duration = 1f;
                heldTower.gameObject.layer = LayerMask.NameToLayer("Tower");
                heldTower.transform.GetChild(0).gameObject.SetActive(true);
                heldTower.GetComponent<Health>().Heal(10000);
                heldTower.enabled = true;
                if (!towerMenu.Place(heldTower)) {
                    heldTower = null;
                }
            } else if (Input.GetMouseButtonDown(1)) {
                towerMenu.Cancel(heldTower);
                placementIndicator.gameObject.SetActive(false);
                heldTower = null;
            }
        }
    }

    private bool MouseTouchingUI() {
        foreach (UIMouseDetector mouseDetector in uiObjects) {
            if (mouseDetector.IsHovered()) {
                return true;
            }
        }
        return false;
    }

    public void RemoveTower(Tower towerBase) {
        validPoints[(int) towerBase.transform.position.y, (int) towerBase.transform.position.x] = true;
        towers[(int) towerBase.transform.position.y, (int) towerBase.transform.position.x] = null;
    }

    public void UpdateCosts() {
        towerMenu.CheckCosts();
    }
}