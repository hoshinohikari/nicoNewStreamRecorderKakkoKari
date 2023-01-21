/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/08/31
 * Time: 18:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Net;
using namaichi.info;
using WebSocket4Net;

namespace namaichi.rec;

/// <summary>
///     Description of IRecorderProcess1.
/// </summary>
public abstract class IRecorderProcess
{
    public List<string> chaseCommentBuf = new();
    public string commentCount = "0";
    public List<string[]> commentReplaceList = null;
    public CookieContainer container = null;
    public DateTime endTime = DateTime.MinValue;
    public double firstSegmentSecond = -1;
    public string[] gotTsCommentList;

    public bool isHokan = false;

    //public long openTime;
    public bool isJikken;
    public bool IsRetry = true;
    public bool isSaveComment = false;
    public TimeSpan jisa;
    public string[] msReq;
    public string[] msStoreReq;

    public string msStoreUri;

    //public bool isTimeShift = false;
    public string msUri;
    internal RecordFromUrl rfu;
    public RecordInfo ri;
    internal RecordingManager rm;

    public long serverTime;
    public long sync = 0;
    public ITimeShiftCommentGetter tscg = null;
    public DateTime tsHlsRequestTime = DateTime.MinValue;
    public TimeSpan tsStartTime;

    public string visitCount = "0";

    public abstract void reConnect();

    public abstract void reConnect(WebSocket ws);

//		abstract public string[] getRecFilePath(long _openTime);
    public abstract string[] getRecFilePath();
    public abstract void sendComment(string s, bool is184);
    public abstract void resetCommentFile();

    public abstract void setSync(int no, double second, string m3u8Url);

    //abstract public void setRealTimeStatistics();
    public void setRealTimeStatistics()
    {
        try
        {
            if (!visitCount.StartsWith("-"))
            {
                Thread.Sleep(10000);
                string visit, comment;
                var ret = getStatistics(rfu.lvid, container, out visit, out comment);
                if (ret)
                    if (!visitCount.StartsWith("-"))
                    {
                        visitCount = "-" + visit;
                        commentCount = "-" + comment;
                    }
            }
        }
        catch (Exception e)
        {
            util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
        }
    }

    internal abstract bool getStatistics(string lvid, CookieContainer cc, out string visit, out string comment);
    public abstract void stopRecording();
    public abstract void chaseCommentSum();
}