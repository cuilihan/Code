<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchAddCustomer.aspx.cs" Inherits="DRP.WEB.Module.Order.BatchAddCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批量增加客户</title>
    <script type="text/javascript">
        $(function () {
            t.init();
        });

        var t = {
            init: function () {
                $("#btnOk").click(function () {
                    var arr = $("#txtCustomerInfo").val().split(/[\r\n]/g);
                    var a = [];
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i] == "" || typeof (arr[i]) == undefined)
                            continue;
                        var arrCustomer = arr[i].split(" ");
                        var json = { "Name": arrCustomer[0], "Mobile": arrCustomer[2], "Sex": arrCustomer[1], "Company": arrCustomer[4], "IDNum": arrCustomer[3] };
                        a.push(json);
                    }

                    var api = frameElement.api;
                    var W = api.opener;
                    var xType = 1;//1:企业团
                    W.fnReceiveCustomerData(a, xType);
                    api.close();
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 10px;">
            <p>输入格式：客户名称 客户性别 联系电话 证件号码 公司名称</p>
            <asp:TextBox ID="txtCustomerInfo" TextMode="MultiLine" EnableTheming="false" CssClass="textbox" Height="180" Width="90%" runat="server" ClientIDMode="Static"></asp:TextBox>
            <div style="margin-top: 10px;">
                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnOk">确定</a>
            </div>
        </div>
    </form>
</body>
</html>
