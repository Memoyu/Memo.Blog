namespace Memo.Blog.Domain.Constants.Security.Permissions;

public static partial class Permissions
{
    [Description("提交记录")]
    public static class SubmissionRecord
    {
        [Description("获取提交记录列表")]
        public const string List = "list:submission_record";
    }
}
