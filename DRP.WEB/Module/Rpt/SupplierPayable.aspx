<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierPayable.aspx.cs" Inherits="DRP.WEB.Module.Rpt.SupplierPayable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商付款汇总表</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="Script/SupplierPayable.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="应付款汇总统计" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    供应商类型：<asp:DropDownList ID="ddlType" Width="120" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                        <asp:ListItem Text="供应商" Value="1"></asp:ListItem>
                        <asp:ListItem Text="景点门票" Value="2"></asp:ListItem>
                        <asp:ListItem Text="导游" Value="3"></asp:ListItem>
                        <asp:ListItem Text="酒店" Value="4"></asp:ListItem>
                        <asp:ListItem Text="车队" Value="5"></asp:ListItem>
                        <asp:ListItem Text="签证机构" Value="6"></asp:ListItem>
                        <asp:ListItem Text="保险公司" Value="7"></asp:ListItem>
                        <asp:ListItem Text="票务机构" Value="9"></asp:ListItem>
                        <asp:ListItem Text="其他供应商" Value="10"></asp:ListItem>
                        <asp:ListItem Text="购物店" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                    <span style="padding-left: 1em;">供应商名称：</span>
                    <asp:TextBox ID="txtName" ClientIDMode="Static" Width="120" runat="server"></asp:TextBox>
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
