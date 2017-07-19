<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="EQU_Need_List.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Need_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        采购申请单管理
                                    </td>
                                   <%-- <td>
                                        <asp:RadioButtonList ID="rbl_xiatui" runat="server" RepeatColumns="2" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search_click">
                                            <asp:ListItem Text="未下推" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="已下推" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>--%>
                                    <td width="40%">
                                        <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="按部门查看："></asp:Label>
                                        <asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="QueryButton_Click">
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:HyperLink ID="addeqpurbill" CssClass="hand" runat="server"><asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/pcadd.gif" />
                                        新增采购申请</asp:HyperLink>&nbsp;&nbsp;
                                       <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"  TargetControlID="addeqpurbill" PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-65"  CacheDynamicResults="false">
                                       </asp:PopupControlExtender>
                                        </td>
                                </tr>
                            </table>
                            <asp:Panel ID="palPSHTLB" style="visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>  
                             <table width="170px">
                             <tr>       
                             <td>
                                  <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">
                                      X</a>
                                  </div>
                                  <br />
                                  <br />
                             </td>
                             </tr>
                             <tr>
                             <td align="left">申请类别:</td>
                                  <td>
                                 <asp:DropDownList ID="dplPSHTLB_Select" runat="server">
                                  <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                  <asp:ListItem Text="设备采购申请" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="备件采购申请" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                </td>
                             </tr>
                             <tr>
                             <td colspan="2" align="center">
                             <br />
                                 <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_OnClick"  UseSubmitBehavior="false"/>
                             </td>
                             </tr>
                             </table>
                          </ContentTemplate>
                          </asp:UpdatePanel>
                         </asp:Panel>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox2 xscroll">
                                            <table id="tab" align="center" class="nowrap cptable fullwidth">
                                                <asp:Repeater ID="EQU_Need_List_Repeater" runat="server" OnItemDataBound="EQU_Need_List_Repeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>单据编号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>设备/备件名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>规格型号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申请时间</strong>
                                                            </td>
                                                            <td>
                                                                <strong>需求时间</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申购理由</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申请人</strong>
                                                            </td>
                                                            <td>
                                                                <strong>使用部门</strong>
                                                            </td>
                                                            <td>
                                                                <strong>制单人</strong>
                                                            </td>
                                                            <%--<td>
                                                                <strong>下推状态</strong>
                                                            </td>--%>
                                                            <td>
                                                                <strong>审批状态</strong>
                                                            </td>
                                                            <td runat="server" id="hedit">
                                                                <strong>修改</strong>
                                                            </td>
                                                            <td runat="server" id="hlookup">
                                                                <strong>查看</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="DocuNum" runat="server" Text='<%#Eval("DocuNum")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="EquName" runat="server" Text='<%#Eval("EquName")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="EquType" runat="server" Text='<%#Eval("EquType")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="EquNum" runat="server" Text='<%#Eval("EquNum")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SQtime" runat="server" Text='<%#Eval("SQtime")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="XQtime" runat="server" Text='<%#Eval("XQtime")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Reason" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="SQRNAME" runat="server" Text='<%#Eval("SQRNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="USEDEPNAME" runat="server" Text='<%#Eval("USEDEPNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="DocuPerson" runat="server" Text='<%#Eval("DocuPerson")%>'></asp:Label>
                                                                <asp:Label ID="DocuPersonID" runat="server" Text='<%#Eval("DocuPersonID")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <%--<td>
                                                                <asp:Label ID="STATE" runat="server" Text='<%#Eval("XiaTui")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="STATETEXT" runat="server" Text='<%#get_pr_state(Eval("XiaTui").ToString())%>'></asp:Label>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="PZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" id="bedit">
                                                                <asp:HyperLink ID="hyp_edit" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="修改"></asp:Label></asp:HyperLink>
                                                            </td>
                                                            <td runat="server" id="blookup">
                                                                <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                                                    <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="16" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
