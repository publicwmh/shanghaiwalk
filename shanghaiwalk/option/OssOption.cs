using System;
namespace shanghaiwalk.option
{
    public class OssOption
    {
        public OssOption()
        {
        }

        public string Endpoint { get; set; }
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string BucketName { get; set; }
        public string ViewPoint { get; set; }
    }
}
