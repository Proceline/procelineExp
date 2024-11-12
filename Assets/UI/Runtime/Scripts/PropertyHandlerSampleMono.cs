using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime.Scripts
{
    public class PropertyHandlerSampleMono : MonoBehaviour
    {
        public PropertyHandlerSample propHandler = new();
        public Text debugText;

        private void Start()
        {
            propHandler.PropertyChanged += OnPropertyChangedToText;
            propHandler.IntValue = 10;
        }
        
        private void OnPropertyChangedToText(object sender, PropertyChangedEventArgs e)
        {
            Debug.Log($"Property '{e.PropertyName}' has changed.");
        }
    }
}
