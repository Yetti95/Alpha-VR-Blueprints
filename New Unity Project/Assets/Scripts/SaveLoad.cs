using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData
{
    public static string playerName = "Test";
    public static int level = 1;

}
public class SaveLoad
{


    public static void Save()
    {
        GameData.playerName = "Aj";
        GameData.level = 10;
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fs = new FileStream("gamesave.bin", FileMode.Create, FileAccess.Write))
        {
            binaryFormatter.Serialize(fs, GameData.playerName);
            binaryFormatter.Serialize(fs, GameData.level);

        }
    }
    public static void Load()
    {
        if (!File.Exists("gamesave.bin"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("gamesave.bin", FileMode.Open, FileAccess.Read))
            {
                GameData.playerName = (string)binaryFormatter.Deserialize(fs);
                GameData.level = (int)binaryFormatter.Deserialize(fs);

            }
        }
    }
}
