using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;


namespace AGRServicos.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public async override void OnActivated(UIApplication application)
        {
            Console.WriteLine("OnActivated called, App is active.");
        }

        public override void WillEnterForeground(UIApplication application)
        {
            Console.WriteLine("App will enter foreground");
        }

        public override void OnResignActivation(UIApplication application)
        {
            Console.WriteLine("OnResignActivation called, App moving to inactive state.");
        }

        public async override void DidEnterBackground(UIApplication application)
        {
            Console.WriteLine("App entering background state.");

            nint taskId = UIApplication.SharedApplication.BeginBackgroundTask(() => { });
            new Task(async () =>
            {
                await Task.Delay(120000);
                double timeRemaining = UIApplication.SharedApplication.BackgroundTimeRemaining;
                Console.WriteLine($"tarefa iniciada {timeRemaining}");

                //Finaliza o seviço.
                UIApplication.SharedApplication.EndBackgroundTask(taskId);
            }).Start();

            Console.WriteLine("App entering background state.");
        }
        // not guaranteed that this will run
        public override void WillTerminate(UIApplication application)
        {
            Console.WriteLine("App is terminating.");
        }
    }
}
