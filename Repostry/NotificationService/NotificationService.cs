using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Models;
using System.Net.Http.Headers;
using System.Runtime;
using Twilio.TwiML.Voice;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Repostry.NotificationService
{
    public class NotificationService
    {

    public string  SendNotification(string title, string message)
    {   
        var serverKey = "AAAAGMz7oR0:APA91bFznGbjO602fQdExF0RvJYyF9BAIvHLOQP40RwLlreL8KaaQDKjIx4CnVDT3QvRmssMB-XVJpv2_JPkjOM5QmANwvuGvGPuCTmLNgZOja6IiufNzBsiti-hCrj2XwQ4IX35Bp8K";
        var senderId = "106518257949";
        var data = new
        {
            to = "/topics/all",
            notification = new
            {
                title = title,
                body = message,
                sound = "default"
            }
        };
        var jsonBody = JsonConvert.SerializeObject(data);
        using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
        {
            httpRequest.Headers.TryAddWithoutValidation("Authorization", $"key={serverKey}");
            httpRequest.Headers.TryAddWithoutValidation("Sender", $"id={senderId}");
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var result =  httpClient.Send(httpRequest);
                if (result.IsSuccessStatusCode)
                {
                 return ("Notification sent successfully.");
                }
                else
                {
                 return ($"Failed to send notification. Error: {result.StatusCode}");
                }
            }
        }
    }


    //public async Task<string> SendNotificationAsync(NotificationBase notification)
    //{
    //    // Initialize the FirebaseApp with credentials
    //    // Load the JSON service account key file
    //    string pathToJsonFile = "D:\\100Plus\\Repostry\\NotificationService\\google-services (1).json";
    //    string json = File.ReadAllText(pathToJsonFile);

    //    // Use the GoogleCredential.FromJson method to create a credential object
    //    using (var stream = new FileStream(pathToJsonFile, FileMode.Open, FileAccess.Read))
    //    {
    //        GoogleCredential credential = GoogleCredential.FromStream(stream);
    //        FirebaseApp.Create(new AppOptions
    //        {
    //            Credential = credential
    //        });
    //    }




    //    // Create a new message
    //    var message = new Message()
    //    {
    //        Notification = new Notification
    //        {
    //            Title = notification.Title,
    //            Body = notification.Body
    //        },
    //       Topic = notification.Topic.ToString(),
    //    };

    //    // Send the message
    //    var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    //    return "Successfully sent message: " + response;
    //}
}
}


//FcmSettings settings = new FcmSettings()
//{
//    SenderId = _config.GetSection("FcmNotification").GetSection("SenderId").Value,
//    ServerKey = _config.GetSection("FcmNotification").GetSection("ServerKey").Value,
//};

//HttpClient httpClient = new HttpClient();

//string authorizationKey = string.Format("keyy={0}", settings.ServerKey);

//httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
//httpClient.DefaultRequestHeaders.Accept
//        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
//var fcm = new FcmSender(settings, httpClient);
//var fcmSendResponse = await fcm.SendAsync(item.FcmToken, res);
