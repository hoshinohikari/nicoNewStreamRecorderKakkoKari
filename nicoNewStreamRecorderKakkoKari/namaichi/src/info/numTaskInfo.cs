/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2018/10/12
 * Time: 0:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace namaichi.info;

/// <summary>
///     Description of numTaskInfo.
/// </summary>
public class numTaskInfo
{
    public DateTime dt;
    public string fileName;
    public int no = -1;
    public int originNo = -1;
    public byte[] res = null;
    public double second;
    public double startSecond = -1;
    public string url;

    public numTaskInfo(int no, string url, double second, string fileName, double startSecond, int originNo = -1)
    {
        this.no = no;
        this.url = url;
        this.second = second;
        this.fileName = fileName;
        dt = DateTime.Now;
        this.originNo = originNo == -1 ? no : originNo;
        this.startSecond = startSecond;
    }
}