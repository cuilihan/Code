<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigateEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.NavigateEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">父节点名称：
                    </td>
                    <td>
                        <asp:Literal ID="lblNavName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>导航名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="NavName" data-options="required:true,validType:'chinese'"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="rowlabel"><span class="red">*</span>页面ID：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="PageID" data-options="required:true,validType:'english'"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导航链接：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="NavUrl" CssClass="textbox" Style="width: 250px; height:26px;" EnableTheming="false" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导航样式名称：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="NavCls" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">是否显示：
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="IsVisual">
                            <asp:ListItem Text="是" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="否" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
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
