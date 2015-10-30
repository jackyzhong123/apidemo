using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiDemo.Infrastructure
{
    public static class BlobAccountUrl
    {
        public const string pyq = "http://cache.awblob.com/";
        public const string hdyimg = "http://hdyimg.awblob.com/";
        public const string ScanService = "hdyscanlogin:";
    }



    public static class BlobString
    {
        //public const string SquarePortrait = "sportrait";//切成正方形之后
        //public const string PortraitThumb = "portrait-t";   //包括个人（朋友号和娱乐号），小广场，//唯一的Guid构成
        //public const string Portrait = "portrait"; //切成正方形之前
        //public const string News = "news";
        //public const string XgcBackground = "xgcbackground";
        //public const string AppIcon = "appicon";
        //public const string NewsThumb = "newsthumb";
        //public const string LinkIcon = "linkicon";
        //public const string Upload = "upload";

        public const string Icons = "icons";//存储web app的icon
        public const string News = "news"; //存储新闻 
        public const string Portrait = "portrait"; //存储各种头像 
        public const string Update = "update"; //存储update用
        public const string Upload = "upload";//存储用户上传图片
        public const string Website = "website"; //存储网站用图片
        public const string General = "general";

        public const string PasswordString = "jzkjdfklsjafkjasdfjsadkjf$%$%^";




    }

    public static class SlicePicType
    {
        public static readonly string pyh = "pyh";
        public static readonly string ylh = "ylh";
        public static readonly string xgc = "xgc";
        public static readonly string pyq_ylh = "pyq_ylh";
        public static readonly string UserInWall = "UserInWall";
        public static readonly string SquareHead = "SquareHead";
    }

    public static class UtilityConfiguration
    {
        public const int QuanQuanCount = 15;
    }
}