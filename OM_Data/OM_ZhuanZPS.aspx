<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_ZhuanZPS.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ZhuanZPS" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    我的审批任务<span style="color: Red">-序号以红色标注的为可审批任务</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/StyleControl.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%;">
                <tr>
                    <td style="width: 10%;">
                        <strong>任务审批</strong>
                    </td>
                    <td align="center">
                        <asp:RadioButtonList ID="rblZT" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSP_OnSelectedIndexChanged">
                            <asp:ListItem Text="全 部" Value="a"></asp:ListItem>
                            <asp:ListItem Text="最近审批" Value="0"></asp:ListItem>
                            <asp:ListItem Text="待审批" Value="1" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center" style="width: 40%;">
                        审批类别：&nbsp;<asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSP_OnSelectedIndexChanged">
                            <asp:ListItem Text="-请选择-" Value="%" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="转正审批" Value="0"></asp:ListItem>
                            <asp:ListItem Text="合同审批" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%;">
                    </td>
                </tr>
            </table>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                        margin: 2px" Width="100%">
                        <asp:GridView ID="grvSP" CssClass="toptable grid" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" OnRowDataBound="grv_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hf_id" runat="server" Value='<%#Eval("ST_ID") %>' />
                                        <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="评审项目" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        审批项目：
                                        <asp:Label ID="PS_ITEM" runat="server" Text='<%#Eval("ST_TYPE").ToString()=="0"?"转正审批":Eval("ST_TYPE").ToString()=="1"?"合同审批":Eval("ST_TYPE").ToString()=="2"?"人员流动审批":"年假审批" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="审批人员" DataField="ST_NAME" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="制单人" DataField="NAME" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="评审状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        状态：
                                        <asp:Label ID="ST_PSZT" runat="server" Text='<%#Eval("ST_PSZT").ToString()=="1"?"正在审批":Eval("ST_PSZT").ToString()=="2"?"审批通过":"被驳回" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="制单日期" DataField="ST_ZDSJ" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text="备注..." ToolTip='<%#Eval("ST_REMARK") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_view" runat="server" CommandArgument='<%#Eval("ST_ID") %>'
                                            CommandName='<%#Eval("ST_TYPE").ToString()+"|"+"View" %>' OnClick="lnkAction_OnClick">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />查看</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <div onclick="javascript:return confirm('修改后将清空历史\r审批记录重新开始审批！\r\r继续修改吗？');">
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Edit(Eval("ST_ID")+"|"+Eval("ST_PER")+"|"+Eval("ST_TYPE")) %>'
                                                runat="server" ToolTip='编辑' Width="100px">
                                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />编辑</asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审批" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_review" runat="server" ForeColor="Red" CommandArgument='<%#Eval("ST_ID") %>'
                                            CommandName='<%#Eval("ST_TYPE").ToString()+"|"+"PinS" %>' OnClick="lnkAction_OnClick">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/shenhe.gif" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#EFF3FB" Height="21px" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                            没有记录!</asp:Panel>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
