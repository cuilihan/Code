<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceMrg.aspx.cs" Inherits="DRP.WEB.Module.Fin.InvoiceMrg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>发票管理</title>
   <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="Script/InvoiceMrg.js" type="text/javascript"></script>
</head> 
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="发票查询" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    发票状态：<asp:DropDownList ID="ddlInvoiceStatus" Width="80" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                        <asp:ListItem Text="申请中" Value="1"></asp:ListItem>
                        <asp:ListItem Text="已开票" Value="2"></asp:ListItem>
                        <asp:ListItem Text="已退回" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已作废" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                    <span style="padding-left: 1em;">发票抬头：</span>
                    <asp:TextBox ID="txtInvoiceName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">发票编号：</span>
                    <asp:TextBox ID="txtInvoiceNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">开票日期：</span>
                    <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    ~
                    <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a> 
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
