var res = {
    serverURL: "Service/Resource.ashx",
    fnInitRouteType: function () { //线路类型
        $("#RouteTypeID").combobox({
            mode: 'remote',
            url: res.serverURL + "?action=1",
            valueField: 'id',
            textField: 'text',
            onSelect: function (rec) {
                var routeTypeID = rec.id;
                if (routeTypeID != "") {
                    res.fnInitDestination(routeTypeID);
                }
            }
        });
    },
    fnInitDestination: function (routeTypeID) { //创建目的地树 
        $('#DestinationID').combotree({
            url: res.serverURL + "?action=2&routeTypeID=" + routeTypeID + "&r=" + getRand(),
            idField: 'id', textField: 'text'
        });
    },
    fnAddContact: function () { //增加业务联系人
        var tblSize = $("#tblItem").find("tr").size();        
        var sb = [];
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + (tblSize + 1) + "</td>");
        sb.push("<td><input type='text' class='textbox' style='width:90px;height:26px;'></td>");
        sb.push("<td><input type='text' class='textbox' style='width:140px;height:26px;'></td>");
        sb.push("<td><input type='text' class='textbox' style='width:140px;height:26px;'></td>");
        sb.push("<td><input type='text' class='textbox' style='width:98%;height:26px;'></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick=\"res.fnDeleteItem(this)\">删除</a></td>");
        sb.push("</tr>");
        $("#tblItem").append(sb.join(""));
    },
    fnGetItem: function () { //获取联系人
        var xml = [];
        xml.push("<document>");
        $("#tblItem").find("tr").each(function () {
            var tdArr = $(this).find("td"); 
            var name = $("input[type='text']", tdArr[1]).val();
            var phone = $("input[type='text']", tdArr[2]).val();
            var fax = $("input[type='text']", tdArr[3]).val(); 
            var comment = $("input[type='text']", tdArr[4]).val();
            if (name == "" && phone == "" && phone == "" && comment == "") { }
            else {
                xml.push("<data>"); 
                xml.push("<Name>" + name + "</Name>");
                xml.push("<Phone>" + phone + "</Phone>");
                xml.push("<Fax>" + fax + "</Fax>");
                xml.push("<Comment>" + comment + "</Comment>"); 
                xml.push("</data>");
            }
        });
        xml.push("</document>");
        return xml.join("");
    },
    fnDeleteItem: function (obj) { //删除业务联系人
        $(obj).parent().parent().remove();
    }
};