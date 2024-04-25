using Core.Unit;
using UnityEngine;

namespace Core.Guild
{
    public enum GuildEntityType
    {
        NoticeBoard, ReceptionDesk, Shop, Temple, Entrance
    }
    
    public abstract class GuildEntity : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Start()
        {
            InitEntity();
        }

        protected abstract void InitEntity();

        public void ReadyForInteraction(Adventure adventureEntity)
        {
            adventureEntity.AdventureAI.targetObject = gameObject;
            adventureEntity.AdventureAI.ChangeState(AdventureStateType.Move);
        }
        public abstract void OnInteraction(Adventure adventureEntity);
        public abstract void EndInteraction();
    }
}
