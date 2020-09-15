using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour {
    [SerializeField] List<GameObject> islands = new List<GameObject>();
    [SerializeField] SpriteRenderer backGroundRenderer = null;
    Vector3 worldDimensions;

    private void Awake() {
        worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        ResizeSea();
        SpawnIslands();
    }

    private void SpawnIslands() {
        int islandId;
        float chancesCreate = 80;

        for (int x = (int)-worldDimensions.x + 1; x < worldDimensions.x; x += 3) {
            for (int y = (int)-worldDimensions.y + 1; y < worldDimensions.y; y += 2) {
                if (x != 0 && y != 0) {
                    if (chancesCreate >= Random.Range(0, 100)) {
                        if (!Physics2D.CircleCast(new Vector3(x, y, 0), 2f, Vector2.zero, 1 << LayerMask.NameToLayer("Island"))) {
                            islandId = Random.Range(0, islands.Count);
                            Instantiate(islands[islandId], new Vector3(x, y, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
                            chancesCreate -= 30;
                        }
                    } else {
                        chancesCreate += 5;
                    }
                }
            }
        }
    }

    private void ResizeSea() {
        backGroundRenderer.size = worldDimensions * 2;
    }
}
