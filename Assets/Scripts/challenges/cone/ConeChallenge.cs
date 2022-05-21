using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ConeChallenge : MonoBehaviour, IWaitCarPath
{

    public PathManager pathManager;
    public int numRandCone = 0;
    public float coneHeightOffset = 0.0f;
    public float coneOffset = 1.0f;
    public int iConePrefab = 0;
    public int nodesAfterStart = 10;
    public GameObject[] conePrefabs;
    private List<GameObject> createdObjects = new List<GameObject>();
    private int coneIndex=0;
    private string coneconfigFile ="./config.json";
    private string predefinedConesFile="./predefinedCones.txt";
    private bool isConfigFileExists=false;
    private bool isCarPresent=false;
    private bool prevIsCarPresent=false;




    private void Update() {
        if(Input.GetKeyDown(KeyCode.V)){
            saveCones();
        }
    }
    public void checkConfigFile(){
        ConfigJson configJson;
        if(File.Exists(coneconfigFile)){
            configJson=JsonUtility.FromJson<ConfigJson>(File.ReadAllText(coneconfigFile));
            if(File.Exists(predefinedConesFile)){
                loadConesFromFile();
                return;
            }
            if(configJson  != null && configJson.createCones){

                

                // int coneCount=int.Parse(File.ReadAllText(coneconfigFile));
                int coneCount=configJson.coneCount;
                numRandCone=coneCount;
                ResetChallenge();
                isConfigFileExists=true;
            }
            else{
                Debug.Log("No Cofig File for Cones");
            }
        }
    }

    public void resetConesOnEpisode(){

        if(File.Exists(coneconfigFile)){

            ConfigJson configJson=JsonUtility.FromJson<ConfigJson>(File.ReadAllText(coneconfigFile));

            if(File.Exists(predefinedConesFile)){
                loadConesFromFile();
                return;
            }
            if(configJson.createCones){
                if(configJson.randomizeConesEveryReset){
                    int coneCount=configJson.coneCount;
                    numRandCone=coneCount;
                    ResetChallenge();
                    isConfigFileExists=true;
                    return;

                }
                if(configJson.coneCount!=numRandCone){
                    int coneCount=configJson.coneCount;
                    numRandCone=coneCount;
                    ResetChallenge();
                }

                // int coneCount=int.Parse(File.ReadAllText(coneconfigFile));
            }
            else{
                foreach (GameObject createdObject in createdObjects)
                {
                    GameObject.Destroy(createdObject);
                }
                Debug.Log("No Cofig File for Cones");
            }
        }

    }
    public void saveCones(){
        using (StreamWriter writer = new StreamWriter("./predefinedCones.txt", false))
        {
            foreach (GameObject createdObject in createdObjects)
            {
                ColCone col = createdObject.GetComponentInChildren<ColCone>();
                Vector3 conepos = col.transform.position;
                string conePosStr="("+(conepos.x+0.70438)+", 0.0649, "+(conepos.z)+")";
                string coneName=createdObject.name;
                writer.WriteLine(conePosStr+ "*" + coneName); 
            }
        }
    }
    public void loadConesFromFile(){
        string[] conesRawInfo = File.ReadAllLines(predefinedConesFile);
        foreach (GameObject createdObject in createdObjects)
        {
            GameObject.Destroy(createdObject);
        }
        createdObjects = new List<GameObject>();
        foreach (string text in conesRawInfo)
        {
            string[] info= text.Split('*');
            string vector3String=info[0].Replace("(", "").Replace(")"," ");//Replace "(" and ")" in the string with ""
            string[] vector3StringValues=vector3String.Split(',');
            Vector3 posVector= new Vector3(float.Parse(vector3StringValues[0]), float.Parse(vector3StringValues[1]), float.Parse(vector3StringValues[2]));
            GameObject go = Instantiate(conePrefabs[iConePrefab], posVector, conePrefabs[iConePrefab].transform.rotation);
            go.name=info[1];
            ColCone col = go.GetComponentInChildren<ColCone>();
            col.name=info[1];
            createdObjects.Add(go);

        }
    }
    public void Init()
    {
        if (!GlobalState.generateRandomCones) { return; }
        foreach (GameObject createdObject in createdObjects)
        {
            GameObject.Destroy(createdObject);
        }
        if(!GlobalState.isPredefinedCones){
            if(!isConfigFileExists){
                numRandCone=GlobalState.coneCount;
            }
            createdObjects = new List<GameObject>();
            Generate();
        }
        else{
            GeneratePredefinedCones();
        }
    }

    public void GeneratePredefinedCones(){
        
    }

    public void Generate()
    {
        if (GlobalState.useSeed) { Random.InitState(GlobalState.seed); };
        for (int i = 0; i < numRandCone; i++)
        {
            RandomCone(i);
        }
    }

    public void ResetChallenge()
    {
        foreach (GameObject createdObject in createdObjects)
        {
            GameObject.Destroy(createdObject);
        }
        createdObjects = new List<GameObject>();
        Generate();
    }

    public void RandomCone(int index)
    {
        if (pathManager.carPath.centerNodes != null && pathManager.carPath.centerNodes.Count > 0)
        {

            int random_index = Random.Range(nodesAfterStart, pathManager.carPath.centerNodes.Count - nodesAfterStart);
            PathNode random_node = pathManager.carPath.centerNodes[random_index];

            Vector3 rand_pos_offset = new Vector3(Random.Range(-coneOffset, coneOffset), 0, Random.Range(-coneOffset, coneOffset));
            Vector3 xz_coords = new Vector3(random_node.pos.x, random_node.pos.y + coneHeightOffset, random_node.pos.z);
            GameObject go = Instantiate(conePrefabs[iConePrefab], xz_coords + rand_pos_offset, conePrefabs[iConePrefab].transform.rotation);
            // Debug.Log(xz_coords+""+rand_pos_offset);
            go.name="Cone_"+random_index;
            ColCone col = go.GetComponentInChildren<ColCone>();
            col.name="Cone_"+random_index;
            if (col != null) { col.index = index; }
            createdObjects.Add(go);
        }
    }
}
