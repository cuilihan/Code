<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeSign.aspx.cs" Inherits="DRP.WEB.Module.CheckIn.IncomeSign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>非订单收入登记</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="Script/IncomeSign.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="非订单收入" runat="server" id="orderTitle" iconcls="icon-reload">
                <div id="toolbar">
                    收入类型：<asp:DropDownList ID="ddlType" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <span style="padding-left: 1em;">收入部门：</span>
                    <asp:DropDownList ID="ddlDeptID" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <span style="padding-left: 1em;">经办人：</span>
                    <asp:TextBox ID="txtOperator" Width="90" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">收入日期：</span>
                    <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    ~
                    <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <a href="javascript:;" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">收入登记</a>
                    <a href="javascript:;" id="btnDelete" class="easyui-linkbutton" iconcls="icon-remove">删除</a>
                </div>
                <table id="tblData">
                </table>
            </div>
        </div>
    </form>
</body>
</html>
