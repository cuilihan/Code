<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartureEdit.aspx.cs" Inherits="DRP.WEB.Module.Glo.DepartureEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>出发地</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>出发地名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Name" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">排序号：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="OrderIndex" CssClass="checkInt textbox" EnableTheming="false" Style="width: 40px; height:26px;" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="statusbar">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
