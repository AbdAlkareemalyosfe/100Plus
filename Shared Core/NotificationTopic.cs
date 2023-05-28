using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Core
{
    public enum NotificationTopic
    {
        //App: This value could represent general notifications related to the app,
        //such as updates, maintenance, or other app-related information.
        App,
       //Bay: This value could represent notifications related to a specific location or geographical area,
       //such as deals or promotions at a nearby bay or harbor.
        Bay,
        //Meals: This value could represent notifications related to food or dining, such as specials or discounts at restaurants.
        Meals,
        Updates,
        Alerts,
        Events,
        News,
        //Offers: This value could represent notifications related to promotions, discounts, or other special offers.
        Offers,
        //Purchase: This value could represent notifications related to purchases,
        //such as order confirmations, shipping updates, or other purchase-related information.
        Purchase,
        //Paid: This value could represent notifications related to transactions or payments,
        //such as payment confirmations, invoice reminders, or other payment-related information.
        Paid,
        //Receive: This value could represent notifications related to receiving something
        //such as a package delivery, a message, or other receive-relatedinformation.
        Receive,
        //SendMeal: This value represents notifications related to sending a meal,
        //such as delivery notifications, order confirmations, or other meal-related information.
        SendMeal
    }
}
