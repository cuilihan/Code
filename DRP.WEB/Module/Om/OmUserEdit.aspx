﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OmUserEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.OmUserEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>维护运维人员</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">机构名称：
                    </td>
                    <td>
                        <asp:Literal ID="OrgName" runat="server"></asp:Literal>
                        <asp:HiddenField runat="server" ID="OrgID" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>用户姓名：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="UserName" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">用户账号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="UserAcct" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">账号密码：
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="AcctPwd" ClientIDMode="Static" />
                        <asp:TextBox runat="server" ClientIDMode="Static" TextMode="Password" ID="txtAcctPwd"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">账号状态：
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="DataStatus">
                            <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <div style="text-align: center;">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
