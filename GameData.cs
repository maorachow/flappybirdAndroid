using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public struct GameScore
{


    public int highScoreEasy { get; set; }
    public int highScoreModerate { get; set; }
    public int highScoreHard { get; set; }
    public GameScore(int highScoreEasy, int highScoreModerate, int highScoreHard)
    {
        this.highScoreEasy = highScoreEasy;
        this.highScoreModerate = highScoreModerate;
        this.highScoreHard = highScoreHard;
    }

}
public class GameData
{
    public static string gameWorldDataPath = AppDomain.CurrentDomain.BaseDirectory;
    public static void SaveGameData()
    {

        FileStream fs;
        if (File.Exists(gameWorldDataPath + "flappyBirdData/GameData/data.json"))
        {
            fs = new FileStream(gameWorldDataPath + "flappyBirdData/GameData/data.json", FileMode.Truncate, FileAccess.Write);//Truncate模式打开文件可以清空。
        }
        else
        {
            fs = new FileStream(gameWorldDataPath + "flappyBirdData/GameData/data.json", FileMode.Create, FileAccess.Write);
        }
        fs.Close();

        string jsonData = JsonSerializer.Serialize(new GameScore(Gameplay.highScoreEasy, Gameplay.highScoreModerate, Gameplay.highScoreHard));
        File.WriteAllText(gameWorldDataPath + "flappyBirdData/GameData/data.json", jsonData);

    }

    public static void ReadJson()
    {


        if (!Directory.Exists(gameWorldDataPath + "flappyBirdData"))
        {
            Directory.CreateDirectory(gameWorldDataPath + "flappyBirdData");

        }
        if (!Directory.Exists(gameWorldDataPath + "flappyBirdData/GameData"))
        {
            Directory.CreateDirectory(gameWorldDataPath + "flappyBirdData/GameData");
        }

        if (!File.Exists(gameWorldDataPath + "flappyBirdData" + "/GameData/data.json"))
        {
            FileStream fs = File.Create(gameWorldDataPath + "flappyBirdData" + "/GameData/data.json");
            fs.Close();
        }

        string jsonData = File.ReadAllText(gameWorldDataPath + "flappyBirdData/GameData/data.json");
        if (jsonData.Length > 0)
        {
            GameScore scoreData = JsonSerializer.Deserialize<GameScore>(jsonData);
            Gameplay.highScoreEasy = scoreData.highScoreEasy;
            Gameplay.highScoreModerate = scoreData.highScoreModerate;
            Gameplay.highScoreHard = scoreData.highScoreHard;
        }

        /*  List<ChunkData> tmpList = new List<ChunkData>();
          foreach (string s in worldData)
          {
              ChunkData tmp = JsonConvert.DeserializeObject<ChunkData>(s);
              tmpList.Add(tmp);
          }
          foreach (ChunkData w in tmpList)
          {
              chunkDataReadFromDisk.Add(new Vector2Int(w.chunkPos.x, w.chunkPos.y), w);
          }*/



    }
}

