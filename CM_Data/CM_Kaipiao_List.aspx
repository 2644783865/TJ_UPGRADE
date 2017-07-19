<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Kaipiao_List.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Kaipiao_List" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    开票管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#<%=GridView1.ClientID%> tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        })
    </script>

    <asp:HiddenField runat="server" ID="UserID" />
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div style="height: 80px">
                <table width="100%">
                    <tr>
                        <td align="right">
                            审核状态:
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblState" RepeatColumns="9" AutoPostBack="true"
                                OnSelectedIndexChanged="rblState_IndexChanged">
                                <asp:ListItem Text="全部" Value="8"></asp:ListItem>
                                <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                                <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="仅串审通过" Value="3"></asp:ListItem>
                                <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="5"></asp:ListItem>
                                <asp:ListItem Text="已开票" Value="6"></asp:ListItem>
                                <asp:ListItem Text="待开票" Value="7"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td valign="middle">
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="CM_Kaipiao_Contract.aspx" runat="server"
                                Target="_blank">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                按合同号汇总</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="CM_Kaipiao_Task.aspx" runat="server"
                                Target="_blank">
                                <asp:Image ID="Image3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                按任务号汇总</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="HyperLink" NavigateUrl="CM_Kaipiao_Detail.aspx?action=add" runat="server"
                                Target="_blank">
                                <asp:Image ID="ImageTo" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                新增开票申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            &nbsp;&nbsp;编号：<asp:TextBox runat="server" ID="txtBH"></asp:TextBox>
                        </td>
                        <td>
                            合同号：
                            <asp:TextBox runat="server" ID="txtContract"></asp:TextBox>
                        </td>
                        <td>
                            开票日期: 从：
                            <asp:TextBox runat="server" ID="txtStart" class="easyui-datebox"></asp:TextBox>
                        </td>
                        <td>
                            到：
                            <asp:TextBox runat="server" ID="txtEnd" class="easyui-datebox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="rblState_IndexChanged" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%;">
            <div style="width: 100%; overflow: scroll">
                <asp:GridView ID="GridView1" Width="1600px" CssClass="toptable grid" Style="white-space: normal"
                    runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                    ShowFooter="true" OnRowDataBound="GridView1_OnRowDataBound">
                    <FooterStyle BackColor="#EFF3FB" Font-Bold="True" ForeColor="#FF0000" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="id" runat="server" Value='<%#Eval("Id") %>' />
                                <asp:HiddenField ID="TaskId" runat="server" Value='<%#Eval("KP_TaskID") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="21px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="KP_CODE" HeaderText="编号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_CONID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_COM" HeaderText="单位名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_SHEBEI" HeaderText="设备名称（单位、型号）" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_BENCIJE" HeaderText="开票金额" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_KPNUMBER" HeaderText="发票号" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_KPDATE" HeaderText="开票时间" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="是否提前开票">
                            <ItemTemplate>
                                <asp:Label ID="lb_Tiqian" runat="server" Text='<%# Eval("KP_TIQIANKP").ToString()=="1"?"是":"否"%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="KP_CONZONGJIA" HeaderText="合同总价" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_ZDRNM" HeaderText="制单人" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Wrap="false">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="KP_ZDTIME" HeaderText="制单时间" HeaderStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="审核状态">
                            <ItemTemplate>
                                <asp:Label ID="lb_spstatus" runat="server" Text='<%# Eval("KP_SPSTATE").ToString()=="0"?"未提交":Eval("KP_SPSTATE").ToString()=="3"?"审核通过":Eval("KP_SPSTATE").ToString()=="4"?"驳回":"审核中"%>'></asp:Label>
                                <%--   0:未提交，1：提交，2：一级通过，3：二级通过，4：驳回--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="会审状态">
                            <ItemTemplate>
                                <asp:Label ID="lb_hsstatus" runat="server" Text='<%# Eval("KP_HSSTATE").ToString()=="0"?"未提交":Eval("KP_HSSTATE").ToString()=="1"?"审核中":Eval("KP_HSSTATE").ToString()=="2"?"已通过":"驳回"%>'></asp:Label>
                                <%--   0:未提交，1：提交，2：一级通过，3：二级通过，4：驳回--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTask_look" CssClass="link" NavigateUrl='<%# "CM_Kaipiao_Detail.aspx?action=view&id="+Eval("Id") %>'
                                    runat="server" Target="_blank">
                                    <asp:Image ID="InfoImage_look" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />
                                    查看
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除和修改" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkEdit" NavigateUrl='<%# "CM_Kaipiao_Detail.aspx?action=edit&id="+Eval("Id") %>'
                                    runat="server">
                                    <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />修改</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("KP_TaskID")%>'
                                    OnClick="lnkDelete_OnClick" OnClientClick="return confirm('确认删除吗?')">
                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />删除</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Height="21px" Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审批" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTask_audit" CssClass="link" NavigateUrl='<%# "CM_Kaipiao_Detail.aspx?action=audit&id="+Eval("Id") %>'
                                    runat="server">
                                    <asp:Image ID="Image15" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />
                                    审批
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开票" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTask_ticket" CssClass="link" NavigateUrl='<%# "CM_Kaipiao_Detail.aspx?action=ticket&id="+Eval("Id") %>'
                                    runat="server">
                                    <asp:Image ID="Image16" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />
                                    审批
                                </asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="重置" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReset" runat="server" CommandArgument='<%# Eval("KP_TaskID")%>'
                                    OnClick="lnkReset_OnClick" OnClientClick="return confirm('确认重置吗?')">
                                    <asp:Image ID="Image13" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />重置</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
            </div>
        </div>
        <div>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
