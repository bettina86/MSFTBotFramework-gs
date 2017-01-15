using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormFlow1.FormFlow
{
    [Serializable]
    public class Enquiry
    {
        [Prompt("What is your Name?")]
        public string Name { get; set; }

        [Prompt("What is your Company Name?")]
        public string Company { get; set; }
        [Prompt("What is your Job Title?")]
        public string JobTitle { get; set; }

        [Prompt("How can we contact you?")]
        public string Phone { get; set; }

        [Prompt("Can we sign up to the mailing list? {||}")]
        public bool SignMeUpToTheMailingList { get; set; }

        [Prompt("What kind of service do you require ? {||}")]
        public Service ServiceRequired { get; set; }
        public enum Service {
            AccountOpening, AccountClosing, FundTransfer,
        }
        public static IForm<Enquiry> BuildEnquiryForm()
        {
            return new FormBuilder<Enquiry>().Build();
        }
    }
}