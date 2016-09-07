<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.UserRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构用户数统计</title>
    <script type="text/javascript">
        $(function () {
            var u = "Service/UserRpt.ashx?r=" + getRand(); 
            dataService.ajaxGet(u, function (data) {
                $("#tblData").html(data);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" style="padding: 10px;" title="机构用户统计" iconcls="icon-reload">
            <table class="tblEdit">
                <tr>
                    <th style="width:30px;">序</th>
                    <th>地区
                    </th>
                    <th style="width: 150px;">机构数</th>
                    <th style="width: 150px;">用户数</th> 
                </tr>
                <tbody id="tblData"></tbody> 
            </table>
        </div>
    </form>
</body>
</html>
