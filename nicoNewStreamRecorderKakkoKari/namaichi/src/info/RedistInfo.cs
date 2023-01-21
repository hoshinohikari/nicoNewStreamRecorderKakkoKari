/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2018/09/10
 * Time: 20:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace namaichi.info;

/// <summary>
///     Description of RedistInfo.
/// </summary>
public class RedistInfo
{
    public int afterFFmpegMode;
    public string[] qualityRank;
    public TimeShiftConfig tsConfig;

    public RedistInfo(string[] args)
    {
        /*
        tsConfig = new TimeShiftConfig(int.Parse(args[2]),
                int.Parse(args[3]), int.Parse(args[4]),
                int.Parse(args[5]), int.Parse(args[6]), 
                int.Parse(args[7]),	int.Parse(args[8]),
                bool.Parse(args[9]), bool.Parse(args[10]), 
                args[11], bool.Parse(args[12]),
                double.Parse(args[13]), bool.Parse(args[14]));
        qualityRank = args[15].Split(',');
        */
        afterFFmpegMode = int.Parse(args[2]);
    }
}