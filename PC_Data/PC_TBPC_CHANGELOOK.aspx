<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_TBPC_CHANGELOOK.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_CHANGELOOK"   MasterPageFile="~/Masters/PopupBase.Master"  Title="变更物料查询"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
<script language="javascript" type="text/javascript">
function viewCondition()
{
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    
</script>
<asp:UpdatePanel ID="gaojin"  runat="server">
<ContentTemplate>
<div style="border: 1px solid #000000; overflow: auto; "> 
   <div>
   <table style="border: 0px; border-style: ridge;" align="center" width="100%">
   <tr><td align="center" colspan="2"><span style="color:Red; font-size: large;">变更物料查看</span></td></tr>
   <tr>
     <td align="right">
     <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
     <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath="" Y="80">
     </asp:ModalPopupExtender>
     </td>
   </tr>
   </table>
    <asp:Panel ID="PanelCondition" runat="server" Width="95%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server">
                        <ContentTemplate>
                            <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td  colspan="8" align="center">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                   <td>
                                       计划跟踪号：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_ptcode" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       项目：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_pjnm" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       工程：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_engnm" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       物料编码：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
                                   </td>
                                </tr>
                                <tr>
                                   <td>
                                       名称：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_marnm" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       规格：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_margg" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       材质：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_marcz" runat="server"></asp:TextBox>
                                   </td>
                                   <td>
                                       国标：
                                   </td>
                                   <td>
                                       <asp:TextBox ID="tb_margb" runat="server"></asp:TextBox>
                                   </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="toptable grid" CellPadding="4" ForeColor="#333333"
            DataKeyNames="chptcode" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound"
            OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" EmptyDataText="没有记录" PageSize="10">
            <PagerTemplate>
                <table width="100%" style="border: 0px; border-style: ridge;" align="center">
                    <tr>
                        <td style="border-bottom-style: ridge; width: 100%; text-align: center">
                            <asp:Label ID="lblCurrrentPage" runat="server" ForeColor="#CC3300"></asp:Label>
                            <span>跳转至</span>
                            <asp:DropDownList ID="page_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="page_DropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span>页</span>
                            <asp:LinkButton ID="lnkBtnFirst" CommandArgument="First" CommandName="page" runat="server">第一页</asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnPrev" CommandArgument="prev" CommandName="page" runat="server">上一页</asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnNext" CommandArgument="Next" CommandName="page" runat="server">下一页</asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnLast" CommandArgument="Last" CommandName="page" runat="server">最后一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </PagerTemplate>
            <Columns>

                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
               <asp:TemplateField HeaderText="变更批号" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="chpcode" runat="server" Text='<%# Eval("chpcode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="计划号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="chptcode" runat="server" Text='<%# Eval("chptcode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
                
                 <asp:BoundField DataField="pjnm" HeaderText="项目" SortExpression="marnm" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="engnm" HeaderText="工程" SortExpression="margg" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="MP_TUHAO" HeaderText="图号/标识号" SortExpression="PUR_TUHAO" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                 
                 <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="marid" runat="server" Text='<%# Eval("marid") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
                
                <asp:BoundField DataField="marnm" HeaderText="名称" SortExpression="marnm" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="margg" HeaderText="规格" SortExpression="margg" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="marcz" HeaderText="材质" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="margb" HeaderText="国标" SortExpression="margb" ItemStyle-Wrap="False"  ItemStyle-HorizontalAlign="Center"/>
                   <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="state" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
                
               <asp:TemplateField HeaderText="变更数量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="chnum" runat="server" Text='<%# Eval("chnum") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
             
                <asp:BoundField DataField="unit" HeaderText="单位" SortExpression="unit" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>
               
                 <asp:TemplateField HeaderText="变更辅助数量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="chfznum" runat="server" Text='<%# Eval("chfznum") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
                <asp:BoundField DataField="fzunit" HeaderText="辅助单位" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>     
                  
                 <asp:BoundField DataField="zxnum" HeaderText="执行数量" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/> 
                 <asp:BoundField DataField="zxfznum" HeaderText="执行辅助数量" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center"/>         
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
         <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />  
        </asp:GridView>
    </div>
    <div>
   
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
