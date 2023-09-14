/* 
    ------------------- Code Monkey -------------------
    
    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Radar : MonoBehaviour {

    [SerializeField] private Transform pfRadarPing;
    [SerializeField] private LayerMask radarLayerMask;

    private Transform sweepTransform;
    private float rotationSpeed;
    private float radarDistance;
    private List<Collider> colliderList;

    private void Awake() {
        sweepTransform = transform.Find("Trail");
        rotationSpeed = 180f;
        radarDistance = 150f;
        colliderList = new List<Collider>();
    }

    private void Update() {
        float previousRotation = (sweepTransform.eulerAngles.y % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        float currentRotation = (sweepTransform.eulerAngles.y % 360) - 180;

        if (previousRotation < 0 && currentRotation >= 0) {
            // Half rotation
            colliderList.Clear();
        }
        Vector3 forward = transform.TransformDirection(sweepTransform.right) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit[] raycastHitArray = Physics.RaycastAll(transform.position, sweepTransform.right, radarDistance);


        foreach (RaycastHit raycastHit in raycastHitArray) {

            if (raycastHit.collider != null) {
                if (raycastHit.collider.gameObject.layer != 8 && raycastHit.collider.gameObject.layer != 9)
                {
                    // Hit something
                    if (!colliderList.Contains(raycastHit.collider))
                    {
                        // Hit this one for the first time
                        colliderList.Add(raycastHit.collider);
                        //CMDebug.TextPopup("Ping!", raycastHit2D.point);
                        RadarPing radarPing = Instantiate(pfRadarPing, raycastHit.point, Quaternion.identity).GetComponent<RadarPing>();
                        radarPing.transform.Rotate(90, 0, 0);
                        //radarPing.transform.SetParent(transform);
                        if (raycastHit.collider.gameObject.GetComponent<ItemHandler>() != null)
                        {
                            // Hit an Item
                            radarPing.SetColor(new Color(0, 1, 0));
                        }
                        if (raycastHit.collider.gameObject.CompareTag("Enemy"))
                        {
                            // Hit an Enemy
                            radarPing.SetColor(new Color(1, 0, 0));
                        }
                        radarPing.SetDisappearTimer(360f / rotationSpeed * 1f);
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.T)) {
            rotationSpeed += 20;
            Debug.Log("rotationSpeed: " + rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            rotationSpeed -= 20;
            Debug.Log("rotationSpeed: " + rotationSpeed);
        }
    }

}
