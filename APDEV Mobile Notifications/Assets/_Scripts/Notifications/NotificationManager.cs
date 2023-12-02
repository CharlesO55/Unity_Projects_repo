using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    private string _defaultChannelID = "default";
    private string _repeatChannelID = "repeat";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.BuildNotifcationChannel();
            this.BuildRepeatingNotificationChannel();
        }
        else
            Destroy(this.gameObject);
    }

    
    private void Start()
    {
        GUIManager.Instance.BtnSendNotif.clicked += SendNotification;
        GUIManager.Instance.BtnSendRepeating.clicked += SendRepeatingNotification;
    }

    private void OnDestroy()
    {

        GUIManager.Instance.BtnSendNotif.clicked -= SendNotification;
        GUIManager.Instance.BtnSendRepeating.clicked -= SendRepeatingNotification;
    }

    private void BuildNotifcationChannel()
    {
        string title = "Default Channel";
        string description = "The default channel";
        AndroidNotificationChannel channel = new AndroidNotificationChannel(_defaultChannelID, title, description, Importance.Default);

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendNotification()
    {
        Debug.Log("Sending notification");

        string title = "Sample notification";
        string message = "Hello";
        System.DateTime time = System.DateTime.Now.AddSeconds(1);

        AndroidNotification notification = new AndroidNotification(title, message, time);
        AndroidNotificationCenter.SendNotification(notification, this._defaultChannelID);
    }

    private void BuildRepeatingNotificationChannel()
    {
        string title = "Repeat Channel";
        string description = "The repeat channel";
        AndroidNotificationChannel channel = new AndroidNotificationChannel(_repeatChannelID, title, description, Importance.High);

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void SendRepeatingNotification()
    {
        Debug.Log("Sending repating");

        string title = "Repeating notification";
        string message = "Repeating";
        System.DateTime time = System.DateTime.Now;


        TimeSpan interval = new TimeSpan(0, 0, 2);

        AndroidNotification notification = new AndroidNotification(title, message, time, interval);
        AndroidNotificationCenter.SendNotification(notification, this._repeatChannelID);
    }
}
