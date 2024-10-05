using UnityEngine;
using Utilities;

public class GridManager : Singleton<GridManager> {
    [SerializeField] private TowerBase[,] towers;
    [SerializeField] private Vector2Int size;
    [SerializeField] private TowerMenu towerMenu;
    private TowerBase heldTower = null;

    public void Start() {
        towers = new TowerBase[size.y, size.x];
    }

    public void InitializePlacement(TowerBase tower) {
        heldTower = tower;
        SpriteRenderer towerRenderer = heldTower.GetComponent<SpriteRenderer>();
        Color fade = towerRenderer.color;
        fade.a = 0.5f;
        towerRenderer.color = fade;
    }

    private void Update() {
        Debug.Log("Grid Manager Update");
        if (heldTower) {
            heldTower.transform.position = Vector3Int.RoundToInt(Helpers.I.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetMouseButtonDown(1)) {
                towers[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = heldTower;
                towerMenu.Place(heldTower);
            } else if (Input.GetMouseButtonDown(0)) {
                towerMenu.Cancel(heldTower);
            }
        }
    }
}
