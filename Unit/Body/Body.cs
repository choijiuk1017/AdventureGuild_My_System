using System;
using System.Collections.Generic;
using Core.Manager;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Unit.Body
{
    [Serializable]
    public class Body
    {
        [SerializeField] private List<BodyPart.BodyPart> bodyParts;
        private List<BodyPart.BodyPart> destroyedBodyParts;

        private Unit bodyOwner;

        public Body(Unit bodyOwner)
        {
            this.bodyOwner = bodyOwner;
            bodyParts = new List<BodyPart.BodyPart>();
            destroyedBodyParts = new List<BodyPart.BodyPart>();
        }

        public void InitBody(List<BodyData> bodyData)
        {
            foreach (var bData in bodyData)
            {
                var bPart = new BodyPart.BodyPart()
                {
                    partID = bData.partID,
                    partName = bData.partName,
                    partHp = bData.partHpPercent,
                    partAccuracy =  bData.partPercent,
                };
                
                bodyParts.Add(bPart);
            }
        }

        public void SetHp(float hp)
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].partHp = (bodyParts[i].partHp / hp) * 100;
                bodyParts[i].partCurHp = bodyParts[i].partHp;
            }
        }

        public BodyPart.BodyPart GetRandomBodyPart()
        {
            int targetIdx = 0;
            float maxPercent = 0;
            
            for (int i = 0; i < bodyParts.Count; i++)
            {
                var curPercent = Random.Range(0, bodyParts[i].partAccuracy);

                if (maxPercent < curPercent)
                {
                    targetIdx = i;
                    maxPercent = curPercent;
                }
            }
            
            return bodyParts[targetIdx];
        }

        public List<BodyPart.BodyPart> GetBodyParts()
        {
            return bodyParts;
        }

        public BodyPart.BodyPart GetBodyPart(int i)
        {
            return bodyParts[i];
        }
    }
}
