$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnBindData();
        $("#btnAdd").click(function () {
            t.fnEdit('');
        });
        $("#btnQuery").click(function () {
            t.fnQuery();
        });
    },
    serverUrl: "Service/OrgInfo.ashx?rnd=" + getRand(),
    fnBindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 35,
            nowrap: true,
            iconCls: 'icon-reload',
            url: t.serverUrl + '&action=1&xType=' + request("xType"),
            idField: 'ID',
            border: false,
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            columns: [[
                { field: 'AreaName', title: '区域名称', width: 50, sortable: true, sortName: "AreaID" },
                { field: 'Name', title: '机构名称', width: 140, sortable: true, sortName: "Name" },
                {
                    field: 'ProDomain', title: '发布网址', width: 120, sortable: true, sortName: "ProDomain", formatter: function (val, rec) {
                        return "<a href='http://" + val + "' target='_blank'>" + val + "</a>";
                    }
                },
                {
                    field: 'DataStatus', title: '状态', width: 50, align: "center", sortable: true, sortName: "DataStatus",
                    formatter: function (val, rec) {
                        return val == "1" ? "启用" : "<span style='color:red;'>禁用</span>";
                    }
                },
                {
                    field: 'CreateDate', title: '注册日期', width: 90, align: 'center', sortable: true, sortName: "CreateDate", formatter: function (val, rec) {
                        var arr = val.split(' ');
                        return arr.length > 0 ? arr[0] : "";
                    }
                },
                {
                    field: 'ExpiryDate', title: '授权使用日期范围', width: 160, align: 'center', sortable: true, sortName: "ExpiryDate", formatter: function (val, rec) {
                        var arr = rec.OpenDate.split(' ');
                        var t1 = arr.length > 0 ? arr[0] : "";

                        arr = val.split(' ');
                        var t2 = arr.length > 0 ? arr[0] : "";
                        return t1 + " / <span style='color:red;'> " + t2 + "</span>";
                    }
                },
                {
                    field: 'OrgAdmin', title: '管理员', width: 80, align: 'center', sortable: true, sortName: "OrgAdmin", formatter: function (val, rec) {
                        var t = val == "" ? "<span style='color:red;'>设置管理员</span>" : val;
                        return "<a href='javascript:;' onclick=\"t.fnSetAdmin('" + rec.ID + "')\">" + t + "</a>";
                    }
                },
                {
                    field: 'NavGroup', title: '应用导航组', width: 80, align: 'center', sortable: true, sortName: "NavGroup", formatter: function (val, rec) {
                        return "<a href='javascript:;' onclick=\"t.fnNavGroupEdit('" + rec.NavGroupID + "')\">" + val + "</a>";
                    }
                },
                 {
                     field: 'QRCode', title: '二维码', width: 80, align: 'center', sortable: true, sortName: "QRCode", formatter: function (val, rec) {
                         var viewBar = "<a href='javascript:;' onclick=\"t.fnViewQRCode('" + rec.ID + "')\">查看</a>"
                         var createBar = "<a href='javascript:;' title='创建二维码' onclick=\"t.fnCreateQRCode('" + rec.ID + "')\">创建</a>"
                         if (val == "") {
                             return createBar;
                         }
                         else {
                             return viewBar + " | " + createBar;
                         }
                     }
                 },
                  {
                      field: 'SmsCount', title: '可发短信数', width: 90, align: 'center', sortable: true, sortName: "SmsCount", formatter: function (val, rec) {
                          var v = val == "" ? 0 : parseInt(val);
                          var sendCount = rec.SendSmsCount == "" ? 0 : parseInt(rec.SendSmsCount);
                          var a = v - sendCount;
                          return "【<span style='color:red;'>" + a + "</span>】<a href='javascript:;' onclick=\"t.fnSmsSetting('" + rec.ID + "')\">设置</a>";
                      }
                  },
                  {
                      field: 'ReceiptAmt', title: '累计收款金额', width: 100, align: 'center', sortable: true, sortName: "ReceiptAmt", formatter: function (val, rec) {
                          var amt = (rec.ReceiptAmt == "" || typeof (rec.ReceiptAmt) == "undefined") ? 0 : rec.ReceiptAmt;
                          var a = "【<a href='javascript:;' onclick=\"t.fnReceiptItem('" + rec.ID + "')\" title='查看收款明细' style='color:red;'>" + amt + "</a>】";
                          var b = "<a href='javascript:;' onclick=\"t.fnReceipt('" + rec.ID + "')\">收款</a>";
                          return a + " " + b;
                      }
                  },
                   {
                       field: 'MaxUserCount', title: '用户数', width: 50, sortable: true, align: 'center', sortName: "MaxUserCount", formatter: function (val, rec) {
                           return "<span style='color:red; font-weight:bold;'>" + (val == "-1" ? "不限" : (val == "" ? "0" : val)) + "</span>";
                       }
                   },
                {
                    field: 'Opt', title: "操作", width: 180, align: 'center', formatter: function (val, rec) {
                        var btnEdit = "<a href=\"javascript:;\" onclick=\"t.fnEdit('" + rec.ID + "')\">编辑</a>";
                        var btnDel = "<a href=\"javascript:;\" onclick=\"t.fnDeleteNode('" + rec.ID + "')\">删除</a>";
                        var setting = "<a href='javascript:;' onclick=\"t.fnExtendSetting('" + rec.ID + "')\">扩展设置</a>";
                        var init = "<a href='javascript:;' onclick=\"t.fnInitData('" + rec.ID + "')\">初始化</a>";
                        return btnEdit + " | " + btnDel + " | " + setting + " | " + init;
                    }
                }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var key = $("#txtKey").val();
        $('#tblData').datagrid("reload", { "key": key });
    },
    fnEdit: function (id) {
        var u = "/Module/Om/OrgInfoEdit.aspx?id=" + id;
        return openWindow({ "title": "编辑机构信息", "width": 600, "height": 550, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnViewQRCode: function (id) {
        return openWindow({ "title": "查看机构二维码", "width": 330, "height": 280, "url": "/Module/Om/QRCode.aspx?id=" + id }, function () {
        });
    },
    fnCreateQRCode: function (id) { //创建机构二维码
        var u = t.serverUrl + "&action=3&id=" + id;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                Notice("二维码创建成功");
                $('#tblData').datagrid("reload");
            } else {
                Alert("创建二维码失败");
            }
        });
    },
    fnReceiptItem: function (id) { //查看收款明细
        var title = "查看收款明细";
        var url = "ReceiptItem.aspx?orgID=" + id;
        addTab(title, url, function () { $('#tblData').datagrid("reload"); });
    },
    fnReceipt: function (id) { //收款
        var u = "/Module/Om/Receipt.aspx?orgID=" + id;
        return openWindow({ "title": "收款", "width": "480", "height": "400", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnNavGroupEdit: function (groupID) { //导航组
        var u = "/Module/Om/NavGroupEdit.aspx?id=" + groupID;
        return openWindow({ "title": "编辑导航组信息", "width": "750", "height": "480", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnSetAdmin: function (id) { //设置管理员
        var u = "/Module/Om/OrgAdmin.aspx?id=" + id;
        return openWindow({ "title": "设置机构管理员", "width": 520, "height": 320, "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnDeleteNode: function (id) {
        if (!confirm("确定要删除吗")) return;
        var url = t.serverUrl + "&action=2&id=" + id;
        dataService.ajaxGet(url, function (data) {
            if (data == "1") {
                Notice("删除成功");
                $('#tblData').datagrid("reload");
            }
            else {
                Notice("删除失败");
            }
        })
    },
    fnInitData: function (id) {
        if (!confirm("初始化会清空原有数据且不能恢复，强烈建议开设机构时初始化一次，请慎用此功能！确定要继续吗?")) return false;
        var u = t.serverUrl + "&action=4&id=" + id;
        dataService.ajaxGet(u, function (data) {
            if (data == "1") {
                Alert("初始化成功");
            }
            else {
                Alert("初始化失败");
            }
        });
    },
    fnSmsSetting: function (orgID) { //短信设置
        var url = "SmsSetting.aspx?orgID=" + orgID;
        addTab("短信设置", url, function () { t.fnQuery(); });
    },
    fnExtendSetting: function (orgID) { //扩展设置
        var u = "/Module/Om/OrgSetting.aspx?id=" + orgID;
        return openWindow({ "title": "扩展设置", "width": "500", "height": "450", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnOpenKjYouApi: function (id, isOpen) { //开通快捷机票接口
        if (confirm("确定要开通机票预订接口吗")) {
            var u = t.serverUrl + "&action=5&id=" + id + "&isOpen=" + isOpen;
            dataService.ajaxGet(u, function (data) {
                if (data == "1") {
                    t.fnBindData();
                    Notice("操作成功");
                }
                else {
                    Alert("操作失败");
                }
            });
        }
    }
};

