using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using Newtonsoft.Json;

namespace MinigameRitmo
{
    public class RitmoJson : MonoBehaviour
    {
        [SerializeField]
        private RitmoControl _ritmoControl;

        private string folderPath;
        private string filePath;
        public string fileName;
        
        [System.Serializable]
        public class ListSave
        {
            public List<ArrowList> arrowsOrder = new List<ArrowList>();
        }
        [SerializeField]
        public ListSave listSave = new ListSave();
        private void Start()
        {
            TakeComponents();
            if (_ritmoControl == null)
                return;
            
        }

        private void Awake()
        {
            CreatePath();
        }

        private void CreatePath()
        {
            folderPath = Path.Combine(Application.dataPath, "JSON/MinigameRitmo");
            filePath = Path.Combine(folderPath, fileName + ".json");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
        }

        [ContextMenu("Salvar JSON")]
        public void SaveJson()
        {
            CreatePath();
            listSave.arrowsOrder = _ritmoControl.arrowsOrder;
            string json = JsonUtility.ToJson(listSave, true);
            File.WriteAllText(filePath, json);
            Debug.Log("✅ JSON salvo em: " + filePath);

            // Atualiza o Unity pra exibir o novo arquivo no Project
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif
        }

        [ContextMenu("Carregar JSON")]
        private void LoadJson()
        {
            CreatePath();
            
            string json = File.ReadAllText(filePath); 
            listSave = JsonUtility.FromJson<ListSave>(json);
            _ritmoControl.arrowsOrder = new  List<ArrowList>(listSave.arrowsOrder);
            Debug.Log("JSON CARREGADO");
                
        }
        private void TakeComponents()
        {
            if (_ritmoControl == null)
            {
                _ritmoControl = GameObject.FindObjectOfType<RitmoControl>().GetComponent<RitmoControl>();
            }
        }
    }
}
