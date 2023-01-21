/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/07/26
 * Time: 17:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace rokugaTouroku.info;

/// <summary>
///     Description of TimeShiftConfig.
/// </summary>
public class TimeShiftConfig
{
    public int endH;
    public int endM;
    public int endS;
    public int endTimeMode;

    public int endTimeSeconds;
    public int h;
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
    public int m;
    public double m3u8UpdateSeconds;
    public string openListCommand;
    public int s;
    public int startTimeMode;

    public string startTimeStr;

    //0-start time 1-continue
    private int startType;
    public int timeSeconds;
    public int timeType; //0-record from 1-recorded until

    public TimeShiftConfig(int startType,
        int h, int m, int s, bool isContinueConcat,
        bool isVposStartTime, int startTimeMode, int endTimeMode,
        bool isAfterStartTimeComment, bool isOpenTimeBaseStart,
        bool isOpenTimeBaseEnd, bool isBeforeEndTimeComment,
        bool isDeletePosTime)
    {
        this.startType = startType;
        this.h = h;
        this.m = m;
        this.s = s;
        this.isContinueConcat = isContinueConcat;
        this.isVposStartTime = isVposStartTime;
        this.startTimeMode = startTimeMode;
        this.endTimeMode = endTimeMode;

        timeSeconds = h * 3600 + m * 60 + s;
        timeType = startType == 0 ? 0 : 1;
        startTimeStr = startType == 0 ? timeSeconds + "s" :
            isContinueConcat ? "continue-concat" : "continue";
        this.isAfterStartTimeComment = isAfterStartTimeComment;
        this.isBeforeEndTimeComment = isBeforeEndTimeComment;
        isOpenTimeBaseStartArg = isOpenTimeBaseStart;
        isOpenTimeBaseEndArg = isOpenTimeBaseEnd;
        this.isDeletePosTime = isDeletePosTime;
    }

    public TimeShiftConfig(int startType,
        int h, int m, int s, int endH, int endM, int endS,
        bool isContinueConcat, bool isOutputUrlList,
        string openListCommand, bool isM3u8List,
        double m3u8UpdateSeconds, bool isOpenUrlList,
        bool isVposStartTime, int startTimeMode, int endTimeMode,
        bool isAfterStartTimeComment, bool isOpenTimeBaseStart,
        bool isOpenTimeBaseEnd, bool isBeforeEndTimeComment,
        bool isDeletePosTime)
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

        timeSeconds = startTimeMode == 0 ? 0 : h * 3600 + m * 60 + s;
        timeType = startType == 0 ? 0 : 1;
        endTimeSeconds = endTimeMode == 0 ? 0 : endH * 3600 + endM * 60 + endS;

        startTimeStr = startType == 0 ? timeSeconds + "s" :
            isContinueConcat ? "continue-concat" : "continue";
        this.isAfterStartTimeComment = isAfterStartTimeComment;
        isOpenTimeBaseStartArg = isOpenTimeBaseStart;
        isOpenTimeBaseEndArg = isOpenTimeBaseEnd;
        this.isBeforeEndTimeComment = isBeforeEndTimeComment;
        this.isDeletePosTime = isDeletePosTime;
    }

    public TimeShiftConfig() : this(0, 0, 0, 0, 0, 0, 0,
        false, false, "notepad {i}", false, 5, false, false, 0, 0, false, false, false, false, true)
    {
    }
}