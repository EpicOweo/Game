using System.IO;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    static bool locked;
    static bool lockedX;
    static bool lockedY;

    public static bool onTrack = false;


    public CinemachineVirtualCamera vCam;
    public CinemachineDollyCart dolly;
    public CinemachinePath cameraPath;

    bool followingPlayerY = true;

    void LateUpdate()
    {

        if(Player.instance == null) return;


        if(onTrack) {

            /*float closest = cameraPath.FindClosestPoint(Player.instance.transform.position, 0, -1, 10);
            
            float normalized = cameraPath.FromPathNativeUnits(closest, CinemachinePathBase.PositionUnits.Normalized);

            float lerped = Mathf.Lerp(dolly.m_Position, normalized, 15 * Time.deltaTime);

            dolly.m_Position = lerped;*/

        } else if(!locked) {

            float minDistFromGround = 5f;

            RaycastHit2D ground = GetRaycastHit(minDistFromGround * 2, Vector2.down);
            RaycastHit2D ceiling = GetRaycastHit(minDistFromGround * 2, Vector2.up);

            float distFromGround = ground ? ground.distance : float.PositiveInfinity;
            float distFromCeil = ceiling ? ceiling.distance : float.PositiveInfinity;
            Vector3 playerPos = Player.instance.rb.transform.position;

            float offset;

            bool lowCeiling = distFromGround + distFromCeil < minDistFromGround * 2;
            /*Debug.Log(lowCeiling);
            Debug.Log(distFromGround);
            Debug.Log(distFromCeil);
            Debug.Log("");*/
            
            float extraOffset = 0;

            if(distFromGround < distFromCeil) {
                offset = distFromGround <= minDistFromGround ? extraOffset + minDistFromGround - distFromGround : 0;
            } else {
                offset = distFromCeil <= minDistFromGround ? distFromCeil - extraOffset - minDistFromGround : 0;
            }


            float threshold = 0.5f;

            offset = Mathf.Abs(offset) < threshold ? 0 : offset;


            Vector3 lerpTo = new(transform.position.x, transform.position.y, transform.position.z);

            if(!lockedX) {
                lerpTo = new(playerPos.x, lerpTo.y, lerpTo.z);
            }
            if(!lockedY) {
                lerpTo = new(lerpTo.x, playerPos.y + offset, lerpTo.z);
            }

            transform.position = Vector3.Lerp(
                transform.position,
                lerpTo,
                15f * Time.deltaTime
            );
            
        }
    }

    RaycastHit2D GetRaycastHit(float checkDist, Vector2 dir) {
        Vector2 pos = Player.instance.transform.position;

        RaycastHit2D hit = Physics2D.Raycast (
            origin:     pos,
            direction:  dir,
            distance:   checkDist,
            layerMask:  LayerMask.GetMask("Ground Refs")
        );

        return hit;
    }

    public void Lock(Vector2 pos) {
        locked = true;
        transform.position = (Vector3)pos + Vector3.forward * transform.position.z;
    }

    public void LockX() {
        lockedX = true;
    }

    public void LockY() {
        lockedY = true;
    }

    public void UnlockX() {
        lockedX = false;
    }

    public void UnlockY() {
        lockedY = false;
    }

    public void Unlock() {
        locked = false;
    }
}
