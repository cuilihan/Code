<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizOrderList.aspx.cs" Inherits="DRP.WEB.Module.Fin.BizOrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>单项业务订单</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="../Order/Script/OrderUtility.js" type="text/javascript"></script>
    <script src="Script/BizOrderList.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="单项业务订单" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    <div style="margin-bottom: 5px;">
                        订单名称：<asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">编号：</span>
                        <asp:TextBox ID="txtOrderNo" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">客户名称：</span><asp:TextBox ID="txtCustomer" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">客户公司名称：</span><asp:TextBox ID="txtCompany" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">供应商名称：</span>
                        <asp:TextBox ID="txtSupplier" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <span style="padding-left: 1em;">日期：</span>
                        <asp:DropDownList ID="ddlDateType" ClientIDMode="Static" Width="90" runat="server">
                            <asp:ListItem Text="录入日期" Value="1"></asp:ListItem>
                            <asp:ListItem Text="出发日期" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                <span style="padding-left: 1em;">未收款：</span>
                        <asp:TextBox ID="sUnCollectedAmt" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eUnCollectedAmt" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        订单状态：
                        <asp:DropDownList ID="ddlOrderStatus" Width="80" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                            <asp:ListItem Text="待确认" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已确认" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已完成" Value="3"></asp:ListItem>
                            <asp:ListItem Text="已取消" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-left: 1em;">下单部门：</span>
                        <asp:DropDownList ID="ddlDept" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <span style="padding-left: 1em;">下单人：</span>
                        <asp:DropDownList ID="ddlCreator" ClientIDMode="Static" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <span id="sPart" style="display:none;" runat="server"><asp:CheckBox runat="server" ID="Part" ClientIDMode="Static" Text="查询参与人员" /></span>
                        <span style="padding-left: 1em;">订单类型：</span>
                        <asp:DropDownList ID="ddlOrderSource" ClientIDMode="Static"
                            AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList>
                   <span style="padding-left: 1em;">最后操作人：</span>
                        <asp:TextBox ID="txtUpdateUserName" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        <a class="easyui-linkbutton" id="btnOk" iconcls="icon-ok">收款确认</a> 
                        <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" iconcls="icon-export" runat="server">导出</asp:LinkButton> 
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
