using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    public void StartGame() {
        // Create the player character.
        var player = new GameObject("Player");
        player.AddComponent<CharacterController>();
        player.AddComponent<Rigidbody2D>();
        player.transform.position = new Vector2(0, 0);

        // Create the world.
        var world = new GameObject("World");
        world.AddComponent<TilemapRenderer>();
        world.AddComponent<TilemapCollider2D>();
        world.transform.position = new Vector2(0, 0);

        // Create the enemies.
        var enemy1 = new GameObject("Enemy1");
        enemy1.AddComponent<EnemyController>();
        enemy1.transform.position = new Vector2(10, 10);

        var enemy2 = new GameObject("Enemy2");
        enemy2.AddComponent<EnemyController>();
        enemy2.transform.position = new Vector2(20, 20);

        // Create the items.
        var item1 = new GameObject("Item1");
        item1.AddComponent<ItemController>();
        item1.transform.position = new Vector2(30, 30);

        var item2 = new GameObject("Item2");
        item2.AddComponent<ItemController>();
        item2.transform.position = new Vector2(40, 40);

        // Start the game.
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop() {
        while (true) {
            // Update the player.
            player.GetComponent<CharacterController>().Update();
            player.GetComponent<Rigidbody2D>().Update();

            // Update the enemies.
            foreach (var enemy in GameObject.FindObjectsOfType<EnemyController>()) {
                enemy.Update();
            }

            // Update the items.
            foreach (var item in GameObject.FindObjectsOfType<ItemController>()) {
                item.Update();
            }

            // Check for collisions.
            var collisions = Physics2D.OverlapCircleAll(player.transform.position, player.GetComponent<CharacterController>().radius);
            foreach (var collision in collisions) {
                var collider = collision.collider;
                if (collider.gameobject.tag == "Enemy") {
                    // Player hit an enemy.
                    // Do something.
                } else if (collider.gameobject.tag == "Item") {
                    // Player picked up an item.
                    // Do something.
                }
            }

            // Yield to the next frame.
            yield return null;
        }
    }
}