using Core.Battle;
using Core.Manager;
using Core.Unit.Body;
using UnityEngine;
using EventType = Core.Manager.EventType;

namespace Core.Unit
{
    public class Unit : MonoBehaviour
    {
        private Animator animator;
        [SerializeField] protected Stat unitStat;
        protected BattleStats unitBattleStats;
        [SerializeField] protected Body.Body unitBody;

        protected void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            animator = GetComponent<Animator>();
            unitBody = new Body.Body(this);
            unitBody.InitBody(BodyPreset.Instance.GetBodyPreset(1));
        }

        protected void SetStat(Stat newStat)
        {
            unitStat = newStat;
            //unitBody.SetHp(unitStat.hp);
        }

        public Stat GetStat()
        {
            return unitStat;
        }

        public BattleStats GetBattleStats()
        {
            return unitBattleStats;
        }

        protected virtual void TakeDamage(Unit attacker, int damage)
        {
            if (Dodge(attacker))
            {
                Debug.Log(this.name + "은 " + attacker.name + "의 공격을 회피했다.");
                return;
            }
            animator.Play("hurt");

            unitBody.GetRandomBodyPart().partHp -= damage;
            unitStat.curHp = Mathf.Max(unitStat.curHp - damage, 0);
        
            if (unitStat.curHp == 0)
                Die();
        }

        public virtual void Attack(Unit target) //, bool isPhysicalAttack)
        {
            animator.Play("attack");
            Debug.Log(target);
            target.TakeDamage(this, unitStat.str);

            /*if (isPhysicalAttack)
        {
            target.TakeDamage(unitStat.str);
        }
        else
        {

        }*/
        }

        protected bool Dodge(Unit attacker)
        {
            return attacker.unitStat.accuracyRate - unitStat.dodgeRate > Random.Range(0, 100f);
        }

        public virtual void Die()
        {
            animator.Play("die");
        }

        public bool IsDie()
        {
            return unitStat.hp == 0;
        }
    }
}
