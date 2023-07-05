using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public MazeController mazeController;  // Reference to the MazeController

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (mazeController.IsMazeGenerated())
        {
            ResizeBackground();
        }
    }


    void ResizeBackground()
    {
        if (mazeController == null || spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("One or more required components are not set in the BackgroundController");
            return;
        }

        int mazeWidth = mazeController.GetMazeWidth();
        int mazeHeight = mazeController.GetMazeHeight();

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;

        Vector2 newScale = new Vector2((mazeWidth * 1.1f) / spriteWidth, (mazeHeight * 1.1f) / spriteHeight);

        transform.localScale = newScale;
    }
}
