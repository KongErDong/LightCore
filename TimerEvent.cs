using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvent
{
    public int Id;
    public string Name;
    public float Duration;//几秒后运行
    public float Interval;//时间间隔
    public int Count;//运行次数
    public object DataContext;//数据
    public Action<object> CallBack;//函数回调
    public Action Finished;//完成回调

    public double destTime=0;//目标时间
    public double startTime = 0;//开始时间
    public double nowTime = 0;//当前时间
    public double executeTime = 0;//从开始到目前为止等待了多久：按毫秒计算

    DateTime baseTime = new DateTime(1970,1,1,0,0,0,0);
    private bool isStart = false;
    public bool isDone = false;
    private int currentCount = 0;
    public TimerEvent(float duration, float interval, int count, string name, int id = -1, object dataContex=null)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Interval = interval;
        Count = count;
        DataContext = dataContex;

        startTime = GetUTCMisseconds();
        nowTime = startTime;
        destTime = startTime + duration * 1000;

        isStart = true;
        isDone = false;
    }
    /// <summary>
    /// 单次：以后可能要取消运行的
    /// </summary>
    /// <param name="duration">几秒后运行</param>
    /// <param name="callback">任务回调</param>
    /// <param name="dataContex">数据</param>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public TimerEvent(float duration,Action<object>callback,object dataContex, int id=-1,string name=null)
    {
        Id = id;
        Name = name;
        Duration = duration;
        CallBack = callback;
        DataContext = dataContex;
        Count = 1;
       
        Interval = 0;
        startTime = GetUTCMisseconds();
        nowTime = startTime;
        destTime = startTime + duration * 1000;
        isStart = true;
        isDone = false;

    }
    /// <summary>
    /// 多次运行：以后可能会取消
    /// </summary>
    /// <param name="duration">几秒后运行</param>
    /// <param name="interval">时间间隔</param>
    /// <param name="count">运行几次</param>
    /// <param name="callback">回调</param>
    /// <param name="dataContex">数据</param>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public TimerEvent(float duration,float interval,int count,Action<object> callback,object dataContex,int id=-1,string name=null)
    {
        Id = id;
        Name = name;
        Duration = duration;
        Interval = interval;
        Count = count;
        CallBack = callback;
        DataContext = dataContex;

        startTime = GetUTCMisseconds();
        nowTime = startTime;
        destTime = startTime + duration * 1000;

        isStart = true;
        isDone = false;

    }
    public void Update()
    {
        if(isStart)
        {
            nowTime = GetUTCMisseconds();
            executeTime = nowTime - startTime;
            if(nowTime>=destTime)
            {
                RunCallBack();
            }
        }
    }
    double GetUTCMisseconds()
    {
        TimeSpan ts = DateTime.UtcNow - baseTime;
        return ts.TotalMilliseconds;
    }
    void RunCallBack()
    {
        if(CallBack!=null)
        {
            CallBack(DataContext);
            currentCount++;
            if(currentCount>=Count)
            {
              
                isDone = true;
                if(isDone)
                {
                    if (Finished != null)
                    {
                        Finished();
                        Reset();
                    }
                }
            }
            destTime = GetUTCMisseconds() + Interval * 1000;
        }
    }
    void Reset()
    {
        Id = -1;
        Name = null;
        Duration = -1;
        Interval = -1;
        Count = -1;
        currentCount = -1;
        destTime = -1;
        nowTime = -1;
        executeTime=0;
        isStart = false;
        CallBack = null;
        DataContext = null;

    }
}
