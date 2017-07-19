<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PM_FHList.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_FHList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品发运台帐
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script src="../JS/EasyUI/Extentions/jeasyui.extensions.validatebox.js" type="text/javascript"></script>

    <script src="../JS/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tgKucun").treegrid({
                url: 'PM_AjaxFahuo.aspx',
                loadMsg: '数据加载中请稍后……',
                queryParams: {
                    method: 'GetKuCun'
                },
                idField: "pid",
                treeField: "bm_zongxu",
                fitColumns: true,
                resizeHandle: "both",
                collapsible: true,
                singleSelect: false,
                pageNumber: 1,
                pageSize: 30,
                pageList: [10, 30, 50],
                pagination: true,
                onLoadSuccess: function() {
                    delete $(this).treegrid('options').queryParams['id'];
                }, columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        }, {
                            field: 'pid', title: '序号Id', width: 40, align: "center", hidden: "true"
                        }, {
                            field: 'cm_bianhao', title: '编号', width: 40, align: "center"
                        },
                        {
                            field: 'cm_proj', title: '项目', width: 40, align: "center"
                        },

                            {
                                field: 'bm_engid', title: '任务号', width: 40, align: "center"
                            }, {
                                field: 'bm_zongxu', title: '总序', width: 40, align: "center"
                            }, {
                                field: 'bm_chaname', title: '设备名称', width: 50
                            }, {
                                field: 'bm_tuhao', title: '图号', width: 40, align: "center"
                            },{
                                field: 'cm_fhnum', title: '发货数量', width: 40, align: "center"
                            }, {
                                field: 'kc_kcnum', title: '库存数量', width: 40, align: "center"
                            }, {
                                field: 'bjnum', title: '比价数量', align: "center", formatter: function(value, row, index) {
                                return '  <input type="text" value="' + value + '"  style="width:40px" onKeyPress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"  />';
                                }
                            }, {
                                field: 'cm_cusname', title: '收货单位', width: 40, align: "center"
                            }, {
                                field: 'tsa_idnote', title: '发货备注', width: 100, align: "center"
                            }, { field: 'bm_ku', title: '库' }

            ]]
            });
        });

    
    </script>

    <script type="text/javascript">
        //得到选中的总序
        function GetSelZongxu() {
            var zong = '';
            var objdata = $("#tgKucun").treegrid('getSelections');
            if (objdata.length == 0) {
                $.messager.show({
                    title: '提示',
                    msg: '请勾选要比价的记录!',
                    timeout: 5000,
                    showType: 'slide'
                });
                return "";
            }
            else {
                for (var i = 0; i < objdata.length; i++) {
                    if (objdata[i].bm_fhstate == '1' || objdata[i].bm_fhstate == '2') {
                        $.messager.show({
                            title: '提示',
                            msg: '该记录中存在已比价的条目，无法再次比价!',
                            timeout: 5000,
                            showType: 'slide'
                        });
                        return "";
                    }
                    if (objdata[i].kc_kcnum == '' || objdata[i].kc_kcnum == '0') {
                        $.messager.show({
                            title: '提示',
                            msg: '库存数量为零，无法比价!',
                            timeout: 5000,
                            showType: 'slide'
                        });
                        return "";
                    }
                }
                var newArray = [];
                $(objdata).each(function(i) {

                    var newobj = {};
                    newobj.tsaid = objdata[i].bm_engid;
                    newobj.zongxu = objdata[i].bm_zongxu;
                    newobj.map = objdata[i].bm_tuhao;
                    newobj.bianhao = objdata[i].cm_bianhao;
                    newobj.fid = objdata[i].cm_fid;
                    newobj.pid = objdata[i].pid;
                    newobj.num = objdata[i].kc_kcnum;
                    newobj.name = objdata[i].bm_chaname;
                    newArray.push(newobj);
                    console.log(objdata[i]);
                    $("#divTable tr").each(function(j) {
                        //  console.log($(this).find("div:eq(1)").html());
                        if ($(this).find("div:eq(1)").html() == objdata[i].pid) {
                            newobj.bjnum = $(this).find("input[type=text]").val();
                        }
                    });

                });
                // console.log($("#divTable tr:eq(" + i + ")").find("input[type=text]"));
                return newArray;
            }
        }

        //发运比价
        $(function() {
            $("#btnFayun").click(function() {
                var zongxu = "";
                zongxu = GetSelZongxu();
               
                if (zongxu != "") {
                    var str = JSON.stringify(zongxu);
                    $.ajax({
                        type: "POST",
                        url: "PM_AjaxFahuo.aspx",
                        data: "method=FayunBJ&data=" + str,
                        success: function(data) {
                            var dataObj = eval("(" + data + ")");
                            if (dataObj.state == "success") {
                                $.messager.show({
                                    title: '提示',
                                    msg: "操作成功！",
                                    timeout: 5000,
                                    showType: 'slide'
                                });
                                $("#tgKucun").treegrid('reload');
                                window.open('PM_fayun_check_detail.aspx?sheetno=' + dataObj.sheetcode);
                            }
                            else {
                                $.messager.show({
                                    title: '提示',
                                    msg: "数据错误，请联系管理员！",
                                    timeout: 5000,
                                    showType: 'slide'
                                });
                            }

                        }
                    });
                }


            });
        });
        //不比价

        $(function() {
            $("#btnBbj").click(function() {
                if (confirm("您确定这是不需比价吗？请确定后再执行")) {
//                    return true;
                }
                else {
                    return false;
                }
                var zongxu = "";
                zongxu = GetSelZongxu();
                if (zongxu != "") {
                    var str = JSON.stringify(zongxu);
                    $.ajax({
                        type: "POST",
                        url: "PM_AjaxFahuo.aspx",
                        data: "method=BuBJ&data=" + str,
                        success: function(data) {
                            var dataObj = eval("(" + data + ")");
                            console.log(dataObj.msg);
                            $.messager.show({
                                title: '提示',
                                msg: dataObj.msg,
                                timeout: 5000,
                                showType: 'slide'
                            });
                            $("#tgKucun").treegrid('reload');
                        }
                    });
                }


            });
        });

        //搜索
        $(function() {
            $("#btnSearch").click(function() {
                var tsaid = $("#txtTsaid").val();
                var proj = $("#txtProj").val();
                var map = $("#txtTuhao").val();
                var name = $("#txtName").val();
                console.log(proj);
                $("#tgKucun").treegrid('load', {
                    method: 'GetKuCun',
                    tsaid: tsaid,
                    proj: proj,
                    map: map,
                    name: name
                });
            });
        });
    
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                任务号：
                            </td>
                            <td>
                                <input type="text" id="txtTsaid" />
                            </td>
                            <td>
                                项目：
                            </td>
                            <td>
                                <input type="text" id="txtProj" />
                            </td>
                            <td>
                                图号：
                            </td>
                            <td>
                                <input type="text" id="txtTuhao" />
                            </td>
                            <td>
                                名称：
                            </td>
                            <td>
                                <input type="text" id="txtName" />
                            </td>
                            <td>
                                <input type="button" value="查询" id="btnSearch" />
                            </td>
                            <td align="right">
                                <input type="button" id="btnBbj" value="不需比价" />
                                <input type="button" id="btnFayun" value="发运询比价" />
                                <asp:Label ID="lab_bianhao" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="divTable">
        <table id="tgKucun" style="height: 400px">
        </table>
    </div>
</asp:Content>
