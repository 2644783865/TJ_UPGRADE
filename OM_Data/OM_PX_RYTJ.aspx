<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_PX_RYTJ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PX_RYTJ" Title="添加人员" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        

    
     //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txtNAME.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }

            $('#win').dialog('close');
        }
    </script>

    <div align="center" style="width: 100%; height: 100%">
        <table width="60%">
            <tr style="height: 80px">
                <td align="center" style="width: 25%">
                    <strong style="font-size: medium">姓名</strong>
                    <asp:TextBox runat="server" ID="txtNAME" onfocus="this.blur()" BackColor="#98FB98"
                        Font-Size="Medium" Height="30px"></asp:TextBox>

                    <%--                    <asp:TextBox ID="txt_first" runat="server" Width="60px" Enabled="false"></asp:TextBox>--%>
                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                    <asp:HyperLink ID="hlSelect1" Visible="true" runat="server" CssClass="hand" onClick="SelPersons1()">
                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                            align="absmiddle" runat="server" />
                        选择
                    </asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnadd" OnClick="btnadd_click" Text="添加" />
                </td>
            </tr>
        </table>
    </div>
    <div>
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
                            <input id="dep" name="dept" />
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
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
        </div>
    </div>
    <input type="hidden" id="hidPerson" value="" />
</asp:Content>
