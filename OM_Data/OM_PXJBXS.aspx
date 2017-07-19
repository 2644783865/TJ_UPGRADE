<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_PXJBXS.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXJBXS" Title="基本学时" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
    function quanxuan(obj){
     if(obj.checked){
       $("#tab [id*='cbxNumber']").attr("checked",true);
     }
     else{
        $("#tab [id*='cbxNumber']").attr("checked",false);
     }
    }
     function check_num(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,6})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:CheckBox runat="server" Text="全选" ID="quanxuan" onclick="quanxuan(this)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <strong>部门：</strong>
                            <asp:DropDownList ID="ddldeparment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldeparment_selectchanged">
                            </asp:DropDownList>
                            <strong>岗位：</strong>
                            <asp:DropDownList ID="ddlgangwei" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgangwei_selectchanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp; <strong>按姓名查询：</strong>
                            <asp:TextBox ID="txtName" runat="server" Width="80px"></asp:TextBox>
                            <asp:Button ID="cxname" runat="server" OnClick="cxname_click" Text="查询" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--                        </td>--%>
                            <%--                        <td align="center">
                            <asp:Button runat="server" ID="btnQuery" OnClick="jbxs_gx" Text="更新基本学时" Width="100px"
                                BackColor="LightGreen" />
                        </td>--%>
                            <%--                       <td id="Td1" runat="server">--%>
                            <asp:HyperLink ID="hlContract3" ToolTip="添加人员" NavigateUrl="OM_PX_RYTJ.aspx" CssClass="link"
                                runat="server">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" runat="server" />添加人员
                            </asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btndaochu" OnClick="btndaochu_click" Text="导出" Width="30px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hylAddContractView" CssClass="hand" runat="server">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />&nbsp;&nbsp;更新基本学时</asp:HyperLink>&nbsp;&nbsp;
                            <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="hylAddContractView"
                                PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-65">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="palPSHTLB" Width="270px" Style="visibility: hidden; border-style: solid;
                                border-width: 1px; border-color: blue; background-color: White;" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                            font-size: 11px; font-weight: bold; position: absolute; top: 5px; right: 10px;">
                                            <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                title="关闭">X</a>
                                        </div>
                                        <br />
                                        <br />
                                        <table style="width: 100%; background-color: White;">
                                            <tr>
                                                <td align="left">
                                                    学时（小时）:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJBXS_GX" runat="server" onblur="javascript:check_num(this)"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="jbxs_gx" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td align="center">
                                        序号
                                    </td>
                                    </td>
                                    <td align="center">
                                        姓名
                                    </td>
                                    <td align="center">
                                        部门
                                    </td>
                                    <td align="center">
                                        岗位
                                    </td>
                                    <td align="center">
                                        基本学时
                                    </td>
                                    <td align="center">
                                        实际学时
                                    </td>
                                    <td align="center">
                                        完成率
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                    ondblclick="javascript:changeback(this);">
                                    <td>
                                        <asp:CheckBox ID="cbxNumber" runat="server" Text='<%#Container.ItemIndex+1%>' />
                                        <asp:HiddenField runat="server" ID="XS_ID" Value='<%#Eval("XS_ID")%>' />
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbxm" runat="server" Text='<%#Eval("XS_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbdepartment" runat="server" Text='<%#Eval("XS_BM")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbgangwei" runat="server" Text='<%#Eval("ST_SEQUEN")%>'></asp:Label>
                                    </td>
                                    <td id="tdtzts" runat="server">
                                        <asp:Label ID="lbJBXS" runat="server" Text='<%#Eval("XS_JBXS") %>'></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbsjxs" runat="server" Text='<%#Eval("SJXS")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbwcl" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
