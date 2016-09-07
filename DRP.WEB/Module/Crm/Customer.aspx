<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="DRP.WEB.Module.Crm.Customer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户列表</title>
    <script src="Script/Customer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="客户管理" iconcls="icon-search">
                <div id="toolbar">
                    客户名称：
                    <asp:TextBox ID="txtName" Width="90" runat="server"></asp:TextBox>
                    手机号：
                    <asp:TextBox ID="txtMobile" Width="90" runat="server"></asp:TextBox>
                    客户类型：
                    <asp:DropDownList ID="ddlCustomerType" runat="server" ClientIDMode="Static" AppendDataBoundItems="true">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    创建部门：
                    <asp:DropDownList ID="ddlDeptID" runat="server" ClientIDMode="Static" AppendDataBoundItems="true">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    创建人：
                    <asp:DropDownList ID="ddlUserID" runat="server" ClientIDMode="Static" AppendDataBoundItems="true">
                        <asp:ListItem Text="所有" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    公司名称：
                    <asp:TextBox ID="txtCompany" Width="90" runat="server"></asp:TextBox>
                    <div style="margin-top: 5px;">
                        <a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">查询</a>
                        <a id="btnAdd" class="easyui-linkbutton" iconcls="icon-add">新增客户</a>
                        <asp:LinkButton ID="btnImp" ClientIDMode="Static" runat="server" CssClass="easyui-linkbutton" iconcls="icon-import">导入客户</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" iconcls="icon-export">导出客户</asp:LinkButton>
                         <a class="easyui-linkbutton" iconcls="icon-message" id="btnSMS">一键群发信息</a>
                        <asp:LinkButton ID="btnDel" ClientIDMode="Static" CssClass="easyui-linkbutton" icon="icon-remove" runat="server" Text="删除"></asp:LinkButton>
                    </div>
                </div>
                <table id="tblData"></table>
            </div>
        </div>
    </form>
</body>
</html>
