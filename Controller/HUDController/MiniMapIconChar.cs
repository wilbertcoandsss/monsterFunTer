using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIconChar : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.getInstance();
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 newPosition = instance.getC().transform.position;
        newPosition.y = spriteRenderer.transform.position.y;
        spriteRenderer.transform.position = newPosition;

        spriteRenderer.transform.rotation = Quaternion.Euler(90f, instance.getC().transform.eulerAngles.y, 0f);
    }
}
