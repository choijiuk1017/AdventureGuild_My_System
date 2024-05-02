using System;

namespace Core.Unit.Body.BodyPart
{
    public enum BodyPartType
    {
        Head, Arm, Leg, Torso
    }
    
    [Serializable]
    public class BodyPart
    {
        public int partID;
        public string partName;
        public float partHp;
        public float partCurHp;
        public float partDefense;
        public float partCurDefense;
        public float partAccuracy;
    }
}
