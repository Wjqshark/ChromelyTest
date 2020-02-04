using System;
using System.Diagnostics;
using Chromely;
using Chromely.CefGlue.Browser.EventParams;
using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Helpers;
using Chromely.Core.Host;
using Chromely.Core.Network;

namespace ChromelyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var startUrl = $"https://threejs.org/examples/#webgl_animation_cloth";


            var config = DefaultConfiguration.CreateForRuntimePlatform();
            config.CefDownloadOptions = new CefDownloadOptions(true, false);
            config.WindowOptions.Position = new WindowPosition(0, 0);
            config.WindowOptions.WindowState = WindowState.Fullscreen;
            config.WindowOptions.Size = new WindowSize(800, 800);
            config.StartUrl = startUrl;


            try
            {
                var builder = AppBuilder.Create();
                builder = builder.UseApp<DemoChromelyApp>();
                builder = builder.UseConfiguration<DefaultConfiguration>(config);
                builder = builder.Build();
                builder.Run(args);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }
        }

        internal static void OnBeforeClose(object sender, BeforeCloseEventArgs e)
        {

        }

        internal static void OnFrameLoaded(object sender, FrameLoadEndEventArgs e)
        {
            var hWnd = Process.GetCurrentProcess().MainWindowHandle;
        }

    }

    public class DemoChromelyApp : BasicChromelyApp
    {
        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);

        }

        public override void RegisterEvents(IChromelyContainer container)
        {
            EnsureContainerValid(container);

            RegisterEventHandler(container, CefEventKey.FrameLoadEnd, new ChromelyEventHandler<FrameLoadEndEventArgs>(CefEventKey.FrameLoadEnd, Program.OnFrameLoaded));
            RegisterEventHandler(container, CefEventKey.BeforeClose, new ChromelyEventHandler<BeforeCloseEventArgs>(CefEventKey.BeforeClose, Program.OnBeforeClose));
        }


        private void RegisterEventHandler<T>(IChromelyContainer container, CefEventKey key, ChromelyEventHandler<T> handler)
        {
            var service = CefEventHandlerTypes.GetHandlerType(key);
            container.RegisterInstance(service, handler.Key, handler);
        }

        public override IChromelyWindow CreateWindow()
        {
            return base.CreateWindow();
        }
    }
}
