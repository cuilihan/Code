$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
        $("#btnAdd").click(function () {
            o.fnEdit('');
        });
        $("#btnDelete").click(function () {
            o.fnDelete();
        });
    },
    url: "Service/IncomeSign.ashx",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1",
            idField: 'ID',
            frozenColumns: [[
                 { field: 'ID', checkbox: true },
                        { field: 'IncomeAmt', title: "收入金额", width: 80, align: 'right', sortable: true, sortName: "IncomeAmt" },
                        { field: 'IncomeType', title: "收入类型", width: 150, sortable: true, sortName: "IncomeTypeID" }

            ]],
            columns: [[
                        { field: 'IncomeMethod', title: "收入方式", align: 'center', width: 70, sortable: true, sortName: "IncomeMethod" },
                        { field: 'IncomeDate', title: '收入日期', align: 'center', sortable: true, sortName: "IncomeDate", width: 90 },
                        { field: 'DeptName', title: '收入部门', width: 160, sortable: true, sortName: "DeptID" },
                        { field: 'Operator', title: '经办人', width: 90, sortable: true, sortName: "Operator" },
                        { field: 'IncomeSource', title: '收入来源', sortable: true, sortName: "IncomeSource", width: 160 },
                        {
                            field: 'Opt', title: '操作', width: 90, align: 'center', formatter: function (val, rec) {
                                var edit = "<a href='javascript:;' onclick=\"o.fnEdit('" + rec.ID + "')\">编辑</a>";
                                var view = "<a href='javascript:;' onclick=\"o.fnView('" + rec.ID + "')\">查看</a>";
                                return view + " | " + edit;
                            }
                        }

            ]],
            singleSelect: false, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var type = $("#ddlType option:selected").val();
        var dept = $("#ddlDeptID option:selected").val();
        var operator = $("#txtOperator").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "Opeator": operator, "TypeID": type, "DeptID": dept, "sDate": sDate, "eDate": eDate });
    },
    fnEdit: function (id) {
        var u = "/Module/CheckIn/IncomeSignEdit.aspx?id=" + id;
        return openWindow({ "title": "收入登记", "width": "660", "height": "380", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDelete: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择收入项目");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var r = o.url + "?action=2&id=" + id + "&r=" + getRand();
        dataService.ajaxGet(r, function (data) {
            if (data == "1") {
                Notice("删除成功");
                o.bindData();
            }
            else {
                Alert("删除失败");
            }
        })
    },
    fnView: function (id) { //查看收入信息
        var u = "/Module/CheckIn/IncomeInfo.aspx?id=" + id;
        return openWindow({ "title": "收入信息", "width": "600", "height": "340", "url": u }, function () { });
    }
};