using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using Utilities;

public class TowerMenu : MonoBehaviour {
    
    [SerializeField] private GameObject[] towers;
    [SerializeField] private CanvasGroup menuGroup;
    [SerializeField] private GameObject towerUIPrefab;
    [SerializeField] private MenuItem[] uiItems;

    public void Start() {
        menuGroup = GetComponent<CanvasGroup>();
        uiItems = new MenuItem[towers.Length];
        for (int i = 0; i < towers.Length; i++) {
            MenuItem item = new MenuItem(Instantiate(towerUIPrefab, menuGroup.transform), towers[i], i);
            item.buy.onClick.AddListener(() => StartPlacingTower(item));
            uiItems[i] = item;
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Hide();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Show();
            return;
        }
    }

    public void Cancel(TowerBase tower) {
        Destroy(tower.gameObject);
    }

    public void Place(TowerBase tower) {
        Globals.instance.money -= tower.Cost;
    }

    public void Show() {
        foreach (MenuItem menuItem in uiItems) {
            menuItem.buy.interactable = Globals.instance.money >= menuItem.towerBase.Cost;
        }
        menuGroup.FadeCanvas(1f, false, this);
    }

    public void Hide() {
        foreach (MenuItem menuItem in uiItems) {
            menuItem.buy.interactable = false;
        }
        menuGroup.FadeCanvas(1f, true, this);
    }

    private void StartPlacingTower(MenuItem item) {
        if (Globals.instance.money < item.towerBase.Cost) {
            return;
        }
        Vector3 mousePos = Helpers.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        GridManager.instance.InitializePlacement(Instantiate(item.towerPrefab, mousePos, Quaternion.identity).GetComponent<TowerBase>());
    }

    [System.Serializable] public class MenuItem {
        public Button buy;
        public Image icon;
        public GameObject towerPrefab;
        public TowerBase towerBase;
        public int index = 0;

        public MenuItem(GameObject uiPrefab, GameObject towerPrefab, int index) {
            buy = uiPrefab.GetComponentInChildren<Button>();
            icon = uiPrefab.GetComponentsInChildren<Image>().First(image => image.gameObject.Has<Tags.UI.MenuImage>());
            icon.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;
            towerBase = towerPrefab.GetComponent<TowerBase>();
            this.towerPrefab = towerPrefab;
            this.index = index;
        }
    }
}