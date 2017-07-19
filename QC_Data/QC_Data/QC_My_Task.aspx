<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_My_Task.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_My_Task" Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 98%; height: 24px">
                    <tr>
                          <td>
                                  按项目名称:
                                </td>
                                <td>
                                <asp:TextBox ID="xmmc" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="xmmc_AutoCompleteExtender" runat="server" 
                                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                    MinimumPrefixLength="1" ServiceMethod="xmmc" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="xmmc" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    任务号：
                                </td> 
                                <td>
                                <asp:TextBox ID="sczh" runat="server"></asp:TextBox>
                                </td>
                                <td>质检员：<asp:DropDownList ID="drp_zjy" runat="server" AutoPostBack="true" 
                                        onselectedindexchanged="drp_zjy_SelectedIndexChanged"></asp:DropDownList></td>
                                
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="btnSearch_Click" />
                        </td>
                        <td align="right" nowrap="nowrap">
                            状态:                         </td>
                        <td align="left" nowrap="nowrap">
                         
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="2" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="进行中" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="TSA_ID">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="White" Height="21px" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="质量任务分工的自增字段" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lb_zenghao" runat="server" Text='<%# Bind("QSA_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任务号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lb_engid" runat="server" Text='<%# Bind("TSA_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="TSA_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                 
                    <asp:BoundField DataField="QSA_QCDATANM" HeaderText="资料员" Visible="false" ItemStyle-HorizontalAlign="Center">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="质检员" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbl_usernm" runat="server" Text='<%# Bind("QSA_QCCLERKNM") %>'></asp:Label>
                            <asp:Label ID="lbl_userid" runat="server" Text='<%# Bind("QSA_QCCLERK") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QSA_DATE" HeaderText="分工时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:TemplateField HeaderText="质检任务" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hy_zhijian" runat="server" NavigateUrl='<%#"QC_My_Task_List.aspx?action=typein&qsaid="+Eval("QSA_ID")%>'
                                Target="_blank">
                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                质检任务
                                <asp:Label ID="lb_MyTask" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="资料登记" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Panel ID="Panel_yanshou_chk_in" runat="server">
                                <asp:HyperLink ID="hy_yanshou" runat="server" NavigateUrl='<%#"QC_My_Task_Data_TypeIn.aspx?engid="+Eval("TSA_ID")+"&qsaid="+Eval("QSA_ID")%>'>
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    资料登记</asp:HyperLink>
                            </asp:Panel>
                            <asp:Panel ID="Panel_yanshou_view" runat="server" Visible="False">
                                <asp:HyperLink ID="hy_yanshou_view" runat="server" NavigateUrl='<%#"QC_My_Task_Data_View.aspx?id=0&qsaid="+Eval("QSA_ID")%>'>
                                    <asp:Image ID="Image4" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    查看资料</asp:HyperLink>
                            </asp:Panel>
                            <asp:Panel ID="Panel_sub" runat="server" Visible="False">
                                <asp:HyperLink ID="hly_sub" runat="server" NavigateUrl='<%#"QC_My_Task_Data_View.aspx?id=1&qsaid="+Eval("QSA_ID")%>'>
                                    <asp:Image ID="Image6" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    查看资料</asp:HyperLink>
                            </asp:Panel>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看进度" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hy_view_progress" runat="server" NavigateUrl='<%#"QC_My_Task_List.aspx?action=view&qsaid="+Eval("QSA_ID")%>'
                                Target="_blank">
                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                查看进度</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="查看变更">
                        <ItemTemplate>
                            <asp:HyperLink ID="hy_view_biangeng" runat="server" NavigateUrl='<%#"QC_My_Task_List_View_Change.aspx?qsaid="+Eval("QSA_ID")%>'
                                Target="_blank">
                                <asp:Image ID="Image21" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                查看变更</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                没有任务!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
