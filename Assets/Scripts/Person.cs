using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : Entity
{
    public int wanderTime = 0;
    public Job job;
    public float moveTick = 0;

    [SerializeField] private int hp = 100;
    private int maxHp = 100;
    private int xp = 0;
    private int nextLevel = 1;
    private int level = 0;
    public float rot = 0;

    private void Awake()
    {
        r = .1f;
        moveTick = Random.Range(0, 13);
    }

    public override void tick()
    {

        if(job != null)
        {
            job.tick();
        }

        if(hp < maxHp && Random.Range(0,6) == 0)
        {
            hp++;
        }

        float speed = .2f;
        if (wanderTime == 0 && job != null && job.hasTarget())
        {
            float xd = job.xTarget - x;
            float yd = job.yTarget - y;
            float rd = job.targetDist + r;
            if (xd * xd + yd * yd < rd * rd)
            {
                job.arrived();
                speed = 0;
            }
            rot = Mathf.Atan2(yd, xd);
        }
        else
        {
            rot += (Random.value - .5f) * Random.value * 2;
        }
        
        if (wanderTime > 0) wanderTime--;
        
        speed += level * 0.1f;
        
        float xt = x + Mathf.Cos(rot) * .4f * speed;
        float yt = y + Mathf.Sin(rot) * .4f * speed;
        if(GameManager.init.isFree(xt, yt, r, this))
        {
            x = xt;
            y = yt;
        }
        else
        {
            if (job != null)
            {
                Entity collided = GameManager.init.getEntityAt(xt, yt, r, this);
                if (collided != null)
                {
                    job.collide(collided);
                }
                else
                {
                    job.cantReach();
                }
            }
            rot = (Random.value) * Mathf.PI * 2;
            wanderTime = Random.Range(0, 31) + 3;
        }         
        
        moveTick += speed;

        transform.position = new Vector3(x, y, 0);

    }

    public void setJob(Job job)
    {
        this.job = job;
        if (job != null) job.init(this);
    }

    
}
