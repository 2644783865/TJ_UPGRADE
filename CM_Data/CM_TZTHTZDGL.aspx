<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_TZTHTZDGL.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_TZTHTZDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    图纸替换通知单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script type="text/javascript">

        $(function() {
            var tzd_bh1 = $("#txtbh").val();
            var spzt1 = $("#ddlSPZT option:selected").val();
            var rw1 = $("#rblRW option:selected").val();
            $("#tabTZBHTZD").treegrid({
                url: 'CM_TZTHTZDAJAX.aspx',
                queryParams: {
                    method: 'BindData',
                    tzd_bh: tzd_bh1,
                    spzt: spzt1,
                    rw: rw1
                },
                nowarp: false, //自动换行
                border: false,
                collapsible: true,
                resizeHandle: "both",
                rownumbers: true,
                idField: 'tzd_id',
                treeField: 'tzd_bh',
                fitColumns: true, //横向自适应，关系到横向滚动条
                pageSize: 10,
                pageList: [10, 20, 30],
                pagination: true,
                loadMsg: '数据加载中请稍后……',
                onDblClickRow: function(rowData) {
                    window.open('CM_TZTHTZD.aspx?action=read&id=' + rowData.tzd_id);
                },
                columns: [[{
                    field: 'tzd_id', checkbox: true, width: 50
                }, {
                    field: 'tzd_bh', title: '编号', width: 40, align: 'center'
                }, {
                    field: 'tzd_gkmc', title: '顾客名称', width: 40, align: 'center'
                },{
                    field: 'tzd_zdsj', title: '制单时间', width: 40, align: 'center'
                },{
                    field: 'tzd_nr', title: '备注', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.tzd_nr + '">' + rowData.tzd_nr.substring(0, 10) + '</span>'
                    }
                },{
                    field: 'nr_th', title: '图号', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_th + '">' + rowData.nr_th.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'nr_tm', title: '图名', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_tm + '">' + rowData.nr_tm.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'nr_bz', title: '备注', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        return '<span title="' + rowData.nr_bz + '">' + rowData.nr_bz.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'tzd_spzt', title: '审批状态', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.tzd_spzt == "0") {
                            return '<span>未审批</span>'
                        }
                        else if (rowData.tzd_spzt == "1") {
                            return '<span>审批中</span>'
                        }
                        else if (rowData.tzd_spzt == "10") {
                            return '<span>已通过</span>'
                        }
                        else {
                            return '<span>未通过</span>'
                        }
                    }
                }, {
                    field: 'Alter', title: '修改', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.tzd_spzt == '0' & rowData.tzd_zdr == rowData.username) {
                            return '<button onclick="Alter(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">修改</button>'
                        }
                        else {
                            return ''
                        }
                    }
                }, {
                    field: 'Delete', title: '删除', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.tzd_spzt == "0" & rowData.tzd_zdr == rowData.username) {
                            return '<button onclick="Delete(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">删除</button>'
                        }
                        else {
                            return ''
                        }
                    }
                }, {
                    field: 'Check', title: '审批', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.tzd_spzt == "0") {
                            if (rowData.username == rowData.tzd_spr1) {
                                return '<button onclick="Check(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">审批</button>'
                            }
                            else {
                                return ''
                            }
                        }
                        else if (rowData.tzd_spzt == "1") {
                            if (rowData.username == rowData.tzd_spr2) {
                                return '<button onclick="Check(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">审批</button>'
                            }
                            else {
                                return ''
                            }
                        }
                        else {
                            return ''
                        }
                    }
                }, {
                    field: 'Refuse', title: '驳回', width: 40, align: 'center',
                    formatter: function(value, rowData) {
                        if (rowData.username == "李利恒") {
                            return '<button onclick="Refuse(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">驳回</button>'
                        }
                        else {
                            return ''
                        }
                    }
                }
                ]]
            });
        })
        function btnAdd_onclick() {
            location.href = "CM_TZTHTZD.aspx?action=add";
        }

        function Alter(obj) {
            location.href = "CM_TZTHTZD.aspx?action=alter&id=" + obj;
        }

        function Delete(obj) {
            location.href = "CM_TZTHTZD.aspx?action=delete&id=" + obj;
        }

        function Check(obj) {
            location.href = "CM_TZTHTZD.aspx?action=check&id=" + obj;
        }

        function Refuse(obj) {
            location.href = "CM_TZTHTZD.aspx?action=refuse&id=" + obj;
        }


        ///******************************************************下面为查询的代码******************************************///

        function Query() {
            var tzd_bh = $("#txtbh").val();
            var spzt = $("#ddlSPZT option:selected").val();
            var rw = $("#rblRW option:selected").val();
            $("#tabTZBHTZD").treegrid('load', {
                method: 'BindData',
                tzd_bh: tzd_bh,
                spzt: spzt,
                rw: rw
            });
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <asp:button ID="btnExport" text="导出" runat="server" OnClick="btnExport_Click" />
                <a id="btnAdd" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onclick="btnAdd_onclick()">新增图纸替换通知单</a>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <table width="100%">
                    <tr>
                        <td>
                            按审批状态：
                            <select id="ddlSPZT" onchange="Query()" name="ddlSPZT">
                                <option value="0" selected="selected">-全部-</option>
                                <option value="1">未审批</option>
                                <option value="2">审批中</option>
                                <option value="3">已通过</option>
                                <option value="4">未通过</option>
                            </select>
                        </td>
                        <td>
                            按任务：
                            <select id="rblRW" onchange="Query()" name="rblRW">
                                <option value="0">-全部-</option>
                                <option value="1" selected="selected">我的任务</option>
                            </select>
                        </td>
                        <td>
                            按编号：
                            <input type="text" id="txtbh" name="txtbh"/>
                            <a id="btnQuery" onclick="Query()" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                                查看</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <table id="tabTZBHTZD" style="height: 420px;" width="100%">
    </table>
</asp:Content>
