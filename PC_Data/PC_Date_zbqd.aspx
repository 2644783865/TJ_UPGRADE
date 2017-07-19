<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.Master"  CodeBehind="PC_Date_zbqd.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Date_zbqd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    招标物料确认    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <script type="text/javascript">
   
   /*关闭窗口*/
        function close() {
            window.close(); 
        } 

   
   </script>

    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

     <table width="100%" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="tbpc_repeater" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        
                                         <td>
                                            <strong>计划跟踪号</strong>
                                        </td>
                                       
                                        <td>
                                            <strong>物料编码</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                       
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <%--<%#Container.ItemIndex + 1 %>--%>
                                           
                                            <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;--%>
                                        </td>
                                         <td>
                                            <asp:Label ID="BG_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                         
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="BG_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:Label ID="BG_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="BG_MARNORM" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="BG_MARTERIAL" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="BG_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                           
                                        </td>
                                        
                                         <td>
                                            <asp:Label ID="BG_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                           
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="18" align="center">
                                    <asp:Panel ID="NoDataPanebg" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                       
                          <br />
                           <table width="100%" >
                           <tr>
                           <td>
                               查询
                           </td>
                           <td>规格：
                           <asp:TextBox ID="tbgg" runat="server"></asp:TextBox>
                               国标：
                           <asp:TextBox ID="tbgb" runat="server"></asp:TextBox>
                           <asp:Button ID="btncx" runat="server" Text="确定" onclick="btncx_Click" />
                           </td>
                           </tr>
                           </table>
                             <br />
                         <table width="100%" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="rep_zb" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                       
                                        <td>
                                            <strong>物料编码</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                          <td>
                                            <strong>供货商</strong>
                                        </td>
                                          <td>
                                            <strong>单价</strong>
                                        </td>
 
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <%--<%#Container.ItemIndex + 1 %>--%>
                                           
                                            <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td>
                                           
                                            <asp:Label ID="lbmaid" runat="server" Text='<%#Eval("IB_MARID")%>' ></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbname" runat="server" Text='<%#Eval("MNAME")%>'></asp:Label>
                                          
                                        </td>
                                        <td>
                                            <asp:Label ID="lbguige" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="lbcaizhi" runat="server" Text='<%#Eval("CAIZHI")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="lbgb" runat="server" Text='<%#Eval("GB")%>'></asp:Label>
                                           
                                        </td>
                                         <td>
                                            <asp:Label ID="lbdw" runat="server" Text='<%#Eval("PURCUNIT")%>'></asp:Label>
                                           
                                        </td>
                                        <td>
                                            <asp:Label ID="lbsupply" runat="server" Text='<%#Eval("CS_NAME")%>'></asp:Label>
                                            <asp:Label ID="lbsuupplyid" runat="server" Text='<%#Eval("IB_SUPPLY")%>'></asp:Label>
                                           
                                        </td>
                                         <td>
                                            <asp:Label ID="lbprice" runat="server" Text='<%#Eval("IB_PRICE")%>'></asp:Label>
                                            <asp:Label ID="lbdate" runat="server" Text='<%#Eval("IB_DATE")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbfidate" runat="server" Text='<%#Eval("IB_FIDATE")%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="18" align="center">
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                 <table>
                 <tr align="center">
                    <td align="center">
                      <asp:Button ID="btn1" runat="server" Text="添加招标物料" onclick="btn1_Click" /> 
                    </td>
                   
                 </tr>
                 
                 </table>       
    
</asp:Content>
