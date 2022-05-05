using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.Collections;

public class AutoEncoderShower : MonoBehaviour
{
    // Start is called before the first frame updates
    [SerializeField] 
    public RawImage rawImage;
    public Texture2D[] tex;
    GameObject car;
    tk.TcpCarHandler carHandler;
    byte[] rawData;
    void Start()
    {
        
        // Object[] textures = Resources.LoadAll("images", typeof(Texture2D));
        // tex = new Texture2D[textures.Length];
        // for (int i = 0; i < textures.Length; i++){
        //   tex[i] = (Texture2D)textures[i];
        // }
        // StartCoroutine(imageRend());
        //string filename = "D:\\Workplace\\ML\\Project\\new_footage\\tub_85_21-11-26\\images\\0_cam_image_array_.jpg";
        //rawData = System.IO.File.ReadAllBytes(filename);
    }
    IEnumerator imageRend () {
        foreach (Texture2D t in tex)
        {
            rawImage.texture=t;
            yield return new WaitForSecondsRealtime(0.1f);

        }
        // for (int i = 0; i < 700; i++)
        // {
        //     // byte[] byteArray = File.ReadAllBytes(@"D:\Workplace\ML\Project\new_footage\tub_85_21-11-26\images\"+ string.Format("{0}_cam_image_array_.jpg",i));
        //     // Texture2D sampleTexture = new Texture2D(160,120);
        //     // bool isLoaded = sampleTexture.LoadImage(byteArray);
        //     rawImage.texture = sampleTexture;

        //         yield return new WaitForSeconds(0.1f);
        // }
	}
    // Update is called once per frame
    Texture2D tempTex;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)){
            rawImage.enabled=!rawImage.enabled;
        }
        if(car==null){
            car=GameObject.Find("TCPClient");

        }
        if(carHandler==null && car != null){
            carHandler=car.GetComponent<tk.TcpCarHandler>();

        }
        if (rawImage.enabled && carHandler !=null ){
            // rawImage.texture=carHandler.encoderTex;
            // byte[] bytes=tex[1].GetRawTextureData();

            tempTex=new Texture2D(1920,1080);
            // Debug.Log(carHandler.encoderBytes);
            // tempTex.LoadImage(rawData);
            // tempTex.LoadImage(carHandler.encoderBytes);
            // ImageConversion.LoadImage(rawImage.texture,bytes);
            string filename = carHandler.encoderImagePath;
            rawData = System.IO.File.ReadAllBytes(filename);
            tempTex.LoadImage(rawData);
            rawImage.texture=tempTex;

            // rawImage.texture.LoadImage(bytes);
            // rawImage.texture.Apply();


            // rawImage.texture.LoadImage(carHandler.encoderBytes);
            // rawImage.texture.Apply();
        }
    }
}
