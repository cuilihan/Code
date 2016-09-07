<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProNav.aspx.cs" Inherits="DRP.WEB.Module.Pro.ProNav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品预订导航</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/ProNav.js" type="text/javascript"></script>
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
        <div id="wraper">
            <div class="tabMenu">
                <ul id="tabMenu">
                    <asp:Literal ID="lblRouteType" runat="server"></asp:Literal>
                </ul>
                <span style="display: inline-block; padding: 3px; position: absolute; right: 5px;"><a class="icon_list" href='ProList.aspx'>切换至列表视图</a></span>
            </div>
            <div style="height: 30px; padding:5px; margin: 5px 0px; background-color: #F1F1FF; border: 1px solid #9CB8E7">
                出团日期：<asp:TextBox ID="sDate" ClientIDMode="Static" Width="100" onclick="WdatePicker()" runat="server"></asp:TextBox>
                至<asp:TextBox ID="eDate" ClientIDMode="Static" Width="100" onclick="WdatePicker()" runat="server"></asp:TextBox>
                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">查询</a>
            </div>
            <table class="tblInfo" id="tblData">
            </table>
            <div style="height: 2px; margin-bottom: 5px;">
            </div>
        </div>
    </form>
</body>
</html>
