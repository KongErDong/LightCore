using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    //单任务
    private static List<TimerEvent> missonList = new List<TimerEvent>();
    private static List<TimerEvent> missionGroupList = new List<TimerEvent>();

    private static Action<object> groupTaskCallback;
    private static bool isGroupMissonStart = false;
    private static object DataContex;
   
   
    public static void Update()
    {
        CheckMisson();
    }
    static void CheckMisson()
    {
        CheckSingleMisson();
        CheckGroupMisson();
    }
    static void CheckSingleMisson()
    {
        int length = missonList.Count;
        TimerEvent temp;
        if (length < 1) return;
        for (int i = length-1; i >=0; i--)
        {
            temp = missonList[i];
            if(temp!=null)
            {
                temp.Update();
                if(temp.isDone)
                {
                    missonList.RemoveAt(i);
                }
            }
        }
    }
    static void CheckGroupMisson()
    {
        if (!isGroupMissonStart) return;
        int length = missionGroupList.Count;
        TimerEvent temp;
        if (length < 1) return;
        for (int i = length - 1; i >= 0; i--)
        {
            temp = missionGroupList[i];
            if(temp!=null)
            {
                temp.Update();
                if(temp.isDone)
                {
                    missionGroupList.RemoveAt(i);
                }
            }
        }
        if(missionGroupList.Count<=0)
        {
            isGroupMissonStart = false;
            if(groupTaskCallback!=null)
            groupTaskCallback(DataContex);
        }
    }
    /// <summary>
    /// 添加：单个任务
    /// </summary>
    /// <param name="mission"></param>
    public static void AddMisson(TimerEvent mission)
    {
        if (mission != null)
            missonList.Add(mission);
    }
    /// <summary>
    /// 添加任务组
    /// </summary>
    /// <param name="missons"></param>
    /// <param name="callBack"></param>
    /// <param name="dataContex"></param>
    public static void AddMissons(TimerEvent[] missons, Action<object> callBack = null, object dataContex = null)
    {
        if (missons != null)
        {
            missionGroupList.AddRange(missons);
            groupTaskCallback = callBack;
            isGroupMissonStart = true;
            DataContex = dataContex;
        }

    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isGroup"></param>
    public static void DeleteMisson(string name,bool isGroup=false)
    {
        if(!isGroup)
        {
            DeleMisson(name);
        }
        else
        {
            DeleMissonOfGroup(name);
        }
      
    }
    /// <summary>
    /// 替换:限于单次运行的任务
    /// </summary>
    /// <param name="newOne"></param>
    /// <param name="isGroup"></param>
    public static void ReplaceMission(TimerEvent newOne, bool isGroup = false)
    {
        if (!isGroup)
        {
            ReplaceMisson(newOne);
        }
        else
        {
            ReplaceMissonOfGroup(newOne);
        }
    }
    /// <summary>
    /// 延迟执行：单次运行的任务
    /// </summary>
    /// <param name="name"></param>
    /// <param name="delayTime"></param>
    /// <param name="isGroup"></param>
    public static void DelayMission(string name,float delayTime,bool isGroup=false)
    {
        if(!isGroup)
        {
            DelayMission(name,delayTime);
        }
        else
        {
            DelayMissionOfGroup(name,delayTime);
        }
    }

    static void DeleMisson(string name)
    {
        if (name == null) return;
        int length = missonList.Count;
        if (length < 1) return;
        TimerEvent temp;
        for (int i = length-1; i >=0; i--)
        {
            temp = missonList[i];
            if(name.Equals(temp.Name))
            {
                missonList.RemoveAt(i);
                break;
            }
        }
        Debug.Log(missonList.Count);
        Debug.Log("删除了>>>>>>");
    }
    static void DeleMissonOfGroup(string name)
    {
        if (name == null) return;
        int length = missionGroupList.Count;
        if (length < 1) return;
        TimerEvent temp;
        for (int i = length - 1; i >= 0; i--)
        {
            temp = missionGroupList[i];
            if (name.Equals(temp.Name))
            {
                missionGroupList.RemoveAt(i);
                break;
            }
        }
    }
    static void ReplaceMisson(TimerEvent newOne)
    {
        int length = missonList.Count;
        if (newOne == null) return;
        if (newOne.Name == null) return;
        TimerEvent temp;
        for (int i = 0; i < length; i++)
        {
            temp = missonList[i];
            if (newOne.Name.Equals(temp.Name))
            {
                temp.CallBack = newOne.CallBack;
                temp.DataContext = newOne.DataContext;
                temp.destTime = temp.startTime + newOne.Duration * 1000;
                break;
             }
        }
    }
    static void ReplaceMissonOfGroup(TimerEvent newOne)
    {
        int length = missionGroupList.Count;
        if (newOne == null) return;
        if (newOne.Name == null) return;
        TimerEvent temp;
        for (int i = 0; i < length; i++)
        {
            temp = missionGroupList[i];
            if (newOne.Name.Equals(temp.Name))
            {
                temp.CallBack = newOne.CallBack;
                temp.DataContext = newOne.DataContext;
                break;
            }
        }
    }
    static void DelayMission(string name,float delayTime)
    {
        int length = missonList.Count;
      
        if (name == null) return;
        TimerEvent temp;
        for (int i = 0; i < length; i++)
        {
            temp = missonList[i];
            if (name.Equals(temp.Name))
            {
                temp.destTime += delayTime * 1000;
                break;
            }
        }
    }
    static void DelayMissionOfGroup(string name, float delayTime)
    {
        int length = missionGroupList.Count;

        if (name == null) return;
        TimerEvent temp;
        for (int i = 0; i < length; i++)
        {
            temp = missionGroupList[i];
            if (name.Equals(temp.Name))
            {
                temp.destTime += delayTime * 1000;
                break;
            }
        }
    }

    public static void ClearAllMission()
    {
        
        int length = missonList.Count;
        if(length>0)
        {
            for (int i = length - 1; i > +0; i--)
            {
                missonList[i] = null;
            }
            missonList.Clear();
        }
        length = missionGroupList.Count;
        if (length > 0)
        {
            for (int i = length - 1; i > +0; i--)
            {
                missionGroupList[i] = null;
            }
            missionGroupList.Clear();
        }


    }
}
