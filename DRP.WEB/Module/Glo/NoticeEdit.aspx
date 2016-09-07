<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeEdit.aspx.cs" Inherits="DRP.WEB.Module.Glo.NoticeEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通知公告维护</title>
    <script src="../../Scripts/Plugin/kindeditor/kindeditor-min.js"></script>
    <script type="text/ecmascript">
        var toolbarItems = ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                   'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                   'insertunorderedlist', '|', 'emoticons', 'link', 'image', '|', 'plainpaste', 'source'];
       
         $(function () {
             KindEditor.ready(function (K) {
                 K.create('#nContent', { 
                     height: 400,
                     uploadJson: K.basePath + '/asp.net/upload_json.ashx',
                     fileManagerJson: K.basePath + '/asp.net/file_manager_json.ashx',
                     afterBlur: function () { this.sync(); },
                     items: toolbarItems
                 });
             });
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px 5px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>标题：</td>
                    <td>
                        <asp:TextBox ID="Subject" runat="server" Width="90%" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel" style="vertical-align: middle;">内容：</td>
                    <td>
                        <asp:TextBox ID="nContent" TextMode="MultiLine" runat="server" Width="90%" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:LinkButton ID="btnSave" OnClientClick="return $('#form1').form('validate');" CssClass="easyui-linkbutton" iconcls="icon-save" OnClick="btnSave_Click" runat="server">保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
