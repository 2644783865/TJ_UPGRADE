<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_LIST.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Ʒ����ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            $("#tgKucun").treegrid({
                url: 'PM_AjaxFinished.aspx',
                collapsible: true,
                loadMsg: '���ݼ��������Ժ󡭡�',
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
                             field: 'bm_zongxu', title: '����', width: 40, align: "center"
                         },
                            {
                                field: 'bm_engid', title: '�����', width: 40, align: "center"
                            },
                               {
                                   field: 'cm_proj', title: '��Ŀ', width: 40, align: "center"
                               },

                {
                    field: 'bm_tuhao', title: 'ͼ��', width: 40, align: "center"
                },

                {
                    field: 'bm_chaname', title: '����', width: 50
                }, { field: 'bm_tuunitwght', title: '����' }, { field: 'bm_singnumber', title: '��̨����' }, { field: 'bm_ku', title: '��' },
                   {
                       field: 'kc_rknum', title: '�������', width: 40, align: "center"
                   },
                                 {
                                     field: 'kc_cknum', title: '��������', width: 40, align: "center"
                                 },
                                {
                                    field: 'kc_kcnum', title: '�������', width: 40, align: "center",
                                     styler: function(value, rowData, rowIndex) {
                                           if (rowData.kc_kcnum == "0") {
                                               return 'background-color:#98FB98;color:red;';
                                            }
                                     }
                                },
                                {
                                    field: 'bm_number', title: '���������', width: 40, align: "center"
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
                            ����ţ�
                        </td>
                        <td>
                            <input type="text" id="txtTsaid" onblur="aa()" />
                        </td>
                        <%--          <td>
                            ��Ŀ��
                        </td>
                        <td>
                            <input type="text" id="txtProj" />
                        </td>--%>
                        <td>
                            ͼ�ţ�
                        </td>
                        <td>
                            <input type="text" id="txtTuhao" onblur="aa()" />
                        </td>
                        <td>
                            ���ƣ�
                        </td>
                        <td>
                            <input type="text" id="txtName" onblur="aa()" />
                        </td>
                        <td>
                            <input type="button" value="��ѯ" id="btnSearch" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                         <td align="right">
                            <input type="hidden" runat="server" id="hidRWH" />
                            <input type="hidden" runat="server" id="hidTH" />
                            <input type="hidden" runat="server" id="hidMC" />
                            <asp:Button ID="btndaochu" runat="server" Text="����" OnClick="btndaochu_click" />
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
                    <strong>�������Ϊ��ɫ����--�����������</strong>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
