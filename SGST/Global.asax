<%@ Application Language="C#" %>
<%@ Import Namespace="SGST" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        string strMac = nics[0].GetPhysicalAddress().ToString();
        string sLogPath = ConfigurationManager.AppSettings["LogFilePath"].ToString();
        string sPath = HttpContext.Current.Server.MapPath(sLogPath);
        string sLogFileName = ConfigurationManager.AppSettings["SMALLCOMPANYNAME"].ToString();
        System.IO.DirectoryInfo _dir = null;
        _dir = new System.IO.DirectoryInfo(sPath);
        if (_dir.Exists == false)
        {
            _dir.Create();
            System.IO.Directory.CreateDirectory(_dir.ToString());
        }
        BcilLib.BcilLogger _obj = new BcilLib.BcilLogger();
        _obj.ChangeInterval = BcilLib.BcilLogger.ChangeIntervals.ciHourly;
        _obj.EnableLogFiles = true;
        _obj.LogDays = 30;
        _obj.LogFilesExt = "Log";
        _obj.LogFilesPath = sPath;
        _obj.LogFilesPrefix = sLogFileName;
        _obj.StartLogging();
        _obj.LogMessage(BcilLib.EventNotice.EventTypes.evtInfo, "BcilAppsInitialize" + "  ::  Main", "Initializing Application.......on " + strMac);
        Common.CommonHelper.mBcilLogger = _obj;
        Common.PCommon.mBcilLogger = _obj;
        _obj.StopLogging();
        _obj = null;
        Common.CommonHelper.mBcilLogger.LogMessage(BcilLib.EventNotice.EventTypes.evtData, System.Reflection.MethodBase.GetCurrentMethod().Name + "  " + strMac, "AppInitialize");
        Common.CommonHelper.mBcilLogger.StartLogging();
        Common.PCommon.mBcilLogger.StartLogging();
        // PreSendRequestHeaders += Application_PreSendRequestHeaders;
        RegisterRoutes(RouteTable.Routes);     
    }
    void Session_Start(object sender, EventArgs e)
    {
        //HttpContext.Current.Session["beingredirected"] = "false";
    }
    static void RegisterRoutes(RouteCollection routes)
    {
        // routes.MapPageRoute("RMPrinting", "RMPrinting", "~/INE/RM/RMPrinting.aspx");
    }
    void Application_AcquireRequestState(Object sender, EventArgs e)
    {
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
    }
</script>
