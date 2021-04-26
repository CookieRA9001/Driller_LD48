using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRAMade_Tweening : MonoBehaviour
{
    public bool rect = true;
    public float rotat_max=0, rotatSpeed_max;
    public float zoomIn_min=1, zoomOut_max=1, zoomSpeed_max;
    public float zoomY_perc=1, zoomX_perc=1;
    public float accele=0;
    public bool zoom, swingRotate, fullRotate;
    private float zoomX, zoomY, rotation;

    private int rotateDir = 1, zoomDir=1;
    private float zoomSpeed=0, roaterSpeed=0;
    private RectTransform RT;
    private Transform T;
    private void Start(){
        if (rect) {
            RT = GetComponent<RectTransform>();
            zoomX = RT.localScale.x;
            zoomY = RT.localScale.y;
            rotation = RT.rotation.z;
        }
        else {
            T = GetComponent<Transform>();
            zoomX = T.localScale.x;
            zoomY = T.localScale.y;
            rotation = T.rotation.z;
        }
        
        if (accele == 0) {
            zoomSpeed = zoomSpeed_max;
            roaterSpeed = rotatSpeed_max;
        }
    }
    void Update(){
        if(rect) { 
            if (zoom) {
                zoomSpeed = Mathf.Max(zoomSpeed + accele * Time.deltaTime, zoomSpeed_max);
                if (zoomDir == 1){
                    float X = Mathf.Min(RT.localScale.x + (zoomSpeed * Time.deltaTime * zoomX_perc), (1+(zoomOut_max-1)*zoomX_perc) * zoomX);
                    float Y = Mathf.Min(RT.localScale.y + (zoomSpeed * Time.deltaTime * zoomY_perc),(1+(zoomOut_max-1)*zoomY_perc) * zoomY);
                    RT.localScale = new Vector3(X,Y, RT.localScale.z);
                    if (X>= (1 + (zoomOut_max - 1) * zoomX_perc) * zoomX || Y >= (1 + (zoomOut_max - 1) * zoomY_perc) * zoomY){
                        zoomDir = -1;
                    }
                }
                else{
                    float X = Mathf.Max(RT.localScale.x - (zoomSpeed * Time.deltaTime * zoomX_perc), (1+(zoomIn_min - 1) * zoomX_perc) * zoomX);
                    float Y = Mathf.Max(RT.localScale.y - (zoomSpeed * Time.deltaTime * zoomY_perc), (1+(zoomIn_min - 1) * zoomY_perc) * zoomY);
                    RT.localScale = new Vector3(X, Y, RT.localScale.z);
                    if (X <= (1 + (zoomIn_min - 1) * zoomX_perc) * zoomX || Y <= (1 + (zoomIn_min - 1) * zoomY_perc) * zoomY) {
                        zoomDir = 1;
                    }
                }
            }
            if (swingRotate) {
                roaterSpeed = Mathf.Min(roaterSpeed + accele * Time.deltaTime, rotatSpeed_max);
                if (rotateDir == 1) {
                    float X = Mathf.Min(RT.rotation.z + (roaterSpeed * Time.deltaTime), rotation + rotat_max);
                    RT.rotation = new Quaternion(RT.rotation.x, RT.rotation.y,X, RT.rotation.w);
                    if (X >= rotation + rotat_max) {
                        rotateDir = -1;
                    }
                }
                else {
                    float X = Mathf.Max(RT.rotation.z - (roaterSpeed * Time.deltaTime), rotation - rotat_max);
                    RT.rotation = new Quaternion(RT.rotation.x, RT.rotation.y, X, RT.rotation.w);
                    if (X <= rotation - rotat_max){
                        rotateDir = 1;
                    }
                }
            }
            else if (fullRotate) {
                if (rotateDir == 1){

                }
                else{

                }
            }
        }
        else { 
            if (zoom) {
                zoomSpeed = Mathf.Max(zoomSpeed + accele * Time.deltaTime, zoomSpeed_max);
                if (zoomDir == 1){
                    float X = Mathf.Min(T.localScale.x + (zoomSpeed * Time.deltaTime * zoomX_perc), (1+(zoomOut_max-1)*zoomX_perc) * zoomX);
                    float Y = Mathf.Min(T.localScale.y + (zoomSpeed * Time.deltaTime * zoomY_perc),(1+(zoomOut_max-1)*zoomY_perc) * zoomY);
                    T.localScale = new Vector3(X,Y, T.localScale.z);
                    if (X>= (1 + (zoomOut_max - 1) * zoomX_perc) * zoomX || Y >= (1 + (zoomOut_max - 1) * zoomY_perc) * zoomY){
                        zoomDir = -1;
                    }
                }
                else{
                    float X = Mathf.Max(T.localScale.x - (zoomSpeed * Time.deltaTime * zoomX_perc), (1+(zoomIn_min - 1) * zoomX_perc) * zoomX);
                    float Y = Mathf.Max(T.localScale.y - (zoomSpeed * Time.deltaTime * zoomY_perc), (1+(zoomIn_min - 1) * zoomY_perc) * zoomY);
                    T.localScale = new Vector3(X, Y, T.localScale.z);
                    if (X <= (1 + (zoomIn_min - 1) * zoomX_perc) * zoomX || Y <= (1 + (zoomIn_min - 1) * zoomY_perc) * zoomY) {
                        zoomDir = 1;
                    }
                }
            }
            if (swingRotate) {
                roaterSpeed = Mathf.Min(roaterSpeed + accele * Time.deltaTime, rotatSpeed_max);
                if (rotateDir == 1) {
                    float X = Mathf.Min(T.rotation.z + (roaterSpeed * Time.deltaTime), rotation + rotat_max);
                    T.rotation = new Quaternion(T.rotation.x,T.rotation.y,X,T.rotation.w);
                    if (X >= rotation + rotat_max) {
                        rotateDir = -1;
                    }
                }
                else {
                    float X = Mathf.Max(T.rotation.z - (roaterSpeed * Time.deltaTime), rotation - rotat_max);
                    T.rotation = new Quaternion(T.rotation.x, T.rotation.y, X, T.rotation.w);
                    if (X <= rotation - rotat_max){
                        rotateDir = 1;
                    }
                }
            }
            else if (fullRotate) {
                if (rotateDir == 1){

                }
                else{

                }
            }
        }
    }
}
