/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/10
 * Time: 16:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;

namespace rokugaTouroku;

/// <summary>
///     Class with program entry point.
/// </summary>
internal sealed class Program
{
    /// <summary>
    ///     Program entry point.
    /// </summary>
    public static string arg = "";

    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Length > 0) arg = util.getRegGroup(args[0], "(lv.+)");

        AppDomain.CurrentDomain.UnhandledException += UnhandleExceptionHandler;
        Thread.GetDomain().UnhandledException += UnhandleExceptionHandler;
        AppDomain.CurrentDomain.UnhandledException += UnhandleExceptionHandler;
        Thread.GetDomain().UnhandledException += UnhandleExceptionHandler;
        Application.ThreadException += threadException;
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        TaskScheduler.UnobservedTaskException += taskSchedulerUnobservedTaskException;
        AppDomain.CurrentDomain.FirstChanceException += firstChanceException;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ServicePointManager.DefaultConnectionLimit = 20;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                               SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

        Application.Run(new MainForm(args));
        //args = new string[]{"lv888"};
        //var a = new MainForm(args);
        //while(true) System.Threading.Thread.Sleep(1000);
    }

    private static void UnhandleExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        util.debugWriteLine("unhandled exception");
        var eo = (Exception)e.ExceptionObject;
        util.showException(eo);
    }

    private static void threadException(object sender, ThreadExceptionEventArgs e)
    {
        util.debugWriteLine("thread exception");
        var eo = e.Exception;
        util.showException(eo);
    }

    private static void taskSchedulerUnobservedTaskException(object sender,
        UnobservedTaskExceptionEventArgs e)
    {
        util.debugWriteLine("task_unobserved exception");
        var eo = (Exception)e.Exception;
        util.showException(eo);
        e.SetObserved();
    }

    private static void firstChanceException(object sender,
        FirstChanceExceptionEventArgs e)
    {
        var frameCount = new StackTrace().FrameCount;
#if DEBUG
        if (util.isLogFile)
            if (frameCount > 150)
            {
                MessageBox.Show("first chance framecount stack " + e.Exception.Message + e.Exception.StackTrace,
                    frameCount + " " + DateTime.Now + " " + arg);
                return;
            }
#else
#endif

        util.debugWriteLine("exception stacktrace framecount " + frameCount);

        util.debugWriteLine("firstchance exception");
        var eo = e.Exception;
        util.showException(eo, false);
    }
}