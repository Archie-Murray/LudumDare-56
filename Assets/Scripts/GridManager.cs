using UnityEngine;
using Utilities;

public class GridManager : Singleton<GridManager> {
    [SerializeField] private TowerBase[,] towers;
    [SerializeField] private Vector2Int size;
    [SerializeField] private TowerMenu towerMenu;
    [SerializeField] private TowerBase heldTower = null;

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
        if (heldTower != null) {
            Debug.Log("Holding Tower!");
            Vector3Int gridPos = Vector3Int.RoundToInt(Helpers.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            heldTower.transform.position = new Vector3(gridPos.x, gridPos.y, 0f);
            if (Input.GetMouseButtonDown(1)) {
                towers[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = heldTower;
                towerMenu.Place(heldTower);
            } else if (Input.GetMouseButtonDown(0)) {
                towerMenu.Cancel(heldTower);
            }
        }
    }
}