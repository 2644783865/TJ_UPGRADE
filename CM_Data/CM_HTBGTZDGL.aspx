<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_HTBGTZDGL.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_HTBGTZDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    合同变更通知单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        $(function() {
            var tzd_bh1 = $("#tzd_bh").val();
            var tzd_hth1 = $("#tzd_hth").val();
            var tzd_xmmc1 = $("#tzd_xmmc").val();
            var tzd_gkmc1 = $("#tzd_gkmc").val();
            var spzt1 = $("#ddlSPZT option:selected").val();
            var rw1 = $("#rblRW option:selected").val();
            $("#tabHTGBTZD").datagrid({
                url: 'CM_HTBGTZDAJAX.aspx',
                queryParams: {
                    method: 'BindData',
                    tzd_bh: tzd_bh1,
                    tzd_hth: tzd_hth1,
                    tzd_xmmc: tzd_xmmc1,
                    tzd_gkmc: tzd_gkmc1,
                    spzt: spzt1,
                    rw: rw1
                },
                nowrap: true,
                rownumbers: true,
                idField: 'tzd_id',
                fitColumns: true,
                pageSize: 10,
                pageList: [10, 20, 30],
                pagination: true,
                loadMsg: '数据加载中请稍后……',
                onDblClickRow: function(rowIndex, rowData) {
                    window.open('CM_HTBGTZD.aspx?action=read&id=' + rowData.tzd_id);
                },
                columns: [[{
                    field: 'tzd_id', checkbox: true, width: 50
                }, {
                    field: 'tzd_bh', title: '编号', width: 40, align: 'center'
                }, {
                    field: 'tzd_hth', title: '合同号', width: 40, align: 'center'
                }, {
                    field: 'tzd_xmmc', title: '项目名称', width: 40, align: 'center'
                }, {
                    field: 'tzd_gkmc', title: '顾客名称', width: 40, align: 'center'
                }, {
                    field: 'tzd_nr', title: '内容', width: 40, align: 'center',
                    formatter: function(value, rowData, rowIndex) {
                        return '<span title="' + rowData.tzd_nr + '">' + rowData.tzd_nr.substring(0, 10) + '</span>'
                    }
                }, {
                    field: 'tzd_spzt', title: '审批状态', width: 40, align: 'center',
                    formatter: function(value, rowData, rowIndex) {
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
                    formatter: function(value, rowData, rowIndex) {
                        if (rowData.tzd_spzt == '0' & rowData.tzd_zdr == rowData.username) {
                            return '<button onclick="Alter(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">修改</button>'
                        }
                        else {
                            return ''
                        }
                    }
                }, {
                    field: 'Delete', title: '删除', width: 40, align: 'center',
                    formatter: function(value, rowData, rowIndex) {
                        if (rowData.tzd_spzt == "0" & rowData.tzd_zdr == rowData.username) {
                            return '<button onclick="Delete(' + rowData.tzd_id + ');return false;" style="background-color:LightGreen;">删除</button>'
                        }
                        else {
                            return ''
                        }
                    }
                }, {
                    field: 'Check', title: '审批', width: 40, align: 'center',
                    formatter: function(value, rowData, rowIndex) {
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
                    formatter: function(value, rowData, rowIndex) {
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
            $("#<%=panSX.ClientID%>").hide();
        })

        function Alter(obj) {
            location.href = "CM_HTBGTZD.aspx?action=alter&id=" + obj;
        }

        function Delete(obj) {
            location.href = "CM_HTBGTZD.aspx?action=delete&id=" + obj;
        }

        function Check(obj) {
            location.href = "CM_HTBGTZD.aspx?action=check&id=" + obj;
        }

        function Refuse(obj) {
            location.href = "CM_HTBGTZD.aspx?action=refuse&id=" + obj;
        }

        function btnQTSX_onclick() {
            $("#<%=panSX.ClientID%>").show();
        }
    </script>

    <script type="text/javascript">
        function btnAdd_onclick() {
            location.href = 'CM_HTBGTZD.aspx?action=add';
        }

        function Query() {
            var tzd_bh = $("#tzd_bh").val();
            var tzd_hth = $("#tzd_hth").val();
            var tzd_xmmc = $("#tzd_xmmc").val();
            var tzd_gkmc = $("#tzd_gkmc").val();
            var spzt = $("#ddlSPZT option:selected").val();
            var rw = $("#rblRW option:selected").val();
            $("#tabHTGBTZD").datagrid('load', {
                method: 'BindData',
                tzd_bh: tzd_bh,
                tzd_hth: tzd_hth,
                tzd_xmmc: tzd_xmmc,
                tzd_gkmc: tzd_gkmc,
                spzt: spzt,
                rw: rw
            });
        }

        function btnResert_onclick() {
            $("input:text").each(function() {
                $(this).val('');
            })
        }

        function btnClose_onclick() {
            $("#<%=panSX.ClientID%>").hide();
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
     <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title" align="right">
                <a id="btnAdd" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onclick="btnAdd_onclick()">新增合同变更通知单</a>
            </div>
        </div>
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
                        </td>
                        <td>
                            按任务：
                            <select id="rblRW" onchange="Query()">
                                <option value="0">全部</option>
                                <option value="1" selected="selected">我的任务</option>
                            </select>
                        </td>
                        <td>
                            <a id="btnQTSX" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onclick="btnQTSX_onclick()">其他筛选</a>
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
                    <input type="text" id="tzd_bh" />
                </td>
                <td>
                    按合同号：
                </td>
                <td>
                    <input type="text" id="tzd_hth" />
                </td>
            </tr>
            <tr>
                <td>
                    按项目名称：
                </td>
                <td>
                    <input type="text" id="tzd_xmmc" />
                </td>
                <td>
                    按顾客名称：
                </td>
                <td>
                    <input type="text" id="tzd_gkmc" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="tabHTGBTZD" style="height: 420px;" width="100%">
    </table>
</asp:Content>
