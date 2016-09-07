$(function () {
    p.init();
});

var p = {
    init: function () {
        $("#btnAdd").click(function () {
            p.fnAddItem();
        });
    },
    fnAddItem: function () {
        var sb = [];
        var i = $("#tblData").find("tr").size() + 1;
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + i + "</td>");
        sb.push("<td><input type='text' name='pName' style='width:240px;' /></td>");
        sb.push("<td><input type='text' name='pUser' style='width:80px;text-align:center;' /></td>");
        sb.push("<td><input type='text' name='pPrice' style='width:80px;text-align:center;' /></td>");
        sb.push("<td><input type='text' name='Comment' style='width:96%;' /></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick=\"p.fnDeleteItem(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblData").append(sb.join(""));
    },
    fnDeleteItem: function (obj) {
        $(obj).parent().parent().remove();
        p.fnSave();
    },
    fnSave: function () {
        Notice("操作完成");
    }
};