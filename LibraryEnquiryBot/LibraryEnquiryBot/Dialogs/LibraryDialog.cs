using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Library;
namespace LibraryEnquiryBot.Dialogs
{
    [Serializable]
    public class LibraryDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync(" Do you want to query something about our library?");
            context.Wait(ActivityStartMethod);
        }

        private async Task ActivityStartMethod(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Do you want to query something about our library?");
            LibraryEntity library = new LibraryEntity();
            var activity = await result as Activity;
            if (activity.Text.Contains("how many books"))
            {
                await context.PostAsync($"There are {library.GetTotalBooksInLibrary()} books in the library. Thanks.");
            }
            else if (activity.Text.Contains("librarian"))
            {
                await context.PostAsync($"The librarian name is {library.LibrarianName}");
            }
            else
            {
                context.Wait(ActivityStartMethod);
            }
            
        }
    }
}