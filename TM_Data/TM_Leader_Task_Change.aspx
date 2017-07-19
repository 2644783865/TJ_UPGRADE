<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Leader_Task_Change.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Leader_Task_Change" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    变更任务审核信息列表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
 <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>
  
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width:85%">
                    <tr>
                    <td style="width:10%">我的任务</td>
                    <td align="right">任务类型:</td>
                    <td>
                    <asp:RadioButtonList ID="rbltask" RepeatColumns="3" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rbltask_SelectedIndexChanged">                
                        <asp:ListItem Text="材料计划" value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="制作明细" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="技术外协" Value="2" ></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td align="right">审核状态:</td>
                    <td>
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="4" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged">                
                        <asp:ListItem Text="待审核" value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="审核中" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="审核通过" Value="2" ></asp:ListItem>
                         <asp:ListItem Text="驳回" Value="3" ></asp:ListItem>
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
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Width="25px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="210px">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Width="100%" Text='<%# Eval("CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                <asp:BoundField DataField="ENGNAME" HeaderText="工程名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"/>
                <asp:BoundField DataField="SUBMITNM" HeaderText="技术员" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"/>
                <asp:BoundField DataField="REVIEWANAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                 <%--<asp:TemplateField HeaderText="技术员" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbltec" runat="server" Text='<%# Eval("TSA_TCCLERKNM") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="REVIEWAADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="42px" Visible="false"/>
                <asp:BoundField DataField="REVIEWBNAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="REVIEWBADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="42px" Visible="false"/>
                <asp:BoundField DataField="REVIEWCNAME" HeaderText="审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="REVIEWCADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="42px" Visible="false"/>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Width="48px">
                    <ItemTemplate>
                        <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="9"?"已下推":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"?"待审中":Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核中...":"驳回" %>'></asp:Label>
                        <%--<asp:Image ID="Image1" ImageUrl='<%#"~/Assets/Images/"+(Convert.ToInt32(Eval("STATE").ToString())==int.Parse("6")?"yes.gif":"no.gif")%>' 
                         border="0" hspace="2" align="absmiddle" runat="server" />--%>                               
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="56px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlMPTask" CssClass="link"
                         NavigateUrl='<%#"TM_MP_Require_Change_Audit.aspx?mp_change_id="+Eval("CODE")%>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="state1" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="56px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlMSTask" CssClass="link" 
                         NavigateUrl='<%#"TM_MS_Detail_Change_Audit.aspx?ms_change_id="+Eval("CODE")%>' runat="server">
                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="state2" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="56px">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlOSTTask" CssClass="link" 
                         NavigateUrl='<%#"TM_Out_Source_Change_Audit.aspx?ost_change_id="+Eval("CODE")%>' runat="server">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="state3" runat="server" Text='<%# Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"||Eval("STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>                                
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有任务!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
