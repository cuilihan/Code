<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="DRP.WEB.Module.T.Setting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报价单模板设置</title>
    <script src="../../Scripts/Plugin/kindeditor/kindeditor-min.js"></script>
    <script type="text/ecmascript">
        var toolbarItems = ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                 'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                 'insertunorderedlist', '|', 'emoticons', 'link','|', 'plainpaste','source'];

        $(function () {
            KindEditor.ready(function (K) {
                K.create('#txtTemplate', {
                    resizeType: 1,
                    height: 300,
                    allowPreviewEmoticons: false,
                    allowImageUpload: false,
                    afterBlur: function () { this.sync(); },
                    items: toolbarItems
                });
            });
        });
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div title="报价单模板设置" iconcls="icon-edit" data-options="region:'center',border:true" style="padding: 10px;">

            <table class="tblInfo">
                <tr>
                    <td>
                        <asp:TextBox ID="txtTemplate" ClientIDMode="Static" EnableTheming="false" runat="server" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>
                    </td>
                    <td style="width: 200px; vertical-align: top;">
                        <div class="line" style="padding-bottom: 5px;">设置变量</div>
                        <div style="padding-left:10px;">
                            @Dept:当前登录用户所在部门名称  
                            <br />
                            @UserName:当前登录用户姓名 
                            <br /> 
                            @UserMobile:当前登录用户联系手机<br />
                            @UserEmail:当前登录用户电子邮件 
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;padding:5px;">
                        <asp:LinkButton ID="btnSave" CssClass="easyui-linkbutton" iconcls="icon-save" OnClick="btnSave_Click" runat="server">保存</asp:LinkButton>
                    </td>
                </tr>
            </table> 
        </div>
    </form>
</body>
</html>
