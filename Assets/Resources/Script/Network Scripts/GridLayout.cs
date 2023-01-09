using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayout : MonoBehaviour
{
    Canvas canvas;
    GridLayoutGroup group;
    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInParent<Canvas>();
        group = this.GetComponent<GridLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        float cellx = canvas.pixelRect.width * 0.1916f;
        float celly = canvas.pixelRect.height * 0.82f/3;
        group.cellSize = new Vector2(cellx , celly);
    }
}
