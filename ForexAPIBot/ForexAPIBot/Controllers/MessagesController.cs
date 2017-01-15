using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ForexAPIBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //var client = new HttpClient() { BaseAddress = new Uri("http://api.fixer.io") };
                //var result = client.GetStringAsync("/latest").Result;
                //var data = ((dynamic)Newtonsoft.Json.Linq.JObject.Parse(result));

                await Conversation.SendAsync(activity, () => new ForexDialog());
                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //// calculate something for us to return
                //int length = (activity.Text ?? string.Empty).Length;

                //// return our reply to the user
                //Activity reply = activity.CreateReply($"The forex result is {data}");
                //await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }

    [Serializable]
    public class ForexDialog : IDialog<Object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedStart);
        }

        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("Hello there. Enter the country code you wanna know the exchange rate about.");
            context.Wait(ReplyWithForexRates);
        }

        public async Task ReplyWithForexRates(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var countryCode = message.Text;
            var client = new HttpClient() { BaseAddress = new Uri("http://api.fixer.io") };
            var addr = "/latest?base=" + countryCode;
            var result = client.GetStringAsync(addr).Result;

            ForexRootObject RateList = JsonConvert.DeserializeObject<ForexRootObject>(result);

            Type type = typeof(Rates);
            StringBuilder ans = new StringBuilder($"Rates for {countryCode} on {RateList.date.ToString()} : \n");
            foreach (PropertyInfo item in type.GetProperties())
            {
                ans.Append(item.Name);
                ans.Append(" = ");
                ans.Append(item.GetValue(RateList.rates, null));
                ans.AppendLine("\n");
            }

            await context.PostAsync($"{ans}");
            await context.PostAsync("Thank you for using the bot.");
            context.Wait(MessageReceivedStart);
        }
    }

    public class Rates
    {
        public double AUD { get; set; }
        public double BGN { get; set; }
        public double BRL { get; set; }
        public double CAD { get; set; }
        public double CHF { get; set; }
        public double CNY { get; set; }
        public double CZK { get; set; }
        public double DKK { get; set; }
        public double GBP { get; set; }
        public double HKD { get; set; }
        public double HRK { get; set; }
        public double HUF { get; set; }
        public double IDR { get; set; }
        public double ILS { get; set; }
        public double INR { get; set; }
        public double JPY { get; set; }
        public double KRW { get; set; }
        public double MXN { get; set; }
        public double MYR { get; set; }
        public double NOK { get; set; }
        public double NZD { get; set; }
        public double PHP { get; set; }
        public double PLN { get; set; }
        public double RON { get; set; }
        public double RUB { get; set; }
        public double SEK { get; set; }
        public double SGD { get; set; }
        public double THB { get; set; }
        public double TRY { get; set; }
        public double ZAR { get; set; }
        public double EUR { get; set; }
    }

    public class ForexRootObject
    {
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }
}