<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketOrder.aspx.cs" Inherits="DRP.WEB.Module.Fin.TicketOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机票订单管理</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/TicketOrderList.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="机票订单管理" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div style="margin-bottom: 5px;">
                        订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">PNR编号：</span>
                        <asp:TextBox ID="txtPNR" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                          <span style="padding-left: 1em;">航班日期：</span>
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">联系人：</span><asp:TextBox ID="txtContact" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">客户公司名称：</span><asp:TextBox ID="txtCompany" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">供应商名称：</span>
                        <asp:TextBox ID="txtSupplier" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                      <span style="padding-left: 1em;">未收款：</span>
                        <asp:TextBox ID="sUnCollectedAmt" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eUnCollectedAmt" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        机票行程：<asp:TextBox ID="txtFlightInfo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">订单状态：</span>
                        <asp:DropDownList ID="ddlOrderStatus" Width="100" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                            <asp:ListItem Text="正常订单" Value="3"></asp:ListItem>
                            <asp:ListItem Text="取消订单" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                         <span style="padding-left: 1em;">下单日期：</span>
                          <asp:TextBox ID="cSData" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="cEDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">最后操作人：</span>
                        <asp:TextBox ID="txtUpdateUserName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">
                            <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                         <a href="javascript:;" id="btnConfirmed" class="easyui-linkbutton" iconcls="icon-ok">收款确认</a>
                            <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" iconcls="icon-export" runat="server">导出</asp:LinkButton> 
                        </span>
                        <asp:HiddenField ID="hidePart" ClientIDMode="Static" runat="server" />
                    </div>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
