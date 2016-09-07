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
    url: "Service/PaySign.ashx",
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
                        { field: 'PayAmt', title: "支出金额", width: 80, align: 'right', sortable: true, sortName: "PayAmt" },
                        { field: 'PayType', title: "支出类型", width: 150, sortable: true, sortName: "PayTypeID" }

            ]],
            columns: [[
                        { field: 'PayMethod', title: "支出方式", width: 80,align:'center', sortable: true, sortName: "PayMethod" },
                        { field: 'PayDate', title: '支出日期', sortable: true, align: 'center', sortName: "PayDate", width: 90 },
                        { field: 'DeptName', title: '支出部门', width: 160, sortable: true, sortName: "DeptID" },
                        { field: 'Operator', title: '经办人', width: 90, sortable: true, sortName: "Operator" },
                        { field: 'Comment', title: '备注', width: 200 },
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
        var u = "/Module/CheckIn/PaySignEdit.aspx?id=" + id;
        return openWindow({ "title": "支出登记", "width": "660", "height": "380", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDelete: function () {
        var id = getDataGridSelectedRow("tblData");
        if (id == "") {
            Alert("请选择支出项目");
            return false;
        }
        if (!confirm("确定要删除吗")) return;
        var url = o.url + "?action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                o.bindData();
            }
            else {
                Alert("删除失败");
            }
        })
    },
    fnView: function (id) { //查看支出信息
        var u = "/Module/CheckIn/PayInfo.aspx?id=" + id;
        return openWindow({ "title": "支出信息", "width": "660", "height": "340", "url": u }, function () { });
    }
};