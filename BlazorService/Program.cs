using System;
using System.Threading.Tasks;
using Totem.App.Service;
using Totem.Timeline.Hosting;

namespace BlazorService
{
    class Program
    {
        public static Task Main()
        {
            return ServiceApp.Run<ApplicationArea>();
        }
    }
    class ApplicationArea : TimelineArea
    {
        public ApplicationArea() : base() { }
    }
}
