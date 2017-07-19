<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PD_DocManage.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.PD_DocManage" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    评审合同管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td align="right" style="width: 60px">
                            审核状态:
                        </td>
                        <td style="width: 200px">
                            <asp:RadioButtonList ID="rbl_mytask" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="我的任务" Selected="True" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center">
                            <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_status_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="待审核" Selected="True" Value="0"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="N"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="text-align: right" width="200px">
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/CM_Data/PD_DocTypeIn.aspx?action=add"
                                runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                新建评审内容
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td align="left" style="width: 80px">
                            项目名称：
                        </td>
                        <td style="width: 15%">
                            <asp:DropDownList ID="ddlpjname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpjname_SelectedIndexChanged">
                                <asp:ListItem Text="请选择"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 80px">
                            设备名称：
                        </td>
                        <td style="width: 15%">
                            <asp:DropDownList ID="ddlengname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlengname_SelectedIndexChanged">
                                <asp:ListItem Text="请选择"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 10%">
                            业主：
                        </td>
                        <td style="width: 15%">
                            <asp:DropDownList ID="ddlyezhu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlyezhu_SelectedIndexChanged">
                                <asp:ListItem Text="请选择"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 10%">
                            制单人：
                        </td>
                        <td style="width: 15%">
                            <asp:DropDownList ID="ddlzhidanren" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlzhidanren_SelectedIndexChanged">
                                <asp:ListItem Text="请选择"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" OnDataBound="GridView1_DataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="主键" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("BC_ID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PROJECT" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ENGINEER" HeaderText="设备名称" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YEZHU" HeaderText="业主" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BC_NUMBER" HeaderText="次数" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JINE" HeaderText="金额" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BP_ACPDATE" HeaderText="日期" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="制单人" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lb_zdr" runat="server" Text='<%# Eval("BC_DRAFTER")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核状态" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lb_status" runat="server" Text='<%# Eval("BP_SPSTATUS")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否通过">
                                <ItemTemplate>
                                    <asp:Label ID="lb_yesorno" ForeColor='<%#Eval("BP_YESORNO").ToString()=="N"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'
                                        runat="server" Text='<%# Eval("BP_YESORNO")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask_xg" CssClass="link" NavigateUrl='<%#"PD_DocTypeIn.aspx?action=update&id="+Eval("BC_ID")%>'
                                        runat="server">
                                        <asp:Image ID="InfoImage_xg" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask_look" CssClass="link" NavigateUrl='<%#"PD_DocpinshenInfo.aspx?action=look&id="+Eval("BC_ID") %>'
                                        runat="server">
                                        <asp:Image ID="InfoImage_look" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        查看
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="评审" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask_ps" CssClass="link" NavigateUrl='<%#"PD_DocpinshenInfo.aspx?action=ps&id="+Eval("BC_ID") %>'
                                        runat="server">
                                        <asp:Image ID="InfoImage_ps" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        评审
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <div style="text-align: left; padding-top: 5px; padding-right: 15px">
                        <asp:Button ID="btnDelete" runat="server" Text="删除" />
                        <asp:Button ID="btnAgain" runat="server" Text="再投标" OnClick="btnAgain_Click" /></div>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rbl_mytask" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="rbl_status" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
