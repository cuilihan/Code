<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="SendSms.aspx.cs" Inherits="DRP.WEB.Module.My.SendSms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发消息</title>
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var m = $("#Mobile").val();
                var s = $("#MsgContent").val();
                if (m == "") {
                    Alert("手机号不能为空");
                    return false;
                }
                if (s == "") {
                    Alert("短信内容不能为空");
                    return false;
                }
                $.ajax({
                    type: "post",
                    url: "Service/Message.ashx?action=4&r=" + getRand(),
                    data: { "Mobile": m, "Content": s },
                    beforeSend: function (XMLHttpRequest) {
                        $("#btnSave").addClass("hide");
                    },
                    success: function (data) {
                        if (data == "1") {
                            closeWindow("发送成功");
                        }
                        else {
                            Alert(data);
                        }
                    },
                    error: function () {
                        $("#btnSave").removeClass("hide");
                        Alert("发送消息时出现异常");
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <table class="tblEdit">
            <tr>
                <td class="rowlabel_90">
                    <span class="red">*</span>手机号：
                </td>
                <td>
                    <asp:TextBox ID="Mobile" ClientIDMode="Static" Width="90%" runat="server"></asp:TextBox>
                    <div>
                        注：多个手机号请用逗号（“，”）分隔。
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rowlabel_90">
                    <span class="red">*</span>短信内容：
                </td>
                <td>
                    <asp:TextBox ID="MsgContent" TextMode="MultiLine" ClientIDMode="Static" EnableTheming="false" Height="120" Width="90%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 5px 10px; text-align: center;">
                    <a id="btnSave" class="easyui-linkbutton" iconcls="icon-save">发送</a>
                    <div>
                        注：短信接收时间大概延迟15分钟左右，剩余可发【<span style="color: red; font-weight: bold;"><asp:Literal runat="server" ID="lblSmsCount"></asp:Literal></span>】
                        条短信！
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
