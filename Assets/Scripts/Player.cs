using UnityEngine;

public class Player : MonoBehaviour
{
    // Flip sprites horizontally based on input
    private void Update()
    {
        // Get the horizontal input value
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Loop through each child object of the parent object
        foreach(Transform child in transform)
        {
            // Get all childed sprite renderers
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                // Flip the sprite horizontally
                if(horizontalInput < 0)
                    spriteRenderer.flipX = false;
                
                else if(horizontalInput > 0)
                    spriteRenderer.flipX = true;
            }
        }
    }
}
