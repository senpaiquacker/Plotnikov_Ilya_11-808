// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataBaseWorker;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using ContentSubscriptions;
using System.Linq;
using System.Collections.Concurrent;
using EchoBot;
namespace Microsoft.BotBuilderSamples.Bots
{
    public class PostBot : ActivityHandler
    {
        //private string memberId;
        //private static BotContext db = new BotContext();
        /*private static Dictionary<string,Subscription> dictionary = new Dictionary<string, Subscription>()
        {
            {"RedditHot", RedditSubscription.RedditHot}
        };*/
        
        private static bool isLoopStarted = false;
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string memberId = turnContext.Activity.Recipient.Id;
            string[] message = turnContext.Activity.Text.Split(' ').Select(a => a.ToLower()).ToArray();
            switch (message[0].ToLower())
            {
                case "/subscribe":
                    if (message.Length == 1)
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text("Please type /subscribe and Name of subreddit after space"), cancellationToken);
                        break;
                    }
                    if (!RedditSubscription.RedditHot.IsKeyCorrect(message[1]))
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text

                        ("Please take existing SubReddit (NOTE: maybe you typed \"r\\*subreddit_name*\" instead of \"*subreddit*\"?"));
                        break;
                    }
                    if (!DBOperations.DoesSubExist(memberId, "RedditHot", message[1]))
                        DBOperations.AddSub(memberId, "RedditHot", message[1]);
                    else await turnContext.SendActivityAsync(MessageFactory.Text("You're already subscribed, retart"));
                    break;
                case "/unsubscribe":
                    if (message.Length == 1)
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text("Please type /unsubscribe and Name of subreddit after space"), cancellationToken);
                        break;
                    }
                    if (DBOperations.DoesSubExist(memberId, "RedditHot", message[1]))
                        DBOperations.RemoveSub(memberId, "RedditHot", message[1]);
                    break;
                default:
                    var replyText = "I'm the fucking bot. I can't talk with you, sorry";
                    await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
                    break;
            }
            if (isLoopStarted)
                return;
            isLoopStarted = true;
            
            while (true)
            {
               var sub = DBOperations.GetSubWithZeroPriority(memberId);
                if (sub == null)
                    continue;
                var subName = sub.Subscription;
                var subKey = sub.SubscriptionKey;
                if (subName == "RedditHot")
                {
                    var subreddit = RedditSubscription.RedditHot;
                    var content = subreddit.Content(subKey).First(content => !DBOperations.HaveUserSeenContent(memberId, content.UniqueID));
                    string text = subKey + ": " + content.ToMessage;
                    await turnContext.SendActivityAsync(MessageFactory.Text(text), cancellationToken);
                    DBOperations.AddSeenContent(memberId, content.UniqueID);
                    DBOperations.ShiftPriority(memberId);
                    Thread.Sleep(5000);
                }
            }
        }
        /*protected override async Task OnMembersRemovedAsync(IList<ChannelAccount> membersRemoved,
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            foreach(var member in membersRemoved)
            {
                DBOperations.RemoveUser(member.Id);
            }
        }*/
        
        protected override async Task OnMembersAddedAsync
            (IList<ChannelAccount> membersAdded, 
            ITurnContext<IConversationUpdateActivity> turnContext, 
            CancellationToken cancellationToken)
        {
            var channelId = turnContext.Activity.Recipient.Id;
            var welcomeText = "Hello and welcome! Welcome to Subreddit Postman";
            if (!DBOperations.CheckUser(channelId))
            {
                DBOperations.AddUser(channelId);  
            }
            await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            //Start();
            //PostExtractor.Begin();
        }
    }
}
