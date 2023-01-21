/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/06
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;

namespace namaichi;

/// <summary>
///     Class with program entry point.
/// </summary>
internal sealed class Program
{
    public static string arg = "";

    /// <summary>
    ///     Program entry point.
    /// </summary>
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
        TaskScheduler.UnobservedTaskException += taskSchedulerUnobservedTaskException!;
        AppDomain.CurrentDomain.FirstChanceException += firstChanceException!;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 |
            SecurityProtocolType.Tls13;

//			args = new string[]{"-nowindo", "lv316266831", "-stdIO"};
//			args = new String[]{"lv316036760", "-ts-start=5m0s", "-ts-end=5m10s", "-afterConvertMode=4"};
        if (Array.IndexOf(args, "-nowindow") == -1)
        {
            Application.Run(new MainForm(args));
        }
        else
        {
            util.isShowWindow = false;
            var a = new MainForm(args);
            while (a.rec.isRecording) Thread.Sleep(1000);
        }
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
        var frameCount = 0;
        try
        {
            frameCount = new StackTrace().FrameCount;
        }
        catch (StackOverflowException)
        {
            //エラーのログを書き込むときにエラーが出た場合はそれ以上ログを書き込まないように
            return;
        }
#if DEBUG
        if (util.isLogFile)
            if (frameCount > 150)
            {
                //						util.debugWriteLine("exception stacktrace framecount " + frameCount);
                MessageBox.Show("first chance framecount stack " + e.Exception.Message + e.Exception.StackTrace,
                    frameCount + " " + DateTime.Now + " " + arg);
                //						if (e.Exception.GetType() == System.IO.IOException
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