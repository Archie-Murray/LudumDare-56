using UnityEngine;
using Utilities;

public class GridManager : Singleton<GridManager> {
    [SerializeField] private TowerBase[,] towers;
    [SerializeField] private bool[,] validPoints;
    [SerializeField] private Vector2Int size;
    [SerializeField] private TowerMenu towerMenu;
    [SerializeField] private TowerBase heldTower = null;
    [SerializeField] private SpriteRenderer placementIndicator;
    [SerializeField] private GameObject indicatorPrefab;

    public void Start() {
        towers = new TowerBase[size.y, size.x];
        validPoints = new bool[size.y, size.x];
        for (int y = 0; y < size.y; y++) {
            for (int x = 9; x < size.x; x++) {
                validPoints[y, x] = true;
            }
        }
        foreach (Nanny nanny in FindObjectsOfType<Nanny>()) {
            foreach (Transform transform in nanny.Locations) {
                Vector2Int point = Vector2Int.RoundToInt(transform.position);
                validPoints[point.y, point.x] = false;
            }
        }
        towerMenu = FindFirstObjectByType<TowerMenu>();
        placementIndicator = Instantiate(indicatorPrefab, -Vector3.one, Quaternion.identity).GetComponent<SpriteRenderer>();
        placementIndicator.gameObject.SetActive(false);
    }

    public void InitializePlacement(TowerBase tower) {
        heldTower = tower;
        SpriteRenderer towerRenderer = heldTower.GetComponent<SpriteRenderer>();
        Color fade = towerRenderer.color;
        fade.a = 0.5f;
        towerRenderer.color = fade;
        placementIndicator.gameObject.SetActive(true);
    }

    private void Update() {
        if (heldTower != null) {
            Debug.Log("Holding Tower!");
            Vector3Int gridPos = Vector3Int.RoundToInt(Helpers.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            heldTower.transform.position = new Vector3(gridPos.x, gridPos.y, 0f);
            placementIndicator.transform.position = heldTower.transform.position;

            bool validPos = validPoints[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x];
            placementIndicator.color = validPos ? Color.green : Color.red;
            if (Input.GetMouseButtonDown(1) && validPos) {
                towers[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = heldTower;
                validPoints[(int)heldTower.transform.position.y, (int)heldTower.transform.position.x] = false;
                towerMenu.Place(heldTower);
                placementIndicator.gameObject.SetActive(false);
            } else if (Input.GetMouseButtonDown(0)) {
                towerMenu.Cancel(heldTower);
                placementIndicator.gameObject.SetActive(false);
            }
        }
    }
}