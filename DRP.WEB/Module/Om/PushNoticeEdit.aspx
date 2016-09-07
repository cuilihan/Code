<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PushNoticeEdit.aspx.cs" Inherits="DRP.WEB.Module.Om.PushNoticeEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
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
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                
                <tr>
                    <td class="rowlabel"><span class="red">*</span>有效日期：
                    </td>
                    <td>
                        <asp:TextBox ID="sDate" onclick="WdatePicker()" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" onclick="WdatePicker()"  ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                     
                </tr>
                 <tr>
                    <td class="rowlabel">链接地址：</td>
                    <td>
                        <asp:TextBox runat="server" Width="300" ID="LinkUrl" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                 
                <tr>
                    <td class="rowlabel">内容：
                    </td>
                    <td>
                        <asp:TextBox ID="nContent" TextMode="MultiLine"   Height="200" Width="90%" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2" style="text-align: center; margin: 20px;">
                        <asp:LinkButton runat="server" OnClick="btnSave_Click" Text="保存" ID="btnSave" CssClass="easyui-linkbutton" iconcls="icon-save"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
