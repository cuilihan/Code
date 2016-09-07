<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectOTAArea.aspx.cs" Inherits="DRP.WEB.Module.Sys.SelectOTAArea" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>选择区域</title>
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/zTree/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.excheck-3.5.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/zTree/jquery.ztree.exedit-3.5.js" type="text/javascript"></script>
    <script type="text/javascript">
        var api = frameElement.api, W = api.opener;

        function bindArea() {
            var setting = {
                data: {
                    simpleData: {
                        enable: true
                    }
                }, view: { showIcon: false, selectedMulti: false, nameIsHTML: true, fontCss: setFontCss_ztree }
            };
            var u = "Service/Area.ashx?action=4&r=" + getRand();
            dataService.ajaxGet(u, function (s) {
                if (s != "") {
                    $.fn.zTree.init($("#treeData"), setting, eval(s));
                }
            });
        }
        $(function () {
            $("#search_condition").val(unescape(request("text")));

            bindArea();

            //search_ztree('treeData', 'search_condition')
            $("#btnOk").click(function () {
                var zTree = $.fn.zTree.getZTreeObj("treeData");
                var nodes = zTree.getSelectedNodes();
                var arrID = [];
                var arrName = [];

                if (nodes[0] == null) {
                    alert("请选择节点"); return false;
                }
                var otaaID = nodes[0].id;
                var otaName = nodes[0].name;
                var id = request("id");
                var name = unescape(request("text"));
                var otdid = request("otdid");
                var jdata = { "aid": id, "otdid": otdid, "otdaid": otaaID, "name": name, "otaName": otaName };
                var u = "Service/Area.ashx?action=6&r=" + getRand() + "&aid="+ id+ "&otdid="+ otdid+ "&otdaid="+ otaaID+ "&name="+ name+ "&otaName="+ otaName;

                dataService.ajaxGet(u, function (data) {
                    if (data == "1") {
                        alert("绑定成功");
                        W.loadData();
                        api.close();
                    } else {
                        alert("操作失败");
                    }
                })

            });
        });

        /**
     * 展开树
     * @param treeId  
     */
        function expand_ztree(treeId) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            treeObj.expandAll(true);
        }

        /**
         * 收起树：只展开根节点下的一级节点
         * @param treeId
         */
        function close_ztree(treeId) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            var nodes = treeObj.transformToArray(treeObj.getNodes());
            var nodeLength = nodes.length;
            for (var i = 0; i < nodeLength; i++) {
                if (nodes[i].id == '0') {
                    //根节点：展开
                    treeObj.expandNode(nodes[i], true, true, false);
                } else {
                    //非根节点：收起
                    treeObj.expandNode(nodes[i], false, true, false);
                }
            }
        }

        /**
         * 搜索树，高亮显示并展示【模糊匹配搜索条件的节点s】
         * @param treeId
         * @param searchConditionId 文本框的id
         */
        function search_ztree(treeId, searchConditionId) {
            searchByFlag_ztree(treeId, searchConditionId, "");
        }

        /**
         * 搜索树，高亮显示并展示【模糊匹配搜索条件的节点s】
         * @param treeId
         * @param searchConditionId     搜索条件Id
         * @param flag                  需要高亮显示的节点标识
         */
        function searchByFlag_ztree(treeId, searchConditionId, flag) {
            //<1>.搜索条件
            var searchCondition = $('#' + searchConditionId).val();
            //<2>.得到模糊匹配搜索条件的节点数组集合
            var highlightNodes = new Array();
            if (searchCondition != "") {
                var treeObj = $.fn.zTree.getZTreeObj(treeId);
                highlightNodes = treeObj.getNodesByParamFuzzy("name", searchCondition, null);
            }
            //<3>.高亮显示并展示【指定节点s】
            highlightAndExpand_ztree(treeId, highlightNodes, flag);
        }

        /**
         * 高亮显示并展示【指定节点s】
         * @param treeId
         * @param highlightNodes 需要高亮显示的节点数组
         * @param flag           需要高亮显示的节点标识
         */
        function highlightAndExpand_ztree(treeId, highlightNodes, flag) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            //<1>. 先把全部节点更新为普通样式
            var treeNodes = treeObj.transformToArray(treeObj.getNodes());
            for (var i = 0; i < treeNodes.length; i++) {
                treeNodes[i].highlight = false;
                treeObj.updateNode(treeNodes[i]);
            }
            //<2>.收起树, 只展开根节点下的一级节点
            close_ztree(treeId);
            //<3>.把指定节点的样式更新为高亮显示，并展开
            if (highlightNodes != null) {
                for (var i = 0; i < highlightNodes.length; i++) {
                    if (flag != null && flag != "") {
                        if (highlightNodes[i].flag == flag) {
                            //高亮显示节点，并展开
                            highlightNodes[i].highlight = true;
                            treeObj.updateNode(highlightNodes[i]);
                            //高亮显示节点的父节点的父节点....直到根节点，并展示
                            var parentNode = highlightNodes[i].getParentNode();
                            var parentNodes = getParentNodes_ztree(treeId, parentNode);
                            treeObj.expandNode(parentNodes, true, false, true);
                            treeObj.expandNode(parentNode, true, false, true);
                        }
                    } else {
                        //高亮显示节点，并展开
                        highlightNodes[i].highlight = true;
                        treeObj.updateNode(highlightNodes[i]);
                        //高亮显示节点的父节点的父节点....直到根节点，并展示
                        var parentNode = highlightNodes[i].getParentNode();
                        var parentNodes = getParentNodes_ztree(treeId, parentNode);
                        treeObj.expandNode(parentNodes, true, false, true);
                        treeObj.expandNode(parentNode, true, false, true);
                    }
                }
            }
        }

        /**
         * 递归得到指定节点的父节点的父节点....直到根节点
         */
        function getParentNodes_ztree(treeId, node) {
            if (node != null) {
                var treeObj = $.fn.zTree.getZTreeObj(treeId);
                var parentNode = node.getParentNode();
                return getParentNodes_ztree(treeId, parentNode);
            } else {
                return node;
            }
        }

        /**
         * 设置树节点字体样式
         */
        function setFontCss_ztree(treeId, treeNode) {
            if (treeNode.id == 0) {
                //根节点
                return { color: "#333", "font-weight": "bold" };
            } else if (treeNode.isParent == false) {
                //叶子节点
                return (!!treeNode.highlight) ? { color: "#ff0000", "font-weight": "bold" } : { color: "#660099", "font-weight": "normal" };

                treeNode.checked = true;
            } else {
                //父节点
                return (!!treeNode.highlight) ? { color: "#ff0000", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };

                treeNode.checked = true;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 5px;">
        <div style="padding: 5px; width: 236px; height: 322px; border: 1px solid #ccc; overflow: auto;">
            <!--搜索框-->
            <div class="padd" style="padding-bottom: 0px;">
                <div class="input-append row-fluid" style="margin-bottom: 0px;">
                    <input id="search_condition" type="text" placeholder="请输入搜索条件" class="span8" style="font-size: 12px" />
                    <button type="button" class="btn btn-info" onclick="search_ztree('treeData', 'search_condition')">搜索</button>
                </div>
            </div>
            <ul id="treeData" class="ztree">
            </ul>
        </div>
        <div style="text-align: right; margin-top: 6px;">
            <a href="javascript:;" plain="true" id="btnOk" class="easyui-linkbutton" iconcls="icon-ok">确定</a> <a href="javascript:;" plain="true" class="easyui-linkbutton" onclick="api.close();"
                iconcls="icon-cancel">取消</a>
        </div>
    </form>
</body>
</html>
