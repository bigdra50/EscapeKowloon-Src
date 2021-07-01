using Sirenix.OdinInspector;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items
{
    public class OutlineHandler
    {
        private Material _material;
        public OutlineHandler(Material mat)
        {
            _material = mat;
        }
        public void Switch(string shaderPropname, int value)
        {
            _material.SetFloat(shaderPropname, value);
        }
        
    }
}