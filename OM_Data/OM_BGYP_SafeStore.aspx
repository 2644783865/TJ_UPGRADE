<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_SafeStore.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_SafeStore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品安全库存
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
        $(function() {
            $("#Checkbox2").click(function() {
                if ($("#Checkbox2").attr("checked")) {
                    $("#tab input[type=checkbox]").attr("checked", "true");
                }
                else {
                    $("#tab input[type=checkbox]").removeAttr("checked");
                }
            });
        })//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                    </tr>
                    <tr>
                        <td>
                            全选/取消<input id="Checkbox2" type="checkbox" />
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="btnAddSafe" CssClass="hand" runat="server" OnClick="btnAddSafe_Click">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />安全库存采购申请</asp:LinkButton>
                            <%--  <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server" NavigateUrl="~/OM_Data/OM_BgypPcApply.aspx?action=safePC">
                               </asp:HyperLink>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    编码
                                </td>
                                <td>
                                    名称
                                </td>
                                <td>
                                    规格及型号
                                </td>
                                <td>
                                    库存数量
                                </td>
                                <td>
                                    安全库存数量
                                </td>
                                <td>
                                    单位
                                </td>
                                <td>
                                    单价
                                </td>
                                <td>
                                    库存单价
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td id="td1" runat="server">
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td>
                                 
                                     <asp:Label ID="lblMaId" Text=' <%#Eval("maId") %>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("name") %>
                                </td>
                                <td>
                                    <%#Eval("canshu") %>
                                </td>
                                <td>
                                    <asp:Label ID="lblNum" Text=' <%#Eval("num") %>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblKC" Text=' <%#Eval("kc") %>' runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("unit") %>
                                </td>
                                <td>
                                    <%#Eval("price") %>
                                </td>
                                <td>
                                    <%#Eval("unPrice") %>
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
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
