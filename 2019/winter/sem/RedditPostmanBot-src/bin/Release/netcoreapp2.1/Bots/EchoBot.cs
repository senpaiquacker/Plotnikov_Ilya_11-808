// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataBaseWorker;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using ContentSubscriptions;
using System.Linq;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        private static BotContext db = new BotContext();
        /*private static Dictionary<string,Subscription> dictionary = new Dictionary<string, Subscription>()
        {
            {"RedditHot", RedditSubscription.RedditHot}
        };*/
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            using (db)
            {


                string memberId = turnContext.Activity.Recipient.Id;
                string[] message = turnContext.Activity.Text.Split(' ');
                if (DBOperational.GetUserSubList(memberId).Count > 0)
                    foreach (var subreddit in DBOperational.GetUserSubList(memberId))
                    {
                        var subName = subreddit.Split(' ')[0];
                        var subKey = subreddit.Split(' ')[1];
                        if (subName == "RedditHot")
                        {
                            var sub = RedditSubscription.RedditHot;
                            var content = sub.Content(subKey).First(content => DBOperational.AddReadPost(memberId, content.UniqueID));
                            string text = content.ToMessage;
                            await turnContext.SendActivityAsync(MessageFactory.Text(text), cancellationToken);
                            Thread.Sleep(3600000);
                        }
                    }
                switch (message[0].ToLower())
                {
                    case "/addsubreddit":
                        if (message.Length == 1)
                        {
                            await turnContext.SendActivityAsync(MessageFactory.Text("Please type /addsubreddit and Name of subreddit after space"), cancellationToken);
                            return;
                        }
                        DBOperational.AddSub(memberId, "RedditHot", message[1]);
                        return;
                    case "/deletesubreddit":
                        if (message.Length == 1)
                        {
                            await turnContext.SendActivityAsync(MessageFactory.Text("Please type /deletesubreddit and Name of subreddit after space"), cancellationToken);
                            return;
                        }
                        DBOperational.RemoveSub(memberId, "RedditHot", message[1]);
                        return;
                    default:
                        var replyText = "I'm bot. I can't talk with you, sorry";
                        await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
                        return;
                }
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            using (db)
            {
                var welcomeText = "Hello and welcome! Welcome to Subreddit Postman";
                foreach (var member in membersAdded)
                {
                    if (member.Id != turnContext.Activity.Recipient.Id)
                    {
                        DBOperational.AddUser(turnContext.Activity.Recipient.Id);
                        await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                    }
                }
            }
        }
    }
}
