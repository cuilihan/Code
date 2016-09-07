<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="DRP.WEB.Module.Sys.Log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日志查询</title>
    <link href="../../Scripts/Plugin/date_time/skin/WdatePicker.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/Log.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            日志查询区间：<input type="text" id="sDate" class="textbox Wdate" style="width: 100px" onclick="WdatePicker()" />
            ~
              <input type="text" id="eDate" class="textbox Wdate" style="width: 100px" onclick="WdatePicker()" />
            <select name="logType" id="logType" style="width: 120px; height:26px;">
                <option value="">日志类型</option>
                <option value="INFO">记录</option>
                <option value="ERROR">错误</option>
                <option value="FATAL">崩溃</option>
                <option value="DEBUG">调试</option>
                <option value="WARN">警告</option> 
            </select>
            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
        </div>
        <table id="tblData"></table>
    </form>
</body>
</html>
