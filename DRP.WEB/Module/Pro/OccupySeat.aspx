<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OccupySeat.aspx.cs" Inherits="DRP.WEB.Module.Pro.OccupySeat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Css/ProBook.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function fnDrawSeat() {
            var tourID = request("id");
            var url = "Service/TourInfo.ashx?tourID=" + tourID + "&action=7&r=" + getRand();
            dataService.ajaxGet(url, function (data) {
                $("#seatNum").html(data);
                $("#seatNum").find("td").click(function () {
                    var clsName = $(this).attr("class");
                    var seatNo = $.trim($(this).text());
                    switch (clsName) {
                        case "seat":
                            $(this).removeClass("seat").addClass("lock");
                            $(this).html("");
                            $(this).attr("tag", seatNo);
                            $(this).attr("title", "已选择座位" + seatNo);
                            break;
                        case "lock":
                            $(this).removeClass("lock").addClass("seat");
                            var tag = $(this).attr("tag");
                            $(this).text(tag);
                            $(this).attr("title", "");
                            break;
                    }
                });
            });
        }
        $(function () {
            fnDrawSeat();
            $("#btnSave").click(function () {
                var arr = [];
                $("#seatNum").find(".lock").each(function () {
                    var t = $(this).attr("tag");
                    arr.push(t);
                });
                var tourID = request("id");
                var strSeat = arr.join(",");
                var url = "Service/TourInfo.ashx?action=8&r=" + getRand(); 
                dataService.ajaxPost(url, { "TourID": tourID, "SeatNo": strSeat }, function (data) {
                    Alert(data == "1" ? "保存成功" : "保存失败");
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px;">
            <div>
                <a class="easyui-linkbutton" id="btnSave" iconcls="icon-save">保存设置</a>
                提示：点击号码选择座位
            </div>
            <div id="seatData" style="border: 1px solid #ccc; margin-top: 10px; padding: 10px 10px 10px 30px;">
                <table class="seatchart">
                    <tr>
                        <td class="seatchart_title" style="height: 22px;" colspan="5">
                            <span>预留座设置</span></td>
                    </tr>
                    <tbody id="seatNum">
                        <!--座位表-->
                    </tbody>
                    <tr>
                        <td colspan="5" class="seatchart_footer">
                            <span class="seatchart_footer_used">已占座</span>
                            <span class="seatchart_footer_lock">预留座</span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
