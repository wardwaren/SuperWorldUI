using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StorageManager
{
    public void SaveData(object objectToSave, string fileName)
        {
            string filePath = Application.persistentDataPath + "/" + fileName + ".bin";
            
            BinaryFormatter Formatter = new BinaryFormatter();
            
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            
            Formatter.Serialize(fileStream, objectToSave);
            
            fileStream.Close();
        }

        public object LoadData(string fileName)
        {
            string filePath = Application.persistentDataPath + "/" + fileName + ".bin";
            
            if (File.Exists(filePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                object obj = Formatter.Deserialize(fileStream);
                fileStream.Close();
                return obj;
            }
            else
            {
                return null;
            }
        }
}
