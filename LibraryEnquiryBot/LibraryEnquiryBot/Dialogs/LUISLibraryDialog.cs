using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library;
using System.Threading.Tasks;

namespace LibraryEnquiryBot.Dialogs
{
    [LuisModel("70e11177-7c42-4300-82b5-e52cdfb55704", "9fc2fdf856cd49e29e71bae07c8558a4")]
    [Serializable]
    public class LUISLibraryDialog : LuisDialog<object>
    {
        public static LibraryEntity library = new LibraryEntity();

        [LuisIntent("bookCount")]
        public async Task BookCount(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"The total Number of books are {library.GetTotalBooksInLibrary()}");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("No Idea what you just said. Please say again.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi There. Welcome to the library. Say something.");
            context.Wait(MessageReceived);
        }

        string bookName = "";
        [LuisIntent("getBookAuthor")]
        public async Task GetBookAuthor(IDialogContext context, LuisResult result)
        {
            EntityRecommendation rec;
            if (result.TryFindEntity("bookName", out rec))
            {
                bookName = rec.Entity;
                string authorName = library.GetBookAuthorName(bookName);
                await context.PostAsync($"The Author of the book {bookName} is {authorName}");
            }
            else
            {
                await context.PostAsync($"The author of {bookName} was not found.");
            }
            context.Wait(MessageReceived);
        }
    }
}