using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class AutoEncoderShower : MonoBehaviour
{
    // Start is called before the first frame updates
    [SerializeField] 
    public RawImage rawImage;
    public Texture2D[] tex;
    GameObject car;
    tk.TcpCarHandler carHandler;
    void Start()
    {
        
        // Object[] textures = Resources.LoadAll("images", typeof(Texture2D));
        // tex = new Texture2D[textures.Length];
        // for (int i = 0; i < textures.Length; i++){
        //   tex[i] = (Texture2D)textures[i];
        // }
        // StartCoroutine(imageRend());
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
            Texture2D texture = new Texture2D(1,1);
            texture.LoadImage( carHandler.encoderBytes);
            rawImage.texture=texture;
        }
    }
}
