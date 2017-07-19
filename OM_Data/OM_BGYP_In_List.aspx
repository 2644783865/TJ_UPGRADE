<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_In_List.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_In_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品入库
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#tgIn").datagrid({
                url: 'OM_BGYPHandler.aspx',
                loadMsg: '数据加载中请稍后……',
                queryParams: {
                    method: 'GetIn'
                },
                idField: "id",

                fitColumns: true,
                resizeHandle: "both",
                singleSelect: true,
                pagination: true,
                pageNumber: 1,
                pageSize: 20,
                pageList: [20, 30, 50],
                onLoadSuccess: function(data) {
                    if (data.rows.length > 0) {
                        //调用mergeCellsByField()合并单元格
                        mergeCellsByField("tgIn", "rkcode");
                    }
                },

                columns: [[
                        {
                            field: 'ck', checkbox: true, width: 30
                        },
                        {
                            field: 'rkcode', title: '入库单号', width: 40, align: "center"
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
                      field: 'num', title: '数量', width: 40, align: "center"
                  },
                   {
                       field: 'unit', title: '单位', width: 30, align: "center"
                   },

                {
                    field: 'uprice', title: '单价', width: 40, align: "center"
                },
                    {
                        field: 'price', title: '总价', width: 40, align: "center"
                    }
                    ,
                    {
                        field: 'makernm', title: '操作人', width: 40, align: "center"
                    }
                    ,
                    {
                        field: 'maketime', title: '入库时间', width: 40, align: "center"
                    },
                       {
                           field: 'note', title: '备注', width: 80, align: "center"
                       }
            ]]
            });
        });

        $(function() {
            //查询
            $("#btnSearch").click(function() {
                $('#tgIn').datagrid('load', {
                    maid: $("#maid").val(),
                    name: $("#name").val(),
                    canshu: $("#canshu").val(),
                    code: $("#rkcode").val(),
                    method: 'GetIn'
                });


            });

            //删除
            $("#btndelete").click(function() {
                var row = $('#tgIn').datagrid("getSelected");
                $.messager.confirm('确认对话框', '将会删除整张入库单,是否确定删除已选择项？', function(r) {
                    if (r) {
                        var param = row.rkcode;
                        $.ajax({

                            url: "OM_BGYPHandler.aspx",
                            data: "method=deleterkd&param=" + param + "",
                            dataType: "json",
                            success: function(r) {
                                if (r.result == "Y") {
                                    alert(r.msg);
                                    $('#tgIn').datagrid('reload');
                                }
                            },
                            error: function(err) {
                                $.messager.show({ title: '提示!', msg: '操作出现错误!' });
                            }
                        });
                    }
                    else {
                        $('#tgIn').datagrid('unselectAll');
                    }
                });
            });



            //            //新增入库

            //            $("#btnNew").click(function() {
            //                var date = new Date();

            //                var time = date.getTime();

            //                window.open("OM_BGYP_IN_Detail.aspx?FLAG=IN&id=" + time, "pop", "");

            //            });

            //查看
            $("#btnLook").click(function() {
                var id = $('#tgIn').datagrid("getSelected").rkcode;
                if (id != null) {
                    window.open("OM_BGYP_IN_Detail.aspx?FLAG=LOOK&Id=" + id, "pop", "");
                }

            });

            //修改
            $("#btnChange").click(function() {
                var id = $('#tgIn').datagrid("getSelected").rkcode;
                if (id != null) {
                    window.open("OM_BGYP_IN_Detail.aspx?FLAG=Change&Id=" + id, "pop", "");
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
                            入库单号<input type="text" id="rkcode" style="width: 100px" />
                        </td>
                        <td>
                            编码<input type="text" id="maid" style="width: 100px" />
                        </td>
                        <td>
                            名称<input type="text" id="name" style="width: 100px" />
                        </td>
                        <td>
                            规格及型号<input type="text" id="canshu" style="width: 100px" />
                        </td>
                        <td>
                            <input type="button" value="查询" id="btnSearch" />
                        </td>
                        <%--    <td>
                            <input type="button" value="新增入库" id="btnNew" />
                        </td>--%>
                        <td>
                            <input type="button" value="查看" id="btnLook" />
                        </td>
                        <td>
                            <input type="button" value="修改" id="btnChange" style="display: none" />
                        </td>
                        <td>
                            <input type="button" value="删除" id="btndelete" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btn_export" Text="导出" OnClick="btn_export_Click" />
                        </td>
                        <td>
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_BGYP_IN_Detail.aspx?FLAG=IN"
                                runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    ImageAlign="AbsMiddle" runat="server" />
                                新增入库
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <table id="tgIn" style="height: 400px">
        </table>
    </div>
</asp:Content>
