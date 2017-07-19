<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_LIST.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品库存查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            $("#tgKucun").treegrid({
                url: 'PM_AjaxFinished.aspx',
                collapsible: true,
                loadMsg: '数据加载中请稍后……',
                queryParams: {
                    method: 'GetKuCun'
                },
                idField: "bm_id",
                treeField: "bm_zongxu",
                fitColumns: true,
                resizeHandle: "both",
                collapsible: true,
                singleSelect: false,
                pageNumber: 1,
                pageSize: 10,
                pageList: [10, 15, 20],
                pagination: true,
                onLoadSuccess: function() {
                    delete $(this).treegrid('options').queryParams['id'];
                }, columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        },
                         {
                             field: 'bm_zongxu', title: '总序', width: 40, align: "center"
                         },
                            {
                                field: 'bm_engid', title: '任务号', width: 40, align: "center"
                            },
                               {
                                   field: 'cm_proj', title: '项目', width: 40, align: "center"
                               },

                {
                    field: 'bm_tuhao', title: '图号', width: 40, align: "center"
                },

                {
                    field: 'bm_chaname', title: '名称', width: 50
                }, { field: 'bm_tuunitwght', title: '单重' }, { field: 'bm_singnumber', title: '单台数量' }, { field: 'bm_ku', title: '库' },
                   {
                       field: 'kc_rknum', title: '入库数量', width: 40, align: "center"
                   },
                                 {
                                     field: 'kc_cknum', title: '出库数量', width: 40, align: "center"
                                 },
                                {
                                    field: 'kc_kcnum', title: '库存数量', width: 40, align: "center",
                                     styler: function(value, rowData, rowIndex) {
                                           if (rowData.kc_kcnum == "0") {
                                               return 'background-color:#98FB98;color:red;';
                                            }
                                     }
                                },
                                {
                                    field: 'bm_number', title: '需入库总数', width: 40, align: "center"
                                }
            ]]
            });
        });

        $(function() {
            $("#btnSearch").click(function() {
                var tsaid = $("#txtTsaid").val();
//                var proj = $("#txtProj").val();
                var map = $("#txtTuhao").val();
                var name = $("#txtName").val();
                $("#tgKucun").treegrid('load', {
                    method: 'GetKuCun',
                    tsaid: tsaid,
//                    proj: proj,
                    map: map,
                    name: name
                });
            });
        });
        
        
        function aa(){
        var a=$("#txtTsaid").val();
        var b=$("#txtTuhao").val();
        var c=$("#txtName").val();
        if(a!=""){
        $("#<%=hidRWH.ClientID %>").val(a);
        }
        if(b!=""){
        $("#<%=hidTH.ClientID %>").val(b);
        }
        if(c!=""){
        $("#<%=hidMC.ClientID %>").val(c);
        }
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            任务号：
                        </td>
                        <td>
                            <input type="text" id="txtTsaid" onblur="aa()" />
                        </td>
                        <%--          <td>
                            项目：
                        </td>
                        <td>
                            <input type="text" id="txtProj" />
                        </td>--%>
                        <td>
                            图号：
                        </td>
                        <td>
                            <input type="text" id="txtTuhao" onblur="aa()" />
                        </td>
                        <td>
                            名称：
                        </td>
                        <td>
                            <input type="text" id="txtName" onblur="aa()" />
                        </td>
                        <td>
                            <input type="button" value="查询" id="btnSearch" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                         <td align="right">
                            <input type="hidden" runat="server" id="hidRWH" />
                            <input type="hidden" runat="server" id="hidTH" />
                            <input type="hidden" runat="server" id="hidMC" />
                            <asp:Button ID="btndaochu" runat="server" Text="导出" OnClick="btndaochu_click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <table id="tgKucun" style="height: 400px">
        </table>
    </div>
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <strong>库存数量为绿色红字--该任务已完成</strong>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
