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
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Tab) && menuGroup.interactable)) {
            Hide();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !menuGroup.interactable) {
            Show();
            return;
        }
    }

    public void Cancel(Tower tower) {
        Destroy(tower.gameObject);
    }

    public void Place(Tower tower) {
        Globals.instance.money -= tower.Cost;
        CheckCosts();
    }

    public void Show() {
        CheckCosts();
        menuGroup.FadeCanvas(1f, false, this);
    }

    private void CheckCosts() {
        foreach (MenuItem menuItem in uiItems) {
            menuItem.buy.interactable = Globals.instance.money >= menuItem.towerBase.Cost;
        }
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
        GridManager.instance.InitializePlacement(Instantiate(item.towerPrefab, mousePos, Quaternion.identity).GetComponent<Tower>());
    }

    [System.Serializable] public class MenuItem {
        public Button buy;
        public Image icon;
        public GameObject towerPrefab;
        public Tower towerBase;
        public int index = 0;

        public MenuItem(GameObject uiPrefab, GameObject towerPrefab, int index) {
            buy = uiPrefab.GetComponentInChildren<Button>();
            icon = uiPrefab.GetComponentsInChildren<Image>().First(image => image.gameObject.Has<Tags.UI.MenuImage>());
            icon.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;
            towerBase = towerPrefab.GetComponent<Tower>();
            this.towerPrefab = towerPrefab;
            this.index = index;
        }
    }
}