<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Packing_List_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Packing_List_View" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    装箱单   
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="80%">
            <tr>
                <td style="width:16%"><b>装箱单查看</b></td>
                 <td align="right">分类查询:</td>
                <td>
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="6" runat="server" 
                        AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged">                
                    <asp:ListItem Text="未提交" value="0,1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="待审核" Value="2" ></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="4,6" ></asp:ListItem>
                    <asp:ListItem Text="驳回" Value="3,5,7" ></asp:ListItem>
                    <asp:ListItem Text="审核通过" Value="8" ></asp:ListItem>
                    <asp:ListItem Text="已下推" Value="9" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
           </tr>
        </table>
    </div>
    </div>
    </div>

 <div class="box-wrapper">
        <div class="box-outer">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PLT_PACKLISTNO" HeaderText="装箱单号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PLT_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PLT_ENGNAME" HeaderText="工程名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PLT_SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PLT_ADATE" HeaderText="批准日期" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lab_state" runat="server" Text='<%# Eval("PLT_STATE").ToString()=="0"||Eval("PLT_STATE").ToString()=="1"?"未提交":Eval("PLT_STATE").ToString()=="2"?"待审核":Eval("PLT_STATE").ToString()=="4"||Eval("PLT_STATE").ToString()=="6"?"审核中...":Eval("PLT_STATE").ToString()=="8"?"通过":Eval("PLT_STATE").ToString()=="9"?"已下推":"驳回" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDetail" CssClass="link" 
                         NavigateUrl='<%# "TM_Packing_List.aspx?id="+Eval("PLT_ID") %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />查看          
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="装箱单变更" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlPlan" CssClass="link" 
                         NavigateUrl='<%# "TM_Packing_List_Change.aspx?pkchange="+Eval("PLT_ID") %>' runat="server" Enabled="false">
                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />变更                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" CssClass="link" 
                         NavigateUrl='<%# "TM_Packing_List.aspx?pkedit="+Eval("PLT_ID") %>' runat="server" Enabled="false">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />编辑                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            <%--</table>--%>
        </div>
    </div>
</asp:Content>
