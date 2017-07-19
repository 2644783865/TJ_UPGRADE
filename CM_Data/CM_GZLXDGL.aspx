<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_GZLXDGL.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_GZLXDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工作联系单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        var id;
        function btnAdd_onclick() {
            location.href = 'CM_GZLXD.aspx?action=add';
        }
        $(function() {
            var spzt = $("#ddlSPZT option:selected").val();
            var rw = $("#rblRW option:selected").val();
            var lxd_bh = $("#lxd_bh").val();
            var lxd_hth = $("#lxd_hth").val();
            var lxd_xmmc = $("#lxd_xmmc").val();
            var lxd_dhdw = $("#lxd_dhdw").val();
            $("#tabGZLXD").treegrid({
                url: 'CM_GZLXDGLAJAX.aspx',
                queryParams: {
                    method: 'BindData',
                    spzt: spzt,
                    rw: rw,
                    lxd_bh: lxd_bh,
                    lxd_hth: lxd_hth,
                    lxd_xmmc: lxd_xmmc,
                    lxd_dhdw: lxd_dhdw
                },
                nowarp: false, //自动换行
                border: false,
                collapsible: true,
                resizeHandle: "both",
                rownumbers: true,
                idField: 'lxd_id',
                treeField: 'lxd_bh',
                fitColumns: true, //横向自适应，关系到横向滚动条
                pageSize: 10,
                pageList: [10, 20, 30],
                pagination: true,
                loadMsg: '数据加载中请稍后……',
                onDblClickRow: function(rowData) {
                    window.open('CM_GZLXD.aspx?action=read&id=' + rowData.lxd_id);
                },
                columns: [[{
                    field: 'lxd_id', checkbox: true, width: 50
                }, {
                    field: 'lxd_bh', title: '编号', width: 40, align: 'center'
                }, {
                    field: 'lxd_dhdw', title: '订货单位', width: 40, align: 'center'
                }, {
                    field: 'lxd_xmmc', title: '项目名称', width: 40, align: 'center'
                }, {
                    field: 'lxd_hth', title: '合同号', width: 40, align: 'center'
                }, {
                    field: 'nr_sbmc', title: '设备名称', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_sbmc + '">' + rowData.nr_sbmc.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'nr_th', title: '图号', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_th + '">' + rowData.nr_th.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'nr_sl', title: '数量', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_sl + '">' + rowData.nr_sl.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'nr_bz', title: '备注', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_bz + '">' + rowData.nr_bz.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'Alter', title: '修改', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.state == "closed") {
                            if ((rowData.lxd_spzt == "0" || rowData.lxd_spzt == "11") && rowData.username == rowData.lxd_zdr) {
                                return '<button onclick="alter(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">修改</button>'
                            }
                            else {
                                return '';
                            }
                        }
                        else {
                            return '';
                        }
                    }
                }, {
                    field: 'Delete', title: '删除', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.state == "closed") {
                            if ((rowData.lxd_spzt == "0" || rowData.lxd_spzt == "11") && rowData.username == rowData.lxd_zdr) {
                                return '<button onclick="Delete(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">删除</button>'
                            }
                            else {
                                return '';
                            }
                        }
                        else {
                            return '';
                        }
                    }
                }, {
                    field: 'Check', title: '审批', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.state == "closed") {
                            if (rowData.lxd_spzt == "0") {
                                if (rowData.lxd_spr1 == rowData.username) {
                                    return '<button onclick="Check(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">审批</button>'
                                }
                                else {
                                    return '';
                                }
                            }
                            else if (rowData.lxd_spzt == "1") {
                                if (rowData.lxd_spr2 == rowData.username) {
                                    return '<button onclick="Check(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">审批</button>'
                                }
                                else {
                                    return '';
                                }
                            }
                            else {
                                return '';
                            }
                        }
                        else {
                            return '';
                        }
                    }
                }, {
                    field: 'Refuse', title: '驳回', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.lxd_spzt == "10") {
                            if (rowData.lxd_splx == "1") {
                                if (rowData.lxd_spr1 == rowData.username) {
                                    return '<button onclick="Refuse(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">驳回</button>'
                                }
                                else {
                                    return '';
                                }
                            }
                            else {
                                if (rowData.lxd_spr2 == rowData.username) {
                                    return '<button onclick="Refuse(' + rowData.lxd_id + ');return false;" style="background-color:LightGreen;">驳回</button>'
                                }
                                else {
                                    return '';
                                }
                            }
                        }
                        else {
                            return '';
                        }
                    }
                }

            ]],
                rowStyler: function(rowData) {
                    console.info(rowData);
                    if (rowData.lxd_id == "") {
                        return 'background-color:#F0F8FF;';
                    }
                }
            });
            $(".datagrid-header div").css('background-color', '#12cccc');
            $("#<%=panSX.ClientID%>").hide();
        })

        function alter(obj) {
            location.href = "CM_GZLXD.aspx?action=alter&id=" + obj;
            //            console.info(obj);
        }

        function Delete(obj) {
            location.href = "CM_GZLXD.aspx?action=delete&id=" + obj;
        }

        function Check(obj) {
            location.href = "CM_GZLXD.aspx?action=check&id=" + obj;
        }

        function Refuse(obj) {
            location.href = "CM_GZLXD.aspx?action=refuse&id=" + obj;
        }

        ///*********************************************下面是查询代码*********************************************///

        function btnQTSX_onclick() {
            $("#<%=panSX.ClientID%>").show();
        }

        function btnResert_onclick() {
            $("input:text").each(function() {
                $(this).val('');
            })
        }

        function btnClose_onclick() {
            $("#<%=panSX.ClientID%>").hide();
        }

        function Query() {
            var spzt = $("#ddlSPZT option:selected").val();
            var rw = $("#rblRW option:selected").val();
            var lxd_bh = $("#lxd_bh").val();
            var lxd_hth = $("#lxd_hth").val();
            var lxd_xmmc = $("#lxd_xmmc").val();
            var lxd_dhdw = $("#lxd_dhdw").val();
            $("#tabGZLXD").treegrid('load', {
                method: 'BindData',
                spzt: spzt,
                rw: rw,
                lxd_bh: lxd_bh,
                lxd_hth: lxd_hth,
                lxd_xmmc: lxd_xmmc,
                lxd_dhdw: lxd_dhdw
            });
        }
       
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            按审批状态：
                            <select id="ddlSPZT" onchange="Query()">
                                <option value="0">-全部-</option>
                                <option value="1">未审批</option>
                                <option value="2">审批中</option>
                                <option value="3">已通过</option>
                                <option value="4">未通过</option>
                            </select>
                            &nbsp; &nbsp; &nbsp; 按任务：
                            <select id="rblRW" onchange="Query()">
                                <option value="0">全部</option>
                                <option value="1" selected="selected">我的任务</option>
                            </select>
                        </td>
                        <td>
                            <a id="btnQTSX" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onclick="btnQTSX_onclick()">其他筛选</a>
                        </td>
                        <td>
                            <a id="btnAdd" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onclick="btnAdd_onclick()">新增工作联系单</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="panSX" runat="server">
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <input type="button" id="btnQuery" onclick="Query()" value="查看" />
                </td>
                <td>
                    <input type="button" id="btnResert" onclick="btnResert_onclick()" value="重置" />
                </td>
                <td>
                    <input type="button" id="btnClose" onclick="btnClose_onclick()" value="关闭" />
                </td>
            </tr>
            <tr>
                <td>
                    按编号:
                </td>
                <td>
                    <input type="text" id="lxd_bh" />
                </td>
                <td>
                    按合同号：
                </td>
                <td>
                    <input type="text" id="lxd_hth" />
                </td>
            </tr>
            <tr>
                <td>
                    按项目名称：
                </td>
                <td>
                    <input type="text" id="lxd_xmmc" />
                </td>
                <td>
                    按订货单位：
                </td>
                <td>
                    <input type="text" id="lxd_dhdw" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div>
        <table id="tabGZLXD" style="height: 420px;" width="100%">
        </table>
    </div>
</asp:Content>
