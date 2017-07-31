<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_Store.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Bgyp_Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品库存查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#tgstore").datagrid({
                url: 'OM_BGYPHandler.aspx',
                loadMsg: '数据加载中请稍后……',
                queryParams: {
                    method: 'GetStore'
                },
                idField: "id",
                fitColumns: true,
                resizeHandle: "both",
                pagination: true,
                pageNumber: 1,
                pageSize: 20,
                pageList: [20, 30, 50],
                columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        },
                         {
                             field: 'maid', title: '编码', width: 40, align: "center"
                         },
                            {
                                field: 'name', title: '名称', width: 40, align: "center"
                            },
                               {
                                   field: 'canshu', title: '规格及型号', width: 40, align: "center"
                               },
                               {
                                   field: 'pname', title: '类别', width: 40, align: "center"
                               },
                  {
                      field: 'num', title: '数量', width: 40, align: "center"
                  },
                   {
                       field: 'unit', title: '单位', width: 30, align: "center"
                   },

                {
                    field: 'unprice', title: '单价', width: 40, align: "center"
                },
                    {
                        field: 'price', title: '总价', width: 40, align: "center"
                    }
            ]]
            });
        });

        $(function() {
            $("#btnSearch").click(function() {
                $('#tgstore').datagrid('load', {
                    maid: $("#maid").val(),
                    name: $("#name").val(),
                    canshu: $("#canshu").val(),
                    pid: $("#pid").combobox('getValue'),
                    method: 'GetStore'
                });


            });
        });

    </script>

    <script type="text/javascript">
        $(function() {
            $("#<%=btnApply.ClientID %>").click(function() {
                var arrTh = $("#tgstore").datagrid("getSelections");
                if (arrTh.length == 0) {
                    alert("请选择至少一条数据");
                }
                else {
                    var idTh = '';
                    for (var i = 0; i < arrTh.length; i++) {
                        idTh += arrTh[i].mid + "|";
                    }
                    window.location = "OM_BGYP_Apply_Detail.aspx?flag=new&idTh=" + idTh;
                }
            });
        });
    
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
               <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table>
                    <tr>
                        <td>
                            编码<input type="text" id="maid" name="maid"/>
                        </td>
                        <td>
                            名称<input type="text" id="name"/>
                        </td>
                        <td>
                            规格及型号<input type="text" id="canshu"/>
                        </td>
                        <td>
                            类别
                            <input id="pid" class="easyui-combobox" name="pid" data-options="valueField:'id',textField:'name',url:'../Basic_Data/BD_AjaxHandler.aspx?method=GetAllPth',panelHeight:'auto'" />
                        </td>
                        <td>
                            <input type="button" value="查询" id="btnSearch" />
                            <input type="button" runat="server" visible="false" value="申请领用" id="btnApply"/>
                            <asp:Button id="btnExport"  Text="导出" onclick="btnExport_Click" runat="server"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <table id="tgstore" style="height: 400px">
        </table>
    </div>
</asp:Content>
