<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnLineRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.OnLineRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线用户统计</title>
    <script src="../Rpt/Script/RptUtility.js"></script>
    <script src="Script/OnLineRpt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="rptBatch" class="easyui-panel" style="padding: 5px;" iconcls="icon-reload" title="用户登录次数统计">
            <div>
                <asp:RadioButtonList ID="rdType" name="rdType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                    <asp:ListItem Text="按月统计" Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Text="按天统计" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div id="rptChart" style="height: 400px;"></div>
            <div id="rptFooter" style="padding-left: 70px; margin: 10px 0px; font-weight: bold; font-size: 13px;">合计：</div>

        </div>
    </form>
</body>
</html>
