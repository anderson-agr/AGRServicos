using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using System.Threading;

namespace AGRServicos.Droid
{
    [Service]
    public class MyService : Service
    {
        private MyServiceBinder _binder;

        public override IBinder OnBind(Intent intent)
        {
            _binder = new MyServiceBinder(this);
            return _binder;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            DoWork();

            // o serviço não para até chamar o metodo  StopSelf() ou estouro de memória
            return StartCommandResult.NotSticky;
        }

        public void DoWork()
        {

            //Mensagem na tela 
            Toast.MakeText(this, "O Serviço foi iniciado", ToastLength.Long).Show();

            var t = new Thread(() =>
            {
                Log.Debug("MyService", "MyService Iniciado");
                Thread.Sleep(4000);
                StartServiceInForeground();
                
                // retira a mensagem 
                //StopForeground(true);

                Thread.Sleep(10000);
                
                //Parar o Serviço. 
                Log.Debug("MyService", "finalizando serviço");
                //StopSelf();
               
            });

            t.Start();
        }

        //mensagem no topo do Celular
        public void StartServiceInForeground()
        {
            var ongoing = new Notification(Resource.Drawable.icon, "service in Foreground");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MyService)), 0);
            ongoing.SetLatestEventInfo(this, "Meu Serviço", "serviço funcionando", pendingIntent);

            // NotificationFlags.ForegroundService notificação de serviço
            StartForeground((int)(NotificationFlags.ForegroundService), ongoing);
        }
    }

    public class MyServiceBinder : Binder
    {
        private readonly MyService _service;

        public MyServiceBinder(MyService myService)
        {
            _service = myService;
        }

        public MyService GetMyService()
        {
            return _service;
        }
    }
}
