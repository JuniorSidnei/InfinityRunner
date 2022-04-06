using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using InfinityRunner.Scriptables;

namespace InfinityRunner.Save {
    
    public static class SaveSystem  {

        public static void SavePlayerStatus(PlayerStatus playerStatus) {
            var formatter = new BinaryFormatter();
            var path = GetStatusPath();
            var stream = new FileStream(path, FileMode.Create);

            var data = new PlayerStatusData(playerStatus);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerStatusData LoadPlayerStatus() {
            
            var path = GetStatusPath();
            
            if (File.Exists(path)) {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var data = formatter.Deserialize(stream) as PlayerStatusData;
                stream.Close();
                return data;
            } 
            else {
                Debug.LogError("Save file not exist in: " + GetStatusPath());
                return null;
            }
        }

        private static string GetStatusPath() {
            return Application.persistentDataPath + "/player.status";
        }
    }
}