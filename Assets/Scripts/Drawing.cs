using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public GeneralController general;
    public Color brushColor = Color.white;
    public float brushSize = 10f;
    public RectTransform canvas;

    public RawImage rawImage;
    private Texture2D canvasTexture;
    private Vector2 previousPosition;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        InitializeCanvasTexture();
    }
   


   void InitializeCanvasTexture()
    {
        rawImage.rectTransform.sizeDelta = new Vector2(canvas.sizeDelta.x, canvas.sizeDelta.y);
        canvasTexture = new Texture2D((int)canvas.sizeDelta.x, (int)canvas.sizeDelta.y);
        rawImage.texture = canvasTexture;

        Color[] clearColorArray = new Color[canvasTexture.width * canvasTexture.height];
        System.Array.Fill(clearColorArray, Color.clear);
        canvasTexture.SetPixels(clearColorArray);
        canvasTexture.Apply();
    }

    void Update()
    {
        if (general.follower.activeSelf && !general.pause && !general.win)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Ended)
                {
                    ClearCanvas();
                    Vector2 localPos = touch.position;

                    float normalizedX = Mathf.InverseLerp(0, Screen.width, localPos.x);
                    float normalizedY = Mathf.InverseLerp(0, Screen.height, localPos.y);

                    int x = Mathf.FloorToInt(normalizedX * canvasTexture.width);
                    int y = Mathf.FloorToInt(normalizedY * canvasTexture.height);

                    previousPosition = new Vector2(x, y);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 localPos = touch.position;

                    float normalizedX = Mathf.InverseLerp(0, Screen.width, localPos.x);
                    float normalizedY = Mathf.InverseLerp(0, Screen.height, localPos.y);

                    int x = Mathf.FloorToInt(normalizedX * canvasTexture.width);
                    int y = Mathf.FloorToInt(normalizedY * canvasTexture.height);

                    DrawOnTexture(x, y);
                }

            }

            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
                {
                    ClearCanvas();
                    Vector2 localPos = Input.mousePosition;

                    float normalizedX = Mathf.InverseLerp(0, Screen.width, localPos.x);
                    float normalizedY = Mathf.InverseLerp(0, Screen.height, localPos.y);

                    int x = Mathf.FloorToInt(normalizedX * canvasTexture.width);
                    int y = Mathf.FloorToInt(normalizedY * canvasTexture.height);

                    previousPosition = new Vector2(x, y);
                }

                if (Input.GetMouseButton(0))
                {
                    Vector2 localPos = Input.mousePosition;

                    float normalizedX = Mathf.InverseLerp(0, Screen.width, localPos.x);
                    float normalizedY = Mathf.InverseLerp(0, Screen.height, localPos.y);

                    int x = Mathf.FloorToInt(normalizedX * canvasTexture.width);
                    int y = Mathf.FloorToInt(normalizedY * canvasTexture.height);

                    DrawOnTexture(x, y);

                }
            }
        }
    }

    void DrawOnTexture(int x, int y)
    {
        int steps = Mathf.FloorToInt(Vector2.Distance(previousPosition, new Vector2(x, y)));
        for (int i = 0; i < steps; i++)
        {
            float lerpAmount = i / (float)steps;
            int drawX = Mathf.FloorToInt(Mathf.Lerp(previousPosition.x, x, lerpAmount));
            int drawY = Mathf.FloorToInt(Mathf.Lerp(previousPosition.y, y, lerpAmount));

            DrawPoint(drawX, drawY);
        }

        canvasTexture.Apply();

        previousPosition = new Vector2(x, y);
    }

    void DrawPoint(int x, int y)
    {
        for (int i = -Mathf.FloorToInt(brushSize / 2); i <= Mathf.FloorToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.FloorToInt(brushSize / 2); j <= Mathf.FloorToInt(brushSize / 2); j++)
            {
                int drawX = x + i;
                int drawY = y + j;

                if (drawX >= 0 && drawX < canvasTexture.width && drawY >= 0 && drawY < canvasTexture.height)
                {
                    canvasTexture.SetPixel(drawX, drawY, brushColor);
                }
            }
        }
    }

    public void ClearCanvas()
    {
        Color[] clearColorArray = new Color[canvasTexture.width * canvasTexture.height];
        System.Array.Fill(clearColorArray, Color.clear);
        canvasTexture.SetPixels(clearColorArray);
        canvasTexture.Apply();
    }
}
