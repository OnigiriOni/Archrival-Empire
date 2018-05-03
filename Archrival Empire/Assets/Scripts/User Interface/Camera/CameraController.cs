using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    #region Variables

    //public float moveSpeed_keys = 100F;   //The moving speed of the CameraContainer if moved with the keyboard
    //public float moveSpeed_mouse = 80F;   //The moving speed of the CameraContainer if moved with the mouse
    public float moveSpeed = 20F;           //The moving speed of the CameraContainer
    public float zoomSpeed = 25F;           //The zoom speed of the CameraContainer
    public float rotateSpeed = 5F;          //The rotation speed of the CameraContainer

    private int mouseMoveZoneSize = 5;      //The size of the zone, when the mouse enters the camera moves
    private float defaultRotation = 45F;    //The angle with the MainCamera is looking down 
    private float defaultDistance = 40F;    //The default distance from the MainCamera to the ViewPoint
    private float maxZoomDistance = 65F;    //The maximum distance from the MainCamera to the ViewPoint
    private float minZoomDistance = 20F;     //The minimum distance from the MainCamera to the ViewPoint

    Vector3 forwardWithoutTilt;             //The forward vector of the MainCamera with Y being zero
    Transform mainCamera;                   //The Transform component of the MainCamera object
    Transform viewPoint;                    //The Transform component of the ViewPoint object
    Terrain terrain;                        //The Terrain component of the Terrain where the Camera is moving over

    //TODO: Needs options for everything. For example disable mouse camera movement etc.

    #endregion

    #region Start and Update

    void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Transform>();
        viewPoint = GameObject.Find("ViewPoint").GetComponent<Transform>();
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();

        mainCamera.Rotate(new Vector3(defaultRotation, 0, 0));
        mainCamera.position -= mainCamera.forward * defaultDistance;
        Cursor.lockState = CursorLockMode.Confined; // TODO: Cursor.logState needs another Controller, UI or similar
    }

    void Update()
    {
        PreCalculations();
        Rotate();
        Move();
        Zoom();
    }

    #endregion

    #region Methods

    void PreCalculations()
    {
        //Since the terrain is build during runtime, it has to doublecheck for the needed component to be created
        //TODO: Load the CameraControler after the map creation is finished
        if (!terrain)
        {
            terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        }

        forwardWithoutTilt = mainCamera.forward;
        forwardWithoutTilt.y = 0;
    }

    void Move()
    {
        if (Input.mousePosition.y >= Screen.height - mouseMoveZoneSize || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += forwardWithoutTilt * moveSpeed * Time.deltaTime;
            SetCameraHeight();
        }

        if (Input.mousePosition.y <= mouseMoveZoneSize || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= forwardWithoutTilt * moveSpeed * Time.deltaTime;
            SetCameraHeight();
        }

        if (Input.mousePosition.x <= mouseMoveZoneSize || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= mainCamera.right * moveSpeed * Time.deltaTime;
            SetCameraHeight();
        }

        if (Input.mousePosition.x >= Screen.width - mouseMoveZoneSize || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += mainCamera.right * moveSpeed * Time.deltaTime;
            SetCameraHeight();
        }

        HoldCameraOnTerrain();
    }

    void Rotate()
    {
        //TODO: Rotate upwards or downwards too ??? from 40° to 90° (default = 45°)
        if (Input.GetMouseButton(2))
        {
            mainCamera.RotateAround(viewPoint.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);
        }
    }

    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Vector3.Distance(viewPoint.position, mainCamera.position) >= minZoomDistance)
            {
                mainCamera.position += mainCamera.forward * zoomSpeed * Time.deltaTime;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Vector3.Distance(viewPoint.position, mainCamera.position) <= maxZoomDistance)
            {
                mainCamera.position -= mainCamera.forward * zoomSpeed * Time.deltaTime;
            }
        }
    }

    //Set the CameraContainer to the terrain height at the current position of the ViewPoint
    void SetCameraHeight()
    {
        transform.position = new Vector3(transform.position.x, terrain.SampleHeight(viewPoint.position), transform.position.z);
    }

    //TODO: Make it possible to locate the terrain not only on Vector3.Zero
    void HoldCameraOnTerrain()
    {
        if (viewPoint.position.x > terrain.terrainData.size.x)
        {
            transform.position = new Vector3(terrain.terrainData.size.x, transform.position.y, transform.position.z);
        }

        if (viewPoint.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        if (viewPoint.position.z > terrain.terrainData.size.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, terrain.terrainData.size.z);
        }

        if (viewPoint.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    #endregion

}