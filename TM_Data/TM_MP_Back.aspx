<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_MP_Back.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Back" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    需用计划驳回
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="genRblstatus" RepeatColumns="6" runat="server" OnSelectedIndexChanged="Search_Click"
                                AutoPostBack="true">
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblMyTask" RepeatColumns="6" runat="server" OnSelectedIndexChanged="Search_Click"
                                AutoPostBack="true">
                                <asp:ListItem Text="我的任务" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            分类查询:
                            <asp:DropDownList ID="ddlSearch" runat="server" Height="20px">
                                <asp:ListItem Text="计划跟踪号" Value="Aptcode"></asp:ListItem>
                                <asp:ListItem Text="物料名称" Value="MNAME"></asp:ListItem>
                                <asp:ListItem Text="物料编码" Value="marid"></asp:ListItem>
                                <asp:ListItem Text="申请人" Value="sqrnm"></asp:ListItem>
                                <asp:ListItem Text="驳回人" Value="backnm"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtSearch" Width="100px" Height="15px"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="查 询" OnClick="Search_Click" CommandName="GeneralCard" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="height: 100%; overflow: scroll">
        <asp:GridView ID="GridView2" Width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" OnRowDataBound="GridView2_RowDataBound"
            Style="white-space: normal">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                        <asp:HiddenField ID="HIDXUHAO" Value='<%#Eval("Id")%>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <%--    <asp:BoundField DataField="Aptcode" HeaderText="计划跟踪号">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="计划跟踪号">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtPTC" Text='<%#Eval("Aptcode") %>' Width="150px"
                            ToolTip='<%#Eval("Aptcode") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="marid" HeaderText="物料编码">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="MNAME" HeaderText="物料名称" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GUIGE" HeaderText="规格">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="num" HeaderText="主数量" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PURCUNIT" HeaderText="主单位" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="fznum" HeaderText="辅助数量" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="marfzunit" HeaderText="辅助单位" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sqrnm" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sqrtime" HeaderText="申请时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="backnm" HeaderText="驳回人" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <%--    <asp:BoundField DataField="note" HeaderText="驳回理由" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="驳回理由">
                    <ItemTemplate>
                        <div style="width: 150px">
                            <label>
                                <%#Eval("note")%></label>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="backtime" HeaderText="驳回时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="status" runat="server" Text='<%# Eval("state").ToString()=="0"?"未处理":Eval("state").ToString()=="1"?"已处理":"未知" %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操  作">
                    <ItemTemplate>
                        <asp:LinkButton ID="hlDelete1" CssClass="link" runat="server" CausesValidation="False"
                            OnClick="hlDeal_OnClick" CommandArgument='<%#Eval("Id")%>'>
                            <asp:Image ID="Image6" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                runat="server" />处理
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:Panel ID="NoDataPanelGen" runat="server" HorizontalAlign="Center" Font-Size="Large">
            没有记录!</asp:Panel>
        <uc1:UCPaging ID="ucPaging1" runat="server" />
    </div>
</asp:Content>
<%--
Id, Aptcode, marid, length, pjid, engid, width, engnm, depid, depnm, sqrid, sqrnm, sqrtime, pur_shape, PUR_TUHAO, planno, num, fznum, marfzunit, backtime, backid, backnm, Expr1, MNAME, FULLNAME, GUIGE, MWEIGHT, MAREA, GB, CAIZHI, TECHUNIT, PURCUNIT, state, note--%>