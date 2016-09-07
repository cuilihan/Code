<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPasswrod.aspx.cs" Inherits="DRP.WEB.Module.My.MyPasswrod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改密码</title>
    <script type="text/ecmascript">
        function fnValid() {
            var p = $("#txtOriPwd").val();
            var p1 = $("#txtNewPwd").val();
            var p2 = $("#txtRePwd").val();
            if (p == "") {
                Alert("请输入原始密码");
                return false;
            }
            if (p1 == "") {
                Alert("请输入新密码");
                return false;
            }
            if (p2 == "") {
                Alert("请输入校验密码");
                return false;
            }
            if (p1 != p2) {
                Alert("两次输入的密码不一至");
                return false;
            }
            return true;
        }
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div title="修改密码" data-options="region:'center',border:true" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">登录用户：
                    </td>
                    <td>
                        <asp:Literal ID="lblUserName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>原始密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOriPwd" TextMode="Password" data-options="required:true" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>新密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>确认密码：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRePwd" runat="server" TextMode="Password" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="padding-left: 160px; margin-top: 20px;">
                <asp:LinkButton ID="btnSave" OnClientClick="return fnValid();" OnLoad="btnSave_Click" CssClass="easyui-linkbutton" iconcls="icon-save" runat="server">修改</asp:LinkButton>
            </div>
            <div style="margin: 20px 0px 10px 20px;">
                <div class="line">
                    安全提示：
                </div>
                <ul class="list">
                    <li>1、密码是登录系统的有效凭证，请安全保存，不保存在电脑或电子邮箱中；
                    </li>
                    <li>2、 为密码设置复杂性，不使用自己及亲友的生日、电话号码、身份证号码中的数字作密码；
                    </li>
                    <li>3、 不和其他网站使用的密码相同；
                    </li>
                    <li>4、 结束系统使用时，请安全退出系统。
                    </li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
