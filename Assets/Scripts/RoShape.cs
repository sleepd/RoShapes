using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoShape : MonoBehaviour
{
    [SerializeField] int[] numbers;
    [SerializeField] float radius;
    [SerializeField] GameObject TextPrefab;
    [SerializeField] float _rotateSpeed = 180f;
    public int currentNumberIndex;
    private GameObject _shapeMesh;
    private List<Vector3> _edgeVertices;
    private bool _isRotating = false;

    private float targetAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shapeMesh = transform.Find("ShapeMesh")?.gameObject;
        if (_shapeMesh == null)
        {
            Debug.LogError("Can't get the Shape Mesh object!");
        }

        CreateShape();
        CreateNumberText();
        InitialShapeRotate();

    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotating)
        {
            float currentZ = _shapeMesh.transform.eulerAngles.z;
            float angle = Mathf.MoveTowardsAngle(currentZ, targetAngle, _rotateSpeed * Time.deltaTime / numbers.Length);

            _shapeMesh.transform.eulerAngles = new Vector3(0, 180, angle);

            if (Mathf.Approximately(angle, targetAngle))
            {
                _isRotating = false; // 旋转完成
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.transform == transform)
            {
                RotateShape();
            }
        }
    }

    void CreateShape()
    {
        Mesh mesh = ShapeGenerator.CreatePolygon(numbers.Length, radius, out _edgeVertices);
        _shapeMesh.GetComponent<MeshFilter>().mesh = mesh;
        if (numbers.Length == 3)
        {
            float height = radius * Mathf.Sin(Mathf.Deg2Rad * 60f);
            Vector3 shapePosition = Vector3.zero;
            shapePosition.y = -(height / 3f);
            _shapeMesh.transform.localPosition = shapePosition;
        }
    }

    void CreateNumberText()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            Vector3 v1 = _edgeVertices[i];
            Vector3 v2 = _edgeVertices[(i + 1) % _edgeVertices.Count];
            GameObject text = Instantiate(TextPrefab, _shapeMesh.transform);
            Vector3 position = (v1 + v2) * 0.5f;

            Vector3 dir = v2 - v1;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float fixedAngle = -angle;

            text.transform.localPosition = position;
            text.transform.localRotation = Quaternion.Euler(0f, 180f, fixedAngle);
            text.GetComponent<TextMeshPro>().text = numbers[i].ToString();
        }
    }

    void InitialShapeRotate()
    {
        _shapeMesh.transform.Rotate(0, 0, -(360 / numbers.Length));
    }

    void RotateShape()
    {
        targetAngle = _shapeMesh.transform.eulerAngles.z + -(360 / numbers.Length);
        _isRotating = true;
    }
}
