/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/07/26
 * Time: 17:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace namaichi.info;

/// <summary>
///     Description of TimeShiftConfig.
/// </summary>
public class TimeShiftConfig
{
    private readonly int endH;
    private readonly int endM;
    private readonly int endS;
    private readonly int h;
    private readonly int m;
    private readonly int s;

    //0-start time 1-continue
    private readonly int startType;
    public int endTimeMode;

    public int endTimeSeconds;
    public bool isAfterStartTimeComment;
    public bool isBeforeEndTimeComment;

    public bool isContinueConcat;
    public bool isDeletePosTime;
    public bool isM3u8List;
    public bool isOpenTimeBaseEndArg;
    public bool isOpenTimeBaseStartArg;
    public bool isOpenUrlList;
    public bool isOutputUrlList;
    public bool isVposStartTime;

    public string lastFileName = null;

    //public int lastSegmentNo = -1;
    public string[] lastFileTime = null;
    public double m3u8UpdateSeconds;
    public string openListCommand;
    public string[] qualityRank;
    public int startTimeMode;
    public int timeSeconds;

    public int timeType; //0-record from 1-recorded until
    //public string startTimeStr;

    public TimeShiftConfig(int startType,
        int h, int m, int s, int endH, int endM, int endS,
        bool isContinueConcat, bool isOutputUrlList,
        string openListCommand, bool isM3u8List,
        double m3u8UpdateSeconds, bool isOpenUrlList,
        bool isVposStartTime, int startTimeMode, int endTimeMode,
        bool isAfterStartTimeComment, bool isBeforeEndTimeComment,
        bool isDeletePosTime, string[] qualityRank)
    {
        this.startType = startType;
        this.h = h;
        this.m = m;
        this.s = s;
        this.endH = endH;
        this.endM = endM;
        this.endS = endS;
        this.isContinueConcat = isContinueConcat;
        this.isOutputUrlList = isOutputUrlList;
        this.openListCommand = openListCommand;
        this.isM3u8List = isM3u8List;
        this.m3u8UpdateSeconds = m3u8UpdateSeconds;
        this.isOpenUrlList = isOpenUrlList;
        this.isVposStartTime = isVposStartTime;
        this.startTimeMode = startTimeMode;
        this.endTimeMode = endTimeMode;
        /*
        timeSeconds = h * 3600 + m * 60 + s;
        timeType = (startType == 0) ? 0 : 1;
        endTimeSeconds = endH * 3600 + endM * 60 + endS;
        */
        timeSeconds = startTimeMode == 0 ? 0 : h * 3600 + m * 60 + s;
        timeType = startType == 0 ? 0 : 1;
        endTimeSeconds = endTimeMode == 0 ? 0 : endH * 3600 + endM * 60 + endS;

        if (startType == 0) this.isContinueConcat = false;
        this.isAfterStartTimeComment = isAfterStartTimeComment;
        this.isBeforeEndTimeComment = isBeforeEndTimeComment;
        this.isDeletePosTime = isDeletePosTime;
        this.qualityRank = qualityRank;
    }

    public TimeShiftConfig() : this(0, 0, 0, 0, 0, 0, 0,
        false, false, "notepad {i}", false, 5, false, false, 0, 0, false, false, true, null)
    {
    }

    public TimeShiftConfig(int startType, int h, int m, int s,
        int endH, int endM, int endS, bool isContinueConcat,
        int timeSeconds, int timeType, int endTimeSeconds,
        bool isOutputUrlList, string openListCommand,
        bool isM3u8List, double m3u8UpdateSeconds,
        bool isOpenUrlList, bool isVposStartTime,
        int startTimeMode, int endTimeMode,
        bool isAfterStartTimeComment,
        bool isOpenTimeBaseStartArg, bool isOpenTimeBaseEndArg,
        bool isBeforeEndTimeComment, bool isDeletePosTime,
        string[] qualityRank)
    {
        this.startType = startType;
        this.h = h;
        this.m = m;
        this.s = s;
        this.endH = endH;
        this.endM = endM;
        this.endS = endS;
        this.isContinueConcat = isContinueConcat;
        this.timeSeconds = timeSeconds;
        this.timeType = timeType;
        this.endTimeSeconds = endTimeSeconds;
        this.isOutputUrlList = isOutputUrlList;
        this.openListCommand = openListCommand;
        this.isM3u8List = isM3u8List;
        this.m3u8UpdateSeconds = m3u8UpdateSeconds;
        this.isOutputUrlList = isOpenUrlList;
        this.isVposStartTime = isVposStartTime;
        this.startTimeMode = startTimeMode;
        this.endTimeMode = endTimeMode;
        this.isAfterStartTimeComment = isAfterStartTimeComment;
        this.isOpenTimeBaseStartArg = isOpenTimeBaseStartArg;
        this.isOpenTimeBaseEndArg = isOpenTimeBaseEndArg;
        this.isBeforeEndTimeComment = isBeforeEndTimeComment;
        this.isDeletePosTime = isDeletePosTime;
        this.qualityRank = qualityRank;
    }

    public TimeShiftConfig clone()
    {
        return new TimeShiftConfig(startType, h, m, s,
            endH, endM, endS, isContinueConcat,
            timeSeconds, timeType, endTimeSeconds,
            isOutputUrlList, openListCommand, isM3u8List,
            m3u8UpdateSeconds, isOpenUrlList, isVposStartTime,
            startTimeMode, endTimeMode, isAfterStartTimeComment,
            isOpenTimeBaseStartArg, isOpenTimeBaseEndArg,
            isBeforeEndTimeComment, isDeletePosTime, qualityRank
        );
    }
}