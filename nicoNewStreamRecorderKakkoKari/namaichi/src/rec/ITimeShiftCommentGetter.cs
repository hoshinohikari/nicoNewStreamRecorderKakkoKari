/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2022/05/13
 * Time: 13:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using namaichi.info;

namespace namaichi.rec;

/// <summary>
///     Description of ITimeShiftCommentGetter.
/// </summary>
public abstract class ITimeShiftCommentGetter
{
    internal int _gotMinTime;
    internal string[] _gotMinXml = new string[2];
    public List<GotCommentInfo> gotCommentList = new();

    public List<GotCommentInfo> gotCommentListBuf = new();

    //internal string programType;
    //internal long openTime = 0;
    //internal long _openTime = 0;
    internal int gotCount = 0;
    internal int gotMinTime;
    internal string[] gotMinXml = new string[2];
    public bool isEnd = false;
    internal bool isLog = true;
    internal bool isReachStartTimeSave = false;
    internal bool isRetry = true;
    internal bool isStore;
    internal bool isVposStartTime;

    internal string lastRealTimeComment = null;

    //internal string fileName;
    internal string recFolderFile;
    internal RecordInfo ri = null;
    public IRecorderProcess rp;
    internal TimeShiftConfig tsConfig = null;

    public abstract void save();

    public void setIsRetry(bool b)
    {
        isRetry = b;
    }

    internal int commentListCompare(GotCommentInfo x, GotCommentInfo y)
    {
        if (x.no >= 0 && y.no >= 0 && x.no != y.no) return x.no - y.no;
        if (x.date != y.date)
            return x.date - y.date;

        if (x.vpos != y.vpos)
            return x.vpos.CompareTo(y.vpos);
        return x.comment.CompareTo(y.comment);
    }

    internal void writeXmlStreamInfo(StreamWriter w)
    {
        var startTime = ri.si.openTime;
        var vposStartTime = isVposStartTime ? (long)rp.firstSegmentSecond : 0;
        if (ri.si.type == "official")
            startTime = ri.si._openTime + vposStartTime;
        else
            startTime = ri.si.openTime + vposStartTime;
        w.WriteLine("<packet xmlns=\"http://posite-c.jp/niconamacommentviewer/commentlog/\">");
        w.WriteLine("<RoomLabel>room</RoomLabel>");
        w.WriteLine("<StartTime>" + startTime + "</StartTime>");
    }
}