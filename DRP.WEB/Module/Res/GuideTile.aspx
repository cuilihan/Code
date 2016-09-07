<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideTile.aspx.cs" Inherits="DRP.WEB.Module.Res.GuideTile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游查询</title>
    <script src="Script/GuideTile.js" type="text/javascript"></script>
    <style type="text/css">
        #tblData a
        {
            display: inline-block;
            margin-right: 20px;
            line-height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="导游查询" iconcls="icon-search" style="padding: 5px 5px 0px 5px;">
                <div id="wraper" style="padding-top: 5px;">
                    <div style="float: left; width: 150px;">
                        共 <span id="iCount" style="font-family: Arial; font-size: 14px; font-weight: bold; color: blue;">0</span> 位导游
                    </div>
                    <div style="text-align: right; padding-right: 5px; width: 140px; float: right;">
                        <a class="icon_list" href='GuideQry.aspx'>切换至列表视图</a>
                    </div>
                    <div style="clear: both; height: 3px;"></div>
                    <table class="tblInfo" id="tblData" style="margin-bottom: 10px;"></table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
