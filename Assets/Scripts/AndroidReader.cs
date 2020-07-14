using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ZXing.PDF417.Internal;
using ZXing.PDF417;

public class AndroidReader : MonoBehaviour
{
    AndroidJavaObject scanditJava = new AndroidJavaObject("com.scandit.datacapture");

    new PDF417Reader idReader;

    /*
     * 
     * https://xinyustudio.wordpress.com/2015/12/31/step-by-step-guide-for-developing-android-plugin-for-unity3d-i/
     * https://www.reddit.com/r/Unity3D/comments/32j3e9/integrating_an_android_library_in_unity/
     * https://docs.scandit.com/data-capture-sdk/android/get-started-id-scanning.html
     * 
     * 
     * 
     */
}
