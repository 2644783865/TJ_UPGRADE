<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="CM_MyContractReviewTask.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_MyContractReviewTask" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    我的审批任务<span style="color: Red">-序号以红色标注的为可审批任务</span>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <%--<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>--%>
    <div class="RightContent">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-inner">
                        <div class="box_right">
                            <div class="box-title">
                                <table width="100%;">
                                    <tr>
                                        <td style="width: 20%;">
                                        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButtonList ID="rblZT" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblZT_OnSelectedIndexChanged">
                                                <asp:ListItem Text="最近审批" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="待审批" Value="1" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left">
                                            <%--审批合同类别：--%>&nbsp;<asp:DropDownList ID="dplHTLB" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblZT_OnSelectedIndexChanged">
                                                <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="销售合同" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="委外合同" Value="1"></asp:ListItem>
                                                <%-- <asp:ListItem Text="厂内分包" Value="2"></asp:ListItem>--%>
                                                <asp:ListItem Text="采购合同" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="办公合同" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="其他合同" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="补充协议" Value="6"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%;">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="box-wrapper">
                        <div class="box-outer">
                            <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                                margin: 2px" Width="100%">
                                <yyc:SmartGridView ID="grvSP" CssClass="toptable grid" runat="server" ShowFooter="true"
                                    Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                    OnRowDataBound="grv_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfcr_id" runat="server" Value='<%#Eval("CR_ID") %>' />
                                                <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="项目名称" DataField="CR_XMMC" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <%--<asp:BoundField HeaderText="工程名称" DataField="PCON_ENGNAME" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>--%>
                                        <asp:BoundField HeaderText="业主名称" DataField="CR_FBSMC" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <%--<asp:BoundField HeaderText="工程分包范围" DataField="CR_FBFW" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>--%>
                                        <asp:BoundField HeaderText="制单人" DataField="CR_ZDRNAME" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <asp:BoundField HeaderText="制单日期" DataField="CR_ZDRQ" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <%--<asp:BoundField HeaderText="金额" DataFormatString="{0:c}" DataField="CR_JIN" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>--%>
                                        <asp:TemplateField HeaderText="合同类别" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("CR_HTLX").ToString()=="0"?"销售合同":Eval("CR_HTLX").ToString()=="1"?"采购合同":Eval("CR_HTLX").ToString()=="2"?"技术外协":Eval("CR_HTLX").ToString()=="3"?"发运合同":Eval("CR_HTLX").ToString()=="4"?"生产外协":Eval("CR_HTLX").ToString()=="5"?"厂内分包":Eval("CR_HTLX").ToString()=="7"?"电气制造":Eval("CR_HTLX").ToString()=="8"?"补充协议":"其他合同" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text="备注..." ToolTip='<%#Eval("CR_NOTE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lbtn_view" runat="server" ForeColor="Red" CommandArgument='<%#Eval("CR_ID") %>'
                                                    OnClick="lnkAction_OnClick" CommandName='<%#Eval("CR_HTLX").ToString()+"|"+"myview" %>'>查看</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lbtn_review" runat="server" ForeColor="Red" CommandArgument='<%#Eval("CR_ID") %>'
                                                    OnClick="lnkAction_OnClick" CommandName='<%#Eval("CR_HTLX").ToString()+"|"+"audit" %>'>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icons/shenhe.gif" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4" />
                                </yyc:SmartGridView>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                            </asp:Panel>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
