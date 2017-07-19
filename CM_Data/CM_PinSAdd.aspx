<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_PinSAdd.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_PinSAdd1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    选择评审人员
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Contract_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function savePick() {
            var r = Save();
            $("#<%=txtPSR.ClientID %>").val(r.st_name);
            $("#<%=hdfPSRID.ClientID %>").val(r.st_id);
            $('#win').dialog('close');
        }

        $(function() {
            $('#<%=ddl_dep.ClientID %>').change(function() {
                if ($('#<%=ddl_dep.ClientID %>').val() != "00") {
                    $('#dep').combobox('select', $('#<%=ddl_dep.ClientID %>').val());
                }
            });
        });
    </script>

    <div style="border: 1px solid #000000; height: 300px">
        <div class="RightContent">
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <table width="100%" class="tabGg">
                    <tr>
                        <td class="r_bg">
                            部门名称：
                        </td>
                        <td class="right_bg">
                            <asp:DropDownList ID="ddl_dep" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="r_bg">
                            评审人员：
                        </td>
                        <td class="right_bg">
                            <input id="txtPSR" type="text" runat="server" readonly="readonly" style="width: 120px" /><input
                                id="hdfPSRID" type="hidden" runat="server" style="width: 10px" />
                            <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" onClick="SelPersons()">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                选择
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="right_bg" style="text-align: center">
                            <asp:Button ID="btn_Continue" runat="server" Text="确定并添加" class="buttOver" OnClick="btn_Continue_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_OK" runat="server" Text="确定并返回" class="buttOver" OnClick="btn_Continue_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_Cancel" runat="server" Text="关 闭" class="buttOver" OnClick="btn_Cancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="07">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
    </div>
</asp:Content>
